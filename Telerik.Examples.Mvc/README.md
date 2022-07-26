# Telerik.Examples.Mvc

The current project consists of a collection of Telerik UI for ASP.NET Core examples that cover common scenarios. It is built over the MVC (Model-View-Controller) design pattern exposed by the ASP.NET Core framework. For more information refer to the dedicated [ASP.NET MVC documentation article by Microsoft](https://dotnet.microsoft.com/apps/aspnet/mvc). 

## Local Setup

1. Clone the repo
2. Open the Telerik.Examples.Mvc.sln file in VS2019 
3. Clean the solution
4. Build the solution and run the project.
5. Navigate to a certain example by adding the ControllerName/ActionMethod to the URL, i.e https://localhost:44361/ajaxbinding/ajaxbinding

> The project uses a local database which is created upon building the project based on the existent migrations.
> Mandatory prerequisite is to have installed ASP.NET Core 3.0
## Additional Example Notes

The following section aims to provide additional information or important notes regarding specific examples. 

### Telerik.Examples.Mvc

#### Editor/EditorContent

This project demonstrates how you can add, read, edit and delete text data using a local database and the UI for ASP.NET Core Editor component

> Please note that the current project doesn't have any XSS attack preventions applied. It is a developer's responsibility to manage these security risks. For more information, please refer to this [Preventing Cross-Site Scripting](https://docs.telerik.com/kendo-ui/controls/editors/editor/preventing-xss) article.

#### MultiSelect/GetPostData

The MultiSelect UI component is a `<select multiple>` element and behaves like one in a POST query - the browser will add a form data field with the name of the widget for each selected item, and the value of the form field will be the value of the item. This means that your model needs to expect a List of values for the given field.

The example also shows how you can get the selected values with JavaScript so you can use them to craft your own query in case you have more specific requirements.

You can read more about this in the [Submit MultiSelect Data to Controller POST](https://docs.telerik.com/aspnet-core/knowledge-base/multiselect-post-data-values) Knowledge Base article.
