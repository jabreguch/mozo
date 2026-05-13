using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Mozo.Comun.Helper.Global;
using System;
using System.IO;
using System.IO.Compression;
using System.Linq;

namespace Mozo.Comun.Implement.Api;

public static class ConfigStartupApi
{
    //IServiceCollection services

    public static void AddMyConfigureServices(this IServiceCollection services)
    {
        services.AddCors();

        services.AddControllers(options => { options.Filters.Add(new ApiFilterAction()); }).AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.WriteIndented = true;
            options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
            //options.JsonSerializerOptions.IgnoreNullValues = true;
        });

        services.AddMemoryCache();
        services.AddDistributedMemoryCache();

        services.AddSession(options =>
        {
            options.IdleTimeout = TimeSpan.FromHours(2);
            options.Cookie.HttpOnly = true;
            options.Cookie.IsEssential = true;
        });


        services.AddScoped<Authentication>();

        services.AddAuthorization(options =>
        {
            var defaultAuthorizationPolicyBuilder =
                new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme);
            defaultAuthorizationPolicyBuilder = defaultAuthorizationPolicyBuilder.RequireAuthenticatedUser();
            options.DefaultPolicy = defaultAuthorizationPolicyBuilder.Build();
        });

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = UtilityJwt.TokenParametro();
                options.EventsType = typeof(Authentication);
            }
        );

        services.AddHttpContextAccessor();


        services.Configure<GzipCompressionProviderOptions>(options =>
            options.Level = CompressionLevel.Optimal);
        services.AddResponseCompression(options =>
        {
            options.EnableForHttps = true;
            options.Providers.Add<GzipCompressionProvider>();
            options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(Glo.MimeTypesCompress);
        });
    }

    public static void FolderTemplateShared(this IApplicationBuilder app, IConfiguration configuration)
    {
        app.UseStaticFiles();

        app.UseStaticFiles(new StaticFileOptions
        {
            FileProvider = new PhysicalFileProvider(Path.Combine(
                configuration.GetSection("AppSettings").GetSection("FolderRoot").Value,
                configuration.GetSection("AppSettings").GetSection("FolderResource").Value, "admin", "assetscustom",
                "template")),
            RequestPath = "/template"
        });
    }

    public static void AddMyConfigure(this IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseCors(builder => builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());

        app.UseResponseCompression();

        app.UseExceptionHandler(err => err.UseCustomErrors(env));

        app.UseHttpsRedirection();

        app.UseRouting();
        app.UseCookiePolicy();
        app.UseSession();


        app.UseAuthentication();
        app.UseAuthorization();
        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }
}