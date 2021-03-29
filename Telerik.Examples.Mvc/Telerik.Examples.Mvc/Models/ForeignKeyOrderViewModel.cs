using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Telerik.Examples.Mvc.Models
{
    public class ForeignKeyOrderViewModel
    {
        public int OrderID
        {
            get;
            set;
        }

        public string CustomerID { get; set; }

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

        //[UIHint("CityEditor")]
        public int? ShipCityId
        {
            get;
            set;
        }
        public CityViewModel City
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
