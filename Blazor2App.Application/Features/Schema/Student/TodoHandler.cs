using Blazor2App.Application.Features.Schema.DTO;
using MediatR;

namespace Blazor2App.Application.Features.Schema.Student
{
    public class TodoHandler : IRequestHandler<CreateTodoCommand, Todo>, IRequestHandler<GetTodosQuery, List<Todo>>
    {
        private readonly List<Todo> _todos = new List<Todo>
        {
            new Todo{ Id = 12, IsCompleted = true, Title = "test for khalid"}
        };


        public Task<Todo> Handle(CreateTodoCommand request, CancellationToken cancellationToken)
        {
            var todo = new Todo
            {
                Id = _todos.Count + 1,
                Title = request.Title,
                IsCompleted = false
            };

            _todos.Add(todo);
            return Task.FromResult(todo);
        }

        public Task<List<Todo>> Handle(GetTodosQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_todos);
        }
    }
}
