using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using GOT.Models;

namespace GOT.ViewModels {
    public class DirectedPathViewModel : Path{
        [Display(Name = "Czy z A do B?")]
        public bool IsFromAToB { get; set; }
        [Display(Name = "Punktacja")]
        public int Score { get; set; }
    }
}
