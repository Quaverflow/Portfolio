using Quaverflow.Data.CovidModels;
using System.Collections.Generic;
using System.Linq;

namespace Covid.Core.Utilities
{
    public static class CountriesListSorter
    {
        public static List<Country> SortCountries(SortListBy sortBy, List<Country> countries)
        {
            return sortBy switch
            {
                SortListBy.Cases => countries.OrderByDescending(c => c.DailySummary[^1].TotalCases).ToList(),
                SortListBy.Deaths => countries.OrderByDescending(c => c.DailySummary[^1].TotalDeaths).ToList(),
                SortListBy.Population => countries.OrderByDescending(c => c.Population).ToList(),
                _ => countries.OrderBy(c => c.Name).ToList(),
            };
        }
    }
}
