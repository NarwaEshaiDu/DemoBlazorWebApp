using Blazor2App.Application.Services;
using MediatR;

namespace Blazor2App.Application.Features.Pokemon.Queries
{
    public class GetByIdHandler : IRequestHandler<GetByIdRequest, GetByIdResponse>
    {
        private readonly IPokemonApiService _pokemonApiService;
        public GetByIdHandler(IPokemonApiService pokemonApiService)
        {
            _pokemonApiService = pokemonApiService;
        }

        public async Task<GetByIdResponse> Handle(GetByIdRequest request, CancellationToken cancellationToken)
        {
            var result = await _pokemonApiService.GetPokemon(request.Id, cancellationToken);

            return new GetByIdResponse { Result = result };
        }
    }
}
