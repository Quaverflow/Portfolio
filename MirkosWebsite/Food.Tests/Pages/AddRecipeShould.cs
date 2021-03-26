using System.Collections.Generic;
using System.Threading.Tasks;
using Food.Core.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using Portfolio.Pages.Food;
using Quaverflow.Data.FoodModels;
using Xunit;

namespace Food.Tests.Pages
{
    public class RecipeWriterShould
    {
        private readonly RecipeWriterModel sut;
        private readonly Mock<IRecipeData> mockRecipeData;
        public Recipe Recipe { get; set; }
        public TempDataDictionary TempData { get; set; }

        public RecipeWriterShould()
        {
            Mock<IIngredientData> mockIngredientData = new();

            mockIngredientData.Setup(x => x.GetAllAsync()).Returns(Task.FromResult(new List<Ingredient>()));


            mockRecipeData = new Mock<IRecipeData>();
            Mock<IHtmlHelper> mockHtmlHelper = new();

            sut = new RecipeWriterModel(mockRecipeData.Object, mockIngredientData.Object, mockHtmlHelper.Object);
        }

        [Fact]
        public async Task ReturnPageInvalidModelState()
        {
            sut.ModelState.AddModelError("x", "Test Error");
            var result = await sut.OnPost();
            Assert.IsType<PageResult>(result);
        }

        [Fact]
        public async Task NotSaveScaleIfModelError()
        {
            sut.ModelState.AddModelError("x", "Test Error");
            sut.Recipe = Recipe;
            await sut.OnPost();
            mockRecipeData.Verify(x => x.Add(It.IsAny<Recipe>()), Times.Never);
        }
    }
}

