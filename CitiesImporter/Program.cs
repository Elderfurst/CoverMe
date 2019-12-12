using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace CitiesImporter
{
    public class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 5)
            {
                Console.WriteLine("CitiesImporter expects 5 paramters in this order: DataSource, Username, Password, InitialCatalog, FilePath");
                Environment.Exit(1);
            }

            var dataSource = args[0];
            var username = args[1];
            var password = args[2];
            var initialCatalog = args[3];
            var filePath = args[4];

            var file = File.ReadAllText(filePath);

            var cities = JsonConvert.DeserializeObject<List<City>>(file);

            var builder = new SqlConnectionStringBuilder
            {
                DataSource = dataSource,
                UserID = username,
                Password = password,
                InitialCatalog = initialCatalog
            };

            using (var connection = new SqlConnection(builder.ConnectionString))
            {
                Console.WriteLine($"Importing {cities.Count} cities");

                connection.Open();

                Console.WriteLine("Truncating cities table for fresh import");

                var truncateQuery = "TRUNCATE TABLE Cities;";

                using (var truncateCommand = new SqlCommand(truncateQuery, connection))
                {
                    var truncatedCount = truncateCommand.ExecuteNonQuery();

                    Console.WriteLine("Truncated the Cities table");
                }

                var dataTable = new DataTable();
                dataTable.Columns.Add("Id");
                dataTable.Columns.Add("Name");
                dataTable.Columns.Add("CountryCode");
                dataTable.Columns.Add("Longitude");
                dataTable.Columns.Add("Latitude");

                foreach (var city in cities)
                {
                    dataTable.Rows.Add(city.Id, city.Name, city.Country, city.Coord.Lon, city.Coord.Lat);
                }

                using (var bulkCopy = new SqlBulkCopy(connection))
                {
                    bulkCopy.BulkCopyTimeout = 3000;
                    bulkCopy.DestinationTableName = "Cities";
                    bulkCopy.NotifyAfter = 1000;
                    bulkCopy.SqlRowsCopied += (sender, eventArgs) => Console.WriteLine($"Wrote {eventArgs.RowsCopied} records");
                    bulkCopy.WriteToServer(dataTable);
                }
            }

            Console.WriteLine("Cities table updated");
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
