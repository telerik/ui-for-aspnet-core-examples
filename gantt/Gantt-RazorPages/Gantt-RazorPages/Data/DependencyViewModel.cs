using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gantt_RazorPages.Data
{
    public class DependencyViewModel : IGanttDependency
    {
        public int DependencyID { get; set; }

        public string PredecessorID { get; set; }
        public string SuccessorID { get; set; }
        public DependencyType Type { get; set; }
    }
}
