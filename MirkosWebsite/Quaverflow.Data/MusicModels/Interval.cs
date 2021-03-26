using System.Collections.Generic;

namespace Quaverflow.Data.MusicModels
{
    public class Interval
    {
        public int Id { get; set; }
        public Degree Degree { get; set; }
        public int Distance { get; set; }
        public List<Mode> Modes { get; set; } = new List<Mode>();
    }
}
