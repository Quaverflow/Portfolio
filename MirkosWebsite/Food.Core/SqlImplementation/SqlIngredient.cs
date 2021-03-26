using Food.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using MusicTechnologies.Data;
using Quaverflow.Data.FoodModels;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace Food.Core.SqlImplementation
{
    public class SqlIngredient : IIngredientData
    {
        private readonly QuaverflowDbContext quaverflowDbContext;
        private readonly HtmlEncoder htmlEncoder;

        public SqlIngredient(QuaverflowDbContext quaverflowDbContext, HtmlEncoder htmlEncoder)
        {
            this.quaverflowDbContext = quaverflowDbContext;
            this.htmlEncoder = htmlEncoder;
        }
        public async Task<List<Ingredient>> GetAllAsync()
        {
            return await quaverflowDbContext.Ingredients
                .Include(i => i.IngredientGroup)
                .Include(i => i.IngredientSubGroup)
                .OrderBy(i => i.Id)
                .ToListAsync();
        }
        public Ingredient Add(Ingredient ingredient)
        {
            ingredient.Name = htmlEncoder.Encode(ingredient.Name); 
            ingredient.ScientificName = htmlEncoder.Encode(ingredient.ScientificName); 
            ingredient.Description = htmlEncoder.Encode(ingredient.Description); 
            quaverflowDbContext.Add(ingredient);
            return ingredient;
        }
        public int Commit()
        {
            return quaverflowDbContext.SaveChanges();
        }

        public async Task<List<Ingredient>> GetByNameAsync(string ingredientName)
        {
            return await quaverflowDbContext.Ingredients
                        .Include(s => s.IngredientGroup)
                        .Include(s => s.IngredientSubGroup)
                        .Where(s => s.Name.Contains(ingredientName) || string.IsNullOrEmpty(ingredientName)).ToListAsync();

        }

        public async Task<Ingredient> GetByIdAsync(int id)
        {
            return await quaverflowDbContext.Ingredients
                .Include(s => s.IngredientGroup)
                .Include(s => s.IngredientSubGroup).FirstOrDefaultAsync(s => s.Id == id);

        }
    }
}
