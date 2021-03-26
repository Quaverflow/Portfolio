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
    public class SqlRecipe : IRecipeData
    {
        private readonly QuaverflowDbContext quaverflowDbContext;
        private readonly HtmlEncoder htmlEncoder;

        public SqlRecipe(QuaverflowDbContext quaverflowDbContext, HtmlEncoder htmlEncoder)
        {
            this.quaverflowDbContext = quaverflowDbContext;
            this.htmlEncoder = htmlEncoder;
        }

        public Recipe Add(Recipe recipe)
        {
            for (var i = 0; i < recipe.Ingredients.Count; i++)
            {
                recipe.Ingredients[i].Ingredient = (from x in quaverflowDbContext.Ingredients
                                                    where x.Id == recipe.Ingredients[i].Ingredient.Id
                                                    select x)
                                                    .FirstOrDefault();
            }

            recipe = Sanitize(recipe);

            quaverflowDbContext.Add(recipe);

            return recipe;
        }
        public int Commit()
        {
            return quaverflowDbContext.SaveChanges();
        }
        public async Task<Recipe> GetByIdAsync(int recipeId)
        {
            return await quaverflowDbContext.Recipes
                                      .Include(r => r.Ingredients)
                                      .ThenInclude(i => i.Ingredient)
                                      .Include(r => r.Steps)
                                      .Where(r => r.Id == recipeId)
                                      .FirstOrDefaultAsync();

        }
        public async Task<Recipe> DeleteAsync(Recipe recipe)
        {
            recipe = await GetByIdAsync(recipe.Id);
            foreach(var ingredient in recipe.Ingredients)
            {
                quaverflowDbContext.Remove(ingredient);
            }
            foreach (var step in recipe.Steps)
            {
                quaverflowDbContext.Remove(step);
            }
            quaverflowDbContext.Recipes.Remove(recipe);
            return recipe;
        }

        public async Task<List<Recipe>> GetAllAsync()
        {
            return await quaverflowDbContext.Recipes
                   .Include(r => r.Ingredients)
                   .ThenInclude(i => i.Ingredient)
                   .Include(r => r.Steps)
                   .ToListAsync();
        }
        public async Task<List<Recipe>> GetByNameAsync(string recipeName)
        {
            return await quaverflowDbContext.Recipes
                                      .Include(r => r.Ingredients)
                                      .ThenInclude(i => i.Ingredient)
                                      .Include(r => r.Steps)
                                      .Where(r => r.Name.Contains(recipeName)
                                      || string.IsNullOrEmpty(recipeName)).ToListAsync();
        }

        private Recipe Sanitize(Recipe recipe)
        {
            recipe.Name = htmlEncoder.Encode(recipe.Name);
            recipe.CreatedBy = htmlEncoder.Encode(recipe.CreatedBy);
            recipe.Video = recipe.Video != null ? htmlEncoder.Encode(recipe.Video) : null;

            foreach (var step in recipe.Steps)
            {
                step.Name = htmlEncoder.Encode(step.Name);
                step.Body = htmlEncoder.Encode(step.Body);
            }

            foreach (var note in recipe.Ingredients)
            {
                note.Notes = note.Notes != null ? htmlEncoder.Encode(note.Notes) : null;
            }

            return recipe;
        }
    }
}
