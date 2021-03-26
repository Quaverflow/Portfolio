using System.Collections.Generic;

namespace Quaverflow.Data.FoodModels

{
    public class IngredientSubGroup
    {
        public int Id { get; set; }
        public string SubGroup { get; set; }
        public IngredientGroup IngredientGroup { get; set; }
        public List<Ingredient> Ingredients { get; set; }
    }
}
