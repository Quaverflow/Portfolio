using Quaverflow.Data.CovidModels;
using System.Collections.Generic;

namespace Covid.Core.Interfaces
{
    public interface IContinentData
    {
        Continent Add(Continent continent);
        int Commit();
        IEnumerable<Continent> GetAll();
    }
}
