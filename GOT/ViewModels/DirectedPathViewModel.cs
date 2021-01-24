using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace GOT.ViewModels {
    public class DirectedPathViewModel : PathViewModel {
        [Display(Name = "Czy z A do B?")]
        public bool IsFromAToB { get; set; }
        [Display(Name = "Punktacja")]
        public int Score { get; set; }
    }
}
