using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using Polly;
using Polly.Retry;

namespace Blazor2App.Server.Controllers
{
    /// <summary>
    /// Base Controller
    /// </summary>

    [Produces("application/json")]
    [ApiController]
    [RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
    public class AllphiControllerBase : ControllerBase
    {
        public readonly AsyncRetryPolicy _policy;

        /// <summary>
        /// CTOR policy
        /// </summary>
        public AllphiControllerBase()
        {
            _policy = Policy
                .Handle<HttpRequestException>()
                .WaitAndRetryAsync(10, retryAttempt => TimeSpan.FromSeconds(2000));

        }

        /// <summary>
        /// Base route including version
        /// </summary>
        protected const string BaseRoute = "api/v{version:apiVersion}/";
    }
}
