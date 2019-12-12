using CoverMe.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoverMe.Services.Interfaces
{
    public interface ICityService
    {
        Task<IEnumerable<City>> SearchCities(string query);
    }
}
