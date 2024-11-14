using Microsoft.AspNetCore.Identity;

namespace GameDayShiftScheduler.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool OneTimePasswordUsed { get; set; }
        public bool SMSEnabled { get; set; }
        public bool EmailEnabled { get; set; }
    }
}
