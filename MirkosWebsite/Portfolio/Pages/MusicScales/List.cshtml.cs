using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MusicTechnologies.Core.Interfaces;
using Quaverflow.Data.MusicModels;

namespace Portfolio.Pages.MusicScales
{
    public class MusicScalesListModel : PageModel
    {
        private readonly IScaleData scaleData;
        private readonly IScaleCalculator scaleCalculator;

        [TempData]
        public string Message { get; set; }

        public List<Scale> Scales { get; set; }
        public List<string> ScaleInRomanNum { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchScale { get; set; }

        public MusicScalesListModel(IScaleData scaleData, IScaleCalculator scaleCalculator)
        {
            this.scaleData = scaleData;
            this.scaleCalculator = scaleCalculator;
        }
        public void OnGet()
        {
            ScaleInRomanNum = new List<string>();

            Scales = scaleData.GetByName(SearchScale)
                              .OrderBy(s => s.Modes[0].Intervals.Count)
                              .ThenBy(s => s.Id)
                              .ToList();

            foreach (var scale in Scales)
            {
                ScaleInRomanNum.Add(scaleCalculator.GetRomanNumbersScale(scale.Modes[0]));
            }
        }
    }
}
