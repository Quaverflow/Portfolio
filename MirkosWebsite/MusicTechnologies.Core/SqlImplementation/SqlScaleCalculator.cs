using MusicTechnologies.Core.Interfaces;
using Quaverflow.Data.MusicModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicTechnologies.Core.SqlImplementation
{
    public class SqlScaleCalculator : IScaleCalculator
    {
        private readonly IModeData modeData;

        public SqlScaleCalculator(IModeData modeData)
        {
            this.modeData = modeData;
        }

        public string GetRomanNumbersScale(Mode mode)
        {
            var intervals = mode.Intervals.OrderBy(i => i.Distance);

            return intervals.Aggregate("", (current, interval) => current + (IntervalToRomanNum(interval) + " | "));

            static string IntervalToRomanNum(Interval interval)
            {
                return interval.Degree switch
                {
                    Degree.Second => "II" + AddAlteration(interval.Distance, 3),
                    Degree.Third => "III" + AddAlteration(interval.Distance, 5),
                    Degree.Fourth => "IV" + AddAlteration(interval.Distance, 6),
                    Degree.Fifth => "V" + AddAlteration(interval.Distance, 8),
                    Degree.Sixth => "VI" + AddAlteration(interval.Distance, 10),
                    Degree.Seventh => "VII" + AddAlteration(interval.Distance, 12),
                    _ => "I" + AddAlteration(interval.Distance, 1)
                };

                static string AddAlteration(int intervalValue, int majScaleValue)
                {
                    var alteration = "";
                    if (intervalValue < majScaleValue)
                    {
                        alteration = new string('♭', majScaleValue - intervalValue);
                    }
                    else if (intervalValue > majScaleValue)
                    {
                        alteration = new string('♯', intervalValue - majScaleValue);
                    }
                    return alteration;
                }
            }
        }

        public async Task<Dictionary<Mode, string>> CalculateScalesFromChordAsync(int?[] nullableDistances, int root, bool penthatonic, bool hexatonic, bool diatonic, bool octatonic, bool nonatonic)
        {
            var distances = from i in nullableDistances
                            where i != null
                            select (int)i;

            var allModes = await modeData.AllModesAsync();
            var matchingModes = GetMatchingModes(distances, allModes);

            var matchingModesWithMatchingLength = ModeLengthsAllowed(penthatonic, hexatonic, diatonic, octatonic, nonatonic, matchingModes)
                                                  .OrderBy(m => m.Intervals.Count)
                                                 .ThenBy(m => m.ScaleId)
                                                 .ToList();

            return TranslateIntervalsToLetterNotation(matchingModesWithMatchingLength, root);

            static IEnumerable<Mode> GetMatchingModes(IEnumerable<int> distances, IEnumerable<Mode> modes)
            {
                var matchingModes = new List<Mode>();
                foreach (var mode in modes)
                {
                    var intervalDistances = mode.Intervals.Select(i => i.Distance);

                    CompareInputToDatabaseModes(intervalDistances, mode);
                }
                return matchingModes;


                void CompareInputToDatabaseModes(IEnumerable<int> intervalDistances, Mode mode)
                {
                    var matching = true;

                    foreach (var distance in distances)
                    {
                        if (!intervalDistances.Contains(distance) &&
                            !InferredIntervalValid(distance, intervalDistances))
                        {
                            matching = false;
                        }
                    }

                    if (matching)
                    {
                        matchingModes.Add(mode);
                    }

                    static bool InferredIntervalValid(int dist, IEnumerable<int> intervalDist)
                    {
                        var enumerable = intervalDist as int[] ?? intervalDist.ToArray();
                        if (enumerable.Contains(dist + 1) && enumerable.Contains(dist + 2))
                        {
                            return false;
                        }

                        if (enumerable.Contains(dist - 1) && enumerable.Contains(dist - 2))
                        {
                            return false;
                        }

                        if (enumerable.Contains(dist - 1) && enumerable.Contains(dist + 1))
                        {
                            return false;
                        }

                        switch (dist)
                        {
                            case 12 when enumerable.Contains(1) && enumerable.Contains(2):
                            case 12 when enumerable.Contains(10) && enumerable.Contains(11):
                            case 12 when enumerable.Contains(1) && enumerable.Contains(11):
                                return false;
                            default:
                                return true;
                        }
                    }
                }
            }

            static IEnumerable<Mode> ModeLengthsAllowed(bool pentha, bool hexa, bool dia, bool octa, bool nona, IEnumerable<Mode> matchingModes)
            {
                if (!pentha)
                {
                    matchingModes = matchingModes.Where(m => m.Intervals.Count != 5);
                }
                if (!hexa)
                {
                    matchingModes = matchingModes.Where(m => m.Intervals.Count != 6);
                }
                if (!dia)
                {
                    matchingModes = matchingModes.Where(m => m.Intervals.Count != 7);
                }
                if (!octa)
                {
                    matchingModes = matchingModes.Where(m => m.Intervals.Count != 8);
                }
                if (!nona)
                {
                    matchingModes = matchingModes.Where(m => m.Intervals.Count <= 8);
                }

                return matchingModes.ToList();
            }
        }


        private static Dictionary<Mode, string> TranslateIntervalsToLetterNotation(IEnumerable<Mode> modes, int root)
        {
            var letterModes = new Dictionary<Mode, string>();
            var notesByFifth = new[] { "C", "G", "D", "A", "E", "B", "F" };
            var notes = new[] { "C", "D", "E", "F", "G", "A", "B" };

            var naturalLetterRoot = GetRootIndexForLetterNotes(notesByFifth, notes, root);

            foreach (var mode in modes)
            {
                var notesString = "";
                var intervals = mode.Intervals.OrderBy(i => i.Distance).ToList();

                var intervalsInLetters = intervals.Select(GetGeneralArrayIndex)
                                                            .Select(generalIndex => notes[SpecificIndex(naturalLetterRoot, generalIndex)])
                                                            .ToList();

                intervalsInLetters = MajorScaleAlterations(root, intervals, intervalsInLetters);

                for (var i = 0; i < intervalsInLetters.Count; i++)
                {
                    intervalsInLetters[i] = ActualAlterations(intervals[i], intervalsInLetters[i]);
                }

                foreach (var letter in intervalsInLetters)
                {
                    notesString += letter + " | ";
                }

                letterModes.Add(mode, notesString);
            }

            return letterModes;

            static int GetRootIndexForLetterNotes(IReadOnlyList<string> letterNotesByFifths, string[] letterNotes, int root)
            {
                return root switch
                {
                    < 0 => Array.IndexOf(letterNotes, letterNotesByFifths[root + 7]),
                    > 6 => Array.IndexOf(letterNotes, letterNotesByFifths[root - 7]),
                    _ => Array.IndexOf(letterNotes, letterNotesByFifths[root]),
                };
            }
            static int GetGeneralArrayIndex(Interval interval)
            {
                return interval.Degree switch
                {
                    Degree.Root => 0,
                    Degree.Second => 1,
                    Degree.Third => 2,
                    Degree.Fourth => 3,
                    Degree.Fifth => 4,
                    Degree.Sixth => 5,
                    _ => 6
                };
            }
            static int SpecificIndex(int root, int index)
            {
                if (index + root > 6)
                {
                    index += root - 7;
                }
                else
                {
                    index += root;
                }
                return index;
            }
            static List<string> MajorScaleAlterations(int root, List<Interval> intervals, List<string> intervalsNotes)
            {
                if (root < 0)
                {
                    intervalsNotes = FlatScale(Math.Abs(root), intervals, intervalsNotes);
                }
                else if (root > 0)
                {
                    intervalsNotes = SharpScale(root, intervals, intervalsNotes);
                }

                return intervalsNotes;


                static List<string> FlatScale(int alterations, List<Interval> intervals, List<string> intervalsNotes)
                {

                    var degreesByFifth = new[]
                    {
                        Degree.Fourth,
                        Degree.Root,
                        Degree.Fifth,
                        Degree.Second,
                        Degree.Sixth,
                        Degree.Third,
                        Degree.Seventh,

                    };

                    var degreeIndex = 0;
                    for (int i = 0; i < alterations; i++)
                    {
                        if (degreeIndex > 6) { degreeIndex = 0; }

                        var counter = degreeIndex;
                        var query = from x in intervals
                                    where x.Degree == degreesByFifth[counter]
                                    select x;

                        foreach (var copy in query)
                        {
                            var index = intervals.IndexOf(copy);
                            intervalsNotes[index] += "♭";
                        }
                        degreeIndex++;

                    }
                    return intervalsNotes;
                }

                static List<string> SharpScale(int alterations, IList<Interval> intervals, List<string> intervalsNotes)
                {

                    var degreesByFourth = new[]
                    {

                        Degree.Seventh,
                        Degree.Third,
                        Degree.Sixth,
                        Degree.Second,
                        Degree.Fifth,
                        Degree.Root,
                        Degree.Fourth,
                    };

                    var degreeIndex = 0;
                    for (int i = 0; i < alterations; i++)
                    {
                        if (degreeIndex > 6) { degreeIndex = 0; }

                        var counter = degreeIndex;
                        var query = from x in intervals
                                    where x.Degree == degreesByFourth[counter]
                                    select x;

                        foreach (var copy in query)
                        {
                            var index = intervals.IndexOf(copy);
                            intervalsNotes[index] += "♯";
                        }
                        degreeIndex++;
                    }
                    return intervalsNotes;
                }


            }
            static string ActualAlterations(Interval interval, string letter)
            {
                return interval.Degree switch
                {
                    Degree.Second => AddAlteration(interval.Distance, 3, letter),
                    Degree.Third => AddAlteration(interval.Distance, 5, letter),
                    Degree.Fourth => AddAlteration(interval.Distance, 6, letter),
                    Degree.Fifth => AddAlteration(interval.Distance, 8, letter),
                    Degree.Sixth => AddAlteration(interval.Distance, 10, letter),
                    Degree.Seventh => AddAlteration(interval.Distance, 12, letter),
                    _ => letter
                };

                static string AddAlteration(int intervalValue, int majScaleValue, string letter)
                {
                    var alteration = "";
                    if (intervalValue < majScaleValue)
                    {
                        if (!letter.Contains("♯"))
                        {
                            alteration = new string('♭', majScaleValue - intervalValue);
                        }
                        else
                        {
                            letter = letter.Remove(letter.Length - 1);
                        }
                    }
                    else if (intervalValue > majScaleValue)
                    {
                        if (!letter.Contains("♭"))
                        {
                            alteration = new string('♯', intervalValue - majScaleValue);
                        }
                        else
                        {
                            letter = letter.Remove(letter.Length - 1);
                        }
                    }
                    return letter + alteration;
                }
            }
        }

        public List<Mode> RemoveDuplicateModes(List<Mode> modes)
        {
            return modes.GroupBy(m => m.Name)
                        .Select(m => m.FirstOrDefault())
                        .ToList();
        }
    }
}

