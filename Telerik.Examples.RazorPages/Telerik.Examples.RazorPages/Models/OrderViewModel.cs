using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Telerik.Examples.RazorPages.Models
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
        [Required]
        public DateTime? OrderDate
        {
            get;
            set;
        }
        [Required]
        public string ShipCity
        {
            get;
            set;
        }
        [Required]
        [MinLength(5,ErrorMessage = "Ship Name must be at least 5 characters long")]
        public string ShipName
        {
            get;
            set;
        }
    }
}
