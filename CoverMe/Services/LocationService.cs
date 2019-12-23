using CoverMe.Configuration;
using CoverMe.Data.Models;
using CoverMe.Services.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace CoverMe.Services
{
    public class LocationService : ILocationService
    {
        private static readonly HttpClient Client = new HttpClient();
        private readonly AppSettings AppSettings;

        public LocationService(AppSettings appSettings)
        {
            AppSettings = appSettings;
        }

        public async Task<IEnumerable<Location>> Search(string query)
        {
            var queryParams = HttpUtility.ParseQueryString(string.Empty);
            queryParams["api-version"] = "1.0";
            queryParams["subscription-key"] = AppSettings.AuthorizationKeys.AzureMapServiceKey;
            queryParams["query"] = query;

            var requestUrlBuilder = new UriBuilder(AppSettings.AzureMapsBaseUrl)
            {
                Query = queryParams.ToString()
            };

            var requestUrl = requestUrlBuilder.ToString();

            var response = await Client.GetAsync(requestUrl);

            var azureLocationResponse = JsonConvert.DeserializeObject<AzureLocationResponse>(await response.Content.ReadAsStringAsync());

            return azureLocationResponse.Results.Select(x => new Location
            {
                Id = $"{x.Position.Lat};{x.Position.Lon}",
                FullAddress = x.Address.FreeFormAddress
            });
        }
    }
}
