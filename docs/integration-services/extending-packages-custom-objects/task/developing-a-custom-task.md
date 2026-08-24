---
title: "Developing a Custom Task"
description: "Developing a Custom Task"
ms.date: "03/06/2017"
ms.service: sql
ms.subservice: integration-services
ms.topic: "reference"
helpviewer_keywords:
  - "custom tasks [Integration Services], about custom tasks"
  - "Task class"
  - "custom tasks [Integration Services]"
  - "SSIS custom tasks"
  - "SSIS custom tasks, about custom tasks"
  - "IDtsTaskUI interface"
  - "DtsTaskAttribute attribute"
  - "tasks [Integration Services], custom"
  - "TaskHost object"
dev_langs:
  - "VB"
  - "CSharp"
---
# Developing a Custom Task

[!INCLUDE[sqlserver-ssis](../../../includes/applies-to-version/sqlserver-ssis.md)]


  [!INCLUDE[ssISnoversion](../../../includes/ssisnoversion-md.md)] uses tasks to perform units of work in support of the extraction, transformation, and loading of data. [!INCLUDE[ssISnoversion](../../../includes/ssisnoversion-md.md)] includes a variety of tasks that perform the most frequently used actions, from executing a SQL statement to downloading a file from an FTP site. If the included tasks and supported actions do not completely meet your requirements, you can create a custom task.  
  
 To create a custom task, you have to create a class that inherits from the [Microsoft.SqlServer.Dts.Runtime.Task](/dotnet/api/microsoft.sqlserver.dts.runtime.task) base class, apply the <xref:Microsoft.SqlServer.Dts.Runtime.DtsTaskAttribute> attribute to your new class, and override the important methods and properties of the base class, including the <xref:Microsoft.SqlServer.Dts.Runtime.Task.Execute%2A> method.  
  
## In This Section  
 This section describes how to create, configure, and code a custom task and its optional custom user interface.  
  
 [Creating a Custom Task](../../../integration-services/extending-packages-custom-objects/task/creating-a-custom-task.md)  
 Describes the first step, which is creating the custom task.  
  
 [Coding a Custom Task](../../../integration-services/extending-packages-custom-objects/task/coding-a-custom-task.md)  
 Describes how to code the principal methods of a custom task.  
  
 [Connecting to Data Sources in a Custom Task](../../../integration-services/extending-packages-custom-objects/task/connecting-to-data-sources-in-a-custom-task.md)  
 Describes how to connect a custom task to a data source.  
  
 [Raising and Defining Events in a Custom Task](../../../integration-services/extending-packages-custom-objects/task/raising-and-defining-events-in-a-custom-task.md)  
 Describes how to raise events and define custom events from the custom task.  
  
 [Adding Support for Debugging in a Custom Task](../../../integration-services/extending-packages-custom-objects/task/adding-support-for-debugging-in-a-custom-task.md)  
 Describes how to create breakpoint targets in the custom task.  
  
 [Developing a User Interface for a Custom Task](../../../integration-services/extending-packages-custom-objects/task/developing-a-user-interface-for-a-custom-task.md)  
 Describes how to create a user interface that shows in [!INCLUDE[ssIS](../../../includes/ssis-md.md)] Designer to configure properties on the custom task.  
  
## Related content

- [Developing Custom Objects for Integration Services](../developing-custom-objects-for-integration-services.md)
- [Persisting Custom Objects](../persisting-custom-objects.md)
- [Building, Deploying, and Debugging Custom Objects](../building-deploying-and-debugging-custom-objects.md)
- [Developing a Custom Connection Manager](../connection-manager/developing-a-custom-connection-manager.md)
- [Developing a Custom Log Provider](../log-provider/developing-a-custom-log-provider.md)
- [Developing a Custom ForEach Enumerator](../foreach-enumerator/developing-a-custom-foreach-enumerator.md)
- [Developing a Custom Data Flow Component](../data-flow/developing-a-custom-data-flow-component.md)
- [Extending the Package with the Script Task](../../extending-packages-scripting/task/extending-the-package-with-the-script-task.md)
- [Comparing Scripting Solutions and Custom Objects](../../extending-packages-scripting/comparing-scripting-solutions-and-custom-objects.md)
