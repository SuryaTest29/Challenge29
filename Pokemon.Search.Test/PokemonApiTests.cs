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
            var shakespeareApiDataMock = new Mock<IShakespeareApiData>();
            var shakespeareApiServiceMock = new ShakespeareApiService(shakespeareApiDataMock.Object);
            var logMock = new Mock<ILogger<PokemonController>>();
            var pokemonController = new PokemonController(pokemonApiServiceMock, shakespeareApiServiceMock, logMock.Object);

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
            Mock<IShakespeareApiService> shakespeareApiServiceMock,
            Mock<ILogger<PokemonController>> logMock,
            string pokemonName,
            ShakespeareApiResult expectedApiResult,
            ShakespeareResult expectedResult)
        {
            // Arrange
            BuildPokemonApiServiceMock(shakespeareApiServiceMock, expectedApiResult);
            var pokemonController = new PokemonController(pokemonApiServiceMock.Object, shakespeareApiServiceMock.Object, logMock.Object);

            // Act
            var actionResult = await pokemonController.Get(pokemonName, CancellationToken.None);

            // Assert
            if (actionResult.Value != null)
            {
                var objectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
                var translated = Assert.IsType<ShakespeareResult>(objectResult.Value);
                Assert.Equal(expectedResult, translated, new TranslateResultComparer());
            }
        }

        [Theory]
        [AutoMoqData]
        public async Task Test_WhenPokemonNotFound_ExpectNotFound(
            Mock<IPokemonApiService> pokemonApiServiceMock,
            Mock<IShakespeareApiService> shakespeareApiServiceMock,
            Mock<ILogger<PokemonController>> logMock,
            string pokemonName)
        {
            // Arrange
            const int expectedNotFound = StatusCodes.Status404NotFound;
            BuildPokemonApiServiceMock(shakespeareApiServiceMock, default);
            var pokemonController = new PokemonController(pokemonApiServiceMock.Object, shakespeareApiServiceMock.Object, logMock.Object);

            // Act
            var actionResult = await pokemonController.Get(pokemonName, CancellationToken.None);
            var statusCodeResult = actionResult.Result as IStatusCodeActionResult;

            // Assert
            Assert.NotNull(statusCodeResult);
            Assert.Equal(expectedNotFound, statusCodeResult.StatusCode);
        }

        private static void BuildPokemonApiServiceMock(
            Mock<IShakespeareApiService> shakespeareApiServiceMock,
            ShakespeareApiResult expectedModel)
        {
            shakespeareApiServiceMock
                .Setup(service => service.Translate(It.IsAny<string>()))
                .ReturnsAsync(() => expectedModel);
        }
    }
}
