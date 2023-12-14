using MediatR;

namespace Blazor2App.Application.Features.Students.Queries
{
    public class GetAllStudentsQuery : IRequest<GetAllStudentsQueryResponse>
    {
        public GetAllStudentsQuery()
        {

        }

        public static GetAllStudentsQuery CreateQuery()
        {
            return new GetAllStudentsQuery();
        }
    }
}
