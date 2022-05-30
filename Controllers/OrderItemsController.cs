using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FlyBuy.Data;
using FlyBuy.Models;
using Microsoft.AspNetCore.Authorization;

namespace FlyBuy.Controllers
{
    public class OrderItemsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrderItemsController(ApplicationDbContext context)
        {
            _context = context;
        }


        [Authorize(Roles = "Admin,Manager,Worker")]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.OrderItems.Include(o => o.Order).Include(o => o.Product);
            
            return View(await applicationDbContext.ToListAsync());
        }

        private bool OrderItemExists(int id)
        {
          return (_context.OrderItems?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
