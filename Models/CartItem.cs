using System.ComponentModel.DataAnnotations;

namespace FlyBuy.Models
{
    public class CartItem
    {
        [Key]
        public string Id { get; set; }
        public Product Product { get; set; }

        public int Quantity { get; set; }

        public string ShoppingCartId { get; set; }
    }
}
