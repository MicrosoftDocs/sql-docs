---
title: "SQL Server Compact Edition Connection Manager Editor (All Page) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "integration-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.dts.designer.sqlmobileconnection.all.f1"
helpviewer_keywords: 
  - "SQL Server Compact Connection Manager Editor"
ms.assetid: f9fbff4b-c502-44b3-8e7b-398d66e82206
author: janinezhang
ms.author: janinez
manager: craigg
---
# SQL Server Compact Edition Connection Manager Editor (All Page)
  Use the **SQL Server Compact Edition Connection Manager** dialog box to specify properties for connecting to a [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Compact database.  
  
 To learn more about the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Compact Edition connection manager, see [SQL Server Compact Edition Connection Manager](connection-manager/sql-server-compact-edition-connection-manager.md).  
  
## Options  
 **AutoShrink Threshold**  
 Specify the amount of free space, as a percentage, that is allowed in the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Compact database before the autoshrink process runs.  
  
 **Default Lock Escalation**  
 Specify the number of database locks that the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Compact database acquires before it tries to escalate locks.  
  
 **Default Lock Timeout**  
 Specify the default interval, in milliseconds, that a transaction will wait for a lock.  
  
 **Flush Interval**  
 Specify the interval, in seconds, between committed transactions to flush data to disk.  
  
 **Locale Identifier**  
 Specify the Locale ID (LCID) of the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Compact database.  
  
 **Max Buffer Size**  
 Specify the maximum amount of memory, in kilobytes, that [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Compact uses before flushing data to disk.  
  
 **Max Database Size**  
 Specify the maximum size, in megabytes, of the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Compact database.  
  
 **Mode**  
 Specify the file mode in which to open the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Compact database. The default value for this property is **Read Write**.  
  
 The Mode option has four values, as described in the following table.  
  
|Value|Description|  
|-----------|-----------------|  
|**Read Only**|Specifies read-only access to the database.|  
|**Read Write**|Specifies read/write permission to the database.|  
|**Exclusive**|Specifies exclusive access to the database.|  
|**Shared Read**|Specifies that other users can read from the database at the same time.|  
  
 **Persist Security Info**  
 Specify whether security information is returned as part of the connection string. The default value for this option is **False**.  
  
 **Temp File Directory**  
 Specify the location of the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Compact temporary database file.  
  
 **Data Source**  
 Specify the name of the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Compact database.  
  
 **Password**  
 Enter the password for the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Compact database.  
  
## See Also  
 [Integration Services Error and Message Reference](../../2014/integration-services/integration-services-error-and-message-reference.md)   
 [SQL Server Compact Edition Connection Manager Editor &#40;Connection Page&#41;](../../2014/integration-services/sql-server-compact-edition-connection-manager-editor-connection-page.md)  
  
  
