using Blazor2App.Application.Features.Schema.DTO;
using Blazor2App.Application.Features.Schema.Student;
using Blazor2App.Application.Features.Students.Queries;
using Blazor2App.Application.Models;
using HotChocolate.Authorization;
using MediatR;

namespace Blazor2App.Application.Features.Schema.Queries
{
   
    public class Query
    {
        [Authorize]
        public IEnumerable<StudentModel> GetStudents([Service] IMediator mediator)
        {
            return mediator.Send(new GetAllStudentsQuery()).Result.Students;
        }

        public async Task<IEnumerable<Todo>> GetTodosAsync([Service] IMediator mediator)
        {
            return (await mediator.Send(new GetTodosQuery()));
        }
    }
}
