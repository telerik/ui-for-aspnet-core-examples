using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutocompleteEditorAllowSettingNewValues.Models;
using AutocompleteEditorAllowSettingNewValues.ViewModels;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;

namespace AutocompleteEditorAllowSettingNewValues.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public JsonResult GetAutocomplete(string text, string methodName)
        {
            List<ResultEntry> lookupResults = new List<ResultEntry>();

            //just getting something back...
            lookupResults.Add(new ResultEntry("1", "apple"));
            lookupResults.Add(new ResultEntry("2", "banana"));
            lookupResults.Add(new ResultEntry("3", "orange"));
            lookupResults.Add(new ResultEntry("4", "strawberry"));
            lookupResults.Add(new ResultEntry("5", "grapefruit"));

            var newLookupResults = lookupResults.Where(p => p.Name.Contains(text));

            return Json(newLookupResults);
        }

        public ActionResult Read([DataSourceRequest] DataSourceRequest request)
        {
            List<GridViewModel> gridModel = new List<GridViewModel>();
            gridModel.Add(new GridViewModel() { Text = "Hello", ID = 0, Person = new ResultEntryViewModel() { ID = "1", MethodName = "Anything", MinimumChars = 3, Name = "Apple", SignDate = DateTime.Now } });
            return Json(gridModel.ToDataSourceResult(request));
        }
    }
}