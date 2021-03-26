using System.Collections.Generic;
using System.Threading.Tasks;
using Food.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Quaverflow.Data.FoodModels;

namespace Portfolio.Pages.Food
{
    public class AddIngredientModel : PageModel
    {
        private readonly IIngredientData ingredientData;
        private readonly IIngredientGroupData ingredientGroupData;
        private readonly IIngredientSubGroupData ingredientSubGroupData;

        [BindProperty]
        public Ingredient NewIngredient { get; set; }
        public List<IngredientGroup> Groups { get; set; }
        public List<IngredientSubGroup> SubGroups { get; set; }

        public AddIngredientModel(IIngredientData ingredientData, IIngredientGroupData ingredientGroupData, IIngredientSubGroupData ingredientSubGroupData)
        {
            this.ingredientData = ingredientData;
            this.ingredientGroupData = ingredientGroupData;
            this.ingredientSubGroupData = ingredientSubGroupData;
        }
        public async Task<IActionResult> OnGet()
        {
            Groups =  await ingredientGroupData.GetAllAsync();
            SubGroups = await ingredientSubGroupData.GetAllAsync();

            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if(!ModelState.IsValid)
            {
                Groups = await ingredientGroupData.GetAllAsync();
                SubGroups = await ingredientSubGroupData.GetAllAsync();

                return Page();
            }
            NewIngredient.IngredientGroup = await ingredientGroupData.GetByIdAsync(NewIngredient.IngredientGroup.Id);
            NewIngredient.IngredientSubGroup = await ingredientSubGroupData.GetByIdAsync(NewIngredient.IngredientSubGroup.Id);
            NewIngredient.ImageFileName = "000.PNG";
            ingredientData.Add(NewIngredient);
            ingredientData.Commit();

            TempData["Message"] = "Ingredient added!";
            return RedirectToPage("./Ingredients");
        }
    }
}
