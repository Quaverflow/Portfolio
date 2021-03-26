using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Quaverflow.Data.MusicModels
{
    public class Scale
    {
        public int Id { get; set; }

        [Required, StringLength(80)]
        public string Name { get; set; }

        [Required, StringLength(40)]
        public string CreatedBy { get; set; }
        public List<Mode> Modes { get; set; } = new List<Mode>();

    }
}
