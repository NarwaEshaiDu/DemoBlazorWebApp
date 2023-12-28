using Blazor2App.Application.Features.Schema.DTO;
using MediatR;

namespace Blazor2App.Application.Features.Schema.Student
{
    public class GetCourses : IRequest<List<CourseModel>>
    {
    }

    public class GetCourseById : IRequest<CourseModel>
    {
        public int Id { get; set; }
    }
}
