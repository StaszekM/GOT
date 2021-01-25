using GOT.Data;
using GOT.Models;
using GOT.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GOT.Controllers {
    public class CheckpointController : Controller {

        private readonly GotDbContext _context;

        public CheckpointController(GotDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> IndexAsync() {
            var myDbContext = _context.Checkpoints.Include(a => a.Area);
            return View(await myDbContext.ToListAsync());
        }

        public IActionResult Create()
        {
            ViewData["AreaId"] = new SelectList(_context.Areas, "AreaId", "AreaName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Obsolete]
        public async Task<IActionResult> CreateAsync([Bind("CheckpointId,XCoords,YCoords,CheckpointName,AreaId,CheckpointDescription")] Checkpoint checkpoint)
        {
            if (ModelState.IsValid)
            {
                _context.Add(checkpoint);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AreaId"] = new SelectList(_context.Areas, "AreaId", "AreaName", checkpoint.AreaId);
            return View(checkpoint);
        }

        public async Task<IActionResult> Delete(int? id) {
            if (id == null)
            {
                return NotFound();
            }

            var checkpoint = await _context.Checkpoints
                .Include(a => a.Area)
                .FirstOrDefaultAsync(m => m.CheckpointId == id);
            if (checkpoint == null)
            {
                return NotFound();
            }

            return View(checkpoint);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id) {
            var checkpoint = await _context.Checkpoints.FindAsync(id);
            _context.Checkpoints.Remove(checkpoint);
            try { 
                await _context.SaveChangesAsync();
            } catch (DbUpdateException)
            {
                var pathsNumber = _context.Paths.Count(a => a.CheckpointAId == id || a.CheckpointBId == id);
                TempData["Message"] = $"Punkt kontrolny nie może być usunięty, ponieważ jest \n" +
                    $"zawarty w {pathsNumber} trasach.";
                TempData["MessageType"] = MessageType.DANGER;
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
