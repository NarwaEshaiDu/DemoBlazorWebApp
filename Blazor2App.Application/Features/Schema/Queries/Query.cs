using Blazor2App.Application.Features.Schema.DTO;
using Blazor2App.Application.Features.Schema.Student;
using MediatR;

namespace Blazor2App.Application.Features.Schema.Queries
{
    public class Query
    {
        public IEnumerable<Todo> GetTodos([Service] IMediator mediator) =>
               mediator.Send(new GetTodosQuery()).Result;
    }
}
