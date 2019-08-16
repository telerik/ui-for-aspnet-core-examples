using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MultiSelectInGrid.Models
{
    public class OrderViewModel
    {
        public OrderViewModel()
        {
            this.Employees = new List<EmployeeViewModel>();
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

        [UIHint("EmployeesEditor")]
        public List<EmployeeViewModel> Employees { get; set; }
    }
}
