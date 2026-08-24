---
title: "Developing a Custom Connection Manager"
description: "Developing a Custom Connection Manager"
ms.date: "03/06/2017"
ms.service: sql
ms.subservice: integration-services
ms.topic: "reference"
helpviewer_keywords:
  - "packages [Integration Services], connections"
  - "custom connection managers [Integration Services], about custom connection managers"
  - "connection managers [Integration Services], custom"
  - "Integration Services packages, connection managers"
  - "SSIS packages, connection managers"
  - "SQL Server Integration Services packages, connection managers"
  - "custom connection managers [Integration Services]"
ms.custom: sfi-ropc-nochange
---
# Developing a Custom Connection Manager

[!INCLUDE[sqlserver-ssis](../../../includes/applies-to-version/sqlserver-ssis.md)]


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
  
## Related content

- [Developing Custom Objects for Integration Services](../developing-custom-objects-for-integration-services.md)
- [Persisting Custom Objects](../persisting-custom-objects.md)
- [Building, Deploying, and Debugging Custom Objects](../building-deploying-and-debugging-custom-objects.md)
- [Developing a Custom Task](../task/developing-a-custom-task.md)
- [Developing a Custom Log Provider](../log-provider/developing-a-custom-log-provider.md)
- [Developing a Custom ForEach Enumerator](../foreach-enumerator/developing-a-custom-foreach-enumerator.md)
- [Developing a Custom Data Flow Component](../data-flow/developing-a-custom-data-flow-component.md)
