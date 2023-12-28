using Blazor2App.Application.Features.Students.Queries;
using Blazor2App.Application.Models;
using HotChocolate.Authorization;
using MediatR;

namespace Blazor2App.Application.Features.Schema.Queries
{
    public class QueryStudents
    {
        [Authorize]
        public IEnumerable<StudentModel> GetStudents([Service] IMediator mediator)
        {
            return mediator.Send(new GetAllStudentsQuery()).Result.Students;
        }
    }
}
