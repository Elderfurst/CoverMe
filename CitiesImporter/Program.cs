using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;

namespace CitiesImporter
{
    public class Program
    {
        private static readonly string dataSource = "";
        private static readonly string username = "";
        private static readonly string password = "";
        private static readonly string initialCatalog = "CoverMe";
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Include the full path to the json file to import");
                Environment.Exit(1);
            }

            var filePath = args[0];

            var file = File.ReadAllText(filePath);

            var cities = JsonConvert.DeserializeObject<List<City>>(file);

            var builder = new SqlConnectionStringBuilder();

            builder.DataSource = dataSource;
            builder.UserID = username;
            builder.Password = password;
            builder.InitialCatalog = initialCatalog;

            using (var connection = new SqlConnection(builder.ConnectionString))
            {
                Console.WriteLine($"Importing {cities.Count} cities");

                connection.Open();

                Console.WriteLine("Truncating cities table for fresh import");

                var truncateQuery = "TRUNCATE TABLE Cities;";

                using (var truncateCommand = new SqlCommand(truncateQuery, connection))
                {
                    var truncatedCount = truncateCommand.ExecuteNonQuery();

                    Console.WriteLine($"Truncated {truncatedCount} cities");
                }

                var insertCount = 0;

                foreach (var city in cities)
                {
                    var cityInsert = $"INSERT INTO Cities (Id, Name, CountryCode, Longitude, Latitude) VALUES ({city.Id}, {city.Name}, {city.Country}, {city.Coord.Lon}, {city.Coord.Lat});";

                    using (var insertCommand = new SqlCommand(cityInsert, connection))
                    {
                        insertCount += insertCommand.ExecuteNonQuery();
                    }
                }

                Console.WriteLine($"Inserted {insertCount} records; expected to insert {cities.Count}");
            }
        }
    }

    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public Coord Coord { get; set; }
    }

    public class Coord
    {
        public float Lon { get; set; }
        public float Lat { get; set; }
    }
}
