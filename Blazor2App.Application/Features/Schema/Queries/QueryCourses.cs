using Blazor2App.Application.Features.Schema.DTO;
using Blazor2App.Application.Features.Schema.Student;
using MediatR;

namespace Blazor2App.Application.Features.Schema.Queries
{
    public class Query { }
    [ExtendObjectType(typeof(Query))]
    public class QueryCourses
    {
        public async Task<IEnumerable<CourseModel>> GetCourses([Service] IMediator mediator)
        {
            return (await mediator.Send(new GetCourses()));
        }

        public async Task<CourseModel> GetCourseById([Service] IMediator mediator, int id)
        {
            return (await mediator.Send(new GetCourseById { Id = id }));
        }
    }
}
