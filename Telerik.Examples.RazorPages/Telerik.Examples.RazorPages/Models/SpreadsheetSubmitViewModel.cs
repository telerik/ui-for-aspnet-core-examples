using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Telerik.Examples.RazorPages.Models
{
    public class SpreadsheetSubmitViewModel
    {
        public IList<Product> Created { get; set; }

        public IList<Product> Destroyed { get; set; }

        public IList<Product> Updated { get; set; }
    }
}
