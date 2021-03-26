using Food.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using MusicTechnologies.Data;
using Quaverflow.Data.FoodModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Food.Core.SqlImplementation
{
    public class SqlIngredientSubGroup : IIngredientSubGroupData
    {
        private readonly QuaverflowDbContext quaverflowDbContext;

        public SqlIngredientSubGroup(QuaverflowDbContext quaverflowDbContext)
        {
            this.quaverflowDbContext = quaverflowDbContext;
        }
        public async Task<List<IngredientSubGroup>> GetAllAsync()
        {
            return await quaverflowDbContext.IngredientSubGroups
                   .Include(i => i.Ingredients)
                   .OrderBy(s => s.SubGroup)
                   .ToListAsync();
        }
        public async Task<IngredientSubGroup> GetByIdAsync(int id)
        {
            return await quaverflowDbContext.IngredientSubGroups
                .Include(i => i.Ingredients)
                .FirstOrDefaultAsync(i => i.Id == id);
        }
    }
}
