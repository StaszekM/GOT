﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using GOT.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using GOT.Models;

namespace GOT.Controllers {
    public enum MessageType
    {
        SUCCESS = 0,
        DANGER = 1,
        WARNING = 2,
        INFO = 3
    }

    public class PathController : Controller {
        private readonly GotDbContext _context;
        public PathController(GotDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index() {
            var myDbContext = _context.Paths.Include(a => a.CheckpointA).Include(b => b.CheckpointB);
            return View(await myDbContext.ToListAsync());
        }

        public IActionResult Create()
        {
            SelectList checkpoints = new SelectList(_context.Checkpoints, "CheckpointId", "CheckpointName");
            if (checkpoints.Count() < 2) {
                TempData["Message"] = "Obecnie nie ma wystarczającej liczby punktów kontrolnych (2), żeby wyznaczyć trasę. Stwórz najpierw punkty kontrolne.";
                TempData["MessageType"] = MessageType.WARNING;
                return RedirectToAction(nameof(Index));
            }
            ViewData["CheckpointId"] = checkpoints;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Obsolete]
        public async Task<IActionResult> Create([Bind("PathId,PathName,ElevationAB,DistanceAB,ElevationBA,DistanceBA,Description,CheckpointAId,CheckpointBId")] Path path) {
            if (ModelState.IsValid) {
                path.CreationDate = DateTime.Today;
                _context.Add(path);
                await _context.SaveChangesAsync();
                TempData["Message"] = "Trasa została dodana.";
                TempData["MessageType"] = MessageType.SUCCESS;
                return RedirectToAction(nameof(Index));
            }
            ViewData["CheckpointId"] = new SelectList(_context.Checkpoints, "CheckpointId", "CheckpointName");
            return View(path);
        }

        public async Task<IActionResult> Edit(int? id) {
            if (id == null) {
                return NotFound();
            }

            var path = await _context.Paths.FindAsync(id);
            if (path == null) {
                return NotFound();
            }
            ViewData["CheckpointId"] = new SelectList(_context.Checkpoints, "CheckpointId", "CheckpointName");
            return View(path);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PathId,PathName,ElevationAB,CreationDate,DistanceAB,ElevationBA,DistanceBA,Description,CheckpointAId,CheckpointBId")] Path path) {
            if (id != path.PathId) {
                return NotFound();
            }

            if (ModelState.IsValid) {
                try {
                    _context.Update(path);
                    await _context.SaveChangesAsync();
                } catch (DbUpdateConcurrencyException) {
                    if (!_context.Paths.Any(element => element.PathId == path.PathId)) {
                        return NotFound();
                    } else {
                        throw;
                    }
                }

                TempData["Message"] = "Zmiany zostały zapisane.";
                TempData["MessageType"] = MessageType.SUCCESS;
                return RedirectToAction(nameof(Index));
            }
            ViewData["CheckpointId"] = new SelectList(_context.Checkpoints, "CheckpointId", "CheckpointName");
            return View(path);
        }

        public async Task<IActionResult> Delete(int? id) {
            if (id == null)
            {
                return NotFound();
            }

            var path = await _context.Paths
                .Include(a => a.CheckpointA).Include(a => a.CheckpointB)
                .FirstOrDefaultAsync(m => m.PathId == id);
            if (path == null)
            {
                return NotFound();
            }

            return View(path);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id) {
            var path = await _context.Paths.FindAsync(id);
            _context.Paths.Remove(path);
            try
            {
                await _context.SaveChangesAsync();
            } catch (DbUpdateException)
            {
                TempData["Message"] = $"Trasa {path.PathName} nie może zostać usunięta, ponieważ istenieją wycieczki ją zawierające.";
                TempData["MessageType"] = MessageType.DANGER;
                return RedirectToAction(nameof(Index));
            }
            TempData["Message"] = $"Trasa {path.PathName} została usunięta.";
            TempData["MessageType"] = MessageType.INFO;
            return RedirectToAction(nameof(Index));
        }

        
    }
}
