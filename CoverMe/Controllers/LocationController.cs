using CoverMe.Data.Models;
using CoverMe.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoverMe.Controllers
{
    public class LocationController : Controller
    {
        private readonly ILocationService LocationService;
        public LocationController(ILocationService locationService)
        {
            LocationService = locationService;
        }

        public async Task<IEnumerable<Location>> Search(string query)
        {
            return await LocationService.Search(query);
        }
    }
}
