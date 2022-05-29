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
    public class RatingsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RatingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
              return _context.Ratings != null ? 
                          View(await _context.Ratings.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Ratings'  is null.");
        }

        
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Description,Rate,Name,Time")] Rating rating)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(rating.Name))
                {
                    rating.Name = "Anonymous";
                }
                _context.Add(rating);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Index" , "Home");
        }

        [HttpPost]
        public JsonResult DeleteAsync(int id)
        {

            var PageRating = _context.Ratings.Find(id);
            _context.Ratings.Remove(PageRating);
            _context.SaveChanges();
            return new JsonResult(Ok());

        }

        private bool RatingExists(int id)
        {
          return (_context.Ratings?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
