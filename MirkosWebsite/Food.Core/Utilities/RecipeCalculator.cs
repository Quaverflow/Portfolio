using System;
using Quaverflow.Data.FoodModels;

namespace Food.Core.Utilities
{
    public static class RecipeCalculator
    {
        public static Recipe Scale(Recipe recipe, int newValue)
        {
            foreach (var ingredient in recipe.Ingredients)
            {
                ingredient.Quantity = Math.Round(ingredient.Quantity * newValue / recipe.Serves, 2);
            }

            return recipe;
        }
        public static string GetUnitApprox(double unit)
        {
            var realNumber = (int)unit;
            var decimalPart = (int)Math.Truncate((unit - realNumber)*100);
            string finalString;

            switch (decimalPart)
            {
                case > 90:
                    realNumber++;
                    finalString = realNumber.ToString();
                    break;
                case > 25:
                    finalString = $"{realNumber}" + DecimalToFraction(decimalPart);
                    break;
                default:
                    finalString = realNumber.ToString();
                    break;
            }

            return "≈" + finalString;

            static string DecimalToFraction(int unit)
            {
                return (unit) switch
                {
                    > 74 => " & 3/4",
                    > 65 => " & 2/3",
                    > 49 => " & 1/2",
                    > 32 => " & 1/3",
                    _ => " & 1/4"
                };
            }

        }
    }
}
