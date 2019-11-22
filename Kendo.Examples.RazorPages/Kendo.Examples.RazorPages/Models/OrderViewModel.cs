using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Kendo.Examples.RazorPages.Models
{
    public class OrderViewModel
    {
        public int OrderID
        {
            get;
            set;
        }

        [Required]
        public decimal? Freight
        {
            get;
            set;
        }

        public DateTime? OrderDate
        {
            get;
            set;
        }

        public string ShipCity
        {
            get;
            set;
        }

        public string ShipName
        {
            get;
            set;
        }
    }
}
