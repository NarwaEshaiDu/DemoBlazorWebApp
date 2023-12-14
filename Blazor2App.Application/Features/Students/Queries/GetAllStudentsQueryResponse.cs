using Blazor2App.Application.Models;

namespace Blazor2App.Application.Features.Students.Queries
{
    public class GetAllStudentsQueryResponse
    {
        public IEnumerable<StudentModel> Students { get; set; }
    }
}
