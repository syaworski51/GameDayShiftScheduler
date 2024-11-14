using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameDayShiftScheduler.Models
{
    /// <summary>
    ///     Represents a sport.
    /// </summary>
    [Table("Sports")]
    public class Sport
    {
        /// <summary>
        ///     The unique ID of this sport.
        /// </summary>
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        ///     The name of this sport.
        /// </summary>
        [Display(Name = "Name")]
        public string Name { get; set; }

        /// <summary>
        ///     The value that goes to the ASP route parameter in links.
        ///     Consists of the name converted to lowercase and any spaces replaced with hyphens ("-").
        /// </summary>
        [Display(Name = "ASP Route Param.")]
        public string AspRouteParameter { get; set; }
    }
}
