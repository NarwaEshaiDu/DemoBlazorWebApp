using Blazor2App.Application.Features.Schema.DTO;
using Blazor2App.Application.Features.Schema.Student;
using Blazor2App.Application.Features.Students.Queries;
using Blazor2App.Application.Models;
using MediatR;

namespace Blazor2App.Application.Features.Schema.Queries
{
    public class Query
    {
        public IEnumerable<StudentModel> GetStudents([Service] IMediator mediator)
        {
            return mediator.Send(new GetAllStudentsQuery()).Result.Students;
        }

        public IEnumerable<Todo> GetTodos([Service] IMediator mediator)
        {
            return mediator.Send(new GetTodosQuery()).Result;
        }
    }
}
