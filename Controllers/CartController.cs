﻿using Microsoft.AspNetCore.Mvc;
using FlyBuy.SessionConfig;
using FlyBuy.Models;
using FlyBuy.Data;
using System.Threading.Tasks;
using System.Linq;

namespace FlyBuy.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CartController(ApplicationDbContext context)
        {
            _context = context;
        }


        public IActionResult Index()
        {
            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart") ?? new List<CartItem>();

            CartViewModel cartVM = new CartViewModel
            {
                CartItems = cart,
                GrandTotal = cart.Sum(x => x.Price * x.Quantity)
            };

            return View(cartVM);
        }

        public async Task<IActionResult> Add(int id)
        {
            Product product = await _context.Products.FindAsync(id);
            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart") ?? new List<CartItem>();
            CartItem cartItem = cart.Where(x => x.ProductId == id).FirstOrDefault();

            if (cartItem == null)
            {
                cart.Add(new CartItem(product));
            }
            else
            {
                cartItem.Quantity += 1;
            }

            HttpContext.Session.SetJson("Cart", cart);
            return RedirectToAction("Index");
        }

        public IActionResult Decrease(int id)
        {
            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart");

            CartItem cartItem = cart.Where(x => x.ProductId == id).FirstOrDefault();

            if (cartItem.Quantity > 1)
            {
                --cartItem.Quantity;
            }
            else
            {
                cart.RemoveAll(x => x.ProductId == id);
            }

            if (cart.Count == 0)
            {
                HttpContext.Session.Remove("Cart");
            }
            else
            {
                HttpContext.Session.SetJson("Cart", cart);
            }

            return RedirectToAction("Index");
        }

        public IActionResult Remove(int id)
        {
            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart");

            cart.RemoveAll(x => x.ProductId == id);

            if (cart.Count == 0)
            {
                HttpContext.Session.Remove("Cart");
            }
            else
            {
                HttpContext.Session.SetJson("Cart", cart);
            }

            return RedirectToAction("Index");
        }


        public IActionResult Clear()
        {
            HttpContext.Session.Remove("Cart");
            if (HttpContext.Request.Headers["X-Requested-With"] != "XMLHttpRequest")
                return Redirect(Request.Headers["Referer"].ToString());

            return Ok();
        }

        [HttpPost]
        public IActionResult CheckOut(Order order)
        {
            ModelState.Remove("Details");

            if (ModelState.IsValid)
            {
                List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart");
                if (cart == null)
                {
                    return NotFound();
                }
                _context.Orders.Add(order);
                _context.SaveChanges();

                foreach (var stCart in cart)
                {
                    OrderItem orderDetails = new OrderItem()
                    {
                        OrderId = order.Id,
                        ProductId = stCart.ProductId,
                        Quantity = stCart.Quantity,
                        Price = (float)stCart.Price
                    };
                    _context.OrderItems.Add(orderDetails);
                    _context.SaveChanges();
                }

                HttpContext.Session.Remove("Cart");            
            }

            return new JsonResult(Ok());

        }
    }
}