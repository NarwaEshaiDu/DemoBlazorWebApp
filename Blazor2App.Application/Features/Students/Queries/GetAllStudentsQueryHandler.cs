using Blazor2App.Application.Models;
using Blazor2App.Application.Repositories;
using FluentValidation;
using MediatR;

namespace Blazor2App.Application.Features.Students.Queries
{
    public class GetAllStudentsQuery : IRequest<GetAllStudentsQueryResponse>
    {
        public GetAllStudentsQuery()
        { }

        public static GetAllStudentsQuery CreateQuery()
        {
            return new GetAllStudentsQuery();
        }
    }

    public class GetAllStudentsQueryResponse
    {
        public IEnumerable<StudentModel> Students { get; set; }
    }

    public class GetAllStudentsQueryValidator : AbstractValidator<GetAllStudentsQuery>
    {
        public GetAllStudentsQueryValidator()
        {
            RuleFor(e => e != null);
        }
    }

    public class GetAllStudentsQueryHandler : IRequestHandler<GetAllStudentsQuery, GetAllStudentsQueryResponse>
    {
        private readonly IStudentRepository _studentRepository;
        public GetAllStudentsQueryHandler(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }
        public async Task<GetAllStudentsQueryResponse> Handle(GetAllStudentsQuery request, CancellationToken cancellationToken)
        {
            var students = await _studentRepository.GetAllStudentsAsync(cancellationToken);

            return new GetAllStudentsQueryResponse { Students = students };
        }
    }
}
