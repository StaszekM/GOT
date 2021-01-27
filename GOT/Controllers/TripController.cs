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
            var tripsWithPaths = await _context.Trips.Where(trip => trip.PathTrips != null && trip.PathTrips.Count() != 0).ToListAsync();
            return View(tripsWithPaths);
        }

        public async Task<IActionResult> Details(int? id) {
            return View();
        }

        public async Task<IActionResult> Plan() {
            int? plannedTripId = GetPlannedTripId();
            Trip plannedTrip;
            if (plannedTripId == null) {
                plannedTrip = new Trip();
                TempData["Elevation"] = 0;
                TempData["Distance"] = 0;

                _context.Add(plannedTrip);
                await _context.SaveChangesAsync();

                SavePlannedTripId(plannedTrip.TripId);
                plannedTrip = await _context.Trips.FindAsync(plannedTrip.TripId);
                return View(plannedTrip);
            }

            plannedTrip = await GetAllTripData((int)plannedTripId);
            plannedTrip.Score = Utils.GetTripScore(plannedTrip);

            TempData["Elevation"] = Utils.GetTotalTripElevation(plannedTrip);
            TempData["Distance"] = Utils.GetTotalTripDistance(plannedTrip);

            return View(plannedTrip);
        }

        public async Task<IActionResult> SavePlan() {
            int? plannedTripId = GetPlannedTripId();
            if (plannedTripId == null) {
                TempData["Message"] = "Twoja sesja wygasła lub nie planowałeś żadnej wycieczki - zaplanuj wycieczkę jeszcze raz.";
                TempData["MessageType"] = MessageType.WARNING;
                return RedirectToAction(nameof(Plan));
            }

            Trip plannedTrip = await GetAllTripData((int)plannedTripId);
            if ((plannedTrip.PathTrips?.Count() ?? 0) == 0) {
                TempData["Message"] = "Nie ma żadnych tras w wycieczce, nie można zapisać.";
                TempData["MessageType"] = MessageType.WARNING;
                return RedirectToAction(nameof(Plan));
            }

            TempData["Message"] = "Wycieczka zapisana.";
            TempData["MessageType"] = MessageType.SUCCESS;
            //TODO save planned trip in dband return to index
            RemovePlannedTripId();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> ShowPathSelection() {
            int? plannedTripId = GetPlannedTripId();
            if (plannedTripId == null) {
                TempData["Message"] = "Twoja sesja wygasła lub nie planowałeś żadnej wycieczki - zaplanuj wycieczkę jeszcze raz.";
                TempData["MessageType"] = MessageType.WARNING;
                return RedirectToAction(nameof(Plan));
            }

            Trip plannedTrip = await GetAllTripData((int)plannedTripId);
            PathSelectionViewModel model;
            if ((plannedTrip.PathTrips?.Count() ?? 0) == 0) {
                model = new PathSelectionViewModel() {
                    AvailablePaths = await _context.Paths.Include(path => path.CheckpointA).Include(path => path.CheckpointB).ToListAsync(),
                    CanChooseBidirectionally = true
                };
            } else {
                PathTrip lastPathTrip = plannedTrip.PathTrips.OrderBy(pathTrip => pathTrip.Order).Last();
                Checkpoint lastCheckpoint = lastPathTrip.IsFromAToB ? lastPathTrip.Path.CheckpointB : lastPathTrip.Path.CheckpointA;

                model = new PathSelectionViewModel() {
                    AvailablePaths = await _context.Paths.Include(path => path.CheckpointA).Include(path => path.CheckpointB).Where(path => path.CheckpointA.CheckpointId == lastCheckpoint.CheckpointId || path.CheckpointB.CheckpointId == lastCheckpoint.CheckpointId).ToListAsync(),
                    CanChooseBidirectionally = false
                };
            }
            return View("PathSelection", model);
        }
        [Route("/Trip/SelectPath,{pathId},{isFromA}")]
        public async Task<IActionResult> SelectPath(int pathId, bool? isFromA) {
            int? plannedTripId = GetPlannedTripId();
            if (plannedTripId == null) {
                TempData["Message"] = "Twoja sesja wygasła lub nie planowałeś żadnej wycieczki - zaplanuj wycieczkę jeszcze raz.";
                TempData["MessageType"] = MessageType.WARNING;
                return RedirectToAction(nameof(Plan));
            }

            Trip plannedTrip = await GetAllTripData((int)plannedTripId);
            Path matchingPath = await _context.Paths.Include(path => path.CheckpointA).Include(path => path.CheckpointB).FirstAsync(path => path.PathId == pathId);
            if (matchingPath == null) {
                return NotFound();
            }

            if (plannedTrip.PathTrips == null) {
                plannedTrip.PathTrips = new List<PathTrip>();
            }

            PathTrip lastPathTrip;
            if (plannedTrip.PathTrips.Count() == 0) {
                lastPathTrip = new PathTrip() { Order = 0 };
            } else {
                lastPathTrip = plannedTrip.PathTrips.OrderByDescending(pathTrip => pathTrip.Order).First();
            }

            if (isFromA == null) {
                int lastCheckpointId = lastPathTrip.IsFromAToB ? lastPathTrip.Path.CheckpointB.CheckpointId : lastPathTrip.Path.CheckpointA.CheckpointId;
                isFromA = matchingPath.CheckpointA.CheckpointId == lastCheckpointId;
            }

            plannedTrip.PathTrips.Add(new PathTrip() { Order = lastPathTrip.Order + 1, PathId = matchingPath.PathId, IsFromAToB = (bool)isFromA, TripId = plannedTrip.TripId });
            _context.Update(plannedTrip);
            await _context.SaveChangesAsync();

            SavePlannedTripId(plannedTrip.TripId);
            return RedirectToAction(nameof(Plan));
        }

        public async Task<IActionResult> RemoveLastPath() {
            int? plannedTripId = GetPlannedTripId();
            if (plannedTripId == null) {
                TempData["Message"] = "Twoja sesja wygasła lub nie planowałeś żadnej wycieczki - zaplanuj wycieczkę jeszcze raz.";
                TempData["MessageType"] = MessageType.WARNING;
                return RedirectToAction(nameof(Plan));
            }

            Trip plannedTrip = await GetAllTripData((int)plannedTripId);
            if ((plannedTrip.PathTrips?.Count() ?? 0) != 0) {
                PathTrip toRemove = plannedTrip.PathTrips.OrderByDescending(pathTrip => pathTrip.Order).First();
                plannedTrip.PathTrips.Remove(toRemove);
                _context.Update(plannedTrip);
                await _context.SaveChangesAsync();
            }

            SavePlannedTripId((int)plannedTripId);
            return RedirectToAction(nameof(Plan));
        }

        public async Task<Trip> GetAllTripData(int tripId) {
            Trip plannedTrip = await _context.Trips.FindAsync(tripId);
            plannedTrip.PathTrips = await _context.PathTrips.Include(pathTrip => pathTrip.Path).ThenInclude(path => path.CheckpointA).ThenInclude(chkpt => chkpt.Area)
                .Include(pathTrip => pathTrip.Path).ThenInclude(path => path.CheckpointB).ThenInclude(chkpt => chkpt.Area)
                .Where(pathTrip => pathTrip.TripId == tripId).ToListAsync();
            return plannedTrip;
        }

        public int? GetPlannedTripId() {
            return HttpContext.Session.GetInt32("PlannedTripId");
        }

        public void SavePlannedTripId(int tripId) {
            HttpContext.Session.SetInt32("PlannedTripId", tripId);
        }

        public void RemovePlannedTripId() {
            HttpContext.Session.Remove("PlannedTripId");
        }
    }
}
