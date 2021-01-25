using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GOT.Models
{
    public class Area
    {
        public int AreaId { get; set; }

        [Required]
        [MaxLength(40)]
        public string AreaName { get; set; }

        public ICollection<Checkpoint> Checkpoints { get; set; }
    }
}
