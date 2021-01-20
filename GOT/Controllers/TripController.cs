using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using GOT.ViewModels;

namespace GOT.Controllers {
    public class TripController : Controller {
        public IActionResult Index() {
            //TODO replace with proper access to database
            List<TripViewModel> result = new List<TripViewModel>();
            result.Add(new TripViewModel { Id = 1, Areas = new string[2] { "Sudety", "Beskidy" }, StartDate = new DateTime(2020, 11, 1), EndDate = new DateTime(2020, 11, 1), Score = 8 });
            //end TODO
            return View(result);
        }
    }
}
