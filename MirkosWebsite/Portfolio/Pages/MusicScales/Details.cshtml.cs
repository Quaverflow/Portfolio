using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MusicTechnologies.Core.Interfaces;
using Quaverflow.Data.MusicModels;

namespace Portfolio.Pages.MusicScales
{
    public class DetailsModel : PageModel
    {
        private readonly IModeData modeData;
        private readonly IScaleCalculator scaleCalculator;
        private readonly IScaleData scaleData;

        public List<Mode> Modes { get; set; }
        public Scale Scale { get; set; }
        public List<string> ScaleInRomanNum { get; set; }

        [TempData]
        public string Message { get; set; }

        public DetailsModel(IModeData modeData, IScaleCalculator scaleCalculator, IScaleData scaleData)
        {
            this.modeData = modeData;
            this.scaleCalculator = scaleCalculator;
            this.scaleData = scaleData;
        }
        public async Task<IActionResult> OnGet(int scaleId)
        {
            Modes = await modeData.AllModesAsync();
            Modes = Modes.Where(s => s.ScaleId == scaleId)
                          .OrderBy(s => s.Id)
                          .ToList();

            ScaleInRomanNum = new List<string>();

            Scale = scaleData.GetById(scaleId);
            foreach (var mode in Modes)
            {
                mode.Intervals = mode.Intervals.OrderBy(i => i.Distance).ToList();
                ScaleInRomanNum.Add(scaleCalculator.GetRomanNumbersScale(mode));
            }
            return Modes == null ? RedirectToPage("./NotFound") : Page();
        }
    }
}
