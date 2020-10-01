using System;
using System.Threading.Tasks;

using Pokemon.Data;
using Pokemon.Models;

namespace Pokemon.Services
{
    public class ShakespeareApiService: IShakespeareApiService
    {
        private readonly IShakespeareApiData _shakespeareApiData;

        public ShakespeareApiService(IShakespeareApiData shakespeareApiData)
        {
            _shakespeareApiData = shakespeareApiData ?? throw new ArgumentNullException(nameof(shakespeareApiData));
        }

        public async Task<ShakespeareApiResult> Translate(string text)
        {
            return await _shakespeareApiData.Translate(text);
        }
    }
}
