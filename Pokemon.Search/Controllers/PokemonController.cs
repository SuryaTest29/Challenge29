using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using Pokemon.Models;
using Pokemon.Services;

namespace Pokemon.Search.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PokemonController : ControllerBase
    {
        private readonly IPokemonApiService _pokemonApiService;
        private readonly ILogger<PokemonController> _logger;

        public PokemonController(IPokemonApiService pokemonApiService, ILogger<PokemonController> logger)
        {
            _pokemonApiService = pokemonApiService ?? throw new ArgumentNullException(nameof(pokemonApiService));
            _logger = logger;
        }

        [HttpGet]
        [Route("{pokemonName}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PokemonResult>> Get([FromRoute] string pokemonName, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(pokemonName))
            {
                return BadRequest("Poke name required.");
            }

            _logger.LogInformation("Pokemon API invoke started");
            var model = await _pokemonApiService.GetByName(pokemonName, cancellationToken).ConfigureAwait(false);
            _logger.LogInformation("Pokemon API invoke finished");

            return model is null ? (ActionResult<PokemonResult>)NotFound() : Ok(model);
        }
    }
}
