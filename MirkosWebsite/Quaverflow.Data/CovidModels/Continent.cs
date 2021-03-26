using System.Collections.Generic;

namespace Quaverflow.Data.CovidModels
{
    public class Continent
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ContinentCode { get; set; }
        public List<Country> Countries { get; set; }
    }
}
