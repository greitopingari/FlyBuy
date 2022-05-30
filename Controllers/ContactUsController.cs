using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FlyBuy.Data;
using FlyBuy.Models;
using SendGrid;
using SendGrid.Helpers.Mail;
using Microsoft.AspNetCore.Authorization;

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

            var apiKey = "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";
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(frm_coll["Email"], frm_coll["Name"]);
            var subject = frm_coll["Subject"];
            var to = new EmailAddress("memaklevis2@gmail.com");
            var plainTextContent = frm_coll["Content"];
            var htmlContent = frm_coll["Content"];
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            await client.SendEmailAsync(msg);

            return RedirectToAction("Index");
        }

        private bool ContactUsExists(int id)
        {
          return (_context.ContactUs?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
