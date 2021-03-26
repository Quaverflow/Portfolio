using Food.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Quaverflow.Data.FoodModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio.Pages.Food
{
    public class IngredientsModel : PageModel
    {
        private readonly IIngredientData ingredientData;
        private readonly IIngredientGroupData ingredientGroupData;

        [TempData]
        public string Message { get; set; }

        public List<IngredientGroup> Groups { get; set; }
        public List<Ingredient> Ingredients { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchIngredient { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchSubgroup { get; set; }

        public string Title { get; set; }
        public int Results { get; set; }

        public IngredientsModel(IIngredientData ingredientData, IIngredientGroupData ingredientGroupData)
        {
            this.ingredientData = ingredientData;
            this.ingredientGroupData = ingredientGroupData;
        }
        public async Task<IActionResult> OnGet()
        {
            Groups = await ingredientGroupData.GetAllAsync();
            Groups = Groups.OrderBy(g => g.Group).ToList();


            if (!string.IsNullOrEmpty(SearchSubgroup))
            {
                Ingredients = await ingredientData.GetAllAsync();
                Ingredients = Ingredients.Where(i => i.IngredientSubGroup.SubGroup == SearchSubgroup)
                                         .OrderBy(i => i.Name).ToList();

                Title = Ingredients.Count > 0 ? Ingredients[0].IngredientSubGroup.SubGroup : "No results";
            }

            else if (!string.IsNullOrEmpty(SearchIngredient))
            {
                Ingredients = await ingredientData.GetByNameAsync(SearchIngredient);
                Ingredients = Ingredients.OrderBy(i => i.Name).ToList();
                Title = "Search: " + SearchIngredient;
            }
            else
            {
                Ingredients = await ingredientData.GetAllAsync();
                Ingredients = Ingredients.OrderBy(i => i.Name).ToList();
                Title = "Ingredients";
            }
            Results = Ingredients.Count;

            return Page();
        }
    }
}
