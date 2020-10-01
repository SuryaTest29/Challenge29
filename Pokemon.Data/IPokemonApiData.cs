using System;
using System.Threading;
using System.Threading.Tasks;

using Pokemon.Models;

namespace Pokemon.Data
{
    public interface IPokemonApiData
    {
        Task<PokemonResult> GetByName(string pokemonName, CancellationToken cancellationToken);
    }
}
