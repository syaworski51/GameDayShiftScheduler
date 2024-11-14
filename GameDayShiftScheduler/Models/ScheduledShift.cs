using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameDayShiftScheduler.Models
{
    /// <summary>
    ///     Represents a shift scheduled for a team member.
    /// </summary>
    [Table("ScheduledShifts")]
    public class ScheduledShift
    {
        /// <summary>
        ///     The unique ID of this shift.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        ///     The ID of the sport for which this shift is being scheduled.
        /// </summary>
        [ForeignKey(nameof(Sport))]
        public Guid SportId { get; set; }

        /// <summary>
        ///     The sport for which this shift is being scheduled.
        /// </summary>
        [Display(Name = "Sport")]
        public Sport Sport { get; set; }

        /// <summary>
        ///     The ID of the shift that this is being scheduled for.
        /// </summary>
        [ForeignKey(nameof(Shift))]
        public Guid ShiftId { get; set; }

        /// <summary>
        ///     The shift that this is being scheduled for (ex. Basketball vs. Fanshawe, Oct 19 @ DBARC, 4:30-10:30pm).
        /// </summary>
        [Display(Name = "Shift")]
        public Shift Shift { get; set; }

        /// <summary>
        ///     The ID of the team member this shift is being scheduled for.
        /// </summary>
        [ForeignKey(nameof(TeamMember))]
        public Guid TeamMemberId { get; set; }

        /// <summary>
        ///     The team member this shift is being scheduled for.
        /// </summary>
        [Display(Name = "Team Member")]
        public TeamMember TeamMember { get; set; }
    }
}
