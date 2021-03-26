using Microsoft.EntityFrameworkCore;
using MusicTechnologies.Core.Interfaces;
using MusicTechnologies.Data;
using Quaverflow.Data.MusicModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicTechnologies.Core.SqlImplementation
{
    public class SqlInterval : IIntervalData
    {
        private readonly QuaverflowDbContext quaverflowDbContext;

        public SqlInterval(QuaverflowDbContext quaverflowDbContext)
        {
            this.quaverflowDbContext = quaverflowDbContext;
        }

        public Interval Add(Interval interval)
        {
            quaverflowDbContext.Add(interval);
            return interval;
        }

        public async Task<List<Interval>> GetExistingAsync(List<Interval> intervals)
        {
            return await intervals.Select(interval => quaverflowDbContext.Intervals
                                  .FirstOrDefault(i => i.Distance == interval.Distance 
                                                           && i.Degree == interval.Degree) ?? interval)
                                  .AsQueryable().ToListAsync();
        }
    }
}
