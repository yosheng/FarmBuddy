namespace FarmBuddy.Service.ThirdApi.Moa;

public class MoaAuthorizationHandler : DelegatingHandler
{
    private readonly string _apiKey;

    public MoaAuthorizationHandler(string apiKey)
    {
        _apiKey = apiKey;
    }

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        if (!string.IsNullOrEmpty(_apiKey) && request.RequestUri != null)
        {
            var uriBuilder = new System.UriBuilder(request.RequestUri);
        
            var query = System.Web.HttpUtility.ParseQueryString(uriBuilder.Query);

            query["api_key"] = _apiKey;

            uriBuilder.Query = query.ToString();
            request.RequestUri = uriBuilder.Uri;
        }
        
        return base.SendAsync(request, cancellationToken);
    }
}