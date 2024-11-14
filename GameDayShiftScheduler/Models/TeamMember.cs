using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameDayShiftScheduler.Models
{
    /// <summary>
    ///     Represents a team member.
    /// </summary>
    [Table("TeamMembers")]
    public class TeamMember
    {
        /// <summary>
        ///     The unique ID of this team member.
        /// </summary>
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        ///     This team member's first name.
        /// </summary>
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        /// <summary>
        ///     This team member's last name.
        /// </summary>
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Full Name")]
        public string FullName => $"{FirstName} {LastName}";
    }
}
