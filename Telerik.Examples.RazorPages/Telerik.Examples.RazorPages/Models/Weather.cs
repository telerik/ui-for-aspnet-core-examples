using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Telerik.Examples.RazorPages.Models
{
    public class Weather
    {
        public int Id { get; set; }
        public double Rain { get; set; }

        public double TMax { get; set; }
        public double Wind { get; set; }
    }
}
