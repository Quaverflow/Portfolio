using System;

namespace Quaverflow.Data.CovidModels
{
    public class CovidDailySummary
    {
        public int Id { get; set; }
        public Country Country { get; set; }
        public DateTime Date { get; set; }
        public int NewCases { get; set; }
        public int TotalCases { get; set; }
        public int NewDeaths { get; set; }
        public int TotalDeaths { get; set; }
        public double RRate { get; set; }
        public double StringencyIndex { get; set; }
        public double GDPPerCapita { get; set; }
    }
}
