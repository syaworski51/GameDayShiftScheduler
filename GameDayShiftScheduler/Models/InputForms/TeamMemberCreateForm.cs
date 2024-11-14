using System.ComponentModel.DataAnnotations;

namespace GameDayShiftScheduler.Models.InputForms
{
    public class TeamMemberCreateForm
    {
        /// <summary>
        ///     The team member's first name.
        /// </summary>
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        /// <summary>
        ///     The team member's last name.
        /// </summary>
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        /// <summary>
        ///     The team member's email address.
        /// </summary>
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }

        /// <summary>
        ///     Whether the admin creating this account has chosen to create the one-time password themselves,
        ///     or to have the one-time password generated for them.
        ///     
        ///     Create password - The admin has chosen to create the one-time password themselves.
        ///     Generate password - A random string of 12 characters will be created.
        /// </summary>
        [Display(Name = "Password Options")]
        public string OneTimePasswordOption { get; set; }

        /// <summary>
        ///     The team member's one-time password. Null if the admin chose to have this password randomly generated.
        /// </summary>
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string? Password { get; set; }

        /// <summary>
        ///     The team member's one-time password, repeated for confirmation. 
        ///     Null if the admin chose to have this password randomly generated.
        /// </summary>
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        public string? ConfirmPassword { get; set; }

        [Display(Name = "Roles")]
        public List<string> Roles { get; set; }
    }
}
