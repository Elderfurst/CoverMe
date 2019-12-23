using System;

namespace NotificationProcessor
{
    public class WeatherService
    {
        private readonly string WeatherUrl;
        private readonly string SubscriptionKey;
        public WeatherService()
        {
            WeatherUrl = Environment.GetEnvironmentVariable("OpenWeatherUrl");
            SubscriptionKey = Environment.GetEnvironmentVariable("OpenWeatherMapSubscriptionKey");
        }
    }
}
