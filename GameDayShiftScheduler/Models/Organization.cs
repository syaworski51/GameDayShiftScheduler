using System.ComponentModel.DataAnnotations.Schema;

namespace GameDayShiftScheduler.Models
{
    [Table("Organizations")]
    public class Organization
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? LogoPath { get; set; }
    }
}
