using System.ComponentModel.DataAnnotations;

namespace Quaverflow.Data.FoodModels
{
    public class RecipeIngredient
    {
        public int Id { get; set; }
        [Required]
        public Ingredient Ingredient { get; set; }
        public UnitMeasure UnitMeasure { get; set; }
        [Required (ErrorMessage = "Please enter a value.")]
        [Range(0.001, double.MaxValue, ErrorMessage = "Please enter a value bigger than 0.")]
        public double Quantity { get; set; }
        [MaxLength(80, ErrorMessage = "Maximum characters allowed is 80.")]
        public string Notes { get; set; }
    }
}
