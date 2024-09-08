using System.ComponentModel.DataAnnotations;

namespace MonPointOfSaleFinal.Entities.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [StringLength(50)]
        [Display(Name = "Category name")]
        [Required(ErrorMessage = "Name can not be empty")]
        public string CategoryName { get; set; }
        [StringLength(150)]
        [Display(Name = "Category description")]        
        public string? CategoryDescription { get; set; }
    }
}
