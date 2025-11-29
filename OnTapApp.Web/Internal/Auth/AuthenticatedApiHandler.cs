using System.Net.Http.Headers;
using Microsoft.AspNetCore.Authentication;

namespace OnTapApp.Web.Internal.Auth;

public class AuthenticatedApiHandler : DelegatingHandler
{
    private readonly IHttpContextAccessor _http;

    public AuthenticatedApiHandler(IHttpContextAccessor http)
    {
        _http = http;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var idToken = await _http.HttpContext!.GetTokenAsync("id_token");

        if (!string.IsNullOrWhiteSpace(idToken))
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", idToken);
        }


        return await base.SendAsync(request, cancellationToken);
    }
}
