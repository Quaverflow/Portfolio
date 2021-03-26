using Quaverflow.Data.MusicModels;
using System.Collections.Generic;
using System.Linq;

namespace MusicTechnologies.Core.Utilities
{
    public static class IntervalsCalculator
    {
        public static List<Interval> SumRelativeDistance(List<Interval> intervals)
        {
            foreach (var interval in intervals)
            {
                interval.Distance += (int)interval.Degree;
            }
            return intervals;
        }

        public static List<Interval> ReverseRelativeDistance(List<Interval> intervals)
        {
            return intervals.Select(interval => new Interval {Degree = interval.Degree, Distance = interval.Distance - (int) interval.Degree}).ToList();
        }

        public static Interval GetNewInterval(Degree degree, int distance)
        {
            return new()
            {
                Degree = degree,
                Distance = distance
            };
        }
    }
}
