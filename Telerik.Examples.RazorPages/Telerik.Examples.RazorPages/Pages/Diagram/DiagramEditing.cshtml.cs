using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Telerik.Examples.RazorPages.Models;

namespace Telerik.Examples.RazorPages.Pages.Diagram
{
    public class DiagramEditingModel : PageModel
    {
        public static IList<OrgDiagramShape> DiagramShapes;
        public static IList<OrgDiagramConnection> DiagramConnections;
        public void OnGet()
        {
            if (DiagramConnections == null)
            {
                DiagramConnections = new List<OrgDiagramConnection>();

                DiagramConnections.Add(new OrgDiagramConnection() { Id = 1, FromShapeId = 1, ToShapeId = 2 });
                DiagramConnections.Add(new OrgDiagramConnection() { Id = 2, FromShapeId = 1, ToShapeId = 3 });
                DiagramConnections.Add(new OrgDiagramConnection() { Id = 3, FromShapeId = 1, ToShapeId = 4 });
                DiagramConnections.Add(new OrgDiagramConnection() { Id = 4, FromShapeId = 2, ToShapeId = 5 });
                DiagramConnections.Add(new OrgDiagramConnection() { Id = 5, FromShapeId = 2, ToShapeId = 6 });
                DiagramConnections.Add(new OrgDiagramConnection() { Id = 6, FromShapeId = 3, ToShapeId = 7 });
                DiagramConnections.Add(new OrgDiagramConnection() { Id = 7, FromShapeId = 3, ToShapeId = 8 });
                DiagramConnections.Add(new OrgDiagramConnection() { Id = 8, FromShapeId = 4, ToShapeId = 9 });
                DiagramConnections.Add(new OrgDiagramConnection() { Id = 9, FromShapeId = 4, ToShapeId = 10 });
            }

            if (DiagramShapes== null)
            {
                DiagramShapes = new List<OrgDiagramShape>();

                DiagramShapes.Add(new OrgDiagramShape() { Id = 1, Color = "", JobTitle = "President" });
                DiagramShapes.Add(new OrgDiagramShape() { Id = 2, Color = "#3399cc", JobTitle = "VP Finance" });
                DiagramShapes.Add(new OrgDiagramShape() { Id = 3, Color = "#3399cc", JobTitle = "VP Customer Relations" });
                DiagramShapes.Add(new OrgDiagramShape() { Id = 4, Color = "#3399cc", JobTitle = "VP Human Resources" });
                DiagramShapes.Add(new OrgDiagramShape() { Id = 5, Color = "#ff9900", JobTitle = "Accountant" });
                DiagramShapes.Add(new OrgDiagramShape() { Id = 6, Color = "#ff9900", JobTitle = "Budget Analyst" });
                DiagramShapes.Add(new OrgDiagramShape() { Id = 7, Color = "#ff9900", JobTitle = "Relations Manager" });
                DiagramShapes.Add(new OrgDiagramShape() { Id = 8, Color = "#ff9900", JobTitle = "Technical Support Manager" });
                DiagramShapes.Add(new OrgDiagramShape() { Id = 9, Color = "#ff9900", JobTitle = "Compensation Manager" });
                DiagramShapes.Add(new OrgDiagramShape() { Id = 10, Color = "#ff9900", JobTitle = "Payroll Specialist" });
            }
        }

        public JsonResult OnPostReadShapes([DataSourceRequest] DataSourceRequest request)
        {
            return new JsonResult(DiagramShapes.ToDataSourceResult(request));
        }

        public JsonResult OnPostReadConnections([DataSourceRequest] DataSourceRequest request)
        {
            return new JsonResult(DiagramConnections.ToDataSourceResult(request));
        }

        public JsonResult OnPostCreateConnection([DataSourceRequest] DataSourceRequest request, OrgDiagramConnection connection)
        {
            connection.Id = DiagramConnections.Count + 1;
            DiagramConnections.Add(connection);

            return new JsonResult(new[] { connection }.ToDataSourceResult(request, ModelState));
        }

        public JsonResult OnPostCreateShape([DataSourceRequest] DataSourceRequest request, OrgDiagramShape shape)
        {
            shape.Id = DiagramShapes.Count + 2;
            DiagramShapes.Add(shape);

            return new JsonResult(new[] { shape }.ToDataSourceResult(request, ModelState));
        }

        public JsonResult OnPostUpdateConnection([DataSourceRequest] DataSourceRequest request, OrgDiagramConnection connection)
        {
            DiagramConnections.Where(x => x.Id == connection.Id).Select(x => connection);

            return new JsonResult(new[] { connection }.ToDataSourceResult(request, ModelState));
        }

        public JsonResult OnPostUpdateShape([DataSourceRequest] DataSourceRequest request, OrgDiagramShape shape)
        {
            DiagramConnections.Where(x => x.Id == shape.Id).Select(x => shape);

            return new JsonResult(new[] { shape }.ToDataSourceResult(request, ModelState));
        }

        public JsonResult OnPostDestroyConnection([DataSourceRequest] DataSourceRequest request, OrgDiagramConnection connection)
        {
            DiagramConnections.Remove(DiagramConnections.FirstOrDefault(x => x.Id == connection.Id));

            return new JsonResult(new[] { connection }.ToDataSourceResult(request, ModelState));
        }

        public JsonResult OnPostDestroyShape([DataSourceRequest] DataSourceRequest request, OrgDiagramShape shape)
        {
            DiagramShapes.Remove(DiagramShapes.FirstOrDefault(x => x.Id == shape.Id));

            return new JsonResult(new[] { shape }.ToDataSourceResult(request, ModelState));
        }
    }
}
