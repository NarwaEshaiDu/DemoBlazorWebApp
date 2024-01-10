using Blazor2App.Application.Features.Students.Commands;

namespace Blazor2App.Server.Controllers.V1.StudentController.Create
{
    /// <summary>
    /// 
    /// </summary>
    public class Command
    {
        /// <summary>
        /// Name of a student
        /// </summary>
        public string Name { get; set; } 
    }

    /// <summary>
    /// 
    /// </summary>
    public static class CommandExtension
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static CreateStudentCommand ToMediatorCommand(this Command request)
        {
            if (request == null) return null;

            return new CreateStudentCommand
            {
                Name = request.Name
            };
        }
    }
}
