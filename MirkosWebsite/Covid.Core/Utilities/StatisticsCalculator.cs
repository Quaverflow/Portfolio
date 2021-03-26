using Quaverflow.Data.CovidModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Covid.Core.Utilities
{
    public static class StatisticsCalculator
    {
        public static Dictionary<string, int> GetCountryDeathsByDayOfWeek(Country country)
        {
            var daysOfTheWeek = GetWeekDictionary();


            foreach (var day in country.DailySummary)
            {

                switch (day.Date.DayOfWeek)
                {
                    case DayOfWeek.Monday:
                        daysOfTheWeek["Monday"] += day.NewDeaths; break;
                    case DayOfWeek.Tuesday:
                        daysOfTheWeek["Tuesday"] += day.NewDeaths; break;
                    case DayOfWeek.Wednesday:
                        daysOfTheWeek["Wednesday"] += day.NewDeaths; break;
                    case DayOfWeek.Thursday:
                        daysOfTheWeek["Thursday"] += day.NewDeaths; break;
                    case DayOfWeek.Friday:
                        daysOfTheWeek["Friday"] += day.NewDeaths; break;
                    case DayOfWeek.Saturday:
                        daysOfTheWeek["Saturday"] += day.NewDeaths; break;
                    default:
                        daysOfTheWeek["Sunday"] += day.NewDeaths; break;
                }
            }
            return daysOfTheWeek;
        }
        public static Dictionary<string, int> GetCountryInfectionsByDayOfWeek(Country country)
        {
            var daysOfTheWeek = GetWeekDictionary();

            foreach (var day in country.DailySummary)
            {
                switch (day.Date.DayOfWeek)
                {
                    case DayOfWeek.Monday:
                        daysOfTheWeek["Monday"] += day.NewCases; break;
                    case DayOfWeek.Tuesday:
                        daysOfTheWeek["Tuesday"] += day.NewCases; break;
                    case DayOfWeek.Wednesday:
                        daysOfTheWeek["Wednesday"] += day.NewCases; break;
                    case DayOfWeek.Thursday:
                        daysOfTheWeek["Thursday"] += day.NewCases; break;
                    case DayOfWeek.Friday:
                        daysOfTheWeek["Friday"] += day.NewCases; break;
                    case DayOfWeek.Saturday:
                        daysOfTheWeek["Saturday"] += day.NewCases; break;
                    default:
                        daysOfTheWeek["Sunday"] += day.NewCases; break;
                }
            }
            return daysOfTheWeek;
        }
        public static Dictionary<string, int> GetWorldDeathsByDayOfWeek(List<Country> countries)
        {
            var DOWCollection = new List<Dictionary<string, int>>();
            foreach (var country in countries)
            {
                DOWCollection.Add(GetCountryDeathsByDayOfWeek(country));
            }
            return SumStats(DOWCollection);
        }
        public static Country GetWorld(List<Country> countries)
        {
            var world = new Country()
            {
                Name = "World"
            };
            countries = countries.OrderByDescending(c => c.DailySummary.Count).ToList();
            foreach (var country in countries)
            {
                if (world.DailySummary == null)
                {
                    world.DailySummary = country.DailySummary.Where(d => d.Date.Year != 2021).ToList();
                    world.Population = country.Population;
                    world.LifeExpectancy = country.LifeExpectancy;
                }
                else
                {
                    PopulateWorld(world, country);
                }
            }
            return world;

            static void PopulateWorld(Country world, Country country)
            {
                for (int i = 0; i < world.DailySummary.Count; i++)
                {
                    if (country.DailySummary.Count > i)
                    {
                        world.DailySummary[i].NewDeaths += country.DailySummary[i].NewDeaths;
                        world.DailySummary[i].NewCases += country.DailySummary[i].NewCases;
                        world.DailySummary[i].TotalDeaths += country.DailySummary[i].TotalDeaths;
                        world.DailySummary[i].TotalCases += country.DailySummary[i].TotalCases;
                    }
                }
                world.Population += country.Population;
                world.LifeExpectancy += country.LifeExpectancy;
            }
        }
        public static Dictionary<string, int> GetWorldInfectionsByDayOfWeek(List<Country> countries)
        {
            var DOWCollection = new List<Dictionary<string, int>>();
            foreach (var country in countries)
            {
                DOWCollection.Add(GetCountryInfectionsByDayOfWeek(country));
            }
            return SumStats(DOWCollection);

        }
        public static double GetDeathToPopulationRatio(Country country)
        {
            return Math.Round((double)country.DailySummary[^1].TotalDeaths / country.Population * 100, 2);
        }
        public static double GetDeathToInfectionRatio(Country country)
        {
            return Math.Round((double)country.DailySummary[^1].TotalDeaths / country.DailySummary[^1].TotalCases * 100, 2);
        }
        private static Dictionary<string, int> SumStats(List<Dictionary<string, int>> DOWCollection)
        {
            var weekDays = GetWeekDictionary();

            foreach (var DOW in DOWCollection)
            {
                weekDays["Monday"] += DOW["Monday"];
                weekDays["Tuesday"] += DOW["Tuesday"];
                weekDays["Wednesday"] += DOW["Wednesday"];
                weekDays["Thursday"] += DOW["Thursday"];
                weekDays["Friday"] += DOW["Friday"];
                weekDays["Saturday"] += DOW["Saturday"];
                weekDays["Sunday"] += DOW["Sunday"];
            }
            return weekDays;
        }
        private static Dictionary<string, int> GetWeekDictionary()
        {
            return new Dictionary<string, int>()
            {
                { "Monday", 0},
                { "Tuesday", 0},
                { "Wednesday", 0},
                { "Thursday", 0},
                { "Friday", 0},
                { "Saturday", 0},
                { "Sunday", 0}
            };
        }
     

    }
}
