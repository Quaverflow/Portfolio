using Covid.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using MusicTechnologies.Data;
using Quaverflow.Data.CovidModels;
using System.Collections.Generic;
using System.Linq;

namespace Covid.Core.SqlImplementations
{
    public class SqlCovidDaily : ICovidDailyData
    {
        private readonly QuaverflowDbContext quaverflowDbContext;

        public SqlCovidDaily(QuaverflowDbContext quaverflowDbContext)
        {
            this.quaverflowDbContext = quaverflowDbContext;
        }
        public CovidDailySummary Add(CovidDailySummary summary)
        {
            quaverflowDbContext.Add(summary);
            return summary;
        }

        public int Commit()
        {
            return quaverflowDbContext.SaveChanges();
        }

        public IEnumerable<CovidDailySummary> GetAll()
        {
            return from c in quaverflowDbContext.CovidDailySummaries.Include(c => c.Country)
                   select c;
        }

        public CovidDailySummary Remove(CovidDailySummary summary)
        {
            quaverflowDbContext.Remove(summary);
            return summary;
        }

        public CovidDailySummary Update(CovidDailySummary summary)
        {
            var entity = quaverflowDbContext.CovidDailySummaries.Attach(summary);
            entity.State = EntityState.Modified;
            return summary;
        }


    }
}
