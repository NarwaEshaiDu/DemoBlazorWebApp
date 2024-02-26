using Blazor2App.Application.Models;
using Blazor2App.Application.Repositories;
using MediatR;

namespace Blazor2App.Application.Features.Students.Queries
{
    public class GetStudentByIdQuery : IRequest<GetStudentByIdQueryResponse>
    {
        public int Id { get; set; }
        public GetStudentByIdQuery(int id)
        {
            Id = id;
        }

        public static GetStudentByIdQuery CreateQuery(int id)
        {
            return new GetStudentByIdQuery(id);
        }
    }

    public class GetStudentByIdQueryResponse
    {
        public StudentModel Student { get; set; }
    }

    public class GetStudentByIdQueryHandler : IRequestHandler<GetStudentByIdQuery, GetStudentByIdQueryResponse>
    {
        private readonly IStudentRepository _studentRepository;
        public GetStudentByIdQueryHandler(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<GetStudentByIdQueryResponse> Handle(GetStudentByIdQuery request, CancellationToken cancellationToken)
        {
            var student = await _studentRepository.GetStudentByIdAsync(request.Id, cancellationToken);

            return new GetStudentByIdQueryResponse { Student = student };
        }
    }
}
