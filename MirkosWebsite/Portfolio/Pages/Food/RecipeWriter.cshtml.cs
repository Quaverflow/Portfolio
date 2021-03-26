using Food.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Portfolio.Pages.Helpers;
using Quaverflow.Data.FoodModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Food.Core.Utilities;

namespace Portfolio.Pages.Food
{

    public class RecipeWriterModel : PageModel
    {
        private readonly IIngredientData ingredientData;

        [BindProperty(SupportsGet = true)]
        public Recipe Recipe { get; set; }
        public List<Ingredient> Ingredients { get; set; }
        public List<SelectListItem> UnitsToDisplay { get; set; }
        public List<SelectListItem> ServesRange { get; set; }

        public RecipeWriterModel(IRecipeData recipeData, IIngredientData ingredientData, IHtmlHelper htmlHelper)
        {
            this.ingredientData = ingredientData;
            UnitsToDisplay = htmlHelper.GetEnumSelectList<UnitMeasure>().ToList();
            ServesRange = new SelectList(Enumerable.Range(1, 100)).ToList();
        }
        public async Task<IActionResult> OnGet()
        {

            Ingredients = await ingredientData.GetAllAsync();
            Ingredients = Ingredients.OrderBy(i => i.Name).ToList();

            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                Ingredients = await ingredientData.GetAllAsync();
                Ingredients = Ingredients.OrderBy(i => i.Name).ToList();
                return Page();
            }
            foreach (var recipeIngredient in Recipe.Ingredients)
            {
                recipeIngredient.Ingredient = await ingredientData.GetByIdAsync(recipeIngredient.Ingredient.Id);
            }

            if (Recipe.Video != null)
            {
                Recipe.Video = FoodUtilities.FormatVideoUrl(Recipe.Video);
            }
            PageContext.HttpContext.Session.SetObjectAsJson("Recipe", Recipe);

            return RedirectToPage("./RecipeSteps");
        }
    }
}
