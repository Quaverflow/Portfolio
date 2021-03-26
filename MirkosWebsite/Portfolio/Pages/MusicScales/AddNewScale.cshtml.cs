using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MusicTechnologies.Core.Interfaces;
using MusicTechnologies.Core.Utilities;
using Quaverflow.Data.MusicModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio.Pages.MusicScales
{
    public class AddNewScaleModel : PageModel
    {
        private readonly IModeData modeData;
        private readonly IScaleData scaleData;
        private readonly IIntervalData intervalData;
        private readonly IScaleCalculator scaleCalculator;

        public IEnumerable<SelectListItem> DegreesToDisplay { get; set; }

        [BindProperty]
        public Scale Scale { get; set; }

        [BindProperty]
        public bool Symmetrical { get; set; }

        [BindProperty]
        public bool NonModal { get; set; }

        public string Message { get; set; }

        public AddNewScaleModel(IModeData modeData, IScaleData scaleData, IIntervalData intervalData, IHtmlHelper htmlHelper, IScaleCalculator scaleCalculator)
        {
            this.modeData = modeData;
            this.scaleData = scaleData;
            this.intervalData = intervalData;
            this.scaleCalculator = scaleCalculator;
            DegreesToDisplay = htmlHelper.GetEnumSelectList<Degree>();

        }
        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (!NonModal && !ScaleValidator.VerifyFullInput(Scale, out var message))
            {
                Message = message;
                return Page();
            }
            if (NonModal)
            {
                Scale.Modes[0].Name ??= Scale.Name;
                Scale.Modes.RemoveRange(1, Scale.Modes.Count - 1);
            }

            Scale.Modes[0].Intervals = IntervalsCalculator.SumRelativeDistance(Scale.Modes[0].Intervals);

            if (!ScaleValidator.AreValuesLogical(Scale.Modes[0].Intervals, out message))
            {
                Message = message;
                return Page();
            }

            Scale.Modes[0].Intervals = Scale.Modes[0].Intervals.Where(i => i.Degree != Degree.None).ToList();


            Scale.Modes = Scale.Modes.Where(m => m.Name != null).ToList();
            Scale.Modes = modeData.Populate(Scale, Scale.Id).ToList();

            foreach (var mode in Scale.Modes)
            {
                mode.Intervals = await intervalData.GetExistingAsync(mode.Intervals);
            }

            if (Symmetrical)
            {
                Scale.Modes = scaleCalculator.RemoveDuplicateModes(Scale.Modes);
            }


            scaleData.Add(Scale);
            scaleData.Commit();

            TempData["Message"] = "Scale Saved";
            return RedirectToPage("./Details", new { scaleId = Scale.Id });
        }
    }
}
