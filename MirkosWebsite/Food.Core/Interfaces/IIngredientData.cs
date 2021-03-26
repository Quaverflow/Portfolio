using Quaverflow.Data.FoodModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Food.Core.Interfaces
{
    public interface IIngredientData
    {
        Task<List<Ingredient>> GetAllAsync();
        Ingredient Add(Ingredient ingredient);
        Task<List<Ingredient>> GetByNameAsync(string ingredientName);
        Task<Ingredient> GetByIdAsync(int id);
        public int Commit();
    }
}
