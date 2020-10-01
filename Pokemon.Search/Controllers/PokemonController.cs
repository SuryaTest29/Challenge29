using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pokemon.Data;
using Pokemon.Models;
using Pokemon.Services;

namespace Pokemon.Search.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PokemonController : ControllerBase
    {
        private readonly IPokemonApiService _pokemonApiService;
        private readonly IShakespeareApiService _shakespeareApiService;
        private readonly ILogger<PokemonController> _logger;

        public PokemonController(IPokemonApiService pokemonApiService, IShakespeareApiService shakespeareApiService, ILogger<PokemonController> logger)
        {
            _pokemonApiService = pokemonApiService ?? throw new ArgumentNullException(nameof(pokemonApiService));
            _shakespeareApiService = shakespeareApiService ?? throw new ArgumentNullException(nameof(shakespeareApiService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        [Route("{pokemonName}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ShakespeareResult>> Get([FromRoute] string pokemonName, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(pokemonName))
            {
                return BadRequest("Poke name required.");
            }

            ShakespeareResult shakespeareResult = null;

            _logger.LogInformation("Pokemon API invoke started");
            var pokemonResult = await _pokemonApiService.GetByName(pokemonName, cancellationToken).ConfigureAwait(false);
            _logger.LogInformation("Pokemon API invoke finished");

            if (pokemonResult != null && pokemonResult.Text.Length > 0)
            {
                _logger.LogInformation("Shakespeare API invoke started");
                var shakespeareApiResult = await _shakespeareApiService.Translate(System.Web.HttpUtility.HtmlEncode(pokemonResult.Text));
                _logger.LogInformation("Shakespeare API invoke finished");

                if (shakespeareApiResult != null)
                {
                    shakespeareResult = new ShakespeareResult
                    {
                        Name = pokemonName,
                        Description = shakespeareApiResult.Contents.Translated
                    };
                }
            }

            return shakespeareResult is null ? (ActionResult<ShakespeareResult>)NotFound() : Ok(shakespeareResult);
        }
    }
}
