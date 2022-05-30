using Microsoft.AspNetCore.Mvc;

namespace FlyBuy.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error/{statusCode}")]
        public IActionResult Index(int statusCode)
        {

            switch (statusCode)
            {
                case 404: return View("NotFound"); break;
            }
            return NoContent();
        }
    }
}
