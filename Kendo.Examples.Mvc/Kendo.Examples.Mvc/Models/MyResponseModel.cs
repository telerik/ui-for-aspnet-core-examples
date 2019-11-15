using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kendo.Examples.Mvc.Models
{
    public class MyResponseModel
    {
        public IEnumerable DataCollection { get; set; }
        public int TotalRecords { get; set; }
    }
}
