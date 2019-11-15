using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Kendo.Examples.Mvc.Models
{
    public class GridViewModel
    {
        public int ID
        {
            get;
            set;
        }

        public string Text
        {
            get;
            set;
        }

        [UIHint("PersonFieldEditor")]
        public ResultEntryViewModel Person
        {
            get;
            set;
        }
    }
}
