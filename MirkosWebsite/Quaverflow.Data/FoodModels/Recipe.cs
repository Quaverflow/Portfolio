using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Quaverflow.Data.FoodModels
{
    public class Recipe
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string CreatedBy { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "The serves value cannot be lower than 1.")]
        public int Serves { get; set; }
        public string Video { get; set; }
        public List<RecipeStep> Steps { get; set; }
        public List<RecipeIngredient> Ingredients { get; set; }
    }
}
