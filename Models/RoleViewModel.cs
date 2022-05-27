using System.ComponentModel.DataAnnotations;

namespace FlyBuy.Models
{
    public class RoleViewModel
    {
        [Required]
        [Display(Name = "Role")]
        public string Name { get; set; }
    }
}
