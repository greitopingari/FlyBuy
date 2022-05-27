using System.ComponentModel.DataAnnotations;

namespace FlyBuy.Models
{
    public class EditRole
    {
        public string Id { get; set; }
        [Required]
        [Display(Name = "Role")]
        public string Name { get; set; }
    }
}
