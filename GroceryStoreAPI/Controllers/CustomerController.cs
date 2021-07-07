using GroceryStoreAPI.Entity;
using GroceryStoreAPI.Helper;
using GroceryStoreAPI.Service;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GroceryStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : Controller
    {
        public readonly ICustomerService _customer;
        public CustomerController(ICustomerService customer)
        {
            _customer = customer;
        }
        // GET: api/<CustomersController>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Customer>))]
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<Customer> customers = await _customer.GetAll();

            if (customers != null)
                return Ok(customers);

            return NotFound();
        }

        // GET api/<CustomersController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Customer))]
        public async Task<IActionResult> Get(int id)
        {
            var customer = await _customer.Get(id);

            if (customer != null)
                return Ok(customer);

            return NotFound();
        }

        // POST api/<CustomersController>
        [HttpPost]
        [ProducesResponseType(201)]
        [ModelValidation]
        public async Task<IActionResult> Add(Customer customer)
        {
            if (customer == null)
                return BadRequest();

            await _customer.Add(customer);

            return CreatedAtAction("Get", new { id = customer.Id }, customer);
        }

        // PUT api/<CustomersController>/5
        [HttpPut]
        [ProducesResponseType(204)]
        [ModelValidation]
        public async Task<IActionResult> Update( [FromBody] Customer customer)
        {
            if (string.IsNullOrWhiteSpace(customer.Name))
                return BadRequest("Name is required");

            await _customer.Update(customer);

            return NoContent();
        }
    }
}
