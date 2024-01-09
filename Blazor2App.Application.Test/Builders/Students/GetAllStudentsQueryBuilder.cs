using Blazor2App.Application.Features.Students.Queries;
using Blazor2App.Shared.Builder;

namespace Blazor2App.Application.Test.Builders.Students
{
    public class GetAllStudentsQueryBuilder : GenericBuilder<GetAllStudentsQuery>
    {
        public GetAllStudentsQueryBuilder()
        {
            SetDefaults(() => new GetAllStudentsQuery());
        }
    }
}
