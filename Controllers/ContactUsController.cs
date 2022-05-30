using FlyBuy.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FlyBuy.Controllers
{
    public class ContactUsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ContactUsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Admin,Manager,Worker")]
        public async Task<IActionResult> Index()
        {
            return _context.ContactUs != null ?
                        View(await _context.ContactUs.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.ContactUs'  is null.");
        }

        [Authorize(Roles = "Admin,Manager,Worker")]
        [HttpPost]
        public JsonResult Delete(int? id)
        {

            var contactUs = _context.ContactUs.Find(id);
            _context.ContactUs.Remove(contactUs);
            _context.SaveChanges();
            return new JsonResult(Ok());
        }

        [AllowAnonymous]
        public async Task<IActionResult> Execute(IFormCollection frm_coll)
        {
            if (ModelState.IsValid)
            {
                var ContactUs = new FlyBuy.Models.ContactUs()
                {
                    Name = frm_coll["Name"],
                    Email = frm_coll["Email"],
                    Message = frm_coll["Content"],
                    Subject = frm_coll["Subject"]
                };
                _context.ContactUs.Add(ContactUs);
                _context.SaveChanges();
            }

            return RedirectToAction("Index", "Home");
        }

        private bool ContactUsExists(int id)
        {
            return (_context.ContactUs?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
