using Quaverflow.Data.FoodModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Food.Core.Interfaces
{
    public interface IIngredientGroupData
    {
        Task<List<IngredientGroup>> GetAllAsync();
        Task<IngredientGroup> GetByIdAsync(int id);
    }
}
