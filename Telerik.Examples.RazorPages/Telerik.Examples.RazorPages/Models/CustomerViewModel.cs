using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Telerik.Examples.RazorPages.Models
{
    public class CustomerViewModel
    {
        public int CustomerID { get; set; }
        public string ContactName { get; set; }
        public string Position { get; set; }
        public string CompanyName { get; set; }
        public string Country { get; set; }
    }
}
