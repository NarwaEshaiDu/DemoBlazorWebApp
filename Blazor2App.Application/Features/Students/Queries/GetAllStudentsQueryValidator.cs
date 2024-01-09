using FluentValidation;

namespace Blazor2App.Application.Features.Students.Queries
{
    public class GetAllStudentsQueryValidator : AbstractValidator<GetAllStudentsQuery>
    {
        public GetAllStudentsQueryValidator()
        {
            RuleFor(e => e != null);
        }
    }
}
