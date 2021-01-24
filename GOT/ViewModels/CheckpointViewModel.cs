using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace GOT.ViewModels {
    //TODO Replace area, representative with proper representation instead of strings
    public class CheckpointViewModel {
        [Display(Name = "Identyfikator")]
        public int Id { get; set; }
        [Display(Name = "Współrzędne X")]
        [Required(ErrorMessage = "Pole jest wymagane")]
        public double XCoords { get; set; }
        [Display(Name = "Współrzędne Y")]
        [Required(ErrorMessage = "Pole jest wymagane")]
        public double YCoords { get; set; }
        [Display(Name = "Nazwa")]
        public string Name { get; set; }
        [Display(Name = "Obszar")]
        public string Area { get; set; }
        [Display(Name = "Reprezentant")]
        public string Representative { get; set; }
    }
}
