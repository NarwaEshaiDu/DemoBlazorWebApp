using Blazor2App.Application.Models;
using Blazor2App.Application.Repositories;
using MediatR;

namespace Blazor2App.Application.Features.Students.Commands
{
    public class CreateStudentCommandHandler : IRequestHandler<CreateStudentCommand, CreateStudentCommandResponse>
    {
        private readonly IStudentRepository _studentRepository;
        public CreateStudentCommandHandler(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<CreateStudentCommandResponse> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
        {
            var id = await _studentRepository.SaveAsync(new StudentModel { Name = request.Name }, cancellationToken);
            return new CreateStudentCommandResponse {  Id = id };
        }
    }
}
