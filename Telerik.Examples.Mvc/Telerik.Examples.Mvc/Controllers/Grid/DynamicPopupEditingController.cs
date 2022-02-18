using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

namespace Telerik.Examples.Mvc.Controllers.Grid
{
    public class DynamicPopupEditingController : Controller
    {
        public static DataTable db = new DataTable();
        public IActionResult DynamicPopupEditing()
        {
            db = GetDataTable(50);
            List<System.Data.DataColumn> columnData = new List<System.Data.DataColumn>();
            foreach(System.Data.DataColumn colData in db.Columns)
            {
                columnData.Add(colData);

            }
            ViewData["modelData"] = columnData;
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

        public IActionResult Customers_Update([DataSourceRequest] DataSourceRequest request, IFormCollection data)
        {
            var dt = GetDataTableColumns();
            var updatedRow = dt.NewRow();
            for (int i = 0; i < db.Rows.Count; i++)
            {
                var itemToBeUpdatedId = data[db.PrimaryKey[0].ToString()][0];
                var row = db.Rows[i];
                if (row[db.PrimaryKey[0]].ToString() == itemToBeUpdatedId)
                {
                    for (var j = 0; j < db.Columns.Count; j++)
                    {
                        if (data[db.Columns[j].ColumnName][0] != null)
                        {
                            TypeConverter typeConverter = TypeDescriptor.GetConverter(db.Columns[j].DataType);
                            row[db.Columns[j].ColumnName] = typeConverter.ConvertFromString(data[db.Columns[j].ColumnName][0]);
                            updatedRow[db.Columns[j].ColumnName] = row[db.Columns[j].ColumnName];
                        }
                    }

                }
            }
            return Json(dt.ToDataSourceResult(request));
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
