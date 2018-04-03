---
title: "SMO Object Model | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
helpviewer_keywords: 
  - "object models [SMO]"
  - "SMO [SQL Server], object model"
  - "SQL Server Management Objects, object model"
ms.assetid: bd6e59b6-ca46-42c0-adb2-c9d64cf6e00b
caps.latest.revision: 29
author: "JennieHubbard"
ms.author: "jhubbard"
manager: "jhubbard"
---
# SMO Object Model
  The SMO object model is made up of a hierarchy of objects. The <xref:Microsoft.SqlServer.Management.Smo.Server> object is the top level object and all instance class objects reside under the <xref:Microsoft.SqlServer.Management.Smo.Server> object.  
  
 The <xref:Microsoft.SqlServer.Management.Smo.Wmi.ManagedComputer> class is a top level class with a separate object hierarchy. The <xref:Microsoft.SqlServer.Management.Smo.Wmi.ManagedComputer> object represents [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] services and network settings available through the WMI Provider.  
  
 Besides the <xref:Microsoft.SqlServer.Management.Smo.Server> and <xref:Microsoft.SqlServer.Management.Smo.Wmi.ManagedComputer> objects, there are several utility classes that represent tasks or operations, such as <xref:Microsoft.SqlServer.Management.Smo.Transfer>, <xref:Microsoft.SqlServer.Management.Smo.Backup>, or <xref:Microsoft.SqlServer.Management.Smo.Restore>  
  
 The SMO object model is made up of several namespaces. For more information, see [SMO Namespaces](../../../2014/database-engine/dev-guide/smo-namespaces.md).  
  
## See Also  
 [SMO Object Model Diagram](../../../2014/database-engine/dev-guide/smo-object-model-diagram.md)   
 [SMO Namespaces](../../../2014/database-engine/dev-guide/smo-namespaces.md)   
 [WMI Provider for Configuration Management Concepts](../../../2014/database-engine/dev-guide/wmi-provider-for-configuration-management-concepts.md)  
  
  