using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Telerik.Examples.RazorPages.Models
{
    public class Category
    {
        public Category()
        {            
            Products = new HashSet<Product>();
        }

        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public byte[] Picture { get; set; }
        
        public virtual ICollection<Product> Products { get; set; }
    }
}
