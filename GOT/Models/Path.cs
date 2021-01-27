using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GOT.Models
{
    public class Path
    {
        public int PathId { get; set; }
        [Display(Name = "Nazwa")]
        [Required(ErrorMessage = "Pole jest wymagane")]
        public string PathName { get; set; }
        
        [Display(Name = "Data utworzenia")]
        public DateTime CreationDate { get; set; }
        [Display(Name = "Suma wejść A-B")]
        [Required(ErrorMessage = "Pole jest wymagane")]
        public int ElevationAB { get; set; }
        [Display(Name = "Długość A-B")]
        [Required(ErrorMessage = "Pole jest wymagane")]
        public int DistanceAB { get; set; }
        [Display(Name = "Suma wejść B-A")]
        [Required(ErrorMessage = "Pole jest wymagane")]
        public int ElevationBA { get; set; }
        [Display(Name = "Długość B-A")]
        [Required(ErrorMessage = "Pole jest wymagane")]
        public int DistanceBA { get; set; }
        [Display(Name = "Opis")]
        public string Description { get; set; }

        [Display(Name = "Punkt A")]
        public int CheckpointAId { get; set; }
        [Display(Name = "Punkt A")]
        public Checkpoint CheckpointA { get; set; }
        [Display(Name = "Punkt B")]
        public int CheckpointBId { get; set; }
        [Display(Name = "Punkt B")]
        public Checkpoint CheckpointB { get; set; }

        public ICollection<PathTrip> PathTrips { get; set; }

    }
}
