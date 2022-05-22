using System.ComponentModel.DataAnnotations;

namespace FlyBuy.Models
{
    public class AgeCategory
    {
        public int Id { get; set; }

        [Display(Name = "Age Category")]
        [Required(ErrorMessage = "Category name is required")]
        [MaxLength(45, ErrorMessage = "The category name can be maximum 45 characters long")]
        public string? Name { get; set; }

        public ICollection<Product>? Products { get; set; }

    }
}
