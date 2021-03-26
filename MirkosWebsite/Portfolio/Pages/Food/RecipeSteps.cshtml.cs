using Food.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Portfolio.Pages.Helpers;
using Quaverflow.Data.FoodModels;
using Food.Core.Utilities;

namespace Portfolio.Pages.Food
{
    public class NewRecipeStepsModel : PageModel
    {
        private readonly IRecipeData recipeData;



        [BindProperty(SupportsGet = true)]
        public Recipe Recipe { get; set; }

        public NewRecipeStepsModel(IRecipeData recipeData)
        {
            this.recipeData = recipeData;
        }
        public void OnGet()
        {
            Recipe = HttpContext.Session.GetObjectFromJson<Recipe>("Recipe");
        }

        public IActionResult OnPost()
        {
            Recipe.Ingredients = HttpContext.Session.GetObjectFromJson<Recipe>("Recipe").Ingredients;

            if (!ModelState.IsValid)
            {
                Recipe = HttpContext.Session.GetObjectFromJson<Recipe>("Recipe");
                return Page();
            }
            foreach (var step in Recipe.Steps)
            {
                if (step.Video != null)
                {
                    step.Video = step.Video != null ? FoodUtilities.FormatVideoUrl(step.Video) : null;
                }
            }

            recipeData.Add(Recipe);
            recipeData.Commit();
            return RedirectToPage("./Recipes");
        }
    }
}
