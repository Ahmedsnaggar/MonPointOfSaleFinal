using System.ComponentModel.DataAnnotations;

namespace MonPointOfSaleFinal.Entities.ViewModels.Administraion
{
    public class ChangePasswordViewModel
    {
        [Display(Name = "Current password")]
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; }
        [Display(Name = "New password")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        [Display(Name = "Confirm password")]
        [DataType(DataType.Password)]
        [Compare("NewPassword")]
        public string ConfirmPassword { get; set; }
    }
}
