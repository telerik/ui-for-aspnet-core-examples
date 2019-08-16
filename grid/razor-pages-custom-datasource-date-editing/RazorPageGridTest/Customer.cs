using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RazorPageGridTest
{
    public class Customer
    {
        public long CustomerId { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public DateTime? ClockOut { get; set; }
    }
}
