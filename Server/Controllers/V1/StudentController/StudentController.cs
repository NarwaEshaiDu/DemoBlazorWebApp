using Asp.Versioning;
using Blazor2App.Application.Features.Students.Queries;
using Blazor2App.Application.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
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
    public class StudentController : AllphiControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMemoryCache _memoryCache;

        /// <summary>
        /// CTOR
        /// </summary>
        /// <param name="mediator"></param>
        /// <param name="memoryCache"></param>
        public StudentController(IMediator mediator, IMemoryCache memoryCache)
        {
            _mediator = mediator;
            _memoryCache = memoryCache;
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
        public async Task<IEnumerable<StudentModel>> GetAllAsync(CancellationToken cancellationToken)
        {

            var cacheKey = "studentsCacheKey";
            if (_memoryCache.TryGetValue(cacheKey, out IEnumerable<StudentModel> students))
            {
                return GetAll.Response.Create(students).StudentModels;
            }
            else
            {
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromSeconds(45))
                    .SetAbsoluteExpiration(TimeSpan.FromSeconds(3600))
                    .SetPriority(CacheItemPriority.Normal);
                var response = await _mediator.Send(GetAllStudentsQuery.CreateQuery(), cancellationToken);

                _memoryCache.Set(cacheKey, response, cacheEntryOptions);
                return (response.Students);
            }
        }

        // GET api/<StudentController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<StudentController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<StudentController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<StudentController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
