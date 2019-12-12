using CoverMe.Data;
using CoverMe.Models;
using CoverMe.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoverMe.Services
{
    public class CityService : ICityService
    {
        private readonly CoverMeDbContext Db;
        private readonly IMemoryCache CityCache;
        private const string CityCacheKey = "CityCache";

        public CityService(CoverMeDbContext dbContext, IMemoryCache Cache)
        {
            Db = dbContext;
            CityCache = Cache;
        }

        public async Task<IEnumerable<City>> SearchCities(string query)
        {
            var cities = await GetCities();

            return cities.Where(x => x.Name.ToUpperInvariant().Contains(query.ToUpperInvariant()));
        }

        private async Task<IEnumerable<City>> GetCities()
        {
            if (CityCache.TryGetValue(CityCacheKey, out IEnumerable<City> cities))
            {
                return cities;
            }

            cities = await Db.Cities.ToListAsync();

            CityCache.Set(CityCacheKey, cities);

            return cities;
        }
    }
}
