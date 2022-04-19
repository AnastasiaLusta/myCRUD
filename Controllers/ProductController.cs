using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace ProductMVC.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController:Controller
    {
        private static readonly List<Product> _products = new();

        [HttpGet]
        public IActionResult Get()
        {
            if (_products.Count == 0)
                return NotFound();

            return Ok(_products);
        }

        [HttpGet]
        public IActionResult Get(int id)
        {
            var newProduct = _products.Find(x => x.Id == id);
            if (newProduct is null)
            {
                return NotFound();
            }
            return Ok(newProduct);
        }

        [HttpPost]
        public IActionResult Add(Product product)
        {
            if (product is null)
                return BadRequest();

            _products.Add(product);
            return Ok(_products);
        }

        [HttpPut]
        public IActionResult Update(Product product)
        {
            if (product is null)
                return BadRequest();

            var oldProduct = _products.Find(x => x.Id == product.Id);

            if (oldProduct is null)
                return NotFound();

            _products.Remove(oldProduct);
            _products.Add(product);

            return Ok(product);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var delProduct = _products.Find(x => x.Id == id);
            if (delProduct is null)
            {
                return NotFound();
            }

            return Ok("The product is deleted");
        }
    }
}
