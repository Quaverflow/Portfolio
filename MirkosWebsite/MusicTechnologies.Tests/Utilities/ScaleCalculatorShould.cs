using MusicTechnologies.Core.Interfaces;
using Moq;
using Xunit;
using MusicTechnologies.Core.SqlImplementation;
using Quaverflow.Data.MusicModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicTechnologies.Tests.Utilities
{
    public class ScaleCalculatorShould
    {
        public readonly Mock<IModeData> MockMode = new();
        private readonly List<Interval> pentatonic;
        private readonly List<Interval> hexatonic;
        private readonly List<Interval> diatonic;
        private readonly List<Interval> octatonic;
        private readonly List<Interval> nonatonic;

        public ScaleCalculatorShould()
        {
            pentatonic = new List<Interval>()
            {
                { new() { Distance = 1, Degree = Degree.Root } },
                { new() { Distance = 3, Degree = Degree.Second } },
                { new() { Distance = 5, Degree = Degree.Third } },
                { new() { Distance = 6, Degree = Degree.Fourth } },
                { new() { Distance = 8, Degree = Degree.Fifth } }
            };

            hexatonic = new List<Interval>()
            {
                { new() { Distance = 1, Degree = Degree.Root } },
                { new() { Distance = 3, Degree = Degree.Second } },
                { new() { Distance = 5, Degree = Degree.Third } },
                { new() { Distance = 7, Degree = Degree.Fourth } },
                { new() { Distance = 9, Degree = Degree.Fifth } },
                { new() { Distance = 11, Degree = Degree.Sixth } }
            };

            diatonic = new List<Interval>()
            {
                { new() { Distance = 1, Degree = Degree.Root } },
                { new() { Distance = 3, Degree = Degree.Second } },
                { new() { Distance = 5, Degree = Degree.Third } },
                { new() { Distance = 6, Degree = Degree.Fourth } },
                { new() { Distance = 8, Degree = Degree.Fifth } },
                { new() { Distance = 10, Degree = Degree.Sixth } },
                { new() { Distance = 12, Degree = Degree.Seventh } }
            };

            octatonic = new List<Interval>()
            {
                { new() { Distance = 1, Degree = Degree.Root } },
                { new() { Distance = 2, Degree = Degree.Second } },
                { new() { Distance = 3, Degree = Degree.Second } },
                { new() { Distance = 5, Degree = Degree.Third } },
                { new() { Distance = 6, Degree = Degree.Fourth } },
                { new() { Distance = 8, Degree = Degree.Fifth } },
                { new() { Distance = 10, Degree = Degree.Sixth } },
                { new() { Distance = 12, Degree = Degree.Seventh } }
            };

            nonatonic = new List<Interval>()
            {
                { new() { Distance = 1, Degree = Degree.Root } },
                { new() { Distance = 2, Degree = Degree.Second } },
                { new() { Distance = 4, Degree = Degree.Second } },
                { new() { Distance = 5, Degree = Degree.Third } },
                { new() { Distance = 7, Degree = Degree.Fourth } },
                { new() { Distance = 8, Degree = Degree.Fifth } },
                { new() { Distance = 9, Degree = Degree.Fifth } },
                { new() { Distance = 10, Degree = Degree.Sixth } },
                { new() { Distance = 12, Degree = Degree.Seventh } }
            };
        }

        [Fact]
        public async Task ReturnPentatonicScaleIfCheckboxChecked()
        {
            MockMode.Setup(x =>  x.AllModesAsync()).Returns(Task.FromResult(new List<Mode>
            { new() { Intervals = pentatonic } }));

            var scaleCalculator = new SqlScaleCalculator(MockMode.Object);

            var testChord = new int?[] { 1, 3, null, 6, null, 10, null };
            var sutCheckboxChecked = await scaleCalculator.CalculateScalesFromChordAsync(testChord, 3, true, false, false, false, false);
            Assert.True(sutCheckboxChecked.Count > 0);

            var sutCheckboxUnchecked = await scaleCalculator.CalculateScalesFromChordAsync(testChord, 3, false, true, true, true, true);
            Assert.True(sutCheckboxUnchecked.Count == 0);
        }

        [Fact]
        public async Task ReturnHexatonicScaleIfCheckboxChecked()
        {
            MockMode.Setup(x => x.AllModesAsync()).Returns(Task.FromResult(new List<Mode>
            { new() { Intervals = hexatonic } }));

            var scaleCalculator = new SqlScaleCalculator(MockMode.Object);

            int?[] testChord = new int?[] { 1, 3, null, 7, 9, null, null };
            var sutCheckboxChecked = await scaleCalculator.CalculateScalesFromChordAsync(testChord, 3, false, true, false, false, false);
            Assert.True(sutCheckboxChecked.Count > 0);

            var sutCheckboxUnchecked = await scaleCalculator.CalculateScalesFromChordAsync(testChord, 3, true, false, true, true, true);
            Assert.True(sutCheckboxUnchecked.Count == 0);
        }

        [Fact]
        public async Task ReturnDiatonicScaleIfCheckboxChecked()
        {
            MockMode.Setup(x => x.AllModesAsync()).Returns(Task.FromResult(new List<Mode>
            { new() { Intervals = diatonic } }));

            var scaleCalculator = new SqlScaleCalculator(MockMode.Object);

            int?[] testChord = new int?[] { 1, 3, null, 6, null, 10, null };
            var sutCheckboxChecked = await scaleCalculator.CalculateScalesFromChordAsync(testChord, 3, false, false, true, false, false);
            Assert.True(sutCheckboxChecked.Count > 0);

            var sutCheckboxUnchecked = await scaleCalculator.CalculateScalesFromChordAsync(testChord, 3, true, true, false, true, true);
            Assert.True(sutCheckboxUnchecked.Count == 0);
        }

        [Fact]
        public async Task ReturnOctatonicScaleIfCheckboxChecked()
        {
            MockMode.Setup(x => x.AllModesAsync()).Returns(Task.FromResult(new List<Mode>
            { new() { Intervals = octatonic } }));

            var scaleCalculator = new SqlScaleCalculator(MockMode.Object);

            int?[] testChord = new int?[] { 1, 3, null, 6, null, 10, null };
            var sutCheckboxChecked = await scaleCalculator.CalculateScalesFromChordAsync(testChord, 3, false, false, false, true, false);
            Assert.True(sutCheckboxChecked.Count > 0);

            var sutCheckboxUnchecked = await scaleCalculator.CalculateScalesFromChordAsync(testChord, 3, true, true, true, false, true);
            Assert.True(sutCheckboxUnchecked.Count == 0);
        }

        [Fact]
        public async Task ReturnNonatonicScaleIfCheckboxChecked()
        {
            MockMode.Setup(x => x.AllModesAsync()).Returns(Task.FromResult(new List<Mode>
            { new() { Intervals = nonatonic } }));

            var scaleCalculator = new SqlScaleCalculator(MockMode.Object);

            int?[] testChord = new int?[] { 1, 4, null, 7, null, 10, null };
            var sutCheckboxChecked = await scaleCalculator.CalculateScalesFromChordAsync(testChord, 3, false, false, false, false, true);
            Assert.True(sutCheckboxChecked.Count > 0);

            var sutCheckboxUnchecked = await scaleCalculator.CalculateScalesFromChordAsync(testChord, 3, true, true, true, true, false);
            Assert.True(sutCheckboxUnchecked.Count == 0);
        }

        [Fact]
        public async Task ReturnValidRomanNumeralScale()
        {
            MockMode.Setup(x => x.AllModesAsync()).Returns(Task.FromResult(new List<Mode>
            {
                new() { Intervals = pentatonic },
                new() { Intervals = hexatonic },
                new() { Intervals = diatonic },
                new() { Intervals = octatonic },
                new() { Intervals = nonatonic }
            }));

            var allModes = await MockMode.Object.AllModesAsync();
            allModes = allModes.OrderBy(m => m.Intervals.Count).ToList();

            var scaleCalculator = new SqlScaleCalculator(MockMode.Object);

            var sutPentatonic = scaleCalculator.GetRomanNumbersScale(allModes[0]);
            Assert.Equal("I | II | III | IV | V | ", sutPentatonic);
            var sutHexatonic = scaleCalculator.GetRomanNumbersScale(allModes[1]);
            Assert.Equal("I | II | III | IV♯ | V♯ | VI♯ | ", sutHexatonic);
            var sutDiatonic = scaleCalculator.GetRomanNumbersScale(allModes[2]);
            Assert.Equal("I | II | III | IV | V | VI | VII | ", sutDiatonic);
            var sutOctatonic = scaleCalculator.GetRomanNumbersScale(allModes[3]);
            Assert.Equal("I | II♭ | II | III | IV | V | VI | VII | ", sutOctatonic);
            var sutNonatonic = scaleCalculator.GetRomanNumbersScale(allModes[4]);
            Assert.Equal("I | II♭ | II♯ | III | IV♯ | V | V♯ | VI | VII | ", sutNonatonic);

        }

        [Fact]
        public async Task RemoveDuplicateModesByName()
        {
            MockMode.Setup(x => x.AllModesAsync()).Returns(Task.FromResult(new List<Mode>
            {
                new() { Intervals = pentatonic, Name = "Duplicate" },
                new() { Intervals = hexatonic, Name = "Duplicate" }
            }));

            var scaleCalculator = new SqlScaleCalculator(MockMode.Object);

            var sut = scaleCalculator.RemoveDuplicateModes(await MockMode.Object.AllModesAsync());

            Assert.True(sut.Count == 1);
        }

        [Fact]
        public async Task KeepNonDuplicateModesByName()
        {
            MockMode.Setup(x => x.AllModesAsync()).Returns(Task.FromResult(new List<Mode>
            {
                new() { Intervals = pentatonic, Name = "I'm not a duplicate" },
                new() { Intervals = hexatonic, Name = "I'm most definitely not a duplicate" }
            }));

            var scaleCalculator = new SqlScaleCalculator(MockMode.Object);

            var sut = scaleCalculator.RemoveDuplicateModes(await MockMode.Object.AllModesAsync());

            Assert.True(sut.Count == 2);
        }
    }
}
