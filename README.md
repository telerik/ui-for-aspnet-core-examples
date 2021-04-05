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

## Referencing the Commerial NuGet Package

All active licence holders have access to the private Telerik NuGet Feed. The `Telerik.Examples.Mvc` and `Telerik.Examples.RazorPages` reference a local NuGet package that is a Trial version of the Telerik UI for ASP.NET Core NuGet. 

In order to add a reference to the commercial NuGet Package follow the steps below:
 
1. Navigate to **Dependencies > Packages** from the Solution explorer in Visual Studio.
1. Right click on **Telerik.UI.for.AspNet.Core.Trial** and select Remove from the context menu.
1. Go to **Tools > NuGet Package Manager > Package Manager Settings**, select **Package Manager Sources** and then click the + button.
1. Choose feed Name, set the feed URL to: https://nuget.telerik.com/nuget and click OK.

    ![KendoUIResources](images/add-nuget-package-source.png)

1. Browse and Install the **Telerik.UI.for.AspNet.Core** NuGet package.

> Visual Studio sometimes caches the NuGet packages and they should be cleared. In order to do this, open the **Tools > NuGet Package Manager > Package Manager Settings** menu command, then select **Clear All NuGet Cache(s)**.

> If an error is present in the Package Manager and a reference to the deleted source is still standing, оpen the NuGet Package Feed and remove the reference manually. 

For more information on how to add the private Telerik NuGet feed and install the package refer to the [NuGet Install in ASP.NET Core article](https://docs.telerik.com/aspnet-core/getting-started/installation/nuget-install)

## Additional Example Notes

The following section aims to provide additional information or important notes regarding specific examples. 

### Telerik.Examples.Mvc

#### Editor/EditorContent

This project demonstrates how you can add, read, edit and delete text data using a local database and the Editor component

> Please note that the current project doesn't have any XSS attack preventions applied. It is a developer's responsibility to manage these security risks. For more information, please refer to this [Preventing Cross-Site Scripting](https://docs.telerik.com/kendo-ui/controls/editors/editor/preventing-xss) article.

#### MultiSelect/GetPostData

The MultiSelect is a `<select multiple>` element and behaves like one in a POST query - the browser will add a form data field with the name of the widget for each selected item, and the value of the form field will be the value of the item. This means that your model needs to expect a List of values for the given field.

The example also shows how you can get the selected values with JavaScript so you can use them to craft your own query in case you have more specific requirements.

You can read more about this in the [Submit MultiSelect Data to Controller POST](https://docs.telerik.com/aspnet-core/knowledge-base/multiselect-post-data-values) Knowledge Base article.

