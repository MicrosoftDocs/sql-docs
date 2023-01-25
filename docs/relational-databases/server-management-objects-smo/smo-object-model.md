---
description: "SMO Object Model"
title: "SMO Object Model | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: 

ms.topic: "reference"
helpviewer_keywords: 
  - "object models [SMO]"
  - "SMO [SQL Server], object model"
  - "SQL Server Management Objects, object model"
ms.assetid: bd6e59b6-ca46-42c0-adb2-c9d64cf6e00b
author: "markingmyname"
ms.author: "maghan"
monikerRange: "=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# SMO Object Model
[!INCLUDE [SQL Server ASDB, ASDBMI, ASDW ](../../includes/applies-to-version/sql-asdb-asdbmi-asa.md)]

  The SMO object model is made up of a hierarchy of objects. The <xref:Microsoft.SqlServer.Management.Smo.Server> object is the top level object and all instance class objects reside under the <xref:Microsoft.SqlServer.Management.Smo.Server> object.  
  
 The <xref:Microsoft.SqlServer.Management.Smo.Wmi.ManagedComputer> class is a top level class with a separate object hierarchy. The <xref:Microsoft.SqlServer.Management.Smo.Wmi.ManagedComputer> object represents [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] services and network settings available through the WMI Provider.  
  
 Besides the <xref:Microsoft.SqlServer.Management.Smo.Server> and <xref:Microsoft.SqlServer.Management.Smo.Wmi.ManagedComputer> objects, there are several utility classes that represent tasks or operations, such as <xref:Microsoft.SqlServer.Management.Smo.Transfer>, <xref:Microsoft.SqlServer.Management.Smo.Backup>, or <xref:Microsoft.SqlServer.Management.Smo.Restore>  
  
 The SMO object model is made up of several namespaces. For more information, see [SMO Namespaces](../../relational-databases/server-management-objects-smo/smo-object-model-namespaces.md).  
  
## See Also  
 [SMO Object Model Diagram](../../relational-databases/server-management-objects-smo/smo-object-model-diagram.md)   
 [SMO Namespaces](../../relational-databases/server-management-objects-smo/smo-object-model-namespaces.md)   
 [WMI Provider for Configuration Management Concepts](../../relational-databases/wmi-provider-configuration/wmi-provider-for-configuration-management.md)  
  
  
