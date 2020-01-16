using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Telerik.Examples.Mvc.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string FirstMidName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }
        public int Age { get; set; }
        public Status Status { get; set; }
    }
}
