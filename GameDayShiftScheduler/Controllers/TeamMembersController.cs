using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GameDayShiftScheduler.Data;
using GameDayShiftScheduler.Models;
using Microsoft.AspNetCore.Identity;
using GameDayShiftScheduler.Models.InputForms;
using GameDayShiftScheduler.Areas.Identity.Pages.Account;

namespace GameDayShiftScheduler.Controllers
{
    public class TeamMembersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public TeamMembersController(ApplicationDbContext context,
                                     SignInManager<ApplicationUser> signInManager,
                                     UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        // GET: TeamMembers
        public async Task<IActionResult> Index()
        {
            return View(await _context.TeamMembers.ToListAsync());
        }

        // GET: TeamMembers/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teamMember = await _context.TeamMembers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (teamMember == null)
            {
                return NotFound();
            }

            return View(teamMember);
        }

        // GET: TeamMembers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TeamMembers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TeamMemberCreateForm form)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser
                {
                    FirstName = form.FirstName,
                    LastName = form.LastName,
                    Email = form.EmailAddress,
                    UserName = form.EmailAddress,
                    OneTimePasswordUsed = false
                };

                string password = form.Password ?? GeneratePassword();
                var result = await _userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    TeamMember teamMember = new TeamMember
                    {
                        Id = Guid.NewGuid(),
                        FirstName = form.FirstName,
                        LastName = form.LastName
                    };

                    _context.TeamMembers.Add(teamMember);
                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }
            }
            
            return View(form);
        }

        // GET: TeamMembers/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teamMember = await _context.TeamMembers.FindAsync(id);
            if (teamMember == null)
            {
                return NotFound();
            }
            return View(teamMember);
        }

        // POST: TeamMembers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,FirstName,PreferredName,LastName,OneTimePasswordUsed")] TeamMember teamMember)
        {
            if (id != teamMember.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(teamMember);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeamMemberExists(teamMember.Id))
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
            return View(teamMember);
        }

        // GET: TeamMembers/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teamMember = await _context.TeamMembers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (teamMember == null)
            {
                return NotFound();
            }

            return View(teamMember);
        }

        // POST: TeamMembers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var teamMember = await _context.TeamMembers.FindAsync(id);
            if (teamMember != null)
            {
                _context.TeamMembers.Remove(teamMember);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TeamMemberExists(Guid id)
        {
            return _context.TeamMembers.Any(e => e.Id == id);
        }

        private string GeneratePassword(int length = 12)
        {
            const string CHARS = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%^&*()_+[]{}|;:,.<>?";
            char[] password = new char[length];
            Random random = new Random();

            for (int index = 0; index < password.Length; index++)
            {
                int charIndex = random.Next(CHARS.Length);
                password[index] = CHARS[charIndex];
            }

            return new string(password);
        }
    }
}
