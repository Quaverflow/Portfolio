using Quaverflow.Data.MusicModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicTechnologies.Core.Interfaces
{
    public interface IScaleCalculator
    {
        Task<Dictionary<Mode, string>> CalculateScalesFromChordAsync(int?[] nullableDistances, int root, bool penthatonic, bool hexatonic, bool diatonic, bool octatonic, bool nonatonic);
        string GetRomanNumbersScale(Mode mode);
        List<Mode> RemoveDuplicateModes(List<Mode> modes);
    }
}
