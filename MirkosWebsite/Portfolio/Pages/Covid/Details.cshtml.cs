using Covid.Core.Interfaces;
using Covid.Core.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Quaverflow.Data.CovidModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Portfolio.Pages.Covid
{
    public class DetailsModel : PageModel
    {
        private readonly ICountryData countryData;

        public Country Country { get; set; }
        public Dictionary<string, int> DeathsDaysOfTheWeek { get; set; }
        public Dictionary<string, int> CasesDaysOfTheWeek { get; set; }
        public double PopulationToDeathRatio { get; set; }
        public DetailsModel(ICountryData countryData)
        {
            this.countryData = countryData;
        }
        public async Task<IActionResult> OnGet(int countryId)
        {
            Country = await countryData.GetByIdAsync(countryId);

            if (Country != null)
            {
                DeathsDaysOfTheWeek = StatisticsCalculator.GetCountryDeathsByDayOfWeek(Country);
                CasesDaysOfTheWeek = StatisticsCalculator.GetCountryInfectionsByDayOfWeek(Country);
                Country.RatioDeathToPopulation = StatisticsCalculator.GetDeathToPopulationRatio(Country);
                Country.InfectionDeathRatio = StatisticsCalculator.GetDeathToInfectionRatio(Country);
            }

            return Country == null ? RedirectToPage("./NotFound") : Page();
        }
    }
}
