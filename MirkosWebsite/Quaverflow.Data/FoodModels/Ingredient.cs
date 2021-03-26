using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Quaverflow.Data.FoodModels
{
    public class Ingredient
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string ScientificName { get; set; }
        public string Description { get; set; }
        public string ImageFileName { get; set; }
        public int IngredientGroupId { get; set; }
        public int IngredientSubGroupId { get; set; }
        public IngredientGroup IngredientGroup { get; set; }
        public IngredientSubGroup IngredientSubGroup { get; set; }
        public List<RecipeIngredient> RecipeIngredient { get; set; }
    }
}
