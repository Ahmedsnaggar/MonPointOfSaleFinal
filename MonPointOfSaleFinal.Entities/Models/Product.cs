using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MonPointOfSaleFinal.Entities.Models
{
    public class Product
    {
        public int Id { get; set; }
        [StringLength(50)]
        public string ProductName { get; set; }
        public double DefaulPrice { get; set; }
        [StringLength(250)]
        public string? ProductImage { get; set; }
        [ForeignKey(nameof(category))]
        public int categoryId { get; set; }
        public Category? category { get; set; }
    }
}
