using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace GameDayShiftScheduler.Models
{
    /// <summary>
    ///     Represents a shift.
    ///     
    ///     Example:
    ///     Basketball vs. Fanshawe Falcons
    ///     Oct 19, 2024, 4:30-10:30pm
    ///     @ DBARC
    /// </summary>
    [Table("Shifts")]
    public class Shift
    {
        /// <summary>
        ///     The unique ID of this shift.
        /// </summary>
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        ///     The ID of the organization this shift belongs to.
        /// </summary>
        [ForeignKey(nameof(Organization))]
        public Guid OrganizationId { get; set; }

        /// <summary>
        ///     The organization this shift belongs to.
        /// </summary>
        [Display(Name = "Organization")]
        public Organization Organization { get; set; }

        /// <summary>
        ///     The ID of the sport for this shift.
        /// </summary>
        [ForeignKey(nameof(Sport))]
        public Guid SportId { get; set; }

        /// <summary>
        ///     The sport for this shift.
        /// </summary>
        [Display(Name = "Sport")]
        public Sport Sport { get; set; }

        /// <summary>
        ///     A brief description of this shift (ex. "Basketball vs. Fanshawe Falcons").
        /// </summary>
        [Display(Name = "Description")]
        public string Title { get; set; }

        /// <summary>
        ///     The location for this shift (ex. "DBARC").
        /// </summary>
        [Display(Name = "Location")]
        public string Location { get; set; }

        /// <summary>
        ///     The start time for this shift (ex. Oct 19, 2024 @ 4:30pm)
        /// </summary>
        [Display(Name = "Start Time")]
        public DateTime StartTime { get; set; }

        /// <summary>
        ///     The end time for this shift (ex. Oct 19, 2024 @ 10:30pm)
        /// </summary>
        [Display(Name = "End Time")]
        public DateTime EndTime { get; set; }

        /// <summary>
        ///     Whether this shift has been published.
        /// </summary>
        public bool IsPublished { get; set; }

        /// <summary>
        ///     Whether the shift has been cancelled.
        /// </summary>
        public bool IsCancelled { get; set; }

        /// <summary>
        ///     The current status of this shift.
        ///     
        ///     Saved - Shift has been saved, but not yet published.
        ///     Published - Shift has been published. All team members working that shift will be notified
        ///                 via SMS or email if they are signed up for those notifications.
        ///     Shift over - The shift has ended.
        ///     Cancelled - The shift has been cancelled. Will be deleted after 7 days.
        /// </summary>
        public string Status
        {
            get
            {
                if (IsPublished)
                {
                    if (DateTime.Now.CompareTo(EndTime) > 0)
                        return "Shift over";

                    if (IsCancelled)
                        return "Cancelled";

                    return "Published";
                }

                return "Saved";
            }
        }
    }
}
