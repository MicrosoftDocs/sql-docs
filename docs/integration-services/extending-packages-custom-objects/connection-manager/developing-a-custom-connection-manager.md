---
title: "Developing a Custom Connection Manager | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: "reference"
helpviewer_keywords: 
  - "packages [Integration Services], connections"
  - "custom connection managers [Integration Services], about custom connection managers"
  - "connection managers [Integration Services], custom"
  - "Integration Services packages, connection managers"
  - "SSIS packages, connection managers"
  - "SQL Server Integration Services packages, connection managers"
  - "custom connection managers [Integration Services]"
ms.assetid: bda0b29e-57f5-4879-b04d-1396dc56daa8
author: janinezhang
ms.author: janinez
manager: craigg
---
# Developing a Custom Connection Manager
  [!INCLUDE[ssISnoversion](../../../includes/ssisnoversion-md.md)] uses connection managers to encapsulate the information needed to connect to an external data source. [!INCLUDE[ssISnoversion](../../../includes/ssisnoversion-md.md)] includes a variety of connection managers that support connections to the most commonly used data sources, from enterprise databases to text files and Excel worksheets. If the connection managers and external data sources supported by [!INCLUDE[ssISnoversion](../../../includes/ssisnoversion-md.md)] do not entirely meet your requirements, you can create a custom connection manager.  
  
 To create a custom connection manager, you have to create a class that inherits from the <xref:Microsoft.SqlServer.Dts.Runtime.ConnectionManagerBase> base class, apply the <xref:Microsoft.SqlServer.Dts.Runtime.DtsConnectionAttribute> attribute to your new class, and override the important methods and properties of the base class, including the <xref:Microsoft.SqlServer.Dts.Runtime.ConnectionManagerBase.ConnectionString%2A> property and the <xref:Microsoft.SqlServer.Dts.Runtime.ConnectionManagerBase.AcquireConnection%2A> method.  
  
> [!IMPORTANT]  
>  Most of the tasks, sources, and destinations that have been built into [!INCLUDE[ssISnoversion](../../../includes/ssisnoversion-md.md)] work only with specific types of built-in connection managers. Before developing a custom connection manager for use with built-in tasks and components, check whether those components restrict the list of available connection managers to those of a specific type. If your solution requires a custom connection manager, you might also have to develop a custom task, or a custom source or destination, for use with the connection manager.  
  
## In This Section  
 This section describes how to create, configure, and code a custom connection manager and its optional custom user interface. The code snippets shown in this section are drawn from the Sql Server Custom Connection Manager Sample.  
  
 [Creating a Custom Connection Manager](../../../integration-services/extending-packages-custom-objects/connection-manager/creating-a-custom-connection-manager.md)  
 Describes how to create the classes for a custom connection manager project.  
  
 [Coding a Custom Connection Manager](../../../integration-services/extending-packages-custom-objects/connection-manager/coding-a-custom-connection-manager.md)  
 Describes how to implement a custom connection manager by overriding the methods and properties of the base class.  
  
 [Developing a User Interface for a Custom Connection Manager](../../../integration-services/extending-packages-custom-objects/connection-manager/developing-a-user-interface-for-a-custom-connection-manager.md)  
 Describes how to implement the user interface class and the form that is used to configure the custom connection manager.  
  
## Related Sections  
  
### Information Common to all Custom Objects  
 For information that is common to all the type of custom objects that you can create in [!INCLUDE[ssISnoversion](../../../includes/ssisnoversion-md.md)], see the following topics:  
  
 [Developing Custom Objects for Integration Services](../../../integration-services/extending-packages-custom-objects/developing-custom-objects-for-integration-services.md)  
 Describes the basic steps in implementing all types of custom objects for [!INCLUDE[ssISnoversion](../../../includes/ssisnoversion-md.md)].  
  
 [Persisting Custom Objects](../../../integration-services/extending-packages-custom-objects/persisting-custom-objects.md)  
 Describes custom persistence and explains when it is necessary.  
  
 [Building, Deploying, and Debugging Custom Objects](../../../integration-services/extending-packages-custom-objects/building-deploying-and-debugging-custom-objects.md)  
 Describes the techniques for building, signing, deploying, and debugging custom objects.  
  
### Information about Other Custom Objects  
 For information on the other types of custom objects that you can create in [!INCLUDE[ssISnoversion](../../../includes/ssisnoversion-md.md)], see the following topics:  
  
 [Developing a Custom Task](../../../integration-services/extending-packages-custom-objects/task/developing-a-custom-task.md)  
 Discusses how to program custom tasks.  
  
 [Developing a Custom Log Provider](../../../integration-services/extending-packages-custom-objects/log-provider/developing-a-custom-log-provider.md)  
 Discusses how to program custom log providers.  
  
 [Developing a Custom ForEach Enumerator](../../../integration-services/extending-packages-custom-objects/foreach-enumerator/developing-a-custom-foreach-enumerator.md)  
 Discusses how to program custom enumerators.  
  
 [Developing a Custom Data Flow Component](../../../integration-services/extending-packages-custom-objects/data-flow/developing-a-custom-data-flow-component.md)  
 Discusses how to program custom data flow sources, transformations, and destinations.  
  
