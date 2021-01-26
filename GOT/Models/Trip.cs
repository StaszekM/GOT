using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GOT.Models
{
    public class Trip
    {
        [Display(Name = "Identyfikator")]
        public int TripId { get; set; }
        [Display(Name = "Punktacja")]
        public int Score { get; set; }
        [Display(Name = "Data rozpoczęcia")]
        public DateTime StartDate { get; set; }
        [Display(Name = "Data zakończenia")]
        public DateTime EndDate { get; set; }
        [Display(Name = "Zatwierdzona")]
        public bool IsApproved { get; set; }
        [Display(Name = "Odbyta")]
        public bool IsCompleted { get; set; }
        [Display(Name = "Opis")]
        public string Description { get; set; }

        public ICollection<PathTrip> PathTrips { get; set; }

    }
}
