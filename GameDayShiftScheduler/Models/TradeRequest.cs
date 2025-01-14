using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameDayShiftScheduler.Models
{
    /// <summary>
    ///     Represents a request made by a user to trade a shift
    /// </summary>
    [Table("TradeRequests")]
    public class TradeRequest
    {
        /// <summary>
        ///     The unique ID of this trade request.
        /// </summary>
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        ///     The date this request was created.
        /// </summary>
        [Display(Name = "Date Created")]
        public DateTime DateCreated { get; set; }

        /// <summary>
        ///     The date this request was submitted to the supervisor. Null if it has not yet been submitted.
        /// </summary>
        [Display(Name = "Date Submitted")]
        public DateTime? DateSubmitted { get; set; }

        /// <summary>
        ///     The date that the supervisor responded to this trade request, whether they approved it or denied it.
        ///     Null if the supervisor has not yet responded.
        /// </summary>
        [Display(Name = "Date Responded")]
        public DateTime? DateResponded { get; set; }

        /// <summary>
        ///     The ID of the team member that initiated this trade request.
        /// </summary>
        [ForeignKey(nameof(TeamMember1))]
        [MaxLength(450)]
        public string TeamMember1Id { get; set; }

        /// <summary>
        ///     The team member that initiated this trade request.
        /// </summary>
        [Display(Name = "Team Member #1")]
        public ApplicationUser TeamMember1 { get; set; }

        /// <summary>
        ///     The ID of the shift that the initiating team member wants to trade.
        /// </summary>
        [ForeignKey(nameof(Shift1))]
        public Guid Shift1Id { get; set; }

        /// <summary>
        ///     The shift that the initiating team member wants to trade.
        /// </summary>
        [Display(Name = "Shift #1")]
        public ScheduledShift Shift1 { get; set; }

        /// <summary>
        ///     The ID of the intended recipient of this trade request.
        /// </summary>
        [ForeignKey(nameof(TeamMember2))]
        [MaxLength(450)]
        public string TeamMember2Id { get; set; }

        /// <summary>
        ///     The ID of the intended recipient of this trade request.
        /// </summary
        [Display(Name = "Team Member #2")]
        public ApplicationUser TeamMember2 { get; set; }

        /// <summary>
        ///     The ID of the shift that the initiating team member wants in return.
        /// </summary>
        [ForeignKey(nameof(Shift2))]
        public Guid Shift2Id { get; set; }

        /// <summary>
        ///     The shift that the initiating team member wants in return.
        /// </summary>
        [Display(Name = "Shift #2")]
        public ScheduledShift Shift2 { get; set; }

        /// <summary>
        ///     The status of this trade request.
        ///     
        ///     Sent - The trade request has been sent to the intended recipent.
        ///     Accepted - The recipient accepted this trade request, and it has been sent to the supervisor for approval.
        ///     Declined - The recipient declined this trade request.
        ///     Approved - The supervisor has approved this trade request.
        ///     Denied - The supervisor has denied this trade request.
        /// </summary>
        [Display(Name = "Status")]
        public string Status { get; set; }

        /// <summary>
        ///     Optional field for the supervisor to leave comments about this trade request.
        /// </summary>
        public string? Comments { get; set; }
    }
}
