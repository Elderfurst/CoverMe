using System.Collections.Generic;

namespace CoverMe.Data.Models
{
    public class AzureLocationResponse
    {
        public AzureSummary Summary { get; set; }
        public List<AzureLocation> Results { get; set; }
    }

    public class AzureSummary
    {
        public string Query { get; set; }
        public string QueryType { get; set; }
        public int QueryTime { get; set; }
        public int NumResults { get; set; }
        public int Offset { get; set; }
        public int TotalResults { get; set; }
        public int FuzzyLevel { get; set; }
    }

    public class AzureLocation
    {
        public string Type { get; set; }
        public string Id { get; set; }
        public float Score { get; set; }
        public AzureAddress Address { get; set; }
        public AzureCoordinate Position { get; set; }
    }

    public class AzureAddress
    {
        public string StreetNumber { get; set; }
        public string StreetName { get; set; }
        public string MunicipalitySubdivision { get; set; }
        public string Municipality { get; set; }
        public string CountrySecondarySubdivision { get; set; }
        public string CountryTertiarySubdivision { get; set; }
        public string CountrySubdivision { get; set; }
        public string PostalCode { get; set; }
        public string extendedPostalCode { get; set; }
        public string CountryCode { get; set; }
        public string Country { get; set; }
        public string CountryCodeISO3 { get; set; }
        public string FreeFormAddress { get; set; }
        public string LocalName { get; set; }
        public string CountrySubdivisonName { get; set; }
    }

    public class AzureCoordinate
    {
        public float Lat { get; set; }
        public float Lon { get; set; }
    }
}
