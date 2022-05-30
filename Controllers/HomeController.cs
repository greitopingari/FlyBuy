using FlyBuy.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using FlyBuy.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace FlyBuy.Controllers
{
    public class HomeController : Controller
    {

        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            ViewData["Exlusive"] = _context.Products.Where(p => p.Exclusive == true).Take(2).OrderBy(p => p.LastUpdated).ToList();
            ViewData["Rating"] = _context.Ratings.OrderBy(t => t.Time).Take(4).ToList();
            ViewData["LatestProducts"] = _context.Products.OrderByDescending(P => P.LastUpdated).ToList();
            return View();
        }

        [AllowAnonymous]
        public async Task<IActionResult> ProductDetail(int? id)
        {
            Random random = new Random();

            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

           var product = await _context.Products.Include(p => p.Category).FirstOrDefaultAsync(m => m.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            var a = await _context.Products.Where(m => m.ProductCategoryId == product.CategoryId).ToListAsync();
            ViewBag.Suggestion = a.OrderBy(x => random.Next()).Take(4);

            return View(product);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


    }
}