---
title: "Changes to behavior in syslockinfo and sp_lock | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
helpviewer_keywords: 
  - "syslockinfo"
  - "sp_lock"
ms.assetid: b9892ae3-ac15-48be-8b52-78dbed6467ed
author: mashamsft
ms.author: mathoma
manager: craigg
---
# Changes to behavior in syslockinfo and sp_lock
  **syslockinfo** and **sp_lock** may return unexpected values. They may also return additional rows, whereas previous versions of **syslockinfo** and **sp_lock** returned a maximum of two rows per lock resource.  
  
 To access information from **syslockinfo** or execute **sp_lock** requires VIEW SERVER STATE permission on the server.  
  
## Component  
 [!INCLUDE[ssDE](../../includes/ssde-md.md)]  
  
## Description  
 In [!INCLUDE[ssVersion2000](../../includes/ssversion2000-md.md)], the **rsc_objid** and **rsc_indid** columns in **syslockinfo** and the **objid** and **indid** columns in **sp_lock** consistently return the object ID and index ID. In [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)], a value of 0 may be returned.  
  
 In [!INCLUDE[ssVersion2000](../../includes/ssversion2000-md.md)], **syslockinfo** and **sp_lock** return a maximum of two rows for any given lock resource in a single transaction. Starting with [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)], when lock partitioning is enabled, multiple rows for the same resource running under one transaction may be returned. There may be up to N + 1 rows returned, where N is the number of CPUs. Also, it is now possible to have GRANTED and WAITING requests displayed for the same resource, which was not possible in [!INCLUDE[ssVersion2000](../../includes/ssversion2000-md.md)].  
  
## Permissions  
 Requires VIEW SERVER STATE permission on the server.  
  
## See Also  
 [Database Engine Upgrade Issues](../../../2014/sql-server/install/database-engine-upgrade-issues.md)   
 [SQL Server 2014 Upgrade Advisor &#91;new&#93;](sql-server-2014-upgrade-advisor.md)  
  
  
