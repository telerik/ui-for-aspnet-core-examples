using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Telerik.Examples.Mvc.Models
{
    public class EditorData
    {
        [Key]
        public int ContentId { get; set; }
        public string EditorContent { get; set; }

    }
}
