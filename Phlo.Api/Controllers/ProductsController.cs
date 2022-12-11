using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Phlo.Api.Context;
using Phlo.Api.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Phlo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private IApplicationDbContext _dbContext;
        private readonly ILogger<ProductsController> _logger;
        public ProductsController(IApplicationDbContext context, ILogger<ProductsController> logger)
        {
            _dbContext = context;
            _logger = logger;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _dbContext.Products.ToListAsync();
            if (products == null) return NotFound();
            return Ok(products);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _dbContext.Products
                .Where(a => a.Id == id)
                .Include(a => a.Orders)
                .FirstOrDefaultAsync();
            if (product == null) return NotFound();

            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> Create(string productName)
        {
            var product = new Product { Name = productName };
            _dbContext.Products.Add(product);
            await _dbContext.SaveChanges();
            return Ok(product.Id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Product productUpdate)
        {
            var product = _dbContext.Products.Where(a => a.Id == id).FirstOrDefault();
            if (product == null) return NotFound();
            else
            {
                product.Name = productUpdate.Name;
                await _dbContext.SaveChanges();
                return Ok(product.Id);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _dbContext.Products.Where(a => a.Id == id).FirstOrDefaultAsync();
            if (product == null) return NotFound();
            _dbContext.Products.Remove(product);
            await _dbContext.SaveChanges();
            return Ok(product.Id);
        }

        [HttpGet("sold")]
        public async Task<IActionResult> ProductsSold()
        {
            var products = await _dbContext.Products
                .Include(a => a.Orders)
                .Where(b => b.Orders.Any())
                .Select(c => new ProductSold
                {
                    Id = c.Id,
                    Name = c.Name,
                    MaxPrice = c.Orders.Max(d => d.Price),
                    MinPrice = c.Orders.Min(d => d.Price),
                    NumberOfOrders = c.Orders.Count()
                }).ToListAsync();

            if (products == null) return NotFound();
            return Ok(products);
        }

    }
}
