using System;
using System.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;

namespace Telerik.Examples.Mvc.Controllers.Grid
{
    public class DynamicBatchEditingController : Controller
    {

        public static DataTable db = new DataTable();
        public IActionResult DynamicBatchEditing()
        {
            db = GetDataTable(50);

            return View(db);
        }


        private DataTable GetDataTable(int howMany)
        {
            DataTable dt = GetDataTableColumns();

            for (int i = 0; i < howMany; i++)
            {
                int index = i + 1;

                DataRow row = dt.NewRow();

                row["OrderID"] = index;
                row["OrderDate"] = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0).AddHours(index);
                row["Freight"] = index * 0.1 + index * 0.01;
                row["ShipName"] = "Name " + index;
                row["ShipCity"] = "City " + index;
                row["ShipCountry"] = "Country " + index;

                dt.Rows.Add(row);
            }

            return dt;
        }
        public IActionResult Customers_Read([DataSourceRequest] DataSourceRequest request)
        {
            return Json(db.ToDataSourceResult(request));
        }

        public IActionResult Customers_Create([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")] IFormCollection models)
        {
            DataTable editedItemsTable = GetDataTableColumns();
            for (int x = 0; x < models.Count; x++)
            {
                if (models.Keys.Contains("models[" + x + "].OrderID"))
                {
                    int index = x;
                    DataRow row = editedItemsTable.NewRow();

                    row["OrderID"] = new Random().Next(models.Count, Int32.MaxValue);
                    row["OrderDate"] = models["models[" + index + "].OrderDate"][0];
                    row["Freight"] = models["models[" + index + "].Freight"][0];
                    row["ShipName"] = models["models[" + index + "].ShipName"][0];
                    row["ShipCity"] = models["models[" + index + "].ShipCity"][0];
                    row["ShipCountry"] = models["models[" + index + "].ShipCountry"][0];

                    editedItemsTable.Rows.Add(row);
                }
            }
            db.Merge(editedItemsTable);

            return Json(editedItemsTable.ToDataSourceResult(request));
        }

        public IActionResult Customers_Update([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")] IFormCollection models)
        {
            DataTable editedItemsTable = GetDataTableColumns();
            for (int x = 0; x < models.Count; x++)
            {
                if (models.Keys.Contains("models[" + x + "].OrderID"))
                {
                    int index = x;
                    DataRow row = editedItemsTable.NewRow();

                    row["OrderID"] = Int32.Parse(models["models[" + index + "].OrderID"][0]);
                    row["OrderDate"] = models["models[" + index + "].OrderDate"][0];
                    row["Freight"] = models["models[" + index + "].Freight"][0];
                    row["ShipName"] = models["models[" + index + "].ShipName"][0];
                    row["ShipCity"] = models["models[" + index + "].ShipCity"][0];
                    row["ShipCountry"] = models["models[" + index + "].ShipCountry"][0];

                    editedItemsTable.Rows.Add(row);
                }
            }
            db.Merge(editedItemsTable);

            return Json(editedItemsTable.ToDataSourceResult(request));
        }
        public IActionResult Customers_Destroy([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")] IFormCollection models)
        {
            for (int i = 0; i < models.Count; i++)
            {

                var primaryKey = db.PrimaryKey[0].ToString();
                if (models.Keys.Contains($"models[{i}].{primaryKey}"))
                {
                    var itemToBeRemoved = models[$"models[{i}].{primaryKey}"][0];

                    foreach (DataRow row in db.Rows)
                    {
                        if (row[db.PrimaryKey[0]].ToString() == itemToBeRemoved)
                        {
                            db.Rows.Remove(row);
                            break;
                        }
                    }
                }
            }
            return Json(db.ToDataSourceResult(request));
        }

        private DataTable GetDataTableColumns()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("OrderID", typeof(int)));
            dt.Columns.Add(new DataColumn("OrderDate", typeof(DateTime)));
            dt.Columns.Add(new DataColumn("Freight", typeof(decimal)));
            dt.Columns.Add(new DataColumn("ShipName", typeof(string)));
            dt.Columns.Add(new DataColumn("ShipCity", typeof(string)));
            dt.Columns.Add(new DataColumn("ShipCountry", typeof(string)));
            dt.PrimaryKey = new DataColumn[] { dt.Columns["OrderID"] };
            return dt;
        }
    }
}
