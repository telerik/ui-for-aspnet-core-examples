using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Telerik.Examples.RazorPages.Models
{
    public class Employee
    {
        public int EmployeeID { get; set; }
        public string Address { get; set; }
        public string BirthDate { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Extension { get; set; }
        public string FirstName { get; set; }
        public string HireDate { get; set; }
        public string HomePhone { get; set; }
        public string LastName { get; set; }
        public string Notes { get; set; }
        public byte[] Photo { get; set; }
        public string PhotoPath { get; set; }
        public string PostalCode { get; set; }
        public string Region { get; set; }
        public int? ReportsTo { get; set; }
        public string Title { get; set; }
        public string TitleOfCourtesy { get; set; }
        
        public virtual Employee ReportsToNavigation { get; set; }
        public virtual ICollection<Employee> InverseReportsToNavigation { get; set; }
    }
}
