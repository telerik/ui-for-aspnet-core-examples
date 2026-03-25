using Kendo.Mvc.UI;

namespace Telerik.Examples.Mvc.Models
{
    public class GanttDependencyViewModel : IGanttDependency
    {
        public int DependencyID { get; set; }
        public int PredecessorID { get; set; }
        public int SuccessorID { get; set; }
        public DependencyType Type { get; set; }
    }
}
