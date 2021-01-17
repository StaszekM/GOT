using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using GOT.ViewModels;

namespace GOT.Controllers {
    public class PathController : Controller {
        public IActionResult Index() {
            //TODO replace with proper access to database
            List<PathViewModel> result = new List<PathViewModel>();
            result.Add(new PathViewModel { Id = 1, Area = "Sudety", Name = "Trasa testowa", CreationDate = new DateTime(2020, 11, 1) });
            //end TODO
            return View(result);
        }

        public IActionResult Create() {
            return View();
        }
    }
}
