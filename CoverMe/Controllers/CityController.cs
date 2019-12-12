using CoverMe.Models;
using CoverMe.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoverMe.Controllers
{
    public class CityController : Controller
    {
        private readonly ICityService CityService;
        public CityController(ICityService cityService) 
        {
            CityService = cityService;
        }

        [HttpGet]
        public async Task<IEnumerable<City>> SearchCities(string query)
        {
            return await CityService.SearchCities(query);
        }
    }
}
