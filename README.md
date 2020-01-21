# aspnet-core-examples
A collection of Telerik UI for ASP.NET Core examples

- The sample ASP.NET Core projects are provided to demonstrate different frequently asked questions, features and scenarios.
- They are not part of our testing procedures and should be regarded as a knowledge base.
- Projects are tested only upon creation as well as when updating or upgrading the project.
- Mandatory prerequisite is to have installed ASP.NET Core 3.0

To run the projects from this repository:

1. Clone the repo

## Telerik.Examples.Mvc

1. Open the Telerik.Examples.Mvc.sln file in VS2019 
2. Clean the solution
3. Build the solution and run the project.
4. Navigate to a certain example by adding the ControllerName/ActionMethod to the URL, i.e https://localhost:44361/ajaxbinding/ajaxbinding

> The project uses a local database which is created upon building the project based on the existent migrations.

## Telerik.Examples.RazorPages

1. Open the Telerik.Examples.RazorPages.sln file in VS2019 
2. Clean the solution
3. Build the solution and run the project.
4. Navigate to a certain example by adding the PageFolder/ViewName to the URL, i.e https://localhost:44361/grid/gridcustomdatasource


## Additional Example Notes

The following section aims to provide additional information or important notes regarding specific examples. 

### Telerik.Examples.Mvc

##### Editor/EditorContent

This project demonstrates how you can add, read, edit and delete text data using a local database and the Editor component

> Please note that the current project doesn't have any XSS attack preventions applied. It is a developer's responsibility to manage these security risks. For more information, please refer to this [Preventing Cross-Site Scripting](https://docs.telerik.com/kendo-ui/controls/editors/editor/preventing-xss) article.

##### MultiSelect/GetPostData

The MultiSelect is a `<select multiple>` element and behaves like one in a POST query - the browser will add a form data field with the name of the widget for each selected item, and the value of the form field will be the value of the item. This means that your model needs to expect a List of values for the given field.

The example also shows how you can get the selected values with JavaScript so you can use them to craft your own query in case you have more specific requirements.

You can read more about this in the [Submit MultiSelect Data to Controller POST](https://docs.telerik.com/aspnet-core/knowledge-base/multiselect-post-data-values) Knowledge Base article.

