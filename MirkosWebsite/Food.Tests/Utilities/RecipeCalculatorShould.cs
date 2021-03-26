using Food.Core.Utilities;
using Quaverflow.Data.FoodModels;
using System.Collections.Generic;
using Xunit;

namespace Food.Tests.Utilities
{
    public class RecipeCalculatorShould
    {
        [Fact]
        public void GetAFormattedResultForApproxUnits()
        {
            var sut = RecipeCalculator.GetUnitApprox(1.5);
            Assert.Equal("≈1 & 1/2", sut);

            sut = RecipeCalculator.GetUnitApprox(3.75);
            Assert.Equal("≈3 & 3/4", sut);

            sut = RecipeCalculator.GetUnitApprox(0.15);
            Assert.Equal("≈0", sut);

            sut = RecipeCalculator.GetUnitApprox(1);
            Assert.Equal("≈1", sut);
        }

        [Fact]
        public void ReturnScaledResultsAccordingToServings()
        {
            var mockRecipe = new Recipe
            {
                Serves = 1,
                Ingredients = new List<RecipeIngredient>
                {
                    new RecipeIngredient
                    {
                        Ingredient = new Ingredient {Name = "Sous vide tires"},
                        Quantity = 100,
                        UnitMeasure = UnitMeasure.g
                    },
                    new RecipeIngredient
                    {
                        Ingredient = new Ingredient {Name = "Fried bricks"},
                        Quantity = 5.02,
                        UnitMeasure = UnitMeasure.oz
                    },
                    new RecipeIngredient
                    {
                        Ingredient = new Ingredient {Name = "Soupe du frigo"},
                        Quantity = 0.03,
                        UnitMeasure = UnitMeasure.l
                    }
                }
            };

            var sut = RecipeCalculator.Scale(mockRecipe, 2);

            Assert.Equal(200, sut.Ingredients[0].Quantity);
            Assert.Equal(10.04, sut.Ingredients[1].Quantity);
            Assert.Equal(0.06, sut.Ingredients[2].Quantity);

        }
    }
}
