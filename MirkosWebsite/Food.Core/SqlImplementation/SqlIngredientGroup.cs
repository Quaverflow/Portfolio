using Food.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using MusicTechnologies.Data;
using Quaverflow.Data.FoodModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Food.Core.SqlImplementation
{
    public class SqlIngredientGroup : IIngredientGroupData
    {
        private readonly QuaverflowDbContext quaverflowDbContext;

        public SqlIngredientGroup(QuaverflowDbContext quaverflowDbContext)
        {
            this.quaverflowDbContext = quaverflowDbContext;
        }
        public async Task<List<IngredientGroup>> GetAllAsync()
        {
            return await quaverflowDbContext.IngredientGroups
                                            .Include(i => i.Ingredients)
                                            .Include(i => i.IngredientSubGroups
                                            .OrderBy(s => s.SubGroup)).ToListAsync();
        }
        public async Task<IngredientGroup> GetByIdAsync(int id)
        {
            return await quaverflowDbContext.IngredientGroups
                   .Include(i => i.Ingredients)
                   .Where(i => i.Id == id)
                   .FirstOrDefaultAsync();
        }
    }
}
