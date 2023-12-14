using Swashbuckle.AspNetCore.Filters;

namespace Blazor2App.Server.Controllers.V1.StudentController.GetAll
{
    public class ResponseExample : IExamplesProvider<Response>
    {
        public Response GetExamples()
        {
            return new Response();
        }
    }
}
