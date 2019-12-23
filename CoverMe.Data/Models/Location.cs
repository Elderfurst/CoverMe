namespace CoverMe.Data.Models
{
    public class Location
    {
        // This is built using a combination of lat and long to fit the Select2 library's format
        public string Id { get; set; }
        public string FullAddress { get; set; }
    }
}
