using System.Threading.Tasks;
using Food.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Quaverflow.Data.FoodModels;

namespace Portfolio.Pages.Food
{
    public class DeleteModel : PageModel
    {
        private readonly IRecipeData recipeData;

        [BindProperty(SupportsGet = true)]
        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; }

        public DeleteModel(IRecipeData recipeData)
        {
            this.recipeData = recipeData;
        }

        public async Task<IActionResult> OnGet()
        {
            Recipe = await recipeData.GetByIdAsync(RecipeId);

            return Recipe == null ? RedirectToPage("./NotFound") : Page();
        }

        public async Task<IActionResult> OnPost()
        {
            Recipe = await recipeData.GetByIdAsync(RecipeId);
            Recipe = await recipeData.DeleteAsync(Recipe);
            recipeData.Commit();

            TempData["Message"] = $"{Recipe.Name} deleted";

            return Recipe == null ? RedirectToPage("./NotFound") : RedirectToPage("./Recipes");
        }
    }
}