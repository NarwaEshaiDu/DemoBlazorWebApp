using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;

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
        /// <summary>
        /// Base route including version
        /// </summary>
        protected const string BaseRoute = "api/v{version:apiVersion}/";
    }
}
