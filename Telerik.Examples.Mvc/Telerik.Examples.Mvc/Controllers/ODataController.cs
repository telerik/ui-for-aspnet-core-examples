using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Examples.Mvc.Models;

namespace Telerik.Examples.Mvc.Controllers
{
    public class ProductsController : ODataController
    {
        [HttpGet]
        [EnableQuery]
        public List<Product> GetProducts()
        {
            var random = new Random();

            var data = Enumerable.Range(1, 100).Select(x => new Product
            {
                Discontinued = x % 2 == 1,
                ProductID = x,
                ProductName = "Product " + x,
                UnitPrice = random.Next(1, 1000),
                Category = new Category { CategoryID = x, CategoryName = "Category" + x },
                UnitsInStock = random.Next(1, 1000),
                UnitsOnOrder = random.Next(1, 1000)

            }).ToList();

            return data;
        }
        [HttpPut]
        [EnableQuery]
        public IActionResult Put([FromODataUri] int key, [FromBody] Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Custom update logic and update the Category field.
            return Updated(product);
        }
        [HttpPost]
        [EnableQuery]
        public IActionResult Post([FromBody] Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Custom create logic and update the Category field.
            return Created(product);
        }
        [HttpDelete]
        public IActionResult Delete([FromODataUri] int key)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Custom delete logic.
            return NoContent();
        }
    }
}
