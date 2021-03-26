using Food.Core.Interfaces;
using MusicTechnologies.Data;
using Quaverflow.Data.FoodModels;

namespace Food.Core.SqlImplementation
{
    public class SqlRecipeIngredient : IRecipeIngredientData
    {
        private readonly QuaverflowDbContext quaverflowDbContext;

        public SqlRecipeIngredient(QuaverflowDbContext quaverflowDbContext)
        {
            this.quaverflowDbContext = quaverflowDbContext;
        }

        public RecipeIngredient Add(RecipeIngredient recipeIngredient)
        {
            quaverflowDbContext.Add(recipeIngredient);
            return recipeIngredient;
        }

    }
}
