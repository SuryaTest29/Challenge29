using System;
using System.Threading;
using System.Threading.Tasks;

using Pokemon.Models;

namespace Pokemon.Services
{
    public interface IPokemonApiService
    {
        Task<PokemonResult> GetByName(string pokemonName, CancellationToken cancellationToken);
    }
}
