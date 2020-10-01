using Pokemon.Models;
using System;
using System.Threading.Tasks;

namespace Pokemon.Data
{
    public interface IShakespeareApiData
    {
        Task<ShakespeareApiResult> Translate(string text);
    }
}
