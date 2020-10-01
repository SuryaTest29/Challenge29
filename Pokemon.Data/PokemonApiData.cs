using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

using PokeApiNet;

using Pokemon.Clients;
using Pokemon.Models;

namespace Pokemon.Data
{
    public class PokemonApiData : IPokemonApiData
    {
        private readonly IPokeApiAgent _pokeApiAgent;

        public PokemonApiData(IPokeApiAgent pokeApiAgent)
        {
            _pokeApiAgent = pokeApiAgent ?? throw new ArgumentNullException(nameof(pokeApiAgent));
        }

        public async Task<PokemonResult> GetByName(string pokemonName, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(pokemonName))
            {
                throw new ArgumentNullException(nameof(pokemonName));
            }

            var pokemonSpecies = await _pokeApiAgent.GetResourceAsync<PokemonSpecies>(pokemonName, cancellationToken).ConfigureAwait(false);

            return pokemonSpecies is null ? default : GetResult(pokemonSpecies);
        }

        private static PokemonResult GetResult(PokemonSpecies pokemonSpecies)
        {
            var pokemonApiResult = new PokemonResult
            {
                Text = pokemonSpecies.FlavorTextEntries.Where(flavorTexts => flavorTexts.Language.Name == "en").Select(TrimLineBreaks).FirstOrDefault()
            };
            return pokemonApiResult;
        }

        private static string TrimLineBreaks(PokemonSpeciesFlavorTexts flavorTexts) => Regex.Replace(flavorTexts.FlavorText, @"\t|\n|\r|\f", " ");
    }
}
