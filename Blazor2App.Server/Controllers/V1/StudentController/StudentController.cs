using Asp.Versioning;
using Blazor2App.Application.Features.Students.Queries;
using Blazor2App.Application.Models;
using Blazor2App.Server.Controllers.V1.StudentController.Create;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Serilog;
using Swashbuckle.AspNetCore.Filters;
using System.Net;

namespace Blazor2App.Server.Controllers.V1.StudentController
{
    /// <summary>
    /// Studentcontroller
    /// </summary>
    [Route(BaseStudentsRoute)]
    [ApiController]
    [ApiVersion(1.0)]
    [Authorize]
    public class StudentController : AllphiControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMemoryCache _memoryCache;
        private static readonly SemaphoreSlim semaphore = new(1, 1);

        /// <summary>
        /// CTOR
        /// </summary>
        /// <param name="mediator"></param>
        /// <param name="memoryCache"></param>
        public StudentController(IMediator mediator, IMemoryCache memoryCache)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
        }

        /// <summary>
        /// Route
        /// </summary>
        protected const string BaseStudentsRoute = BaseRoute + "student";

        /// <summary>
        /// Get students.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns>A List of students</returns>
        [HttpGet("")]
        [ProducesResponseType(typeof(IEnumerable<StudentModel>), (int)HttpStatusCode.OK)]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(GetAll.ResponseExample))]
        public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
        {
            //TODO, move this to handler - -- - find middlware logic, not rewrite this whole logic for every hanlder lets say
            var cacheKey = "studentsCacheKey";
            if (_memoryCache.TryGetValue(cacheKey, out IEnumerable<StudentModel> students))
            {
                Log.Logger.Warning("Student list still populated.");
                return Ok(GetAll.Response.Create(students).StudentModels);
            }
            else
            {
                try
                {
                    await semaphore.WaitAsync(cancellationToken);
                    if (_memoryCache.TryGetValue(cacheKey, out students))
                    {
                        Log.Logger.Warning("Student list still populated.");
                        return Ok(GetAll.Response.Create(students).StudentModels);
                    }
                    else
                    {
                        var cacheEntryOptions = new MemoryCacheEntryOptions()
                           .SetSlidingExpiration(TimeSpan.FromSeconds(45))
                           .SetAbsoluteExpiration(TimeSpan.FromSeconds(3600))
                           .SetPriority(CacheItemPriority.Normal)
                           .SetSize(1024);
                        var response = await _mediator.Send(GetAllStudentsQuery.CreateQuery(), cancellationToken);

                        Log.Logger.Warning("populating students list");
                        _memoryCache.Set(cacheKey, response.Students, cacheEntryOptions);
                        return Ok(response.Students);
                    }
                }
                finally
                {
                    semaphore.Release();
                }
            }
        }

        // GET api/<StudentController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("")]
        public async Task<IActionResult> CreateStudentAsync(Command command, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(command.ToMediatorCommand(), cancellationToken);
            return Ok(response.Id);
        }

        // DELETE api/<StudentController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
