using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mozo.Comun.Helper.Enu;
using Mozo.Comun.Helper.Global;
using Mozo.Comun.Implement.Mvc;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Mozo.Comun.Implement.Http;

[Serializable]
public static class HttpClientUtil
{
    public static async Task<T> All<T>(HttpClient http, string url, object Model, int security = 1)
    {
        var QueryString = Model.GetQueryString2();
        var request = new HttpRequestMessage(HttpMethod.Get, string.Concat(url, "?", QueryString));

        if (security == Security.Anonymous) request.Headers.Add("Anonymous", "true");

        var httpResponse = await http.SendAsync(request);
        //HttpResponseMessage httpResponse = await http.GetAsync(string.Concat(NoUrl, "?", QueryString));
        return await Result<T>(httpResponse);
    }


    public static async Task<T> One<T>(HttpClient http, string url, object model, int security = 1)
    {
        if (model == null)
            return Activator.CreateInstance<T>();

        var CoEstadoPagina = model.EstadoPagina();
        if (CoEstadoPagina == EnuCommon.CoEstadoPagina.Nuevo)
        {
            var ModelSend1 = default(T);
            ModelSend1 = ModelSend1.StateModel();
            ModelSend1 = ModelSend1.AssignNotNullProperties(model);
            return ModelSend1;
        }

        var QueryString = model.GetQueryString2();

        var request = new HttpRequestMessage(HttpMethod.Get, string.Concat(url, "?", QueryString));

        if (security == Security.Anonymous) request.Headers.Add("Anonymous", "true");

        // var result = new SdkResult<IMasterConfiguration>();


        //{

        //    Url = "http://emby.media/community/index.php?/blog/rss/1-media-browser-developers-blog",
        //    Progress = new Progress<double>(),
        //    UserAgent = "Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/46.0.2490.42 Safari/537.36",
        //    BufferContent = false
        //};


        //if (security == Security.Token)
        //{
        //    GlobalTokenModel Token = GetToken();
        //    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token.NoAccessToken);
        //}

        var httpResponse = await http.SendAsync(request);
        //HttpResponseMessage httpResponse = await http.GetAsync( string.Concat(url, "?", QueryString));
        var ModelSend2 = await Result<T>(httpResponse);
        if (CoEstadoPagina == EnuCommon.CoEstadoPagina.Edicion)
        {
            ModelSend2 = ModelSend2.StateModel();
            ModelSend2 = ModelSend2.AssignNotNullProperties(model);
        }

        return ModelSend2;
    }


    public static async Task<T> Post<T>(HttpClient http, string url, object model, int security = 1)
    {
        var data = new StringContent(model.Serializa(), Encoding.UTF8, "application/json");

        var request = new HttpRequestMessage(HttpMethod.Post, url);
        request.Content = data;


        if (security == Security.Anonymous) request.Headers.Add("Anonymous", "true");

        //HttpResponseMessage httpResponse = await http.PostAsync(NoUrl, data);
        var httpResponse = await http.SendAsync(request);
        var Model = await Result<T>(httpResponse);

        Model = Model.StateModel();
        Model = Model.AssignNotNullProperties(model);
        return Model;
    }

    //static Func<ObjectResult, IActionResult> convert = delegate (ObjectResult problem)
    //{ return problem; };
    private static async Task<T> Result<T>(HttpResponseMessage httpResponse)
    {
        var modelSend = default(T);
        if ((httpResponse.StatusCode == HttpStatusCode.OK)
            | (httpResponse.StatusCode == HttpStatusCode.Accepted)
            | (httpResponse.StatusCode == HttpStatusCode.Created))
        {
            var data = await httpResponse.Content.ReadAsStreamAsync();
            modelSend = await data.DeserializaStream<T>();
        }
        else if (httpResponse.StatusCode == HttpStatusCode.NotFound)
        {
        }
        //else if ((int)httpResponse.StatusCode >= 500)
        //{

        //}
        else
        {
            string json = await httpResponse.Content.ReadAsStringAsync();
            ProblemDetails details = null;
            details = JsonConvert.DeserializeObject<ProblemDetails>(json);

            if (EnuCommon.GLOBAL_IS_AJAX)
            {

                ProblemException detailsException = new();
                if (details == null)
                {
                    detailsException.Status = details.Status;
                    detailsException.Detail = details.Detail;
                    detailsException.Title = details.Title;
                    detailsException.Instance = details.Instance;
                    detailsException.Type = details.Type;
                }
                else
                {
                    detailsException.Status = (int)httpResponse.StatusCode;
                    detailsException.Detail = httpResponse.ReasonPhrase + " - " + httpResponse.RequestMessage.RequestUri.AbsolutePath;
                    detailsException.Title = httpResponse.ReasonPhrase;
                    detailsException.Instance = httpResponse.RequestMessage.RequestUri.AbsolutePath;
                    //detailsException.Type = httpResponse.RequestMessage. . .Type;
                }

                throw detailsException;
            }
            else
            {
                if (details == null)
                {
                    details = new();
                    details.Status = (int)httpResponse.StatusCode;
                    details.Detail = httpResponse.ReasonPhrase + " - " + httpResponse.RequestMessage.RequestUri.AbsolutePath;
                    details.Title = httpResponse.ReasonPhrase;
                    details.Instance = httpResponse.RequestMessage.RequestUri.AbsolutePath;
                    //details.Type = httpResponse.RequestMessage. . .Type;
                }

                Yo.AnadirMensaje(details);
            }









            //convert(new ObjectResult(details)
            //{
            //    StatusCode = details.Status
            //});
            //return (false, modelSend, new ObjectResult(details)
            //{
            //    StatusCode = details.Status
            //});
        }

        return modelSend;
    }

    public static bool IsAjaxRequest(HttpContext context)
    {
        if (context == null)
            throw new ArgumentNullException(nameof(context));
        if (context.Request.Query["X-Requested-With"] == "XMLHttpRequest")
            return true;
        if (context.Request.Headers != null)
            return context.Request.Headers["X-Requested-With"] == "XMLHttpRequest";
        return false;
    }
}