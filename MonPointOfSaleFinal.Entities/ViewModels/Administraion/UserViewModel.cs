using Microsoft.AspNetCore.Identity;

namespace MonPointOfSaleFinal.Entities.ViewModels.Administraion
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public IList<string> UserRoles { get; set; }
    }
}
