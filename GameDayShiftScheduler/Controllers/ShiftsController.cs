using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GameDayShiftScheduler.Data;
using GameDayShiftScheduler.Models;
using GameDayShiftScheduler.Data.Migrations;

namespace GameDayShiftScheduler.Controllers
{
    public class ShiftsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ShiftsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Shifts
        public async Task<IActionResult> Index(string sport = "all", string viewShifts = "upcoming")
        {
            IQueryable<Shift> shifts = _context.Shifts
                .Include(s => s.Sport)
                .OrderBy(s => s.StartTime);

            if (sport != "all")
                shifts = shifts.Where(s => s.Sport.AspRouteParameter == sport);

            Dictionary<Shift, List<ScheduledShift>> scheduledShifts = GetScheduledShifts(sport, viewShifts);
            ViewBag.ScheduledShifts = scheduledShifts;

            return View(await shifts.ToListAsync());
        }

        // GET: Shifts/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shift = await _context.Shifts
                .Include(s => s.Sport)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (shift == null)
            {
                return NotFound();
            }

            return View(shift);
        }

        // GET: Shifts/Create
        public IActionResult Create()
        {
            var sports = _context.Sports.OrderBy(s => s.Name);
            ViewBag.Sports = new SelectList(sports, "Id", "Name");

            var teamMembers = _context.TeamMembers
                .OrderBy(m => m.FirstName)
                .ThenBy(m => m.LastName);
            ViewBag.TeamMembers = teamMembers.ToList();

            return View();
        }

        // POST: Shifts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SportId,Description,Location,ShiftStartTime,ShiftEndTime,TeamMembersRequired")] Shift shift)
        {
            if (ModelState.IsValid)
            {
                shift.Id = Guid.NewGuid();
                _context.Add(shift);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Sports"] = new SelectList(_context.Sports, "Id", "Name", shift.SportId);
            return View(shift);
        }

        // GET: Shifts/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shift = await _context.Shifts.FindAsync(id);
            if (shift == null)
            {
                return NotFound();
            }
            ViewData["SportId"] = new SelectList(_context.Sports, "Id", "Id", shift.SportId);
            return View(shift);
        }

        // POST: Shifts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,SportId,Description,Location,ShiftStartTime,ShiftEndTime,TeamMembersRequired")] Shift shift)
        {
            if (id != shift.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(shift);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShiftExists(shift.Id))
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
            ViewData["SportId"] = new SelectList(_context.Sports, "Id", "Id", shift.SportId);
            return View(shift);
        }

        // GET: Shifts/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shift = await _context.Shifts
                .Include(s => s.Sport)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (shift == null)
            {
                return NotFound();
            }

            return View(shift);
        }

        // POST: Shifts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var shift = await _context.Shifts.FindAsync(id);
            if (shift != null)
            {
                _context.Shifts.Remove(shift);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShiftExists(Guid id)
        {
            return _context.Shifts.Any(e => e.Id == id);
        }

        private Dictionary<Shift, List<ScheduledShift>> GetScheduledShifts(string sport = "all",
                                                                           string viewShifts = "upcoming")
        {
            Dictionary<Shift, List<ScheduledShift>> scheduledShiftsDictionary = new();

            IQueryable<ScheduledShift> scheduledShifts = FilterScheduledShifts(viewShifts);
            IQueryable<Shift> shifts = scheduledShifts
                .Select(s => s.Shift)
                .Distinct();

            foreach (var shift in shifts)
            {
                List<ScheduledShift> _scheduledShifts = scheduledShifts
                    .Where(s => s.Shift == shift)
                    .ToList();

                scheduledShiftsDictionary.Add(shift, _scheduledShifts);
            }

            return scheduledShiftsDictionary;
        }

        private IQueryable<ScheduledShift> FilterScheduledShifts(string sport = "all", string viewShifts = "upcoming")
        {
            IQueryable<ScheduledShift> shifts = _context.ScheduledShifts
                .Include(s => s.Shift)
                .Include(s => s.Sport)
                .Include(s => s.TeamMember)
                .OrderBy(s => s.Shift.StartTime);

            DateTime currentDate = DateTime.Now;

            if (sport != "all")
                shifts = shifts.Where(s => s.Sport.AspRouteParameter == sport);

            switch (viewShifts)
            {
                case "past":
                    shifts = shifts.Where(s => s.Shift.StartTime.Date.CompareTo(currentDate) < 0);
                    break;

                default:
                    shifts = shifts.Where(s => s.Shift.StartTime.Date.CompareTo(currentDate) > 0);
                    break;
            }

            return shifts;
        }
    }
}
