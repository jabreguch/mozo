using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace Mozo.Comun.Implement.Http;

public class TokenHandler : DelegatingHandler
{
    private readonly IHttpClientToken _tokenClient;
    //private readonly IHttpContextAccessor _accessor;

    public TokenHandler(
            IHttpClientToken tokenClient)
    //, IHttpContextAccessor accessor)
    {
        _tokenClient = tokenClient
                       ?? throw new ArgumentNullException(nameof(tokenClient));
        //  _accessor = accessor;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        //var savedToken =  _accessor.HttpContext.Features;// .GetTokenAsync("access_token");
        IEnumerable<string> headerValues = null;
        //string isAnonymous = string.Empty;
        //if (request.Headers.TryGetValues("Anonymous", out headerValues))
        //{
        //    isAnonymous = headerValues.FirstOrDefault();
        //}
        if (!request.Headers.TryGetValues("Anonymous", out headerValues))
        {
            var token = _tokenClient.PostTokenAsync();

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token.NoToken);
        }


        return await base.SendAsync(request, cancellationToken);


        //HttpResponseMessage response = await base.SendAsync(request, cancellationToken);
        //return response;
    }
}

public class AuthenticationDelegatingHandler : DelegatingHandler
{
    //private readonly ITokenClient _tokenClient;

    //public AuthenticationDelegatingHandler(
    //  ITokenClient tokenClient)
    //{
    //    _tokenClient = tokenClient
    //        ?? throw new ArgumentNullException(nameof(tokenClient));
    //}

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        //GlobalTokenModel token = await _tokenClient.PostTokenAsync();

        //request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token.NoAccessToken);

        return await base.SendAsync(request, cancellationToken);


        //HttpResponseMessage response = await base.SendAsync(request, cancellationToken);
        //return response;
    }
}

//public class AuthenticationAnonymousDelegatingHandler : DelegatingHandler
//{
//    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
//    {
//        //HttpClientHandler handler = new HttpClientHandler()
//        //{
//        //    AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
//        //};

//         HttpResponseMessage response = await base.SendAsync(request, cancellationToken);

//        return response;
//    }
//}