using System.Collections.Generic;
using System.Linq;
using Quaverflow.Data.MusicModels;

namespace MusicTechnologies.Core.Utilities
{
    public static class ScaleValidator
    {
        public static bool VerifyFullInput(Scale scale, out string message)
        {
            var modes = scale.Modes;
            var intervals = modes[0].Intervals;
            for (var i = 0; i < intervals.Count; i++)
            {
                if (intervals[i].Degree == Degree.None && modes[i].Name != null)
                {
                    message = "All scales must have both a Degree and a Mode name. Unused rows should be left entirely empty.";
                    return false;
                }
                else if (intervals[i].Degree != Degree.None && modes[i].Name == null)
                {
                    message = "All scales must have both a Degree and a Mode name. Unused rows should be left entirely empty.";
                    return false;
                }
            }
            message = null;
            return true;
        }

        public static bool AreValuesLogical(List<Interval> intervals, out string message)
        {
            intervals = intervals.Where(i => i.Degree != Degree.None).ToList();

            for (var i = 1; i < intervals.Count; i++)
            {
                if (intervals[i].Distance <= intervals[i - 1].Distance)
                {
                    message = "The value of a note can't be lower than the preceding one. (example: the Third can't be lower than the Ninth)";
                    return false; 
                }
                if (intervals[i].Degree == Degree.Seventh && intervals[i].Distance > 12)
                {
                    message = "the Seventh degree can't be higher than the Root.";
                    return false; 
                }
            }
            message = null;
            return true;
        }
    }
}
