using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace GOT.ViewModels {
    //TODO replace points with proper representation instead of strings
    public class PathViewModel {

        public int Id { get; set; }
        [Display(Name = "Nazwa")]
        public string Name { get; set; }
        [Display(Name = "Obszar")]
        public string Area { get; set; }
        [Display(Name = "Data utworzenia")]
        public DateTime CreationDate { get; set; }
        [Display(Name = "Punkt A")]
        public string PointA { get; set; }
        [Display(Name = "Punkt B")]
        public string PointB { get; set; }
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
        
    }
}
