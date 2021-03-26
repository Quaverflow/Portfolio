using System.Collections.Generic;

namespace Quaverflow.Data.FoodModels

{
    public class IngredientGroup
    {
        public int Id { get; set; }
        public string Group { get; set; }
        public List<Ingredient> Ingredients { get; set; }
        public List<IngredientSubGroup> IngredientSubGroups { get; set; }
    }
}
