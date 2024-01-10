using MediatR;

namespace Blazor2App.Application.Features.Students.Commands
{
    public class CreateStudentCommand : IRequest<CreateStudentCommandResponse>
    {
        public string Name { get; set; }

        //empty CTOR needed for x unit scenario
        public CreateStudentCommand()
        { }

        public static CreateStudentCommand CreateCommand(string name)
        {
            return new CreateStudentCommand { Name = name };
        }
    }
}
