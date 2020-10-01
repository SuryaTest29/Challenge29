using System;
using System.Threading.Tasks;
using Newtonsoft.Json;

using Pokemon.Clients;
using Pokemon.Models;

namespace Pokemon.Data
{
    public class ShakespeareApiData : IShakespeareApiData
    {
        private readonly IShakespeareApiAgent _shakespeareApiAgent;

        public ShakespeareApiData(IShakespeareApiAgent shakespeareApiAgent)
        {
            _shakespeareApiAgent = shakespeareApiAgent ?? throw new ArgumentNullException(nameof(shakespeareApiAgent));
        }

        public async Task<ShakespeareApiResult> Translate(string text)
        {
            var resultContent = await _shakespeareApiAgent.Translate(text);
            var shakespeareApiResult = JsonConvert.DeserializeObject<ShakespeareApiResult>(resultContent);

            return shakespeareApiResult;
        }
    }
}
