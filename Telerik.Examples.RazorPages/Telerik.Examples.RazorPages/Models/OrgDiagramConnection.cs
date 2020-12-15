using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Telerik.Examples.RazorPages.Models
{
    public  class OrgDiagramConnection
    {
        public long Id { get; set; }
        public long? FromShapeId { get; set; }
        public long? ToShapeId { get; set; }
    }
}
