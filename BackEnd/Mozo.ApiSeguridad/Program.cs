using FluentValidation;

using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi;

using Mozo.ApiSeguridad.Helper;
using Mozo.ApiSeguridad.Helper.Exceptions;
using Mozo.CatalogoComposition;
using Mozo.Helper.Global;
using Mozo.Helper.Services;
using Mozo.Helper.Services.Storage;


//using Mozo.ApiSeguridad.EndPoint.Login;
using Mozo.HelperWeb.Token;
using Mozo.LoginComposition;

using Mozo.MaestroComposition;
using Mozo.SeguridadComposition;

using System.IO.Compression;
using System.Text.Json.Serialization;
WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

#region Servicios


builder.Configuration.AddJsonFile("appsettingsapishared.json", optional: false);

string allowedOrigins = builder.Configuration.GetSection("AppSettings").GetSection("AllowedHosts").Value!;
builder.Services.AddCors(opt => opt.AddDefaultPolicy(config =>
{
    config.WithOrigins(allowedOrigins)
    .AllowAnyHeader()
    .AllowAnyMethod();
}));


var storage = builder.Configuration.GetSection("Storage");

Directory.CreateDirectory(storage["FolderDocument"]!);
Directory.CreateDirectory(storage["FolderResource"]!);
Directory.CreateDirectory(storage["FolderTemporary"]!);

builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddEndpointsMetadataProviderApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Sistema Mozo API",
        Description = "Este es un web api para trabajar con los datos de la empresa",
        Contact = new OpenApiContact
        {
            Email = "jabregu@jne.gob.pe",
            Name = "Inkasoftware",
            //Url = new Uri("https://portal.jne.gob.pe/portal")
        },
        License = new OpenApiLicense
        {
            Name = "MIT",
            //Url = new Uri("https://portal.jne.gob.pe/portal")
        }
    });


    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Ingresa el token JWT"
    });


    c.OperationFilter<AuthorizationFilterSwagger>();

    //c.ParameterFilter<ParameterFilter>();

    /*
        c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                }, new string[]{}
            }
        });
    */
});


builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});

//Register.Connection(builder.Configuration);

builder.Services.AddAuthentication().AddJwtBearer(opt =>
{
    //opt.RequireHttpsMetadata = false;
    //opt.SaveToken = true;
    opt.TokenValidationParameters = UtilityJwt.TokenParametro(builder.Configuration);
    //opt.EventsType = typeof(Authentication);

});
builder.Services.AddAuthorization();
//builder.Services.AddAntiforgery();

builder.Services.AddValidatorsFromAssemblyContaining<Program>();
//builder.Services.AddProblemDetails();
builder.Services.AddCustomProblemDetails();

builder.Services.AddScoped<Authentication>();

builder.Services.AddOutputCache(); // Para las consultas que no cambian mucho

//builder.Services.AddScoped<IAlmacenaArchivo, AlmacenaArchivo>();

//builder.Services.AddScoped<UserClaims, FromClaimsBinder>();
//builder.Services.AddScoped<UserClaims, UserContext>();
//builder.Services.AddScoped<UserClaims>(sp =>
//{
//    HttpContext http = sp.GetRequiredService<IHttpContextAccessor>().HttpContext!;
//    return new UserContext(http!.User);
//});

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.WriteIndented = true;
    options.SerializerOptions.IncludeFields = true;
});


builder.Services.AddResponseCompression(options =>
{
    options.EnableForHttps = true;
    options.Providers.Add<BrotliCompressionProvider>();
    options.Providers.Add<GzipCompressionProvider>();
});

builder.Services.Configure<BrotliCompressionProviderOptions>(options =>
{
    options.Level = CompressionLevel.Fastest;
});

builder.Services.Configure<GzipCompressionProviderOptions>(options =>
{
    options.Level = CompressionLevel.SmallestSize;
});




string connectionString = builder.Configuration.GetSection("ConnectionStrings").GetSection("WebLog").Value!;

//builder.Services.AddScoped<PersonaBusiness>(x => new(connectionString));
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IClaimsService, ClaimsService>();
builder.Services.AddScoped<UserContextService>();
builder.Services.AddScoped<UserContext>();
// Solo agrego negocio, no DAL directamente
builder.Services.SeguridadComposition(connectionString);
builder.Services.MaestroComposition(connectionString);
builder.Services.LoginComposition(connectionString);
builder.Services.CatalogoComposition(connectionString);





// ===== Configuración =====
builder.Services.Configure<StorageOptions>(
    builder.Configuration.GetSection("Storage")
);

builder.Services.Configure<ImageStandardOptions>(
    builder.Configuration.GetSection("ImageStandard")
);


// Tamaño máximo del request (debe ser >= MaxFileSizeBytes)
builder.Services.Configure<FormOptions>(opts =>
{
    var storageOpts = builder.Configuration
        .GetSection("Storage")
        .Get<StorageOptions>();

    opts.MultipartBodyLengthLimit = storageOpts?.MaxFileSizeBytes ?? 10485760;
});

// ===== Servicios =====
builder.Services.AddScoped<IImageProcessor, ImageProcessor>();
builder.Services.AddScoped<IFileStorageService, LocalFileStorageService>();


#endregion


WebApplication app = builder.Build();

#region Middleware

app.UseResponseCompression();

StorageOptions storageOpts = app.Services
    .GetRequiredService<IOptions<StorageOptions>>().Value;

if (Directory.Exists(storageOpts.FolderDocument))
{
    app.UseStaticFiles(new StaticFileOptions
    {
        FileProvider = new PhysicalFileProvider(storageOpts.FolderDocument),
        RequestPath = "/uploads",
        OnPrepareResponse = ctx =>
        {
            ctx.Context.Response.Headers.Append(
                "Cache-Control", "public,max-age=2592000");
        }
    });
}



if (!app.Environment.IsDevelopment())
{
   app.UseHttpsRedirection();
}
app.UseSwagger();
app.UseSwaggerUI();
app.UseExceptionHandler();
app.UseStatusCodePages();
app.UseCors();


app.UseOutputCache();

app.UseAuthentication();
app.UseAuthorization();

//app.UseAntiforgery();
#endregion


//app.MapWithAutoTag("/catalogo/atributo", typeof(Mozo.Api.Catalogo.AtributoEndPoints), g => Mozo.Api.Catalogo.AtributoEndPoints.MapAtributo(g));
//app.MapWithAutoTag("/catalogo/producto-atributo", typeof(Mozo.Api.Catalogo.ProductoAtributoEndPoints), g => Mozo.Api.Catalogo.ProductoAtributoEndPoints.MapProductoAtributo(g));
//app.MapWithAutoTag("/catalogo/producto", typeof(Mozo.Api.Catalogo.ProductoEndPoints), g => Mozo.Api.Catalogo.ProductoEndPoints.MapProducto(g));

/*
app.MapWithAutoTag("/catalogo/producto-impuesto", typeof(Mozo.Api.Catalogo.ProductoImpuestoEndPoints), g => Mozo.Api.Catalogo.ProductoImpuestoEndPoints.MapProductoImpuesto(g));

app.MapWithAutoTag("/catalogo/producto-precio", typeof(Mozo.Api.Catalogo.ProductoPrecioEndPoints), g => Mozo.Api.Catalogo.ProductoPrecioEndPoints.MapProductoPrecio(g));
app.MapWithAutoTag("/catalogo/producto-stock", typeof(Mozo.Api.Catalogo.ProductoStockEndPoints), g => Mozo.Api.Catalogo.ProductoStockEndPoints.MapProductoStock(g));




app.MapWithAutoTag("/seguridad/modulo-usuario", typeof(Mozo.Api.Seguridad.ModuloUsuarioEndPoints), g => Mozo.Api.Seguridad.ModuloUsuarioEndPoints.MapModuloUsuario(g));

app.MapWithAutoTag("/seguridad/permiso", typeof(Mozo.Api.Seguridad.PermisoEndPoints), g => Mozo.Api.Seguridad.PermisoEndPoints.MapPermiso(g));


app.MapWithAutoTag("/login/empresa", typeof(Mozo.Api.Login.EmpresaEndPoints), g => Mozo.Api.Login.EmpresaEndPoints.MapEmpresa(g));
app.MapWithAutoTag("/login/ingreso", typeof(Mozo.Api.Login.IngresoEndPoints), g => Mozo.Api.Login.IngresoEndPoints.MapIngreso(g));

app.MapWithAutoTag("/login/modulo", typeof(Mozo.Api.Login.ModuloEndPoints), g => Mozo.Api.Login.ModuloEndPoints.MapModulo(g));
app.MapWithAutoTag("/login/modulo-usuario", typeof(Mozo.Api.Login.ModuloUsuarioEndPoints), g => Mozo.Api.Login.ModuloUsuarioEndPoints.MapModuloUsuario(g));
app.MapWithAutoTag("/login/redirect", typeof(Mozo.Api.Login.RedirectEndPoints), g => Mozo.Api.Login.RedirectEndPoints.MapRedirect(g));


app.MapWithAutoTag("/maestro/persona", typeof(Mozo.Api.Maestro.PersonaEndPoints), g => Mozo.Api.Maestro.PersonaEndPoints.MapPersona(g));

app.MapWithAutoTag("/maestro/tipo-particular", typeof(Mozo.Api.Maestro.TipoParticularEndPoints), g => Mozo.Api.Maestro.TipoParticularEndPoints.MapTipo(g));

app.MapWithAutoTag("/maestro/persona-tipo", typeof(Mozo.Api.Maestro.PersonaTipoEndPoints), g => Mozo.Api.Maestro.PersonaTipoEndPoints.MapPersonaTipo(g));

*/

//app.MapWithAutoTag("/maestro/ubigeo", typeof(Mozo.Api.Maestro.UbigeoEndPoints), g => Mozo.Api.Maestro.UbigeoEndPoints.MapUbigeo(g));

app.MapWithAutoTag("/maestro/tipo-particular", typeof(Mozo.Api.Maestro.TipoParticularEndPoints), g => Mozo.Api.Maestro.TipoParticularEndPoints.MapTipoParticular(g));

app.MapWithAutoTag("/login/permiso", typeof(Mozo.Api.Login.PermisoEndPoints), g => Mozo.Api.Login.PermisoEndPoints.MapPermiso(g));


app.MapWithAutoTag("/login/menu", typeof(Mozo.Api.Login.MenuEndPoints), g => Mozo.Api.Login.MenuEndPoints.MapMenu(g));


app.MapWithAutoTag("/maestro/tipo-general", typeof(Mozo.Api.Maestro.TipoGeneralEndPoints), g => Mozo.Api.Maestro.TipoGeneralEndPoints.MapTipoGeneral(g));
app.MapWithAutoTag("/maestro/pais", typeof(Mozo.Api.Maestro.PaisEndPoints), g => Mozo.Api.Maestro.PaisEndPoints.MapPais(g));
app.MapWithAutoTag("/maestro/documento-identidad", typeof(Mozo.Api.Maestro.DocumentoIdentidadEndPoints), g => Mozo.Api.Maestro.DocumentoIdentidadEndPoints.MapDocumentoIdentidad(g));
app.MapWithAutoTag("/maestro/red-social", typeof(Mozo.Api.Maestro.RedSocialEndPoints), g => Mozo.Api.Maestro.RedSocialEndPoints.MapRedSocial(g));
app.MapWithAutoTag("/maestro/archivo", typeof(Mozo.Api.Maestro.ArchivoEndPoints), g => Mozo.Api.Maestro.ArchivoEndPoints.MapArchivo(g));


app.MapWithAutoTag("/seguridad/modulo", typeof(Mozo.Api.Seguridad.ModuloEndPoints), g => Mozo.Api.Seguridad.ModuloEndPoints.MapModulo(g));
app.MapWithAutoTag("/seguridad/perfil", typeof(Mozo.Api.Seguridad.PerfilEndPoints), g => Mozo.Api.Seguridad.PerfilEndPoints.MapPerfil(g));
app.MapWithAutoTag("/seguridad/menu", typeof(Mozo.Api.Seguridad.MenuEndPoints), g => Mozo.Api.Seguridad.MenuEndPoints.MapMenu(g));
app.MapWithAutoTag("/seguridad/pagina", typeof(Mozo.Api.Seguridad.PaginaEndPoints), g => Mozo.Api.Seguridad.PaginaEndPoints.MapPagina(g));
app.MapWithAutoTag("/seguridad/perfil-pagina", typeof(Mozo.Api.Seguridad.PerfilPaginaEndPoints), g => Mozo.Api.Seguridad.PerfilPaginaEndPoints.MapPerfilPagina(g));
app.MapWithAutoTag("/seguridad/empresa", typeof(Mozo.Api.Seguridad.EmpresaEndPoints), g => Mozo.Api.Seguridad.EmpresaEndPoints.MapEmpresa(g));
app.MapWithAutoTag("/seguridad/empresa-modulo", typeof(Mozo.Api.Seguridad.EmpresaModuloEndPoints), g => Mozo.Api.Seguridad.EmpresaModuloEndPoints.MapEmpresaModulo(g));


app.MapGet("/debug-storage", (IOptions<StorageOptions> opts) =>
{
    var folder = opts.Value.FolderDocument;
    var archivoTest = Path.Combine(
        folder,
        "empresa_9", "entidad_2", "9", "tipo_4",
        "231a636a9bbb4969876c482e58098507.webp");

    return Results.Ok(new
    {
        FolderDocument = folder,
        FolderExiste = Directory.Exists(folder),
        ArchivoTest = archivoTest,
        ArchivoExiste = File.Exists(archivoTest),
        DirectorioActual = Directory.GetCurrentDirectory()
    });
});


app.Run();
