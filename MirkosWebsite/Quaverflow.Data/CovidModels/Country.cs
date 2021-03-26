using System.Collections.Generic;

namespace Quaverflow.Data.CovidModels
{
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CountryCode { get; set; }
        public Continent Continent { get; set; }
        public int Population { get; set; }
        public double? HospitalBedsPerThousand { get; set; }
        public double? LifeExpectancy { get; set; }
        public double? PopulationDensity { get; set; }
        public List<CovidDailySummary> DailySummary { get; set; }
        public double? HumanDevelopmentIndex { get; set; }
        public double RatioDeathToPopulation { get; set; }
        public double InfectionDeathRatio { get; set; }

    }
}
