using System.ComponentModel.DataAnnotations.Schema;

namespace CoverMe.Models
{
    public class City
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string CountryCode { get; set; }
        public float Longitude { get; set; }
        public float Latitude { get; set; }
    }
}
