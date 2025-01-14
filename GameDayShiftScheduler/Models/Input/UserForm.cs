using System.ComponentModel.DataAnnotations;

namespace GameDayShiftScheduler.Models.Input
{
    public class UserForm
    {
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }

        public Guid OrganizationId { get; set; }
        
        public List<string> Roles { get; set; }
    }
}
