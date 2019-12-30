using System.Collections.Generic;

namespace CoverMe.Data.Models
{
    public class AzureTimeZoneResponse
    {
        public string Version { get; set; }
        public string ReferenceUtcTimestamp { get; set; }
        public List<AzureTimeZone> TimeZones { get; set; }
    }

    public class AzureTimeZone
    {
        public string Id { get; set; }
        public AzureTimeZoneName Names { get; set; }
        public AzureTimeZoneReferenceTime ReferenceTime { get; set; }
    }

    public class AzureTimeZoneName
    {
        public string ISO6391LanguageCode { get; set; }
        public string Generic { get; set; }
        public string Standard { get; set; }
        public string Daylight { get; set; }
    }

    public class AzureTimeZoneReferenceTime
    {
        public string Tag { get; set; }
        public string StandardOffset { get; set; }
        public string DaylightSavings { get; set; }
        public string WallTime { get; set; }
        public int PosixTzValidYear { get; set; }
        public string PosixTz { get; set; }
        public string Sunrise { get; set; }
        public string Sunset { get; set; }
    }
}
