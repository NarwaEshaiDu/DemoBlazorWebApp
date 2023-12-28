using Blazor2App.Application.Features.Schema.DTO;
using Blazor2App.Application.Features.Schema.Student;
using MediatR;

namespace Blazor2App.Application.Features.Schema.Mutations
{
    public class Mutation
    {
        public CourseModel CreateCourse([Service] IMediator mediator, string title) =>
            mediator.Send(new CreateCourseCommand { Title = title }).Result;
    }
}
