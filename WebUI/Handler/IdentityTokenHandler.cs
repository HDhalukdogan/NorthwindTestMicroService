using System.Net.Http.Headers;

namespace WebUI.Handler
{
    public class IdentityTokenHandler : DelegatingHandler
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public IdentityTokenHandler(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = _contextAccessor.HttpContext.User.FindFirst("token").Value;
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await base.SendAsync(request, cancellationToken);
            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                return null ;
            }
            return response;
        }
    }
}
