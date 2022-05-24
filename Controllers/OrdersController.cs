using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FlyBuy.Data;
using FlyBuy.Models;

namespace FlyBuy.Controllers
{
    public class OrdersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrdersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
         
              return _context.Orders != null ? 
                          View(await _context.Orders.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Orders'  is null.");
            
        }

        [HttpPost]
        public JsonResult Delete(int? id)
        {

            var order = _context.Orders.Find(id);
            _context.Orders.Remove(order);
            _context.SaveChanges();
            return new JsonResult(Ok());
        }

        private bool OrderExists(int id)
        {
          return (_context.Orders?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
