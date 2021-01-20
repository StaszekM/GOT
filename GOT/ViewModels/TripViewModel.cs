using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace GOT.ViewModels {
    //TODO replace points and areas with proper representation instead of strings
    public class TripViewModel {
        [Display(Name = "Identyfikator")]
        public int Id { get; set; }
        [Display(Name = "Data rozpoczęcia")]
        public DateTime StartDate { get; set; }
        [Display(Name = "Data zakończenia")]
        public DateTime EndDate { get; set; }
        [Display(Name = "Punktacja")]
        public int Score { get; set; }
        [Display(Name = "Punkty kontrolne")]
        public IEnumerable<string> PointNames { get; set; }
        [Display(Name = "Ustalone obszary")]
        public IEnumerable<string> Areas { get; set; }
        [Display(Name = "Zatwierdzona")]
        public bool IsApproved { get; set; }
        [Display(Name = "Odbyta")]
        public bool IsCompleted { get; set; }

    }
}
