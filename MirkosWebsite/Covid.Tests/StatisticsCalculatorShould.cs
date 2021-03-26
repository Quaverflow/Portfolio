using Quaverflow.Data.CovidModels;
using System.Collections.Generic;
using System;
using Xunit;
using Covid.Core.Utilities;

namespace Covid.Tests
{
    public class StatisticsCalculatorShould
    {
        private readonly Country mockCountry = new()
        {
            DailySummary = new List<CovidDailySummary> 
            { 
                new() { NewDeaths = 1, NewCases = 10, TotalDeaths = 1, TotalCases = 10, Date = new DateTime(2020, 10, 24)},
                new() { NewDeaths = 4, NewCases = 40, TotalDeaths = 5, TotalCases = 50, Date = new DateTime(2020, 10, 30)},
                new() { NewDeaths = 5, NewCases = 50, TotalDeaths = 10, TotalCases = 100, Date = new DateTime(2020, 10, 31)}
            },
            Population = 10000,
        };

        [Fact]
        public void AccuratelyGiveDeathToPopulationRatio()
        {
            var sut = StatisticsCalculator.GetDeathToPopulationRatio(mockCountry);

            Assert.Equal(0.1, sut);
        }
        [Fact]
        public void AccuratelyGiveDeathToInfectionRatio()
        {
            var sut = StatisticsCalculator.GetDeathToInfectionRatio(mockCountry);

            Assert.Equal(10, sut);
        }

        [Fact]
        public void SumDayOfTheWeekDeaths()
        {
            var sut = StatisticsCalculator.GetCountryDeathsByDayOfWeek(mockCountry);
            Assert.Equal(6, sut["Saturday"]);
        }

        [Fact]
        public void SumDayOfTheWeekInfections()
        {
            var sut = StatisticsCalculator.GetCountryInfectionsByDayOfWeek(mockCountry);
            Assert.Equal(60, sut["Saturday"]);
        }

        [Fact]
        public void SumDayOfTheWeekDeathWorld()
        {
            var mockCountries = new List<Country> { mockCountry, mockCountry, mockCountry, mockCountry };

            var sut = StatisticsCalculator.GetWorldDeathsByDayOfWeek(mockCountries);

            Assert.Equal(24, sut["Saturday"]);
        }

        [Fact]
        public void SumDayOfTheWeekInfectionsWorld()
        {
            var mockCountries = new List<Country> { mockCountry, mockCountry, mockCountry, mockCountry };

            var sut = StatisticsCalculator.GetWorldInfectionsByDayOfWeek(mockCountries);

            Assert.Equal(240, sut["Saturday"]);
        }
    }
}
