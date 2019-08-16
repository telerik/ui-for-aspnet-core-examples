using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ListBoxAsEditor.Models
{
    public class OrderViewModel
    {
        public OrderViewModel()
        {
            this.Locations = new List<Location>();
        }
		public int OrderID
		{
			get;
			set;
		}

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
        
        public List<Location> Locations { get;  set; }
    }
}
