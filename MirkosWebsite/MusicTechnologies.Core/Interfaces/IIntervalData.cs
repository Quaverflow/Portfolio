using Quaverflow.Data.MusicModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicTechnologies.Core.Interfaces
{
    public interface IIntervalData
    {
        Interval Add(Interval interval);
        Task<List<Interval>> GetExistingAsync(List<Interval> intervals);
    }
}
