using Quaverflow.Data.CovidModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Covid.Core.Interfaces
{
    public interface ICountryData
    {
        Country Add(Country country);
        Task<Country> GetByIdAsync(int id);
        Task<List<Country>> GetByNameAsync(string name);
        Task<List<Country>> GetAllAsync();
    }
}
