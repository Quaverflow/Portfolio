using Covid.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using MusicTechnologies.Data;
using Quaverflow.Data.CovidModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Covid.Core.SqlImplementations
{
    public class SqlCountry : ICountryData
    {
        private readonly QuaverflowDbContext quaverflowDbContext;

        public SqlCountry(QuaverflowDbContext quaverflowDbContext)
        {
            this.quaverflowDbContext = quaverflowDbContext;
        }
        public Country Add(Country country)
        {
            quaverflowDbContext.Add(country);
            return country;
        }

        public async Task<List<Country>> GetByNameAsync(string name)
        {
            return await quaverflowDbContext.Countries
                                      .Include(c => c.DailySummary
                                      .OrderBy(d => d.Date))
                                      .Where(c => c.Name.ToLower()
                                      .Contains(name.ToLower())).ToListAsync();
        }
        public async Task<List<Country>> GetAllAsync()
        {
            return await quaverflowDbContext.Countries
                                      .Include(c => c.DailySummary
                                      .OrderBy(d => d.Date)).ToListAsync();
        }
        public async Task<Country> GetByIdAsync(int id)
        {
            return await quaverflowDbContext.Countries
                                      .Include(c => c.DailySummary
                                      .OrderBy(d => d.Date))
                                      .Where(c => c.Id == id)
                                      .FirstOrDefaultAsync();
        }
    }
}
