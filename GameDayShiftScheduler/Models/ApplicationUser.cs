using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameDayShiftScheduler.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        
        public string LastName { get; set; }

        public string FullName => $"{FirstName} {LastName}";

        [ForeignKey(nameof(Organization))]
        public Guid OrganizationId { get; set; }
        
        public Organization Organization { get; set; }
        
        public bool OneTimePasswordUsed { get; set; }
        
        public bool SMSEnabled { get; set; }
        
        public bool EmailEnabled { get; set; }

        public string? ProfilePicturePath { get; set; } = "~/images/default-profile-picture.png";
    }
}
