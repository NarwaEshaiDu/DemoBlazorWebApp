using Blazor2App.Application.Infra.Exceptions;
using Blazor2App.Application.Models;
using Blazor2App.Application.Repositories;
using MediatR;

namespace Blazor2App.Application.Features.Students.Commands
{
    public class UpdateStudentCommand : IRequest<UpdateStudentCommandResponse>
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public DateTime LastModified { get; set; }

        public static UpdateStudentCommand CreateCommand(string name)
        {
            return new UpdateStudentCommand { Name = name };
        }
    }
    public class UpdateStudentCommandResponse
    {
        public StudentModel StudentModel { get; set; }
    }

    public class UpdateStudentCommandHandler : IRequestHandler<UpdateStudentCommand, UpdateStudentCommandResponse>
    {
        private readonly IStudentRepository _studentRepository;

        public UpdateStudentCommandHandler(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<UpdateStudentCommandResponse> Handle(UpdateStudentCommand request, CancellationToken cancellationToken)
        {
            var student = await _studentRepository.GetStudentByIdAsync(request.Id, cancellationToken);

            if (student.LastModified == request.LastModified)
            {
                _ = await _studentRepository.UpdateAsync(new StudentModel { Name = request.Name }, cancellationToken);

                student.LastModified = request.LastModified;
                return new UpdateStudentCommandResponse { StudentModel = student };
            }

            throw new OptimisticConcurrencyException("This record has already been updated by another user. Please refresh the page and try again.");
        }
    }
}
