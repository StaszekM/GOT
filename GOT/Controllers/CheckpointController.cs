using GOT.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GOT.Controllers {
    public class CheckpointController : Controller {
        public IActionResult Index() {
            //TODO replace with proper communication with database
            List<CheckpointViewModel> result = new List<CheckpointViewModel>();
            result.Add(new CheckpointViewModel { Id = 1, Area = "Sudety", Name = "Śnieżka", XCoords = 9.0001f, YCoords = 8.001f, Representative = "Jan Kowalski" });
            //end TODO
            return View(result);
        }

        public async Task<IActionResult> Create() {
            return View();
        }

        public async Task<IActionResult> Delete(int? id) {
            //TODO replace with proper access to database
            var result = new CheckpointViewModel { Id = 1, Area = "Sudety", Name = "Śnieżka", XCoords = 9.0001f, YCoords = 8.001f, Representative = "Jan Kowalski" };
            //end TODO
            return View(result);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id) {
            //TODO replace with proper access to database
            TempData["Message"] = "Punkt kontrolny \"Śnieżka\" został usunięty.";
            TempData["MessageType"] = MessageType.INFO;
            //end TODO
            return RedirectToAction(nameof(Index));
        }
    }
}
