using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MusicTechnologies.Core.Interfaces;
using Quaverflow.Data.MusicModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Portfolio.Pages.ScaleCalculator
{
    public class ScaleCalculatorModel : PageModel
    {
        private readonly IScaleCalculator scaleCalculator;

        [BindProperty(SupportsGet =true)]
        public int? Root { get; set; }

        [BindProperty(SupportsGet = true)]
        public int?[] Distances { get; set; }

        public Dictionary<Mode, string> ModesWithLetterNotation { get; set; }

        [BindProperty(SupportsGet = true)]
        public Scale Scale { get; set; }

        [BindProperty(SupportsGet = true)]
        public Mode[] Modes { get; set; }

        [BindProperty(SupportsGet = true)]
        public bool Penthatonic { get; set; }
        [BindProperty(SupportsGet = true)]
        public bool Hexatonic { get; set; }
        [BindProperty(SupportsGet = true)]
        public bool Diatonic { get; set; }
        [BindProperty(SupportsGet = true)]
        public bool Octatonic { get; set; }
        [BindProperty(SupportsGet = true)]
        public bool Nonatonic { get; set; }

        public ScaleCalculatorModel(IScaleCalculator scaleCalculator)
        {
            this.scaleCalculator = scaleCalculator;
        }
        public async Task<IActionResult> OnGet()
        {
            if (Distances.Length == 0) { Distances = new int?[7]; }

            if (Root != null)
            {
                ModesWithLetterNotation = await scaleCalculator.CalculateScalesFromChordAsync(Distances, (int)Root, Penthatonic, Hexatonic, Diatonic, Octatonic, Nonatonic);
            }
            return Page();
        }
    }
}