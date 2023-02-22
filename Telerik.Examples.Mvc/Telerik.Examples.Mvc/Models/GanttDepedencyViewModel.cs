using Kendo.Mvc.UI;

namespace Telerik.Examples.Mvc.Models
{
    public class GanttDepedencyViewModel : IGanttDependency
    {
        public int DependencyID { get; set; }

        public string PredecessorID { get; set; }
        public string SuccessorID { get; set; }
        public DependencyType Type { get; set; }
    }
}
