using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Telerik.Examples.RazorPages.Models;

namespace Telerik.Examples.RazorPages.Pages.CheckBoxGroup
{
    public class CheckBoxGroupIndexModel : PageModel
    {
        public List<IInputGroupItem> itemsList { get; set; }
        [BindProperty]
        public CheckBoxGroupViewModel CheckBoxGroupModel { get; set; }
        public void OnGet()
        {
            if (CheckBoxGroupModel == null)
            {
                itemsList = new List<IInputGroupItem>()
                {
                    new InputGroupItemModel()
                    {
                        Label = "Red",
                        Value = "one"
                    },
                     new InputGroupItemModel()
                    {
                        Label = "Green",
                        Value = "two"
                    },
                      new InputGroupItemModel()
                    {
                        Label = "Blue",
                        Value = "three"
                    }
                };

                CheckBoxGroupModel = new CheckBoxGroupViewModel() { Items = itemsList, CheckBoxGroupValue = new string[] { "two" } };
            }
        }
    }
}
