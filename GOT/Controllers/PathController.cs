using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using GOT.ViewModels;

namespace GOT.Controllers {
    public enum MessageType {
        SUCCESS = 0,
        DANGER = 1,
        WARNING = 2,
        INFO = 3
    }
    public class PathController : Controller {
        public async Task<IActionResult> Index() {
            //TODO replace with proper access to database
            List<PathViewModel> result = new List<PathViewModel>();
            result.Add(new PathViewModel { Id = 1, Area = "Sudety", Name = "Trasa testowa", CreationDate = new DateTime(2020, 11, 1) });
            result.Add(new PathViewModel { Id = 1, Area = "Sudety", Name = "Trasa testowa2", CreationDate = new DateTime(2020, 11, 1) });
            //end TODO
            return View(result);
        }

        public async Task<IActionResult> Create() {
            return View();
        }

        public async Task<IActionResult> Delete(int? id) {
            //TODO replace with proper access to database
            var result = new PathViewModel { Id = 1, Area = "Sudety", Name = "Trasa testowa", CreationDate = new DateTime(2020, 11, 1) };
            //end TODO
            return View(result);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id) {
            //TODO replace with proper access to database
            TempData["Message"] = "Trasa \"Trasa testowa\" została usunięta.";
            TempData["MessageType"] = MessageType.INFO;
            //end TODO
            return RedirectToAction(nameof(Index));
        }

        
    }
}
