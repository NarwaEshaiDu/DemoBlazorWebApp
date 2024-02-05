namespace Blazor2App.Application.Services
{
    public interface IPokemonApiService
    {
        Task<string> GetPokemon(string id, CancellationToken cancellationToken);
    }
}
