using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using ProductMVC.Models;

namespace ProductMVC.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController:Controller
    {
        private readonly ApplicationContext _db;

        public ProductController(ApplicationContext context)
        {
            _db = context;
        }

        [HttpGet]
        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            if (_db.Products.Count() == 0)
                return NotFound();

            return await _db.Products.ToListAsync();
        }

        [HttpGet]
        public async Task<ActionResult<Product>> GetAsync(long id)
        {
            var myProduct = await _db.Products.FindAsync(id);

            if (myProduct is null)
            {
                return NotFound();
            }

            return myProduct;
        }

        [HttpPost]
        public async Task<ActionResult<Product>> AddAsync(Product product)
        {
            if (product is null)
                return BadRequest();

            _db.Products.Add(product);
            await _db.SaveChangesAsync();

            return new ObjectResult(product) {StatusCode = StatusCodes.Status201Created};
        }

        [HttpPut]
        public async Task<ActionResult<Product>> UpdateAsync(Product product)
        {
            if (product is null)
                return BadRequest();

            var oldProduct = await _db.Products.FindAsync(product.Id);

            if (oldProduct is null)
                return NotFound();

            _db.Products.Update(product);
            await _db.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete]
        public async Task<ActionResult<Product>> DeleteAsync(long id)
        {
            var delProduct = await _db.Products.FindAsync(id);
            if (delProduct is null)
            {
                return NotFound();
            }

            _db.Products.Remove(delProduct);
            await _db.SaveChangesAsync();

            return NoContent();
        }
    }
}
