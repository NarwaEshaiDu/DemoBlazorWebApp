using Blazor2App.Application.Repositories;
using MediatR;

namespace Blazor2App.Application.Features.Students.Queries
{
    public class GetAllStudentsQueryHandler : IRequestHandler<GetAllStudentsQuery, GetAllStudentsQueryResponse>
    {
        private readonly IStudentRepository _studentRepository;
        public GetAllStudentsQueryHandler(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }
        public async Task<GetAllStudentsQueryResponse> Handle(GetAllStudentsQuery request, CancellationToken cancellationToken)
        {
            var students = await _studentRepository.GetAllStudentsAsync(cancellationToken).ConfigureAwait(false);

            return new GetAllStudentsQueryResponse { Students = students };
        }
    }
}
