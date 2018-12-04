---
title: "SMO Object Model | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
ms.topic: "reference"
helpviewer_keywords: 
  - "object models [SMO]"
  - "SMO [SQL Server], object model"
  - "SQL Server Management Objects, object model"
ms.assetid: bd6e59b6-ca46-42c0-adb2-c9d64cf6e00b
author: stevestein
ms.author: sstein
manager: craigg
---
# SMO Object Model
  The SMO object model is made up of a hierarchy of objects. The <xref:Microsoft.SqlServer.Management.Smo.Server> object is the top level object and all instance class objects reside under the <xref:Microsoft.SqlServer.Management.Smo.Server> object.  
  
 The <xref:Microsoft.SqlServer.Management.Smo.Wmi.ManagedComputer> class is a top level class with a separate object hierarchy. The <xref:Microsoft.SqlServer.Management.Smo.Wmi.ManagedComputer> object represents [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] services and network settings available through the WMI Provider.  
  
 Besides the <xref:Microsoft.SqlServer.Management.Smo.Server> and <xref:Microsoft.SqlServer.Management.Smo.Wmi.ManagedComputer> objects, there are several utility classes that represent tasks or operations, such as <xref:Microsoft.SqlServer.Management.Smo.Transfer>, <xref:Microsoft.SqlServer.Management.Smo.Backup>, or <xref:Microsoft.SqlServer.Management.Smo.Restore>  
  
 The SMO object model is made up of several namespaces. For more information, see [SMO Namespaces](smo-object-model-namespaces.md).  
  
## See Also  
 [SMO Object Model Diagram](smo-object-model-diagram.md)   
 [SMO Namespaces](smo-object-model-namespaces.md)   
 [WMI Provider for Configuration Management Concepts](../wmi-provider-configuration/wmi-provider-for-configuration-management.md)  
  
  
