using Quaverflow.Data.MusicModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicTechnologies.Core.Interfaces
{
    public interface IModeData
    {
        Task<List<Mode>> AllModesAsync();
        Task<List<Mode>> GetModesAndIntervalsByScaleIdAsync(int scaleId);
        IEnumerable<Mode> Populate(Scale scale, int scaleId);
        List<Mode> Delete(List<Mode> modes);
    }
}
