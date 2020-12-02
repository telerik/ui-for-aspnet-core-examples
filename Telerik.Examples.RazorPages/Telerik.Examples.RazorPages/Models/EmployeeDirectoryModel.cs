using System;
using System.ComponentModel.DataAnnotations;

namespace Telerik.Examples.RazorPages.Models
{
    public class EmployeeDirectoryModel
    {
        [ScaffoldColumn(false)]
        public int EmployeeId { get; set; }

        [Required]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [ScaffoldColumn(false)]
        public int? ReportsTo { get; set; }

        public string Position { get; set; }

        [ScaffoldColumn(false)]
        public bool hasChildren { get; set; }

        private DateTime? hireDate;
        [DataType(DataType.Date)]
        [Display(Name = "Hire Date")]
        public DateTime? HireDate
        {
            get
            {
                return hireDate;
            }
            set
            {
                if (value.HasValue)
                {
                    hireDate = value.Value;
                }
                else
                {
                    hireDate = null;
                }
            }
        }
    }
}
