using Quaverflow.Data.FoodModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Food.Core.Interfaces
{
    public interface IRecipeData
    {
        Recipe Add(Recipe recipe);
        int Commit();
        Task<Recipe> GetByIdAsync(int recipeId);
        Task<Recipe> DeleteAsync(Recipe recipe);
        Task<List<Recipe>> GetAllAsync();
        Task<List<Recipe>> GetByNameAsync(string recipeName);
    }
}
