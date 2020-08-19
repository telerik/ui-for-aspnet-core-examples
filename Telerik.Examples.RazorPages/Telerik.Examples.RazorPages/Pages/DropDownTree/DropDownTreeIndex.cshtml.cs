using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Telerik.Examples.RazorPages.Models;

namespace Telerik.Examples.RazorPages.Pages.DropDownTree
{
    public class DropDownTreeIndexModel : PageModel
    {
        public static IList<HierarchicalViewModel> result = new List<HierarchicalViewModel>()
        {
            new HierarchicalViewModel() { ID = 1, ParentID = null, HasChildren = true, Name = "Parent Item 1" },
            new HierarchicalViewModel() { ID = 2, ParentID = null, HasChildren = true, Name = "Parent Item 2" },
            new HierarchicalViewModel() { ID = 3, ParentID = null, HasChildren = true, Name = "Parent Item 3" },
            new HierarchicalViewModel() { ID = 4, ParentID = 1, HasChildren = false, Name = "Child Item 1" },
            new HierarchicalViewModel() { ID = 5, ParentID = 1, HasChildren = false, Name = "Child Item 2" },
            new HierarchicalViewModel() { ID = 6, ParentID = 2, HasChildren = false, Name = "Child Item 3" },
            new HierarchicalViewModel() { ID = 7, ParentID = 2, HasChildren = false, Name = "Child Item 4" },
            new HierarchicalViewModel() { ID = 8, ParentID = 3, HasChildren = false, Name = "Child Item 5" },
            new HierarchicalViewModel() { ID = 9, ParentID = 3, HasChildren = false, Name = "Child Item 6" }
        };

        public JsonResult OnGetDropDownTreeRead(int? id)
        { 
            var data = result.Where(x => id.HasValue ? x.ParentID == id : x.ParentID == null)
                .Select(item => new {
                    id = item.ID,
                    Name = item.Name,
                    hasChildren = item.HasChildren
                });

            return new JsonResult(data);
        }
    }
}