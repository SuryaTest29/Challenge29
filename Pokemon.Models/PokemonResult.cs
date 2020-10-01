using System;
using System.Collections.Generic;
using System.Text;

namespace Pokemon.Models
{
    public class PokemonResult: IPokemonResult
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
