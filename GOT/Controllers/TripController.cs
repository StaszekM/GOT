using GOT.Data;
using GOT.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GOT.Controllers {
    public class TripController : Controller {
        private readonly GotDbContext _context;
        public TripController(GotDbContext context)
        {
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
            ViewData["PathId"] = new SelectList(_context.Paths, "PathId", "PathName");
            return View(new Trip());
        }
    }
}
