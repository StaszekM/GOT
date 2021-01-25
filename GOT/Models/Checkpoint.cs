using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GOT.Models
{
    public class Checkpoint
    {
        [Display(Name = "Identyfikator")]
        public int CheckpointId { get; set; }
        [Display(Name = "Nazwa")]
        [Required(ErrorMessage = "Pole jest wymagane")]
        public string CheckpointName { get; set; }
        [Display(Name = "Współrzędne X")]
        [Required(ErrorMessage = "Pole jest wymagane")]
        public double XCoords { get; set; }
        [Display(Name = "Współrzędne Y")]
        [Required(ErrorMessage = "Pole jest wymagane")]
        public double YCoords { get; set; }
        [Display(Name = "Obszar")]
        public int AreaId { get; set; }
        [Display(Name = "Obszar")]
        public Area Area { get; set; }
        [Display(Name = "Opis Punktu Kontrolnego")]
        public string CheckpointDescription { get; set; }


        public ICollection<Path> PathsA { get; set; }
        public ICollection<Path> PathsB { get; set; }
    }
}
