using Food.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using Portfolio.Pages.Food;
using Quaverflow.Data.FoodModels;
using System.Threading.Tasks;
using Xunit;

namespace Food.Tests.Pages
{
    public class AddIngredientShould
    {
        private readonly AddIngredientModel sut;
        private readonly Ingredient ingredient;
        private readonly Mock<IIngredientData> mockIngredientData;
        public TempDataDictionary TempData { get; set; }
        public AddIngredientShould()
        {
            mockIngredientData = new();
            Mock<IIngredientGroupData> mockIngredientGroupData = new();
            Mock<IIngredientSubGroupData> mockIngredientSubGroupData = new();

            sut = new(mockIngredientData.Object, mockIngredientGroupData.Object, mockIngredientSubGroupData.Object);

           ingredient = new Ingredient
            {
                Name = "Hummingbird infused roti canai",
                Description = "This ingredient should never exist",
                IngredientGroup = new IngredientGroup { Group = "Other" },
                IngredientSubGroup = new IngredientSubGroup { SubGroup = "Other" },
                ScientificName = "NOT ALLLOWED TO EXIST"
            };

            TempData = new TempDataDictionary(new DefaultHttpContext(), Mock.Of<ITempDataProvider>());

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
            sut.NewIngredient = ingredient;
            await sut.OnPost();
            mockIngredientData.Verify(x => x.Add(It.IsAny<Ingredient>()), Times.Never);
        }

        [Fact]
        public async Task SaveScaleIfModelValid()
        {

            Ingredient savedIngredient = null;

            mockIngredientData.Setup(x => x.Add(It.IsAny<Ingredient>())).Returns(ingredient).Callback<Ingredient>(x => savedIngredient = x);

            sut.NewIngredient = ingredient;
            sut.TempData = TempData;
            await sut.OnPost();

            mockIngredientData.Verify(x => x.Add(It.IsAny<Ingredient>()), Times.Once);

            Assert.Equal(ingredient.Name, savedIngredient.Name);
            Assert.Equal(ingredient.IngredientGroup, savedIngredient.IngredientGroup);
            Assert.Equal(ingredient.IngredientSubGroup, savedIngredient.IngredientSubGroup);


        }

        [Fact]
        public async Task ReturnDetailsPageOnScaleSaved()
        {
            sut.NewIngredient = ingredient;
            sut.TempData = TempData;
            var result = await sut.OnPost();
            var pageResult = Assert.IsType<RedirectToPageResult>(result);

            Assert.Equal("./Ingredients", pageResult.PageName);
        }
    }
}
