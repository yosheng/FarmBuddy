namespace FarmBuddy.Service.ThirdApi.Cwa;

/// <summary>
/// DelegatingHandler for adding Authorization header to CWA API requests
/// </summary>
public class CwaAuthorizationHandler : DelegatingHandler
{
    private readonly string _apiKey;

    public CwaAuthorizationHandler(string apiKey)
    {
        _apiKey = apiKey;
    }

    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        if (!string.IsNullOrEmpty(_apiKey) && request.RequestUri != null)
        {
            var uriBuilder = new System.UriBuilder(request.RequestUri);
        
            var query = System.Web.HttpUtility.ParseQueryString(uriBuilder.Query);

            query["Authorization"] = _apiKey;

            uriBuilder.Query = query.ToString();
            request.RequestUri = uriBuilder.Uri;
        }

        return await base.SendAsync(request, cancellationToken);
    }
}