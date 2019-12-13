namespace CoverMe.Configuration
{
    public class AppSettings
    {
        public string AzureMapsBaseUrl { get; set; }
        public AuthorizationKeys AuthorizationKeys { get; set; }
    }

    public class AuthorizationKeys
    {
        public string OpenWeather { get; set; }
        public string NotificationProcessor { get; set; }
        public string AzureMapServiceClientId { get; set; }
        public string AzureMapServiceKey { get; set; }
    }
}
