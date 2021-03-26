using Microsoft.EntityFrameworkCore;
using Quaverflow.Data.CovidModels;
using Quaverflow.Data.FoodModels;
using Quaverflow.Data.MusicModels;

namespace MusicTechnologies.Data
{
    public class QuaverflowDbContext : DbContext
    {
        public QuaverflowDbContext(DbContextOptions<QuaverflowDbContext> options) 
            : base(options)
        {

        }
        public DbSet<Mode> Modes { get; set; }
        public DbSet<Scale> Scales { get; set; }
        public DbSet<Interval> Intervals { get; set; }

        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<IngredientGroup> IngredientGroups { get; set; }
        public DbSet<IngredientSubGroup> IngredientSubGroups { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<RecipeIngredient> RecipeIngredients { get; set; }

        public DbSet<Continent> Continents { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<CovidDailySummary> CovidDailySummaries { get; set; }
    }
}
