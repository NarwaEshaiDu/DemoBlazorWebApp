using Blazor2App.Application.Features.Pokemon.Queries;
using HotChocolate.Authorization;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Blazor2App.Server.Controllers
{
    /// <summary>
    /// Pokemon API
    /// </summary>
    [Route(Pokemon)]
    [Authorize]
    [Asp.Versioning.ApiVersion(1.0)]
    public class PokeApiController : AllphiControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        protected const string Pokemon = BaseRoute + "pokemon";

        private readonly IMediator _mediator;

        /// <summary>
        /// CTOR
        /// </summary>
        /// <param name="mediator"></param>
        public PokeApiController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get Pokemon abilities by its name.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(string id, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(GetByIdRequest.CreateQuery(id), cancellationToken);
            return Ok(response);
        }
    }
}
