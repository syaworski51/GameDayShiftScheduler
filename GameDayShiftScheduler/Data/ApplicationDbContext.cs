using GameDayShiftScheduler.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GameDayShiftScheduler.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Sport> Sports { get; set; } = default!;
        public DbSet<Shift> Shifts { get; set; } = default!;
        public DbSet<ScheduledShift> ScheduledShifts { get; set; } = default!;
        public DbSet<Organization> Organizations { get; set; } = default!;
    }
}
