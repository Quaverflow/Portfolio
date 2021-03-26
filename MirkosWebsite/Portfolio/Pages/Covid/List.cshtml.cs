using System.Collections.Generic;
using System.Threading.Tasks;
using Covid.Core.Interfaces;
using Covid.Core.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Quaverflow.Data.CovidModels;

namespace Portfolio.Pages.Covid
{
    public class ListModel : PageModel
    {
        private readonly ICountryData countryData;
        private readonly IHtmlHelper htmlHelper;

        [BindProperty(SupportsGet =true)]
        public string SearchCountry { get; set; }
        public IEnumerable<SelectListItem> SortBy { get; set; }

        [BindProperty(SupportsGet =true)]
        public SortListBy SortChoice { get; set; }
        public List<Country> Countries { get; set; }
        
        public ListModel(ICountryData countryData, IHtmlHelper htmlHelper)
        {
            this.countryData = countryData;
            this.htmlHelper = htmlHelper;
        }
       
        public async Task<IActionResult> OnGet()
        {
            SortBy = htmlHelper.GetEnumSelectList<SortListBy>();

            if(!string.IsNullOrWhiteSpace(SearchCountry))
            {
                Countries = await countryData.GetByNameAsync(SearchCountry);
            }
            else
            {
                Countries = await countryData.GetAllAsync();
            }

            Countries = CountriesListSorter.SortCountries(SortChoice, Countries);

            return Page();

        }
    }
}
