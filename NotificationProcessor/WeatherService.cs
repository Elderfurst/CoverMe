using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace NotificationProcessor
{
    public class WeatherService
    {
        private readonly HttpClient HttpClient;
        public WeatherService()
        {
            HttpClient = new HttpClient()
            {
                BaseAddress = new Uri(Environment.GetEnvironmentVariable("DarkSkyUrl")),
            };
        }

        public async Task<WeatherResult> GetWeatherByLocation(float lat, float lon)
        {
            //Request weather for the specified latitude and longitude while excluding a lot of the unnecesary data to save response size
            var weatherResponse = 
                await HttpClient
                .GetAsync($"{lat},{lon}?exclude=minutely,hourly,currently,alerts,flags");

            return JsonConvert
                .DeserializeObject<WeatherResult>(await weatherResponse.Content.ReadAsStringAsync());
        }
    }
}
