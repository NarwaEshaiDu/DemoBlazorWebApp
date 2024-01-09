using Blazor2App.Application.Models;

namespace Blazor2App.Server.Controllers.V1.StudentController.GetAll
{
    public class Response
    {
        public IEnumerable<StudentModel> StudentModels { get; set; }

        public static Response Create(IEnumerable<StudentModel> studentModels) => new Response { StudentModels = studentModels };
    }
}
