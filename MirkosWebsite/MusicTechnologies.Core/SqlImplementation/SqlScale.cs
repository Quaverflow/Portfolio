using Microsoft.EntityFrameworkCore;
using MusicTechnologies.Core.Interfaces;
using MusicTechnologies.Data;
using Quaverflow.Data.MusicModels;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;

namespace MusicTechnologies.Core.SqlImplementation
{
    public class SqlScale : IScaleData
    {
        private readonly QuaverflowDbContext quaverflowDbContext;
        private readonly HtmlEncoder htmlEncoder;

        public SqlScale(QuaverflowDbContext quaverflowDbContext, HtmlEncoder htmlEncoder)
        {
            this.quaverflowDbContext = quaverflowDbContext;
            this.htmlEncoder = htmlEncoder;
        }

        public Scale Add(Scale newScale)
        {
            newScale.Name = htmlEncoder.Encode(newScale.Name);
            foreach (var mode in newScale.Modes)
            {
                mode.Name = htmlEncoder.Encode(mode.Name);
            }
            quaverflowDbContext.Add(newScale);
            return newScale;
        }

        public int Commit()
        {
            return quaverflowDbContext.SaveChanges();
        }

        public Scale Delete(Scale scale)
        {
            if (scale != null)
            {
                quaverflowDbContext.Scales.Remove(scale);
            }
            return scale;
        }


        public Scale GetById(int scaleId)
        {
            return quaverflowDbContext.Scales.Include(s => s.Modes)
                                                    .FirstOrDefault(s => s.Id == scaleId);
        }

        public IEnumerable<Scale> GetByName(string ScaleName)
        {
            return quaverflowDbContext.Scales
                        .Include(s => s.Modes)
                        .ThenInclude(m => m.Intervals)
                        .Where(s => s.Name.Contains(ScaleName) || string.IsNullOrEmpty(ScaleName));

        }
    }
}
