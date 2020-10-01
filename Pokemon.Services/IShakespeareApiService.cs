using System;
using System.Threading.Tasks;

using Pokemon.Models;

namespace Pokemon.Services
{
    public interface IShakespeareApiService
    {
        Task<ShakespeareApiResult> Translate(string text);
    }
}
