using System.Collections.Generic;
using System.Threading.Tasks;
using Covid.Core.Interfaces;
using Covid.Core.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Quaverflow.Data.CovidModels;

namespace Portfolio.Pages.Covid
{
    public class InternationalStatisticsModel : PageModel
    {
        private readonly ICountryData countryData;

        public List<Country> Countries { get; set; }
        public Dictionary<string, int> DeathsDaysOfTheWeek { get; set; }
        public Dictionary<string, int> CasesDaysOfTheWeek { get; set; }
        public  Country World { get; set; }
        public string X { get; set; }
        public InternationalStatisticsModel(ICountryData countryData)
        {
            this.countryData = countryData;
        }
        public async Task<IActionResult> OnGet()
        {
            Countries = await countryData.GetAllAsync();
            DeathsDaysOfTheWeek = StatisticsCalculator.GetWorldDeathsByDayOfWeek(Countries);
            CasesDaysOfTheWeek = StatisticsCalculator.GetWorldInfectionsByDayOfWeek(Countries);
            World = StatisticsCalculator.GetWorld(Countries);

            foreach(var country in Countries)
            {
                country.RatioDeathToPopulation = StatisticsCalculator.GetDeathToPopulationRatio(country);
                country.InfectionDeathRatio = StatisticsCalculator.GetDeathToInfectionRatio(country);
            }

            return Page();
        }
    }
}
