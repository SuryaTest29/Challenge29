using System;
using System.Threading;
using System.Threading.Tasks;

using Pokemon.Data;
using Pokemon.Models;

namespace Pokemon.Services
{
    public class PokemonApiService : IPokemonApiService
    {
        private readonly IPokemonApiData _pokemonApiData;

        public PokemonApiService(IPokemonApiData pokemonApiData)
        {
            _pokemonApiData = pokemonApiData ?? throw new ArgumentNullException(nameof(pokemonApiData));
        }

        public async Task<PokemonResult> GetByName(string pokemonName, CancellationToken cancellationToken)
        {
            return await _pokemonApiData.GetByName(pokemonName, cancellationToken);
        }
    }
}
