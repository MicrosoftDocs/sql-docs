---
title: "Developing a Custom Task | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: "reference"
dev_langs: 
  - "VB"
  - "CSharp"
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
ms.assetid: dcbd8615-fa6d-4ddb-b8a5-0b19dddd6239
author: janinezhang
ms.author: janinez
manager: craigg
---
# Developing a Custom Task
  [!INCLUDE[ssISnoversion](../../../includes/ssisnoversion-md.md)] uses tasks to perform units of work in support of the extraction, transformation, and loading of data. [!INCLUDE[ssISnoversion](../../../includes/ssisnoversion-md.md)] includes a variety of tasks that perform the most frequently used actions, from executing an SQL statement to downloading a file from an FTP site. If the included tasks and supported actions do not completely meet your requirements, you can create a custom task.  
  
 To create a custom task, you have to create a class that inherits from the <xref:Microsoft.SqlServer.Dts.Runtime.Task> base class, apply the <xref:Microsoft.SqlServer.Dts.Runtime.DtsTaskAttribute> attribute to your new class, and override the important methods and properties of the base class, including the <xref:Microsoft.SqlServer.Dts.Runtime.Task.Execute%2A> method.  
  
## In This Section  
 This section describes how to create, configure, and code a custom task and its optional custom user interface.  
  
 [Creating a Custom Task](creating-a-custom-task.md)  
 Describes the first step, which is creating the custom task.  
  
 [Coding a Custom Task](coding-a-custom-task.md)  
 Describes how to code the principal methods of a custom task.  
  
 [Connecting to Data Sources in a Custom Task](connecting-to-data-sources-in-a-custom-task.md)  
 Describes how to connect a custom task to a data source.  
  
 [Raising and Defining Events in a Custom Task](raising-and-defining-events-in-a-custom-task.md)  
 Describes how to raise events and define custom events from the custom task.  
  
 [Adding Support for Debugging in a Custom Task](adding-support-for-debugging-in-a-custom-task.md)  
 Describes how to create breakpoint targets in the custom task.  
  
 [Developing a User Interface for a Custom Task](developing-a-user-interface-for-a-custom-task.md)  
 Describes how to create a user interface that shows in [!INCLUDE[ssIS](../../../includes/ssis-md.md)] Designer to configure properties on the custom task.  
  
## Related Sections  
  
### Information Common to all Custom Objects  
 For information that is common to all the type of custom objects that you can create in [!INCLUDE[ssISnoversion](../../../includes/ssisnoversion-md.md)], see the following topics:  
  
 [Developing Custom Objects for Integration Services](../developing-custom-objects-for-integration-services.md)  
 Describes the basic steps in implementing all kinds of custom objects for [!INCLUDE[ssISnoversion](../../../includes/ssisnoversion-md.md)].  
  
 [Persisting Custom Objects](../persisting-custom-objects.md)  
 Describes custom persistence and explains when it is necessary.  
  
 [Building, Deploying, and Debugging Custom Objects](../building-deploying-and-debugging-custom-objects.md)  
 Describes the techniques for building, signing, deploying, and debugging custom objects.  
  
### Information about Other Custom Objects  
 For information about the other types of custom objects that you can create in [!INCLUDE[ssISnoversion](../../../includes/ssisnoversion-md.md)], see the following topics:  
  
 [Developing a Custom Connection Manager](../connection-manager/developing-a-custom-connection-manager.md)  
 Discusses how to program custom connection managers.  
  
 [Developing a Custom Log Provider](../log-provider/developing-a-custom-log-provider.md)  
 Discusses how to program custom log providers.  
  
 [Developing a Custom ForEach Enumerator](../foreach-enumerator/developing-a-custom-foreach-enumerator.md)  
 Discusses how to program custom enumerators.  
  
 [Developing a Custom Data Flow Component](../data-flow/developing-a-custom-data-flow-component.md)  
 Discusses how to program custom data flow sources, transformations, and destinations.  
  
![Integration Services icon (small)](../../media/dts-16.gif "Integration Services icon (small)")  **Stay Up to Date with Integration Services**<br /> For the latest downloads, articles, samples, and videos from Microsoft, as well as selected solutions from the community, visit the [!INCLUDE[ssISnoversion](../../../includes/ssisnoversion-md.md)] page on MSDN:<br /><br /> [Visit the Integration Services page on MSDN](https://go.microsoft.com/fwlink/?LinkId=136655)<br /><br /> For automatic notification of these updates, subscribe to the RSS feeds available on the page.  
  
## See Also  
 [Extending the Package with the Script Task](../../extending-packages-scripting/task/extending-the-package-with-the-script-task.md)   
 [Comparing Scripting Solutions and Custom Objects](../../extending-packages-scripting/comparing-scripting-solutions-and-custom-objects.md)  
  
  
