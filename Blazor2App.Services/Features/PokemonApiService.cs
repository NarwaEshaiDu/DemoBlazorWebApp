using Blazor2App.Application.Services;
using Microsoft.Extensions.Configuration;

namespace Blazor2App.Services.Features
{
    public class PokemonApiService : Blazor2AppApiService, IPokemonApiService
    {
        public PokemonApiService(IHttpClientFactory httpClientFactory, IConfiguration configuration) : base(httpClientFactory, configuration)
        {
        }

        public async Task<string> GetPokemon(string name, CancellationToken cancellationToken)
        {
            return (await GetAsync<object>("https://pokeapi.co/api/v2/pokemon/" + name, cancellationToken)).ToString()!;
        }
    }
}
