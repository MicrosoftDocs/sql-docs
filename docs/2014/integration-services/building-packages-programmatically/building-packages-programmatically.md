---
title: "Building Packages Programmatically | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services 
ms.topic: "reference"
ms.assetid: 7474b1f4-7607-4f28-a6fd-67f7db1dd3f8
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Building Packages Programmatically
  If you need to create packages dynamically, or to manage and execute [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] packages outside the development environment, you can manipulate packages programmatically. In this approach, you have a continuous range of options:  
  
-   Load and execute an existing package without modification.  
  
-   Load an existing package, reconfigure it (for example, for a different data source), and execute it.  
  
-   Create a new package, add and configure components object by object and property by property, save it, and execute it.  
  
 You can use the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] object model to write code that creates, configures, and executes packages in any managed programming language. For example, you may want to create metadata-driven packages that configure their connections or their data sources, transformations, and destinations based on the selected data source and its tables and columns.  
  
 This section describes and demonstrates how to create and configure a package programmatically line by line. At the less complex end of the range of package programming options, you can simply load and run an existing package without modification as described in [Running and Managing Packages Programmatically](../run-manage-packages-programmatically/running-and-managing-packages-programmatically.md).  
  
 An intermediate option not described here is that of loading an existing package as a template, reconfiguring it (for example, for a different data source), and executing it. You can also use the information in this section to modify the existing objects in a package.  
  
> [!NOTE]  
>  When you use an existing package as a template and modify existing columns in the data flow, you may have to remove existing columns and call the <xref:Microsoft.SqlServer.Dts.Pipeline.PipelineComponent.ReinitializeMetaData%2A> method of affected components.  
  
## In This Section  
 [Creating a Package Programmatically](../building-packages-programmatically/creating-a-package-programmatically.md)  
 Describes how to create a package programmatically.  
  
 [Adding Tasks Programmatically](../building-packages-programmatically/adding-tasks-programmatically.md)  
 Describes how to add tasks to the package.  
  
 [Connecting Tasks Programmatically](../building-packages-programmatically/connecting-tasks-programmatically.md)  
 Describes how to control execution of the containers and tasks in a package based on the result of the execution of a previous task or container.  
  
 [Adding Connections Programmatically](../building-packages-programmatically/adding-connections-programmatically.md)  
 Describes how to add connection managers to a package.  
  
 [Working with Variables Programmatically](../building-packages-programmatically/working-with-variables-programmatically.md)  
 Describes how to add and use variables during package execution.  
  
 [Handling Events Programmatically](../building-packages-programmatically/handling-events-programmatically.md)  
 Describes how to handle package and task events.  
  
 [Enabling Logging Programmatically](../building-packages-programmatically/enabling-logging-programmatically.md)  
 Describes how to enable logging for a package or task, and how to apply custom filters to log events.  
  
 [Adding the Data Flow Task Programmatically](../building-packages-programmatically/adding-the-data-flow-task-programmatically.md)  
 Describes how to add and configure the Data Flow task and its components.  
  
 [Discovering Data Flow Components Programmatically](../building-packages-programmatically/discovering-data-flow-components-programmatically.md)  
 Describes how to detect the components that are installed on the local computer.  
  
 [Adding Data Flow Components Programmatically](../building-packages-programmatically/adding-data-flow-components-programmatically.md)  
 Describes how to add a component to a data flow task.  
  
 [Connecting Data Flow Components Programmatically](../building-packages-programmatically/connecting-data-flow-components-programmatically.md)  
 Describes how to connect two data flow components.  
  
 [Selecting Input Columns Programmatically](../building-packages-programmatically/selecting-input-columns-programmatically.md)  
 Describes how to select input columns from those that are provided to a component by upstream components in the data flow.  
  
 [Saving a Package Programmatically](../building-packages-programmatically/saving-a-package-programmatically.md)  
 Describes how to save a package programmatically.  
  
## Reference  
 [Integration Services Error and Message Reference](../integration-services-error-and-message-reference.md)  
 Lists the predefined [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] error codes with their symbolic names and descriptions.  
  
## Related Sections  
 [Extending Packages with Scripting](../extending-packages-scripting/extending-packages-with-scripting.md)  
 Discusses how to extend the control flow by using the Script task, and how to extend the data flow by using the Script component.  
  
 [Extending Packages with Custom Objects](../extending-packages-custom-objects/extending-packages-with-custom-objects.md)  
 Discusses how to create program custom tasks, data flow components, and other package objects for use in multiple packages.  
  
 [Running and Managing Packages Programmatically](../run-manage-packages-programmatically/running-and-managing-packages-programmatically.md)  
 Discusses how to enumerate, run, and manage packages and the folders in which they are stored.  
  
## External Resources  
  
-   CodePlex samples, [Integration Services Product Samples](https://go.microsoft.com/fwlink/?LinkID=131204), on www.codeplex.com/MSFTISProdSamples  
  
-   Blog entry, [Performance profiling your custom extensions](https://go.microsoft.com/fwlink/?LinkId=238831), on blogs.msdn.com.  
  
![Integration Services icon (small)](../media/dts-16.gif "Integration Services icon (small)")  **Stay Up to Date with Integration Services**<br /> For the latest downloads, articles, samples, and videos from Microsoft, as well as selected solutions from the community, visit the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] page on MSDN:<br /><br /> [Visit the Integration Services page on MSDN](https://go.microsoft.com/fwlink/?LinkId=136655)<br /><br /> For automatic notification of these updates, subscribe to the RSS feeds available on the page.  
  
## See Also  
 [SQL Server Integration Services](../sql-server-integration-services.md)  
  
  
