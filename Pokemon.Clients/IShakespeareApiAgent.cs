using System;
using System.Threading.Tasks;

namespace Pokemon.Clients
{
    public interface IShakespeareApiAgent
    {
        Task<string> Translate(string text);
    }
}
