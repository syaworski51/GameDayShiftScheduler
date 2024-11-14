using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GameDayShiftScheduler.Data;
using GameDayShiftScheduler.Models;

namespace GameDayShiftScheduler.Controllers
{
    public class SportsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SportsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Sports
        public async Task<IActionResult> Index()
        {
            IQueryable<Sport> sports = _context.Sports.OrderBy(s => s.Name);
            return View(await sports.ToListAsync());
        }

        // GET: Sports/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sport = await _context.Sports.FirstOrDefaultAsync(m => m.Id == id);

            if (sport == null)
            {
                return NotFound();
            }

            return View(sport);
        }

        // GET: Sports/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Sports/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,TeamMembersRequired")] Sport sport)
        {
            if (ModelState.IsValid)
            {
                sport.Id = Guid.NewGuid();
                sport.AspRouteParameter = GenerateAspRouteParameterValue(sport.Name);
                
                _context.Sports.Add(sport);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(sport);
        }

        // GET: Sports/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sport = await _context.Sports.FindAsync(id);
            if (sport == null)
            {
                return NotFound();
            }
            return View(sport);
        }

        // POST: Sports/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name")] Sport sport)
        {
            if (id != sport.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sport);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SportExists(sport.Id))
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
            return View(sport);
        }

        // GET: Sports/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sport = await _context.Sports
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sport == null)
            {
                return NotFound();
            }

            return View(sport);
        }

        // POST: Sports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var sport = await _context.Sports.FindAsync(id);
            if (sport != null)
            {
                _context.Sports.Remove(sport);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SportExists(Guid id)
        {
            return _context.Sports.Any(e => e.Id == id);
        }

        private async Task AddSportIfNotInDB(Sport sport)
        {
            bool sportExists = _context.Sports.Any(s => s.Name == sport.Name);

            if (!sportExists)
            {
                _context.Sports.Add(sport);
                await _context.SaveChangesAsync();
            }
        }

        /// <summary>
        ///     Generate a value for a new sport's ASP route parameter value based on its name.
        /// </summary>
        /// <param name="name">The name of the sport.</param>
        /// <returns>The name of the sport, converted to lowercase and spaces separated by hyphens.</returns>
        private string GenerateAspRouteParameterValue(string name)
        {
            return name
                .ToLower()  // Convert the name to lowercase
                .Trim()  // Trim any extra whitespace
                .Replace(' ', '-');  // Replace spaces with hyphens
        }
    }
}
