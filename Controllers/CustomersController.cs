using Data.Data;
using Data.Models.Domain;
using Data.Repositories.CustomerRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly CustomerOrdersDbContext dbContext;
        public ICustomerRepository _repo { get; set; }

        public CustomersController(ICustomerRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public IActionResult GetAllCustomers()
        {
            var customers = dbContext.Customers.ToList();
            _repo.GetType
            return Ok(customers);
        }

        [HttpPost]
        public IActionResult CreateCustomer(Customer customer)
        {
            _repo.Create(customer);
            _repo.Save();
            return Ok(customer);
        }
    }
}
