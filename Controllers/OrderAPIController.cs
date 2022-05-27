using FlyBuy.Data;
using FlyBuy.Models;
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
        public IActionResult Index()
        {
            List<Order> orders = _context.Orders.ToList();
            List<OrderItem> order_items = _context.OrderItems.ToList();

            var items = orders.GroupBy(u => u.CreatedDate, (key, items) => new
            {
                Month = key,
                Total = orders.Where(x => x.CreatedDate == key).Count()
            }
            ).OrderBy(o => o.Month).ToList();

            return Ok(items);
        }
    }
}
