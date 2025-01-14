using GameDayShiftScheduler.Models.Input;
using System.ComponentModel.DataAnnotations;

namespace GameDayShiftScheduler.Models.InputForms
{
    public class UserCreateForm : UserForm
    {
        /// <summary>
        ///     The team member's one-time password. Null if the admin chose to have this password randomly generated.
        /// </summary>
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        /// <summary>
        ///     The team member's one-time password, repeated for confirmation. 
        ///     Null if the admin chose to have this password randomly generated.
        /// </summary>
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }
    }
}
