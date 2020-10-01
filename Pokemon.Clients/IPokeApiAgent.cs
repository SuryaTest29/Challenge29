using System;
using System.Threading;
using System.Threading.Tasks;

using PokeApiNet;

namespace Pokemon.Clients
{
    public interface IPokeApiAgent
    {
        Task<TNamedApiResource> GetResourceAsync<TNamedApiResource>(string pokemonName, CancellationToken cancellationToken) where TNamedApiResource : NamedApiResource;
    }
}
