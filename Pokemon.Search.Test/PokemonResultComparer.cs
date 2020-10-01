using System;
using System.Collections.Generic;

using Pokemon.Models;

namespace Pokemon.Search.Test
{
    public class PokemonResultComparer : IEqualityComparer<IPokemonResult>
    {
        public bool Equals(IPokemonResult left, IPokemonResult right)
        {
            return left != null &&
                   right != null &&
                   left.Name == right.Name &&
                   left.Description == right.Description;
        }

        public int GetHashCode(IPokemonResult pokemonResult)
        {
            unchecked
            {
                var hash = 17;
                hash *= GetHashCode(pokemonResult.Name);
                hash *= GetHashCode(pokemonResult.Description);
                return hash;
            }
        }

        private static int GetHashCode(object obj) => 23 + obj.GetHashCode();
    }
}