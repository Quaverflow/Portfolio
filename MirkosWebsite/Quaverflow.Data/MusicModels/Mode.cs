using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Quaverflow.Data.MusicModels
{
    public class Mode
    {
        public int Id { get; set; }

        [StringLength(80)]
        public string Name { get; set; }

        public Scale Scale { get; set; }
        public int ScaleId { get; set; }
        public List<Interval> Intervals { get; set; } = new List<Interval>();


    }
}
