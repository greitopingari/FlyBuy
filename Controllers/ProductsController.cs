using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FlyBuy.Data;
using FlyBuy.Models;
using Newtonsoft.Json;

namespace FlyBuy.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _HostEnvironment;

        public ProductsController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _HostEnvironment = hostEnvironment;
        }

       
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Products.Include(p => p.Category).Include(p => p.ProductCategory);
            return View(await applicationDbContext.ToListAsync());
        }

      
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.AgeCategories, "Id", "Name");
            ViewData["ProductCategoryId"] = new SelectList(_context.ProductCategories, "Id", "Name");
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Price,Description,LastUpdated,AgeCategory,Rating,ImageFile,CategoryId,ProductCategoryId")] Product product)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _HostEnvironment.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(product.ImageFile.FileName);
                string extension = Path.GetExtension(product.ImageFile.FileName);
                product.Image = fileName += DateTime.Now.ToString("yymmssfff") + extension;
                string path = Path.Combine(wwwRootPath + "/Images/Products", fileName);

                using (var fileSteam = new FileStream(path, FileMode.Create))
                {
                    await product.ImageFile.CopyToAsync(fileSteam);
                }

                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.AgeCategories, "Id", "Name", product.CategoryId);
            ViewData["ProductCategoryId"] = new SelectList(_context.ProductCategories, "Id", "Name", product.ProductCategoryId);
            return View(product);
        }

       
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }
            var product = await _context.Products.FindAsync(id);

            TempData["imgPath"] = product.Image;

            if (product == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.AgeCategories, "Id", "Name", product.CategoryId);
            ViewData["ProductCategoryId"] = new SelectList(_context.ProductCategories, "Id", "Name", product.ProductCategoryId);
            return View(product);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price,Description,LastUpdated,AgeCategory,Rating,ImageFile,CategoryId,ProductCategoryId")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (product.ImageFile == null)
            {
                product.Image = TempData["imgPath"].ToString();
                _context.Entry(product).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {

                var imagePath = Path.Combine(_HostEnvironment.WebRootPath + "\\Images\\Products", TempData["imgPath"].ToString());
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }

                try
                {
                    string wwwRootPath = _HostEnvironment.WebRootPath;
                    string fileName = Path.GetFileNameWithoutExtension(product.ImageFile.FileName);
                    string extension = Path.GetExtension(product.ImageFile.FileName);
                    product.Image = fileName += DateTime.Now.ToString("yymmssfff") + extension;
                    string path = Path.Combine(wwwRootPath + "/Images/Products", fileName);

                    using (var fileSteam = new FileStream(path, FileMode.Create))
                    {
                        await product.ImageFile.CopyToAsync(fileSteam);
                    }

                    _context.Update(product);
                    await _context.SaveChangesAsync();
            }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
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
            ViewData["CategoryId"] = new SelectList(_context.AgeCategories, "Id", "Name", product.CategoryId);
            ViewData["ProductCategoryId"] = new SelectList(_context.ProductCategories, "Id", "Name", product.ProductCategoryId);
            return View(product);
        }

        [HttpPost]
        public JsonResult Delete(int? id)
        {

            Product Produkti = _context.Products.Find(id);

            if(Produkti.Image != null)
            {
                var imagePath = Path.Combine(_HostEnvironment.WebRootPath + "\\Images\\Products", Produkti.Image);

                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }


                _context.Products.Remove(Produkti);
                _context.SaveChanges();
                return new JsonResult(Ok());
            }

            _context.Products.Remove(Produkti);
            _context.SaveChanges();
            return new JsonResult(Ok());

        }

        private bool ProductExists(int id)
        {
          return (_context.Products?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
