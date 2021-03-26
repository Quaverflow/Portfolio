using System.ComponentModel.DataAnnotations;

namespace Quaverflow.Data.FoodModels
{
    public class RecipeStep
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Body { get; set; }
        public Recipe Recipe { get; set; }
        public string Video { get; set; }
    }
}
