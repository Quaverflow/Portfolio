using MusicTechnologies.Core.Utilities;
using Quaverflow.Data.MusicModels;
using System.Collections.Generic;
using Xunit;

namespace MusicTechnologies.Tests.Utilities
{
    public class ScaleValidatorShould
    {
        private readonly List<Interval> intervals;

        public ScaleValidatorShould()
        {
            intervals = new List<Interval>()
            {
                new() { Degree = Degree.Root, Distance = 1 },
                new() { Degree = Degree.Second, Distance = 4 },
                new() { Degree = Degree.Third, Distance = 3 }
            };
        }

        [Fact]
        public void ReturnFalseFullInput()
        {
            var modes = new List<Mode>
            {
                new() { Name = null, Intervals = intervals },
                new() { Name = "Hi" },
                new() { Name = "Welcome" }
            };
            var scale = new Scale { Modes = modes };

            var sut = ScaleValidator.VerifyFullInput(scale, out _);

            Assert.False(sut);
        }

        [Fact]
        public void ReturnTrueFullInput()
        {
            var modes = new List<Mode>
            {
                new() { Name = "Hello!", Intervals = intervals },
                new() { Name = "Hi" },
                new() { Name = "Welcome" }
            };

            var scale = new Scale { Modes = modes };

            var sut = ScaleValidator.VerifyFullInput(scale, out _);

            Assert.True(sut);
        }

        [Fact]
        public void ReturnFalseAreValuesLogical()
        {
            var sut = ScaleValidator.AreValuesLogical(intervals, out _);
            Assert.False(sut);
        }
        [Fact]
        public void ReturnTrueAreValuesLogical()
        {
            intervals[2].Distance = 6;
            var sut = ScaleValidator.AreValuesLogical(intervals, out _);
            Assert.True(sut);
        }
    }
}
