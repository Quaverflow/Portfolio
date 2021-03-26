using Microsoft.EntityFrameworkCore;
using MusicTechnologies.Core.Interfaces;
using MusicTechnologies.Data;
using Quaverflow.Data.MusicModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MusicTechnologies.Core.Utilities;

namespace MusicTechnologies.Core.SqlImplementation

{
    public class SqlMode : IModeData
    {
        private readonly QuaverflowDbContext quaverflowDbContext;
        private readonly IIntervalData intervalData;

        public SqlMode(QuaverflowDbContext quaverflowDbContext, IIntervalData intervalData)
        {
            this.quaverflowDbContext = quaverflowDbContext;
            this.intervalData = intervalData;
        }

        public IEnumerable<Mode> Populate(Scale scale, int scaleId)
        {
            var modes = scale.Modes;
            var intervals = modes[0].Intervals;
            for (var i = 0; i < intervals.Count; i++)
            {
                intervals[i] = IntervalsCalculator.GetNewInterval(intervals[i].Degree, intervals[i].Distance);
            }

            modes[0].Intervals = intervals;
            modes[0].ScaleId = scaleId;
            if (modes.Count > 1)
            {
                for (var i = 1; i < modes.Count; i++)
                {
                    modes[i].Intervals = AddIntervals(modes[i - 1].Intervals);
                    modes[i].ScaleId = scaleId;
                }
            }
            return modes;
        }
        private static List<Interval> AddIntervals(List<Interval> intervals)
        {
            var difference = -(intervals[1].Distance - intervals[0].Distance);
            int distance;


            var newIntervals = new List<Interval>();

            for (var i = 0; i < intervals.Count - 1; i++)
            {
                distance = difference + intervals[i + 1].Distance;
                newIntervals.Add(IntervalsCalculator.GetNewInterval(intervals[i].Degree, distance));
            }
            distance = difference + 13;
            newIntervals.Add(IntervalsCalculator.GetNewInterval(intervals[^1].Degree, distance));

            if (newIntervals.Count < 7)
            {
                newIntervals = AssignDegreesForNonDiatonicScale(newIntervals);
            }

            return newIntervals;

            static List<Interval> AssignDegreesForNonDiatonicScale(List<Interval> intervals)
            {
                var degrees = new Degree[]
                {
                    Degree.Root,
                    Degree.Second,
                    Degree.Third,
                    Degree.Fourth,
                    Degree.Fifth,
                    Degree.Sixth,
                    Degree.Seventh
                };

                var leap = Array.IndexOf(degrees, intervals[1].Degree);

                for (int i = 0; i < intervals.Count - 1; i++)
                {
                    var degreeIndex = Array.IndexOf(degrees, intervals[i + 1].Degree) - leap;
                    intervals[i].Degree = degrees[degreeIndex];
                }

                intervals[^1].Degree = degrees[7 - leap];

                return intervals;
            }
        }
        public async Task<List<Mode>> AllModesAsync()
        {
            return await quaverflowDbContext.Modes
                   .Include(s => s.Intervals)
                   .Include(s => s.Scale).ToListAsync();
        }
      
        public List<Mode> Delete(List<Mode> modes)
        {
            foreach (var mode in modes)
            {
                if (mode != null)
                {
                    quaverflowDbContext.Modes.Remove(mode);
                }
            }
            return modes;
        }
        public async Task<List<Mode>> GetModesAndIntervalsByScaleIdAsync(int scaleId)
        {
            return await quaverflowDbContext.Modes.Include(m => m.Intervals)
                                                   .Where(m => m.ScaleId == scaleId)
                                                   .ToListAsync();
        }
    }
}