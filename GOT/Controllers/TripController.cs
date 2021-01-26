using GOT.Data;
using GOT.Models;
using GOT.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GOT.Controllers {
    public class TripController : Controller {
        private readonly GotDbContext _context;
        public TripController(GotDbContext context) {
            _context = context;
        }
        public async Task<IActionResult> Index() {
            var myDbContext = _context.Trips;
            return View(await myDbContext.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id) {
            return View();
        }

        public async Task<IActionResult> Plan() {
            Trip? plannedTrip = RetrievePlannedTrip();
            if (plannedTrip == null) {
                plannedTrip = new Trip();
                TempData["Elevation"] = 0;
                TempData["Distance"] = 0;
                SavePlannedTrip(plannedTrip);
            }
            TempData["Elevation"] = 222; //sample value
            TempData["Distance"] = 100; //sample valie

            return View(plannedTrip);
        }

        [HttpPost]
        public async Task<IActionResult> SavePlan() {
            Trip? plannedTrip = RetrievePlannedTrip();
            if (plannedTrip == null) {
                TempData["Message"] = "Twoja sesja wygasła lub nie planowałeś żadnej wycieczki - zaplanuj wycieczkę jeszcze raz.";
                TempData["MessageType"] = MessageType.WARNING;
                return RedirectToAction(nameof(Plan));
            }

            TempData["Message"] = "Wycieczka zapisana.";
            TempData["MessageType"] = MessageType.SUCCESS;
            //TODO save planned trip in dband return to index
            RemovePlannedTrip();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> ShowPathSelection() {
            Trip? plannedTrip = RetrievePlannedTrip();
            if (plannedTrip == null) {
                TempData["Message"] = "Twoja sesja wygasła lub nie planowałeś żadnej wycieczki - zaplanuj wycieczkę jeszcze raz.";
                TempData["MessageType"] = MessageType.WARNING;
                return RedirectToAction(nameof(Plan));
            }

            PathSelectionViewModel model;
            if ((plannedTrip.PathTrips?.Count() ?? 0) == 0) {
                model = new PathSelectionViewModel() {
                    AvailablePaths = await _context.Paths.Include(path => path.CheckpointA).Include(path => path.CheckpointB).ToListAsync(),
                    CanChooseBidirectionally = true
                };
            } else {
                PathTrip lastPathTrip = plannedTrip.PathTrips.OrderBy(pathTrip => pathTrip.Order).Last();
                Checkpoint lastCheckpoint = lastPathTrip.Path.IsFromAToB ? lastPathTrip.Path.CheckpointB : lastPathTrip.Path.CheckpointA;

                model = new PathSelectionViewModel() {
                    AvailablePaths = await _context.Paths.Include(path => path.CheckpointA).Include(path => path.CheckpointB).Where(path => path.CheckpointA.CheckpointId == lastCheckpoint.CheckpointId || path.CheckpointB.CheckpointId == lastCheckpoint.CheckpointId).ToListAsync(),
                    CanChooseBidirectionally = false
                };
            }
            return View("PathSelection", model);
        }
        [Route("/Trip/SelectPath,{pathId},{isFromA}")]
        public async Task<IActionResult> SelectPath(int pathId, bool? isFromA) {
            Trip? plannedTrip = RetrievePlannedTrip();
            if (plannedTrip == null) {
                TempData["Message"] = "Twoja sesja wygasła lub nie planowałeś żadnej wycieczki - zaplanuj wycieczkę jeszcze raz.";
                TempData["MessageType"] = MessageType.WARNING;
                return RedirectToAction(nameof(Plan));
            }
            //TODO update trip in DB with newly selected path, return trip planning view with updated trip model
            SavePlannedTrip(plannedTrip);
            return RedirectToAction(nameof(Plan));
        }

        public async Task<IActionResult> RemoveLastPath() {
            Trip? plannedTrip = RetrievePlannedTrip();
            if (plannedTrip == null) {
                TempData["Message"] = "Twoja sesja wygasła lub nie planowałeś żadnej wycieczki - zaplanuj wycieczkę jeszcze raz.";
                TempData["MessageType"] = MessageType.WARNING;
                return RedirectToAction(nameof(Plan));
            }
            //TODO remove last path and return to trip planning view with updated trip model
            SavePlannedTrip(plannedTrip);
            return RedirectToAction(nameof(Plan));
        }

        public Trip? RetrievePlannedTrip() {
            string? tripData = HttpContext.Session.GetString("PlannedTrip");
            if (tripData == null) {
                return null;
            }

            return Newtonsoft.Json.JsonConvert.DeserializeObject<Trip>(tripData);
        }

        public void SavePlannedTrip(Trip trip) {
            HttpContext.Session.SetString("PlannedTrip", Newtonsoft.Json.JsonConvert.SerializeObject(trip));
        }

        public void RemovePlannedTrip() {
            HttpContext.Session.Remove("PlannedTrip");
        }
    }
}
