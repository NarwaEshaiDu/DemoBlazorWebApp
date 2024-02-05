using MediatR;

namespace Blazor2App.Application.Features.Pokemon.Queries
{
    public class GetByIdRequest : IRequest<GetByIdResponse>
    {
        public string Id { get; set; }
        public GetByIdRequest()
        {
            
        }
        public GetByIdRequest(string id)
        {
            Id = id;
        }

        public static GetByIdRequest CreateQuery(string id)
        {
            return new GetByIdRequest(id);
        }
    }
}
