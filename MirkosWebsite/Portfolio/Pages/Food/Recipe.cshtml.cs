using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Food.Core.Interfaces;
using Food.Core.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Quaverflow.Data.FoodModels;

namespace Portfolio.Pages.Food
{
    public class RecipeModel : PageModel
    {

        private readonly IRecipeData recipeData;

        [TempData]
        public string Message { get; set; }

        [BindProperty(SupportsGet = true)]
        public int RecipeId { get; set; }
        public List<SelectListItem> ServesRange { get; set; }

        [BindProperty(SupportsGet = true)]
        public int ModifiedServes { get; set; }

        public Dictionary<int, string> NonScalableUnits { get; set; }

        [BindProperty]
        public Recipe Recipe { get; set; }

        public RecipeModel(IRecipeData recipeData)
        {
            this.recipeData = recipeData;
            ServesRange = new SelectList(Enumerable.Range(1, 100)).ToList();
        }
        public async Task<IActionResult> OnGet()
        {
            Recipe = await recipeData.GetByIdAsync(RecipeId);
            NonScalableUnits = new Dictionary<int, string>();

            if (Recipe != null)
            {

                if (ModifiedServes == 0)
                {
                    ModifiedServes = Recipe.Serves;
                }

                if (ModifiedServes != Recipe.Serves)
                {
                    Recipe = RecipeCalculator.Scale(Recipe, ModifiedServes);

                    NonScalableUnits = Recipe.Ingredients
                                             .Where(i =>
                                             i.UnitMeasure == UnitMeasure.unit ||
                                             i.UnitMeasure == UnitMeasure.tbsp ||
                                             i.UnitMeasure == UnitMeasure.cup ||
                                             i.UnitMeasure == UnitMeasure.pinch ||
                                             i.UnitMeasure == UnitMeasure.tsp)
                                             .ToDictionary(i => i.Id, i => RecipeCalculator.GetUnitApprox(i.Quantity));
                }
                Recipe.Serves = ModifiedServes;
            }

            return Recipe == null ? RedirectToPage("./NotFound") : Page();
        }
    }
}
