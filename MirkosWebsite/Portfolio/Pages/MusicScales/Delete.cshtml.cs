using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MusicTechnologies.Core.Interfaces;
using Quaverflow.Data.MusicModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Portfolio.Pages.MusicScales
{
    public class DeleteModel : PageModel
    {
        private readonly IScaleData scaleData;
        private readonly IModeData modeData;

        [BindProperty(SupportsGet = true)]
        public int ScaleId { get; set; }

        public string ScaleName { get; set; }
        public Scale Scale { get; set; }
        public List<Mode> Modes { get; set; }

        public DeleteModel(IScaleData scaleData, IModeData modeData)
        {
            this.scaleData = scaleData;
            this.modeData = modeData;
        }

        public IActionResult OnGet()
        {
            Scale = scaleData.GetById(ScaleId);
            TempData["Message"] = $"{ScaleName} deleted";

            return Scale == null ? RedirectToPage("./NotFound") : Page();
        }

        public async Task<IActionResult> OnPost()
        {
            Modes = await modeData.GetModesAndIntervalsByScaleIdAsync(ScaleId);

            
            Scale = scaleData.Delete(scaleData.GetById(ScaleId));

            Modes = modeData.Delete(Modes);


            scaleData.Commit();

            TempData["Message"] = $"{Scale.Name} deleted";

            return Scale == null ? RedirectToPage("./NotFound") : RedirectToPage("./List");
        }
    }
}
