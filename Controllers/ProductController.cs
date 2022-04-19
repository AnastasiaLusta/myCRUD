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
        private static List<Product> products = new();

        [HttpGet]
        public IActionResult Get()
        {
            if (products.Count == 0)
                return NotFound();

            return Ok(products);
        }

        [HttpGet]
        public IActionResult Get(int id)
        {
            var newProduct = products.Find(x => x.Id == id);
            if (newProduct is null)
            {
                return NotFound();
            }
            return Ok(newProduct);
        }

        [HttpPost]
        public IActionResult Add(Product product)
        {
            if (product is null) return NotFound();

            products.Add(product);
            return Ok(products);
        }

        [HttpPut]
        public IActionResult Update(Product product)
        {
            var oldProduct = products.Find(x => x.Id == product.Id);

            if (oldProduct is null)
                return NotFound();

            products.Remove(oldProduct);
            products.Add(product);

            return Ok(product);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var delProduct = products.Find(x => x.Id == id);
            if (delProduct is null)
            {
                return NotFound();
            }

            return Ok("The product is deleted");
        }
    }
}
