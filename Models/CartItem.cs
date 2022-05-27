using System.ComponentModel.DataAnnotations;

namespace FlyBuy.Models
{
    public class CartItem
    {
        [Key]
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Total { get { return Quantity * Price; } }
        public string Image { get; set; }

        public CartItem()
        {
        }


        public CartItem(Product product , int quantity)
        {
            ProductId = product.Id;
            ProductName = product.Name;

            if (quantity == 0)
            {
                Quantity = 1;
            }
            else
            {
                Quantity = quantity;
            }

            Price = product.Price;
            Image = product.Image;
        }
    }
}
