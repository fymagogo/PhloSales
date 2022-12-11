using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Phlo.Api.Context;
using Phlo.Api.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Phlo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {

        private IApplicationDbContext _dbContext;
        private readonly ILogger<OrdersController> _logger;
        public OrdersController(IApplicationDbContext context, ILogger<OrdersController> logger)
        {
            _dbContext = context;
            _logger = logger;
        }

        // GET: api/<OrdersController>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var orders = await _dbContext.Orders.ToListAsync();
            if (orders == null) return NotFound();
            return Ok(orders);
        }

        // GET api/<OrdersController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var order = await _dbContext.Orders
                .Where(a => a.Id == id)
                .FirstOrDefaultAsync();
            if (order == null) return NotFound();

            return Ok(order);
        }

        // POST api/<OrdersController>
        [HttpPost]
        public async Task<IActionResult> Create(CreateUpdateOrder order)
        {
            var newOrder = new Order { CustomerName = order.CustomerName, Price = order.Price, ProductId = order.ProductId };
            _dbContext.Orders.Add(newOrder);
            await _dbContext.SaveChanges();
            return Ok();
        }

        // PUT api/<OrdersController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CreateUpdateOrder orderUpdate)
        {
            var order = _dbContext.Orders.Where(a => a.Id == id).FirstOrDefault();
            if (order == null) return NotFound();
            else
            {
                order.CustomerName = orderUpdate.CustomerName;
                order.Price = orderUpdate.Price;
                order.ProductId = orderUpdate.ProductId;
                await _dbContext.SaveChanges();
                return Ok(order.Id);
            }
        }

        // DELETE api/<OrdersController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var orders = await _dbContext.Orders.Where(a => a.Id == id).FirstOrDefaultAsync();
            if (orders == null) return NotFound();
            _dbContext.Orders.Remove(orders);
            await _dbContext.SaveChanges();
            return Ok();
        }

    }
}
