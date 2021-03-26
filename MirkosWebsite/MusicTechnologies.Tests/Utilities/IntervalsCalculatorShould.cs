using MusicTechnologies.Core.Utilities;
using Quaverflow.Data.MusicModels;
using System.Collections.Generic;
using Xunit;

namespace MusicTechnologies.Tests.Utilities
{
    public class IntervalsCalculatorShould
    {
        public readonly List<Interval> Intervals = new()
        {
            new Interval { Degree = Degree.Root, Distance = 0},
            new Interval { Degree = Degree.Second, Distance = 0},
            new Interval { Degree = Degree.Third, Distance = 0},
            new Interval { Degree = Degree.Fourth, Distance = 0},
            new Interval { Degree = Degree.Fifth, Distance = 0},
            new Interval { Degree = Degree.Sixth, Distance = 0},
            new Interval { Degree = Degree.Seventh, Distance = 0}
        };


        [Fact]
        public void SumRelativeDistance()
        {
            var sut = IntervalsCalculator.SumRelativeDistance(Intervals);

            Assert.Equal(1, sut[0].Distance);
            Assert.Equal(3, sut[1].Distance);
            Assert.Equal(5, sut[2].Distance);
            Assert.Equal(6, sut[3].Distance);
            Assert.Equal(8, sut[4].Distance);
            Assert.Equal(10, sut[5].Distance);
            Assert.Equal(12, sut[6].Distance);
        }

        [Fact]
        public void GetNewInterval()
        {
            var sut = IntervalsCalculator.GetNewInterval(Degree.Second, 3);

            Assert.Equal(3, sut.Distance);
            Assert.Equal(Degree.Second, sut.Degree);

        }
    }
}
