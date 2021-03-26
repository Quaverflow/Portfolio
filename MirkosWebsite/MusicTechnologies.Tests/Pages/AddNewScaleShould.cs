using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using MusicTechnologies.Core.Interfaces;
using Portfolio.Pages.MusicScales;
using Quaverflow.Data.MusicModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace MusicTechnologies.Tests.Pages
{
    public class AddNewScaleShould
    {
        private readonly AddNewScaleModel sut;
        private readonly Mock<IModeData> mockModeData;
        public Scale MockScale { get; set; }
        private readonly Mock<IScaleData> mockScaleData;
        public TempDataDictionary TempData { get; set; }

        public AddNewScaleShould()
        {
            mockScaleData = new Mock<IScaleData>();
            mockModeData = new Mock<IModeData>();
            Mock<IIntervalData> mockIntervalData = new();
            Mock<IScaleCalculator> mockScaleCalculator = new();
            Mock<IHtmlHelper> mockHtmlHelper = new();
            sut = new AddNewScaleModel(mockModeData.Object, mockScaleData.Object, mockIntervalData.Object, mockHtmlHelper.Object, mockScaleCalculator.Object);

            List<Interval> intervals = new()
            {
                new Interval { Degree = Degree.Root, Distance = 1 },
                new Interval { Degree = Degree.Second, Distance = 3 },
                new Interval { Degree = Degree.Third, Distance = 5 },
                new Interval { Degree = Degree.Fifth, Distance = 8 },
                new Interval { Degree = Degree.Sixth, Distance = 10 }
            };

            List<Mode> modes = new()
            {
                new Mode { Name = "Major Pentatonic", Intervals = intervals },
                new Mode { Name = "Egyptian Scale", Intervals = new List<Interval> { intervals[1], intervals[2], intervals[3], intervals[4], intervals[0] } },
                new Mode { Name = "Man Gong", Intervals = new List<Interval> { intervals[2], intervals[3], intervals[4], intervals[0], intervals[1] } },
                new Mode { Name = "Ritsusen", Intervals = new List<Interval> { intervals[3], intervals[4], intervals[0], intervals[1], intervals[2] } },
                new Mode { Name = "Minor Pentatonic", Intervals = new List<Interval> { intervals[4], intervals[0], intervals[1], intervals[2], intervals[3] } }
            };

            MockScale = new Scale { Name = "Mock", Modes = modes, CreatedBy = "ME", Id = 1 };
            TempData = new TempDataDictionary(new DefaultHttpContext(), Mock.Of<ITempDataProvider>());

        }


        [Fact]
        public async Task ReturnPageInvalidModelState()
        {
            sut.ModelState.AddModelError("x", "Test Error");
            IActionResult result = await sut.OnPost();
            Assert.IsType<PageResult>(result);
        }

        [Fact]
        public async Task NotSaveScaleIfModelError()
        {
            sut.ModelState.AddModelError("x", "Test Error");
            sut.Scale = MockScale;
            await sut.OnPost();
            mockScaleData.Verify(x => x.Add(It.IsAny<Scale>()), Times.Never);
        }

        [Fact]
        public async Task SaveScaleIfModelValid()
        {
            Scale savedScale = null;

            mockScaleData.Setup(x => x.Add(It.IsAny<Scale>())).Returns(MockScale).Callback<Scale>(x => savedScale = x);
            mockModeData.Setup(x => x.Populate(It.IsAny<Scale>(), 1)).Returns(MockScale.Modes).Callback<Scale, int>((s, _) => { savedScale = s; });

            sut.Scale = MockScale;
            sut.TempData = TempData;
            await sut.OnPost();

            mockScaleData.Verify(x => x.Add(It.IsAny<Scale>()), Times.Once);

            Assert.Equal(MockScale.Name, savedScale.Name);
            Assert.Equal(MockScale.CreatedBy, savedScale.CreatedBy);
        }

        [Fact]
        public async Task ReturnDetailsPageOnScaleSaved()
        {
            mockScaleData.Setup(x => x.Add(It.IsAny<Scale>())).Returns(MockScale).Callback<Scale>(_ => { });
            mockModeData.Setup(x => x.Populate(It.IsAny<Scale>(), 1)).Returns(MockScale.Modes).Callback<Scale, int>((_, _) => { });
            sut.Scale = MockScale;
            sut.TempData = TempData;
            var result = await sut.OnPost();
            var pageResult = Assert.IsType<RedirectToPageResult>(result);

            Assert.Equal("./Details", pageResult.PageName);
        }
    }
}
