using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Telerik.Examples.RazorPages.Models
{
    public class StoreViewModel
    {
        public int StoreID { get; set; }

        [Display(Name = "Store Name")]
        public string StoreName { get; set; }

        [UIHint("Number")]
        [Display(Name = "Employees Count")]
        public int EmployeesCount { get; set; }

        [DataType(DataType.Currency)]
        public decimal Budget { get; set; }

        [UIHint("CustomerEditor")]
        public virtual ICollection<Customer> Customers { get; set; }

    }
}
