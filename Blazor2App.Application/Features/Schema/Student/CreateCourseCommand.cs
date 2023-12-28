using Blazor2App.Application.Features.Schema.DTO;
using MediatR;

namespace Blazor2App.Application.Features.Schema.Student
{
    public class CreateCourseCommands : IRequest<List<CourseModel>>
    {
        public string Title { get; set; }
    }

    public class CreateCourseCommand : IRequest<CourseModel>
    {
        public string Title { get; set; }
    }
}
