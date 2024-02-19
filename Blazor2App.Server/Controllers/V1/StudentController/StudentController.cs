using Blazor2App.Application.Features.Students.Queries;
using Blazor2App.Application.Models;
using Blazor2App.Server.Controllers.V1.StudentController.Create;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Polly;
using Polly.Registry;
using Polly.Retry;
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
    [Asp.Versioning.ApiVersion(1.0)]
    [Authorize]
    public class StudentController : AllphiControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMemoryCache _memoryCache;
        private static readonly SemaphoreSlim semaphore = new(1, 1);
        private readonly ResiliencePipeline<HttpResponseMessage> pipeline;
        private readonly AsyncRetryPolicy retryPolicy;

        /// <summary>
        /// CTOR
        /// </summary>
        /// <param name="mediator"></param>
        /// <param name="memoryCache"></param>
        public StudentController(IMediator mediator, IMemoryCache memoryCache, ResiliencePipelineProvider<string> pipelineProvider)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
            pipeline = pipelineProvider.GetPipeline<HttpResponseMessage>("my-pipeline");
            // Define a Retry policy
            retryPolicy = Policy
               .Handle<HttpRequestException>()  // Adjust based on your needs
               .RetryAsync(3, async (exception, retryCount) =>
               {
                   // Log or perform additional actions on each retry
                   Log.Logger.Warning($"Retry {retryCount} due to {exception.Message}");

                   // Add a delay or any other logic between retries if needed
                   await Task.Delay(1000); // 1 second delay, adjust as needed
               });
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

            throw new HttpRequestException("shayane");

            //Middleware is not the solution for this case, too many dependencies.
            //Use IoC instead.
            //Maybe use interceptors ?
            //Move to handler ? 
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


        [HttpGet("test")]
        [ProducesResponseType(typeof(IEnumerable<StudentModel>), (int)HttpStatusCode.OK)]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(GetAll.ResponseExample))]
        public async Task Task1(CancellationToken cancellationToken)
        {
            await retryPolicy.ExecuteAndCaptureAsync(async () => await GetAllAsync(cancellationToken));

        }


        // GET api/<StudentController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }


        /// <summary>
        /// Get student by Id
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
    }
}