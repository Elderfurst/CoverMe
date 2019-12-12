namespace CoverMe.Configuration
{
    public class AppSettings
    {
        public AuthorizationKeys AuthorizationKeys { get; set; }
    }

    public class AuthorizationKeys
    {
        public string OpenWeather { get; set; }
        public string NotificationProcessor { get; set; }
    }
}
