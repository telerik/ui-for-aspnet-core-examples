using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Telerik.Examples.Mvc.Models
{
    public class EmployeeViewModel
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
