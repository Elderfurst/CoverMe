using CoverMe.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoverMe.Services.Interfaces
{
    public interface ILocationService
    {
        Task<IEnumerable<Location>> Search(string query);
    }
}
