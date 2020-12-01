using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Telerik.Examples.RazorPages.Models
{
    public class Product
    {
        public int ProductID { get; set; }
        public int CategoryID { get; set; }

        public string ProductName { get; set; }
        public string ImageUrl { get; set; }

        public decimal UnitPrice { get; set; }

        public int UnitsInStock { get; set; }

        public bool Discontinued { get; set; }
    }
}
