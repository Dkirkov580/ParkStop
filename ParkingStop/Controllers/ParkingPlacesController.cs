using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ParkingStop.Data;

namespace ParkingStop.Controllers
{
    public class ParkingPlacesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ParkingPlacesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ParkingPlaces
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ParkingPlaces.Include(p => p.Categories);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ParkingPlaces/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ParkingPlaces == null)
            {
                return NotFound();
            }

            var parkingPlace = await _context.ParkingPlaces
                .Include(p => p.Categories)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (parkingPlace == null)
            {
                return NotFound();
            }

            return View(parkingPlace);
        }

        // GET: ParkingPlaces/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id");
            return View();
        }

        // POST: ParkingPlaces/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Size,CategoryId,Price,Status,IsInvalid,RegisterOn")] ParkingPlace parkingPlace)
        {
            if (ModelState.IsValid)
            {
                _context.Add(parkingPlace);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id", parkingPlace.CategoryId);
            return View(parkingPlace);
        }

        // GET: ParkingPlaces/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ParkingPlaces == null)
            {
                return NotFound();
            }

            var parkingPlace = await _context.ParkingPlaces.FindAsync(id);
            if (parkingPlace == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id", parkingPlace.CategoryId);
            return View(parkingPlace);
        }

        // POST: ParkingPlaces/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Size,CategoryId,Price,Status,IsInvalid,RegisterOn")] ParkingPlace parkingPlace)
        {
            if (id != parkingPlace.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(parkingPlace);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ParkingPlaceExists(parkingPlace.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id", parkingPlace.CategoryId);
            return View(parkingPlace);
        }

        // GET: ParkingPlaces/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ParkingPlaces == null)
            {
                return NotFound();
            }

            var parkingPlace = await _context.ParkingPlaces
                .Include(p => p.Categories)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (parkingPlace == null)
            {
                return NotFound();
            }

            return View(parkingPlace);
        }

        // POST: ParkingPlaces/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ParkingPlaces == null)
            {
                return Problem("Entity set 'ApplicationDbContext.ParkingPlaces'  is null.");
            }
            var parkingPlace = await _context.ParkingPlaces.FindAsync(id);
            if (parkingPlace != null)
            {
                _context.ParkingPlaces.Remove(parkingPlace);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ParkingPlaceExists(int id)
        {
          return (_context.ParkingPlaces?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
