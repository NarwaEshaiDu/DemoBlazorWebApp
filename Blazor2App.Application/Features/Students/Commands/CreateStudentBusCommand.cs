using Blazor2App.Application.Bus;

namespace Blazor2App.Application.Features.Students.Commands
{
    public class CreateStudentBusCommand : BaseMessage, IBusCommand
    {
        /// <summary>
        /// Default
        /// </summary>
        public CreateStudentBusCommand()
        { }

        /// <summary>
        /// Create new instance of the command
        /// </summary>
        /// <returns></returns>
        public static CreateStudentBusCommand CreateCommand() { return new CreateStudentBusCommand(); }
    }
}
