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

        #region Foods
        private string[] ParseQuery()
        {
            return HttpContext.Request.QueryString.ToString()
                                .Split("%")[0]
                                .Replace('?', ' ')
                                .Split("=");
        }

        private static List<FoodModel> Foods()
        {
            return new List<FoodModel>()
            {
                new FoodModel {FoodID = 1, IsPartOf = null, hasChildren = true, FoodName = "Fruit"},
                new FoodModel {FoodID = 2, IsPartOf = 1, hasChildren = false, FoodName = "Banana"},
                new FoodModel {FoodID = 3, IsPartOf = null, hasChildren = true, FoodName = "Vegatables"},
                new FoodModel {FoodID = 4, IsPartOf = 3, hasChildren = false, FoodName = "Potato"},
            };
        }
        [HttpGet("odata/Foods")]
        [EnableQuery]
        public List<FoodModel> GetFoods()
        {
            var queryKeyValuePairs = ParseQuery();

            int? key = null;

            if (queryKeyValuePairs[0].Trim() == "key")
            {
                key = int.Parse(queryKeyValuePairs[1].Replace("&", ""));

            }
            List<FoodModel> data = Foods();

            data = data.Where(v => key.HasValue ? v.IsPartOf == key : v.IsPartOf == null)
                   .ToList();

            return data.ToList();
        }

        #endregion

        #region Products
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
        #endregion
    }
}
