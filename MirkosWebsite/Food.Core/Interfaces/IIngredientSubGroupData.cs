using Quaverflow.Data.FoodModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Food.Core.Interfaces
{
    public interface IIngredientSubGroupData
    {
        Task<List<IngredientSubGroup>> GetAllAsync();
        Task<IngredientSubGroup> GetByIdAsync(int id);
    }
}
