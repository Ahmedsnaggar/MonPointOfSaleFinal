using System.ComponentModel.DataAnnotations;

namespace MonPointOfSaleFinal.Entities.Models
{
    public class Customer
    {
        public int Id { get; set; }
        [StringLength(50)]
        public string CustomerName { get; set; }
        public string? Address { get; set; }
        [StringLength(20)]
        public string? PhoneNumber { get; set; }
        [StringLength(150)]
        public string? Email { get; set; }
        [StringLength(100)]
        public string? WebSite { get; set; }

    }
}
