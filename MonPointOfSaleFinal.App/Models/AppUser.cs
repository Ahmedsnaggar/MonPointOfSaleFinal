using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace MonPointOfSaleFinal.App.Models
{
    public class AppUser : IdentityUser
    {
        [StringLength(100)]
        public string? Address { get; set; }
    }
}
