using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Mozo.Comun.Helper.Enu;
using Mozo.Comun.Helper.Global;
using Mozo.Comun.Implement.Mvc;
using Mozo.Model;
using Mozo.Model.Empresa;
using Mozo.Model.Maestro;
using Mozo.Model.Seguridad;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Mozo.Comun.Implement.Http
{

    interface IHttpConnect
    {
        GlobalTokenModel GetToken();
        Task<T> OneSync<T>(EnuSeguridad.ModuloModel Modulo, string NoUrl, object Model, int security = 1);
        Task<T> OneAsync<T>(EnuSeguridad.ModuloModel Modulo, string NoUrl, object Model, int security = 1);
        Task<T> ListAsync<T>(EnuSeguridad.ModuloModel Modulo, string NoUrl, object Model, int security = 1);
        Task<T> PostAsync<T>(EnuSeguridad.ModuloModel Modulo, string NoUrl, object Model, int security = 1);
    }

    public class HttpConnect : IHttpConnect
    {
        private readonly HttpClient _httpClient;
        private readonly IMemoryCache _cache;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly List<ModuloModel> ModuloList;





        public HttpConnect(IHttpClientFactory clientFactory, IConfiguration configuration, IMemoryCache cache)
        {
            _httpClientFactory = clientFactory;
            _configuration = configuration;
            _cache = cache;
            _httpClient = _httpClientFactory.CreateClient("httpShared");
            _httpClient.DefaultRequestHeaders.Add("Accept-Encoding", "gzip");
            ModuloList = GetAllModuloActivo();
        }

        public List<ModuloModel> GetAllModuloActivo()
        {
            List<ModuloModel> r = null;
            if (_cache.TryGetValue("Modulo_GetAllModuloActivo", out r)) { }
            else
            {
                //string sss = c.HttpContext.Request.Path;
                ////http://localhost/Mozo.ApiSoporte/              
                Uri uri = new Uri(string.Concat(ServiceProviderYo.UrlServer(_cache), _configuration.GetSection("Server").GetSection("Api").GetSection("Seguridad").Value.CheckSlashEnd(), "Cuenta/ModuloApi/GetAll"));

                //string QueryString = Model.GetQueryString2();
                //ModuloModel Modulo2 = ModuloList.Find(x => x.CoModulo == Modulo.CoModulo);
                //Uri uri = new Uri(string.Concat(Modulo2.NoUrlWebApi, NoUrl, "?", QueryString));

                HttpResponseMessage httpResponse = _httpClient.GetAsync(uri).Result;
                if (httpResponse.IsSuccessStatusCode)
                {
                    HttpContent httpContent = httpResponse.Content;
                    byte[] StreamToken = httpContent.ReadAsByteArrayAsync().Result;
                    r = StreamToken.DeserializaByte<List<ModuloModel>>();
                }

                string TxUrlServer = ServiceProviderYo.UrlServer(_cache);
                foreach (ModuloModel Modulo in r)
                {
                    Modulo.NoUrlWeb = string.Concat(TxUrlServer, Modulo.NoUrlWeb.CheckSlashEnd());
                    Modulo.NoUrlWebApi = string.Concat(TxUrlServer, Modulo.NoUrlWebApi.CheckSlashEnd());
                }


                //var responseContent = httpResponse.Content;

                // by calling .Result you are synchronously reading the result
                //string responseString = responseContent.ReadAsStringAsync().Result;

                //r = await GetResult<List<ModuloModel>>(httpResponse);


                //HttpResponseMessage res = _httpClient.GetAsync(uri).Result;
                //if (res.IsSuccessStatusCode)
                //{
                //    HttpContent httpContent = res.Content;
                //    byte[] StreamToken = httpContent.ReadAsByteArrayAsync().Result;
                //    r = StreamToken.DeserializaByte<List<ModuloModel>>();
                //}

                _cache.Set("Modulo_GetAllModuloActivo", r);
            }
            return r;
        }

        public GlobalTokenModel GetToken()
        {
            GlobalTokenModel Token = Yo.GetToken();
            DateTime FeUtcExpiration = Token.FeUtcExpiration.FechaUtc().AddMinutes(EnuCommon.GLOBAL_TOTAL_MINUTE_EXTRA_TOKEN);
            DateTime FeUtcNow = DateTimeOffset.UtcNow.DateTime;

            if (FeUtcNow > FeUtcExpiration)
            {
                StringContent data = new StringContent(Token.Serializa(), Encoding.UTF8, "application/json");
                //using (HttpClient ApiA = new HttpClient())
                //{
                ModuloModel Modulo = ModuloList.Find(x => x.CoModulo == EnuSeguridad.Modulo.Seguridad.CoModulo);
                Uri uri = new Uri(string.Concat(Modulo.NoUrlWebApi, "AccountApi/RenewToken"));
                HttpResponseMessage res = _httpClient.PostAsync(uri, data).Result;
                if (res.IsSuccessStatusCode)
                {
                    HttpContent httpContent = res.Content;
                    byte[] StreamToken = httpContent.ReadAsByteArrayAsync().Result;
                    Token = StreamToken.DeserializaByte<GlobalTokenModel>();
                }
                //}
            }
            Yo.SetToken(Token);
            return Token;
        }

        public async Task<T> OneAsync<T>(EnuSeguridad.ModuloModel modulo, string url, object model, int security = 1)
        {
            if (model == null)
                return Activator.CreateInstance<T>();

            int? CoEstadoPagina = model.EstadoPagina();
            if (CoEstadoPagina == EnuCommon.CoEstadoPagina.Nuevo)
            {
                T ModelSend1 = default(T);
                ModelSend1 = ModelSend1.StateModel();
                ModelSend1 = ModelSend1.AssignNotNullProperties(model);
                return ModelSend1;
            }


            if (security == Security.Token)
            {
                GlobalTokenModel Token = GetToken();
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token.NoAccessToken);
            }

            string QueryString = model.GetQueryString2();

            ModuloModel moduloFind = ModuloList.Find(x => x.CoModulo == modulo.CoModulo);
            Uri uri = new Uri(string.Concat(moduloFind.NoUrlWebApi, url, "?", QueryString));

            // Uri uri = new Uri(string.Concat(Modulo.GetUrlWebService(_configuration), NoUrl, "?", QueryString));
            HttpResponseMessage httpResponse = await _httpClient.GetAsync(uri);

            T ModelSend = await GetResult<T>(httpResponse);

            if (CoEstadoPagina == EnuCommon.CoEstadoPagina.Edicion)
            {
                ModelSend = ModelSend.StateModel();
                ModelSend = ModelSend.AssignNotNullProperties(model);
            }
            return ModelSend;
        }
        public async Task<T> OneSync<T>(EnuSeguridad.ModuloModel Modulo, string NoUrl, object Model, int security = 1)
        {
            if (Model == null)
                return (T)new object { };

            //_httpClient.BaseAddress = new Uri(_configuration.GetSection("WebService").GetSection("Condominio").Value);

            if (security == Security.Token)
            {
                GlobalTokenModel Token = GetToken();
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token.NoAccessToken);
            }
            string QueryString = Model.GetQueryString2();

            ModuloModel Modulo2 = ModuloList.Find(x => x.CoModulo == Modulo.CoModulo);
            Uri uri = new Uri(string.Concat(Modulo2.NoUrlWebApi, NoUrl, "?", QueryString));
            HttpResponseMessage httpResponse = await _httpClient.GetAsync(uri);
            return await GetResult<T>(httpResponse);
        }
        private async Task<T> GetResult<T>(HttpResponseMessage httpResponse)
        {
            T modelSend = default(T);
            if (httpResponse.StatusCode == HttpStatusCode.OK
                | httpResponse.StatusCode == HttpStatusCode.Accepted
                | httpResponse.StatusCode == HttpStatusCode.Created)
            {

                Stream data = await httpResponse.Content.ReadAsStreamAsync();
                modelSend = await data.DeserializaStream<T>();
            }
            else if (httpResponse.StatusCode == HttpStatusCode.NotFound)
            {
                //Stream Data = await httpResponse.Content.ReadAsStreamAsync();
                //ModelSend = null;
            }
            else if (httpResponse.StatusCode == HttpStatusCode.BadRequest)
            {
                //Stream Data = await httpResponse.Content.ReadAsStreamAsync();
                //ModelSend = null;
            }
            else
            {
                string json = await httpResponse.Content.ReadAsStringAsync();
                ProblemDetails details = JsonConvert.DeserializeObject<ProblemDetails>(json);
                Yo.AnadirMensaje(details);

                //return details;

                //Exception ex = new Exception(details.Title);
                //ex.Source = "HTTPCONNECT";
                //ex.Data["detail"] = details.Detail;

                //Stream Data = await httpResponse.Content.ReadAsStreamAsync();
                //MensajeModel Mensaje = await Data.DeserializaStream<MensajeModel>();
                //throw ex;
                //Yo.AnadirMensaje(Mensaje);
            }
            return modelSend;
        }

        //HttpClient client = new HttpClient();
        //public async Task<string> GetResponseString()
        //{
        //    var request = "http://localhost:51843/api/values/getMessage?id=1";
        //    var response = await client.GetAsync(request);
        //    var content = await response.Content.ReadAsStringAsync();
        //    return content;
        //}

        //public async Task<System.Net.Http.HttpResponseMessage> LstAsync<HttpResponseMessage>(EnuSeguridad.ModuloModel Modulo, string NoUrl, object Model, int security = 1)
        //{
        //    if (security == Security.Token)
        //    {
        //        GlobalTokenModel Token = GetToken();
        //        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token.NoAccessToken);
        //    }
        //    string QueryString = Model.GetQueryString2();
        //    ModuloModel Modulo2 = ModuloList.Find(x => x.CoModulo == Modulo.CoModulo);
        //    Uri uri = new Uri(string.Concat(Modulo2.NoUrlWebApi, NoUrl, "?", QueryString));
        //    //Uri uri = new Uri(string.Concat(Modulo.GetUrlWebService(_configuration), NoUrl, "?", QueryString));
        //    return await _httpClient.GetAsync(uri);
        //    //return httpResponse;
        //    //return await GetResult<T>(httpResponse);
        //}

        //private async Task<System.Net.Http.HttpResponseMessage> Lst(int CoModulo, string NoUrl, object Model, int security = 1)
        //{
        //    if (security == Security.Token)
        //    {
        //        GlobalTokenModel Token = GetToken();
        //        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token.NoAccessToken);
        //    }
        //    string QueryString = Model.GetQueryString2();
        //    ModuloModel Modulo2 = ModuloList.Find(x => x.CoModulo == CoModulo);
        //    Uri uri = new Uri(string.Concat(Modulo2.NoUrlWebApi, NoUrl, "?", QueryString));
        //    //Uri uri = new Uri(string.Concat(Modulo.GetUrlWebService(_configuration), NoUrl, "?", QueryString));
        //    return await _httpClient.GetAsync(uri);
        //    //return httpResponse;
        //    //return await GetResult<T>(httpResponse);
        //}

        public async Task<T> ListAsync<T>(EnuSeguridad.ModuloModel Modulo, string NoUrl, object Model, int security = 1)
        {
            if (security == Security.Token)
            {
                GlobalTokenModel Token = GetToken();
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token.NoAccessToken);
            }
            string QueryString = Model.GetQueryString2();
            ModuloModel Modulo2 = ModuloList.Find(x => x.CoModulo == Modulo.CoModulo);
            Uri uri = new Uri(string.Concat(Modulo2.NoUrlWebApi, NoUrl, "?", QueryString));
            HttpResponseMessage httpResponse = await _httpClient.GetAsync(uri);
            return await GetResult<T>(httpResponse);
        }

        //public async Task<HttpResponseMessage> Get(EnuSeguridad.ModuloModel Modulo, string NoUrl, object Model, int security = 1)
        //{
        //    if (security == Security.Token)
        //    {
        //        GlobalTokenModel Token = GetToken();
        //        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token.NoAccessToken);
        //    }
        //    string QueryString = Model.GetQueryString2();
        //    ModuloModel Modulo2 = ModuloList.Find(x => x.CoModulo == Modulo.CoModulo);
        //    Uri uri = new Uri(string.Concat(Modulo2.NoUrlWebApi, NoUrl, "?", QueryString));
        //    return await _httpClient.GetAsync(uri);
        //}

        //public EnuSeguridad.ModuloModel Empresa => EnuSeguridad.Modulo.Empresa;



        //public async Task<HttpResponseMessage> Seguridad(string url, object Model, int security = 1)
        //{
        //    return await Get(EnuSeguridad.Modulo.Seguridad.CoModulo, url, Model, security);
        //}

        public async Task<T> PostAsync<T>(EnuSeguridad.ModuloModel Modulo, string NoUrl, object Model, int security = 1)
        {
            if (security == Security.Token)
            {
                GlobalTokenModel Token = GetToken();
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token.NoAccessToken);
            }

            StringContent data = new StringContent(Model.Serializa(), Encoding.UTF8, "application/json");


            ModuloModel Modulo2 = ModuloList.Find(x => x.CoModulo == Modulo.CoModulo);
            Uri uri = new Uri(string.Concat(Modulo2.NoUrlWebApi, NoUrl));

            //Uri uri = new Uri(string.Concat(Modulo.GetUrlWebService(_configuration), NoUrl));

            HttpResponseMessage httpResponse = await _httpClient.PostAsync(uri, data);
            T ModelSend = await GetResult<T>(httpResponse);

            ModelSend = ModelSend.StateModel();
            ModelSend = ModelSend.AssignNotNullProperties(Model);
            return ModelSend;
        }
        public async Task<SelectList> Empresa_GetAllActivo()
        {
            IEnumerable<EmpresaModel> r = await this.ListAsync<IEnumerable<EmpresaModel>>(EnuSeguridad.Modulo.Seguridad, "EmpresaApi/GetAllActivo", new EmpresaModel(), Security.Anonymous);
            return new SelectList(r, "CoEmpresa", "NoEmpresa");
        }
        public async Task<SelectList> Local_GetAllActivo()
        {
            IEnumerable<LocalModel> r = await ListAsync<IEnumerable<LocalModel>>(EnuSeguridad.Modulo.Empresa, "LocalApi/GetAllActivo", new LocalModel());
            return new SelectList(r, "CoLocal", "NoLocal");
        }






        public async Task<SelectList> Persona_GetAllActivo(PersonaModel c, string CoPersona = null)
        {
            IEnumerable<PersonaModel> r = await ListAsync<IEnumerable<PersonaModel>>(EnuSeguridad.Modulo.Maestro, "PersonaApi/GetAllActivo", c);
            return new SelectList(r, "CoPersona", "NoNombreCompleto", CoPersona);
        }
        public async Task<SelectList> Tipo_GetAllHijoActivo(TipoModel c, string CoTipo = null)
        {
            IEnumerable<TipoModel> r = await ListAsync<IEnumerable<TipoModel>>(EnuSeguridad.Modulo.Maestro, "TipoApi/GetAllHijoActivo", c);
            return new SelectList(r, "CoTipo", "NoTipo", CoTipo);
        }
        public async Task<SelectList> Tipo_GetAllHijoActivoXGrupo(TipoModel c, string CoTipo = null)
        {
            IEnumerable<TipoModel> r = await ListAsync<IEnumerable<TipoModel>>(EnuSeguridad.Modulo.Maestro, "TipoApi/GetAllHijoActivoXGrupo", c);
            return new SelectList(r, "CoSubTipo", "NoSubTipo", CoTipo, "NoTipo");
        }
        public async Task<SelectList> Tipo_GetAllActivo(TipoModel c, string CoTipo = null, int CoTipoIncluye = 0, int FlSigla = 0)
        {
            IEnumerable<TipoModel> r = await ListAsync<IEnumerable<TipoModel>>(EnuSeguridad.Modulo.Maestro, "TipoApi/GetAllActivo", c);
            if (CoTipoIncluye > 0)
            {
                r = r.Where(x => x.NoSigla.Contains(CoTipoIncluye.ToString()));
            }
            return new SelectList(r, "CoTipo", FlSigla == 0 ? "NoTipo" : "NoSigla", CoTipo);
        }
        public async Task<SelectList> Tipo_GetAllGrupo(TipoModel c, string CoTipo = null)
        {
            IEnumerable<TipoModel> r = await ListAsync<IEnumerable<TipoModel>>(EnuSeguridad.Modulo.Maestro, "TipoApi/GetAllGrupo", c);
            return new SelectList(r, "CoTipo", "TxDescripcion", CoTipo);
        }
        public async Task<SelectList> TipoGeneral_GetAllActivo(TipoGeneralModel c, string CoTipo = null, int FlSigla = 0)
        {
            IEnumerable<TipoGeneralModel> r = await ListAsync<IEnumerable<TipoGeneralModel>>(EnuSeguridad.Modulo.Maestro, "TipoGeneralApi/GetAllActivo", c);
            return new SelectList(r, "CoTipo", FlSigla == 0 ? "NoTipo" : "NoSigla", CoTipo);
        }
        public async Task<SelectList> TipoGeneral_GetAllModuloActivo(TipoGeneralModel c, string CoTipo = null, int FlSigla = 0)
        {
            IEnumerable<TipoGeneralModel> r = await ListAsync<IEnumerable<TipoGeneralModel>>(EnuSeguridad.Modulo.Maestro, "TipoGeneralApi/GetAllModuloActivo", c);
            return new SelectList(r, "CoTipo", FlSigla == 0 ? "NoTipo" : "NoSigla", CoTipo);
        }
        public async Task<SelectList> Ubigeo_GetAllDistrito(UbigeoModel c, string CoDistrito = null)
        {
            IEnumerable<UbigeoModel> r = await ListAsync<IEnumerable<UbigeoModel>>(EnuSeguridad.Modulo.Maestro, "UbigeoApi/GetAllDistrito", c);
            return new SelectList(r, "CoDistrito", "NoDistrito", CoDistrito);
        }
        public async Task<SelectList> Ubigeo_GetAllProvincia(UbigeoModel c, string CoProvincia = null)
        {
            IEnumerable<UbigeoModel> r = await ListAsync<IEnumerable<UbigeoModel>>(EnuSeguridad.Modulo.Maestro, "UbigeoApi/GetAllProvincia", c);
            return new SelectList(r, "CoProvincia", "NoProvincia", CoProvincia);
        }
        public async Task<SelectList> Ubigeo_GetAllDepartamento(IMemoryCache cache, string CoDepartamento = null)
        {
            IEnumerable<UbigeoModel> r = null; // = await ListAsync<IEnumerable<UbigeoModel>>(new EnuSeguridad.Modulo.Maestro(), "UbigeoApi/GetAllDepartamento", new UbigeoModel());
            if (cache.TryGetValue("Ubigeo_GetAllDepartamento", out r))
            {

            }
            else
            {
                r = await ListAsync<IEnumerable<UbigeoModel>>(EnuSeguridad.Modulo.Maestro, "UbigeoApi/GetAllDepartamento", new UbigeoModel());
                r.Where(c => c.CoDepartamento.Num() > 25).ToList().ForEach(cc => cc.NoTipoDocumento = "Exterior");
                r.Where(c => c.CoDepartamento.Num() <= 25).ToList().ForEach(cc => cc.NoTipoDocumento = "Interior");
                cache.Set("Ubigeo_GetAllDepartamento", r);
            }


            return new SelectList(r, "CoDepartamento", "NoDepartamento", CoDepartamento, "NoTipoDocumento");

        }
        public async Task<SelectList> Modulo_GetAllActivoSinArea()
        {
            IEnumerable<ModuloModel> r = await ListAsync<IEnumerable<ModuloModel>>(EnuSeguridad.Modulo.Seguridad, "ModuloApi/GetAllActivoSinArea", new ModuloModel());
            return new SelectList(r, "CoModulo", "NoModulo");
        }






        public async Task<SelectList> Modulo_GetAllActivo()
        {
            IEnumerable<ModuloModel> r = await ListAsync<IEnumerable<ModuloModel>>(EnuSeguridad.Modulo.Seguridad, "ModuloApi/GetAllActivo", new ModuloModel());
            return new SelectList(r, "CoModulo", "NoArea");
        }
        public async Task<SelectList> ModuloUsuario_GetAllModulo(ModuloUsuarioModel c, string CoPersona = null)
        {
            IEnumerable<ModuloUsuarioModel> r = await ListAsync<IEnumerable<ModuloUsuarioModel>>(EnuSeguridad.Modulo.Seguridad, "ModuloUsuarioApi/GetAllModulo", c);
            return new SelectList(r, "CoPersona", "NoNombreCompleto", CoPersona);
        }
        public async Task<SelectList> Perfil_GetAllActivo(PerfilModel c)
        {
            IEnumerable<PerfilModel> r = await ListAsync<IEnumerable<PerfilModel>>(EnuSeguridad.Modulo.Seguridad, "PerfilApi/GetAllActivo", c);
            return new SelectList(r, "CoPerfil", "NoPerfil");
        }
        public async Task<SelectList> Permiso_GetAllActivo(PermisoModel c)
        {
            IEnumerable<PermisoModel> r = await ListAsync<IEnumerable<PermisoModel>>(EnuSeguridad.Modulo.Seguridad, "PermisoApi/GetAllActivo", c);
            return new SelectList(r, "CoPersona", "NoNombreCompleto");
        }
        public async Task<SelectList> EmpresaModulo_GetAllActivo(EmpresaModuloModel c)
        {
            IEnumerable<EmpresaModuloModel> r = await ListAsync<IEnumerable<EmpresaModuloModel>>(EnuSeguridad.Modulo.Seguridad, "EmpresaModuloApi/GetAllActivo", c);
            return new SelectList(r, "CoModulo", "NoModulo");
        }


    }


}
