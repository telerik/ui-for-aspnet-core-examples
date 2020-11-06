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
    }
}
