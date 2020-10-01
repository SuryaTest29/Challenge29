using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

using Moq;
using Xunit;

using Pokemon.Services;
using Pokemon.Search.Controllers;
using Microsoft.Extensions.Logging;
using Pokemon.Models;
using Pokemon.Data;

namespace Pokemon.Search.Test
{
    public class PokemonApiTests
    {
        [Theory]
        [InlineData(default(string))]
        [InlineData("")]
        [InlineData(" ")]
        public async Task Test_WhenPokemonNameIsInvalid_ExpectBadRequest(string pokemonName)
        {
            // Arrange
            const int expectedBadRequest = StatusCodes.Status400BadRequest;
            var pokemonApiDataMock = new Mock<IPokemonApiData>();
            var pokemonApiServiceMock = new PokemonApiService(pokemonApiDataMock.Object);
            var logMock = new Mock<ILogger<PokemonController>>();
            var pokemonController = new PokemonController(pokemonApiServiceMock, logMock.Object);

            // Act
            var actionResult = await pokemonController.Get(pokemonName, CancellationToken.None);
            var statusCodeResult = actionResult.Result as IStatusCodeActionResult;

            // Assert
            Assert.NotNull(statusCodeResult);
            Assert.Equal(expectedBadRequest, statusCodeResult.StatusCode);
        }

        [Theory]
        [AutoMoqData]
        public async Task Test_WhenPokemonNameIsValid_ExpectTranslatedPokemon(
            Mock<IPokemonApiService> pokemonApiServiceMock,
            Mock<ILogger<PokemonController>> logMock,
            string pokemonName,
            PokemonResult expectedPokemonResult)
        {
            // Arrange
            BuildPokemonApiServiceMock(pokemonApiServiceMock, expectedPokemonResult);
            //var logMock = new Mock<ILogger<PokemonController>>();
            var pokemonController = new PokemonController(pokemonApiServiceMock.Object, logMock.Object);

            // Act
            var actionResult = await pokemonController.Get(pokemonName, CancellationToken.None);

            // Assert
            var objectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var translatedPokemon = Assert.IsType<PokemonResult>(objectResult.Value);
            Assert.Equal(expectedPokemonResult, translatedPokemon, new PokemonResultComparer());
        }

        [Theory]
        [AutoMoqData]
        public async Task Test_WhenPokemonNotFound_ExpectNotFound(
            Mock<IPokemonApiService> pokemonApiServiceMock,
            Mock<ILogger<PokemonController>> logMock,
            string pokemonName)
        {
            // Arrange
            const int expectedNotFound = StatusCodes.Status404NotFound;
            BuildPokemonApiServiceMock(pokemonApiServiceMock, default);
            //var logMock = new Mock<ILogger<PokemonController>>();
            var pokemonController = new PokemonController(pokemonApiServiceMock.Object, logMock.Object);

            // Act
            var actionResult = await pokemonController.Get(pokemonName, CancellationToken.None);
            var statusCodeResult = actionResult.Result as IStatusCodeActionResult;

            // Assert
            Assert.NotNull(statusCodeResult);
            Assert.Equal(expectedNotFound, statusCodeResult.StatusCode);
        }

        private static void BuildPokemonApiServiceMock(
            Mock<IPokemonApiService> pokemonApiServiceMock,
            PokemonResult expectedPokemonModel)
        {
            pokemonApiServiceMock
                .Setup(service => service.GetByName(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(() => expectedPokemonModel);
        }
    }
}
