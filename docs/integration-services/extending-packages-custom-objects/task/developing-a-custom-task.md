---
description: "Developing a Custom Task"
title: "Developing a Custom Task | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: integration-services
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
author: chugugrace
ms.author: chugu
---
# Developing a Custom Task

[!INCLUDE[sqlserver-ssis](../../../includes/applies-to-version/sqlserver-ssis.md)]


  [!INCLUDE[ssISnoversion](../../../includes/ssisnoversion-md.md)] uses tasks to perform units of work in support of the extraction, transformation, and loading of data. [!INCLUDE[ssISnoversion](../../../includes/ssisnoversion-md.md)] includes a variety of tasks that perform the most frequently used actions, from executing an SQL statement to downloading a file from an FTP site. If the included tasks and supported actions do not completely meet your requirements, you can create a custom task.  
  
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
  
## Related Sections  
  
### Information Common to all Custom Objects  
 For information that is common to all the type of custom objects that you can create in [!INCLUDE[ssISnoversion](../../../includes/ssisnoversion-md.md)], see the following topics:  
  
 [Developing Custom Objects for Integration Services](../../../integration-services/extending-packages-custom-objects/developing-custom-objects-for-integration-services.md)  
 Describes the basic steps in implementing all kinds of custom objects for [!INCLUDE[ssISnoversion](../../../includes/ssisnoversion-md.md)].  
  
 [Persisting Custom Objects](../../../integration-services/extending-packages-custom-objects/persisting-custom-objects.md)  
 Describes custom persistence and explains when it is necessary.  
  
 [Building, Deploying, and Debugging Custom Objects](../../../integration-services/extending-packages-custom-objects/building-deploying-and-debugging-custom-objects.md)  
 Describes the techniques for building, signing, deploying, and debugging custom objects.  
  
### Information about Other Custom Objects  
 For information about the other types of custom objects that you can create in [!INCLUDE[ssISnoversion](../../../includes/ssisnoversion-md.md)], see the following topics:  
  
 [Developing a Custom Connection Manager](../../../integration-services/extending-packages-custom-objects/connection-manager/developing-a-custom-connection-manager.md)  
 Discusses how to program custom connection managers.  
  
 [Developing a Custom Log Provider](../../../integration-services/extending-packages-custom-objects/log-provider/developing-a-custom-log-provider.md)  
 Discusses how to program custom log providers.  
  
 [Developing a Custom ForEach Enumerator](../../../integration-services/extending-packages-custom-objects/foreach-enumerator/developing-a-custom-foreach-enumerator.md)  
 Discusses how to program custom enumerators.  
  
 [Developing a Custom Data Flow Component](../../../integration-services/extending-packages-custom-objects/data-flow/developing-a-custom-data-flow-component.md)  
 Discusses how to program custom data flow sources, transformations, and destinations.  
  
## See Also  
 [Extending the Package with the Script Task](../../../integration-services/extending-packages-scripting/task/extending-the-package-with-the-script-task.md)   
 [Comparing Scripting Solutions and Custom Objects](../../../integration-services/extending-packages-scripting/comparing-scripting-solutions-and-custom-objects.md)  
  
  
