using Food.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Quaverflow.Data.FoodModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio.Pages.Food
{
    public class RecipesModel : PageModel
    {
        private readonly IRecipeData recipeData;

        [TempData]
        public string Message { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchRecipe { get; set; }
        public List<Recipe> Recipes { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Title { get; set; }
        public RecipesModel(IRecipeData recipeData)
        {
            this.recipeData = recipeData;
        }
        public async Task<IActionResult> OnGet()
        {
            if (!string.IsNullOrEmpty(SearchRecipe))
            {
                Recipes = await recipeData.GetByNameAsync(SearchRecipe);
                Recipes = Recipes.OrderBy(i => i.Name).ToList();

                Title = "Search: " + SearchRecipe;

            }
            else
            {
                Recipes = await recipeData.GetAllAsync();
                Recipes = Recipes.OrderBy(i => i.Name).ToList();

                Title = "Ingredients";
            }

            return Page();
        }


    }
}
