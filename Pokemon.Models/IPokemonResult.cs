using System;

namespace Pokemon.Models
{
    public interface IPokemonResult
    {
        string Name { get; set; }
        string Description { get; set; }
    }
}
