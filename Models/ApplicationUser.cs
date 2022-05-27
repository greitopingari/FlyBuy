using Microsoft.AspNetCore.Identity;

namespace FlyBuy.Models
{
    public class ApplicationUser : IdentityUser
    {
        public DateTime BirthDate { get; set; }
    }
}
