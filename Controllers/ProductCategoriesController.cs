using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FlyBuy.Data;
using FlyBuy.Models;
using System.Web.Helpers;

namespace FlyBuy.Controllers
{
    public class ProductCategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductCategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ProductCategories
        public async Task<IActionResult> Index()
        {
              return _context.ProductCategories != null ? 
                          View(await _context.ProductCategories.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.ProductCategories'  is null.");
        }


        // GET: ProductCategories/Create
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public JsonResult Create(ProductCategory productCategory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productCategory);
                _context.SaveChanges();
                
            }
            return new JsonResult(Ok());
        }


        // GET: ProductCategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ProductCategories == null)
            {
                return NotFound();
            }

            var productCategory = await _context.ProductCategories.FindAsync(id);
            if (productCategory == null)
            {
                return NotFound();
            }
            return View(productCategory);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] ProductCategory productCategory)
        {
            if (id != productCategory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductCategoryExists(productCategory.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(productCategory);
        }

        [HttpPost]
        public JsonResult Delete(int? id)
        {
            var ProducCategory = _context.ProductCategories.Find(id);
            _context.ProductCategories.Remove(ProducCategory);
            _context.SaveChanges();
            return new JsonResult(Ok());
        }

        private bool ProductCategoryExists(int id)
        {
          return (_context.ProductCategories?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
