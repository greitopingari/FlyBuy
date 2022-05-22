using FlyBuy.Data;
using FlyBuy.Models;
using Microsoft.AspNetCore.Mvc;

namespace FlyBuy.Controllers
{
    public class OrdersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrdersController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ShoppingCart()
        {
            var cart_items = GetCartItems();
            return View(cart_items);
        }
        public void AddItemToCart(int id)
        {
            var shoppingCart_GUID = SetCartId();
            var shoppingCartItem = _context.ShoppingCartItems.SingleOrDefault(n => n.Product.Id == id && n.ShoppingCartId == shoppingCart_GUID);

            if (shoppingCartItem == null)
            {
                shoppingCartItem = new CartItem()
                {
                    Id = Guid.NewGuid().ToString(),
                    Product = _context.Products.SingleOrDefault(p => p.Id == id),
                    Quantity = 1,
                    ShoppingCartId = shoppingCart_GUID
                };


                _context.ShoppingCartItems.Add(shoppingCartItem);
            }
            else
            {
                shoppingCartItem.Quantity++;
            }
            _context.SaveChanges();
        }

        public void RemoveItemFromCart(int id)
        {
            var shoppingCart_GUID = SetCartId();
            var shoppingCartItem = _context.ShoppingCartItems.SingleOrDefault(n => n.Product.Id == id && n.ShoppingCartId == shoppingCart_GUID);

            if (shoppingCartItem != null)
            {
                if (shoppingCartItem.Quantity > 1)
                {
                    shoppingCartItem.Quantity--;
                }
                else
                {
                    _context.ShoppingCartItems.Remove(shoppingCartItem);
                }
            }
            _context.SaveChanges();
        }

        // Complete Order
        public async Task<IActionResult> CompleteOrder(FormCollection frc)
        {
            var items = GetCartItems();

            //Get Order Information by Form
            var order = new Order()
            {
                CustomerName = frc["cusName"],
                CustomerPhone = frc["cusPhone"],
                CustomerEmail = frc["cusEmail"],
                CustomerAddress = frc["cusAddress"],
            };
            _context.Orders.Add(order);
            _context.SaveChanges();

            //Get Order Items by Cart session
            foreach (var item in items)
            {
                var orderItem = new OrderItem()
                {
                    Quantity = item.Quantity,
                    ProductId = item.Product.Id,
                    OrderId = order.Id,
                    Price = (int)item.Product.Price
                };
                await _context.OrderItems.AddAsync(orderItem);
            }
            await _context.SaveChangesAsync();

            // Reset Cart
            EmptyCart();
            return View("OrderCompleted");
        }


        // Set A generated ID
        public string SetCartId()
        {
            return Guid.NewGuid().ToString();
        }

        // Get list of items that are in current Cart ID
        public List<CartItem> GetCartItems()
        {
            return _context.ShoppingCartItems.Where(c => c.ShoppingCartId == SetCartId()).ToList();
        }

        public void EmptyCart()
        {
            var cartItems = GetCartItems();
            foreach (var item in cartItems)
            {
                _context.ShoppingCartItems.Remove(item);
            }
            _context.SaveChanges();
        }
    }
}
