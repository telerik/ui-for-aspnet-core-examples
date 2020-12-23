using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Telerik.Examples.RazorPages.Models
{
    public class ElectricityProduction
    {

        public string Year { get; set; }
        public int Solar { get; set; }
        public int Nuclear { get; set; }
        public int Hydro { get; set; }
        public int Wind { get; set; }
    }
}
