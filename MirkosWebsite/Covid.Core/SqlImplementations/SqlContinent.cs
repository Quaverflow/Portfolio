using Covid.Core.Interfaces;
using MusicTechnologies.Data;
using Quaverflow.Data.CovidModels;
using System.Collections.Generic;
using System.Linq;

namespace Covid.Core.SqlImplementations
{
    public class SqlContinent : IContinentData
    {
        private readonly QuaverflowDbContext quaverflowDbContext;

        public SqlContinent(QuaverflowDbContext quaverflowDbContext)
        {
            this.quaverflowDbContext = quaverflowDbContext;
        }
        public Continent Add(Continent continent)
        {
            quaverflowDbContext.Add(continent);
            return continent;
        }

        public int Commit()
        {
            return quaverflowDbContext.SaveChanges();
        }

        public IEnumerable<Continent> GetAll()
        {
            return from c in quaverflowDbContext.Continents
                   select c;
        }
    }
}
