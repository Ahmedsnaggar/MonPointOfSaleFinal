using System.ComponentModel.DataAnnotations;

namespace MonPointOfSaleFinal.Entities.ViewModels.Account
{
    public class RegisterViewModel
    {
        [StringLength(256)]
        [Display(Name ="User name")]
        public string UserName { get; set; }
        [StringLength(256)]
        [Display(Name = "User email")]
        [EmailAddress(ErrorMessage = "Email not in valid format")]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
