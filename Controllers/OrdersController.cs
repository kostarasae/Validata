using Data.Data;
using Data.Models.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly CustomerOrdersDbContext dbContext;
        public OrdersController(CustomerOrdersDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetAllOrders()
        {
            var orders = dbContext.Orders.ToList();
            return Ok(orders);
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetOrderById([FromRoute] Guid id) 
        {
            var order = dbContext.Orders.Find(id);
            if (order == null) 
            {
                return NotFound();
            }
            return Ok(order);
         }

        [HttpPost]
        public IActionResult AddOrder([FromBody] Order addOrderRequest)
        {
            var orderDomainModel = new Order
            {
                Id = addOrderRequest.Id,
                Items = addOrderRequest.Items,
                Date = DateTime.UtcNow,
                TotalPrice = addOrderRequest.TotalPrice,
                Customer = addOrderRequest.Customer
            };

            dbContext.Orders.Add(orderDomainModel);
            dbContext.SaveChanges();

            return CreatedAtAction(nameof(GetOrderById), new { id = orderDomainModel.Id }, orderDomainModel);
        }        
    }
}
