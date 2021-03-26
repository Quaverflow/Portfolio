using Quaverflow.Data.MusicModels;
using System.Collections.Generic;

namespace MusicTechnologies.Core.Interfaces
{
    public interface IScaleData
    {
        Scale GetById(int scaleId);
        IEnumerable<Scale> GetByName(string scaleName);
        Scale Delete(Scale scale);
        Scale Add(Scale newScale);
        int Commit();
    }
}
