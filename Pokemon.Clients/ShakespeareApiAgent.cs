using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon.Clients
{
    public class ShakespeareApiAgent : IShakespeareApiAgent
    {
        public async Task<string> Translate(string text)
        {
            string apiResult = string.Empty;

            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri("https://api.funtranslations.com/translate/");
                httpClient.DefaultRequestHeaders.Add("X-Funtranslations-Api-Secret", "H_TJSIJ0__F3JaD6mXl_GAeF");
                var httpResponseTask = httpClient.GetAsync("shakespeare.json?text=" + text);
                httpResponseTask.Wait();
                var apiResponse = httpResponseTask.Result;

                if (apiResponse.IsSuccessStatusCode)
                {
                    apiResult = await apiResponse.Content.ReadAsStringAsync();
                }
            }

            return apiResult;
        }
    }
}
