using FlyBuy.Data;
using Microsoft.AspNetCore.Mvc;
namespace FlyBuy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderAPIController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public OrderAPIController(ApplicationDbContext context)
        {
            _context = context;
        }

        //Filter by Quarter

        [HttpGet]
        public IActionResult Index(int? quarter)
        {
            if (quarter == null)
            {
                return Ok(_context.Orders);
            }
            else if (quarter >= 1 && quarter <= 3)
            {
                var orders = _context.Orders.Where(x => x.CreatedDate.Month >= 1 && x.CreatedDate.Month <= 3).Count();
                return Ok("Q1 / Total Orders : " + orders);
            }
            else if (quarter >= 4 && quarter <= 6)
            {
                var orders = _context.Orders.Where(x => x.CreatedDate.Month >= 4 && x.CreatedDate.Month <= 6).Count();
                return Ok("Q2 / Total Orders : " + orders);
            }
            else if (quarter >= 7 && quarter <= 9)
            {
                var orders = _context.Orders.Where(x => x.CreatedDate.Month >= 7 && x.CreatedDate.Month <= 9).Count();
                return Ok("Q3 / Total Orders : " + orders);
            }
            else
            {
                var orders = _context.Orders.Where(x => x.CreatedDate.Month >= 10 && x.CreatedDate.Month <= 12).Count();
                return Ok("Q4 / Total Orders : " + orders);
            }
            return Ok();
        }
    }
}
