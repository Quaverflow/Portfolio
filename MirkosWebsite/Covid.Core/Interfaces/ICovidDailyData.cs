using Quaverflow.Data.CovidModels;
using System.Collections.Generic;

namespace Covid.Core.Interfaces
{
    public interface ICovidDailyData
    {
        IEnumerable<CovidDailySummary> GetAll();
        CovidDailySummary Add(CovidDailySummary summary);
        int Commit();
        CovidDailySummary Update(CovidDailySummary summary);
        CovidDailySummary Remove(CovidDailySummary summary);
    }
}
