using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualBasic.FileIO;
using Quaverflow.Data.FoodModels;

namespace Food.Core
{
    public class FoodReader
    {

        public static List<Ingredient> GetIngredients()
        {
            TextFieldParser parser = new TextFieldParser("D:/Coding studies/MirkosWebsite/Portfolio/Datasets/Food/Food2.csv")
            {
                HasFieldsEnclosedInQuotes = true
            };
            parser.SetDelimiters(",");

            string[] fields;
            var counter = 0;
            var ingredients = new List<Ingredient>();
            var ingredientsG = new List<IngredientGroup>() { new IngredientGroup() { Group = "Other" } };
            var ingredientsSG = new List<IngredientSubGroup>() {
                new IngredientSubGroup()
                {
                    SubGroup = "Other",
                    IngredientGroup = ingredientsG[0]
                }};


            while (!parser.EndOfData)
            {
                fields = parser.ReadFields();
                var ingredient = new Ingredient();

                if (int.TryParse(fields[0], out int x))
                {
                    ingredient.Name = fields[1];
                    ingredient.ScientificName = fields[2];

                    var description = fields[3];

                    var substring1 = "<i>";
                    while (description.Contains(substring1))
                    {
                        var indexSub1 = description.IndexOf(substring1);
                        description = description.Remove(indexSub1, substring1.Length);
                    }
                        
                    var substring2 = "</i>";
                    while (description.Contains(substring2))
                    {
                        var indexSub2 = description.IndexOf(substring2);
                        description = description.Remove(indexSub2, substring2.Length);
                    }
                    ingredient.Description = description;


                    ingredient.ImageFileName = $"{counter}.PNG";
                    if (IsNewGroup(ingredients, fields[11]) || string.IsNullOrWhiteSpace(fields[11]))
                    {
                        if (string.IsNullOrWhiteSpace(fields[11]))
                        {
                            var gr = ingredientsG[0];
                            ingredient.IngredientGroup = gr;
                        }
                        else
                        {
                            var gr = new IngredientGroup() { Group = fields[11] };
                            ingredient.IngredientGroup = gr;
                            ingredientsG.Add(gr);
                        }
                    }
                    else
                    {
                        ingredient.IngredientGroup = (IngredientGroup)ingredientsG.Where(i => i.Group == fields[11]).FirstOrDefault();
                    }
                    if (IsNewSubGroup(ingredients, fields[12]) || string.IsNullOrWhiteSpace(fields[11]))
                    {
                        if (string.IsNullOrWhiteSpace(fields[11]))
                        {
                            var sgr = ingredientsSG[0];
                            ingredient.IngredientSubGroup = sgr;
                        }
                        else
                        {
                            var sgr = new IngredientSubGroup()
                            {
                                SubGroup = fields[12],
                                IngredientGroup = (IngredientGroup)ingredientsG.Where(i => i.Group == fields[11]).FirstOrDefault()
                            };
                            ingredient.IngredientSubGroup = sgr;
                            ingredientsSG.Add(sgr);
                        }
                    }
                    else
                    {
                        ingredient.IngredientSubGroup = ingredientsSG.Where(i => i.SubGroup == fields[12]).FirstOrDefault();
                    }

                    ingredients.Add(ingredient);
                    counter++;
                }
            }
            return ingredients;

            static bool IsNewGroup(List<Ingredient> ingredients, string group)
            {
                return !ingredients.Where(i => i.IngredientGroup.Group == group).Any();
            }
            static bool IsNewSubGroup(List<Ingredient> ingredients, string subGroup)
            {
                return !ingredients.Where(i => i.IngredientSubGroup.SubGroup == subGroup).Any();
            }
        }
    }
}
