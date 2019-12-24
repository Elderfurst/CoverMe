using System.Collections.Generic;

namespace NotificationProcessor
{
    public class WeatherResult
    {
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public string TimeZone { get; set; }
        public Daily Daily { get; set; }
    }

    public class Daily
    {
        public string Summary { get; set; }
        public string Icon { get; set; }
        public List<DailyData> Data { get; set; }
    }
    
    public class DailyData
    {
        public ulong Time { get; set; }
        public string Summary { get; set; }
        public float PrecipIntensity { get; set; }
        public float PrecipIntensityMax { get; set; }
        public ulong PrecipIntensityMaxTime { get; set; }
        public float PrecipProbability { get; set; }
        public string PrecipType { get; set; }
    }
}
