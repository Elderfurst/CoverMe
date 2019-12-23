namespace CoverMe.Data.Models
{
    public class UnsubscribeRequest
    {
        public string PhoneNumber { get; set; }
        public string PhoneNumberCountryCode { get; set; }
        public string EmailAddress { get; set; }
    }
}
