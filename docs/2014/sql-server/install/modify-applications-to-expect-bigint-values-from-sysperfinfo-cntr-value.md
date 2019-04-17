---
title: "Modify applications to expect bigint values from sysperfinfo.cntr_value | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
helpviewer_keywords: 
  - "sysperfinfo"
  - "bigint values [SQL Server]"
ms.assetid: b0345303-6e9a-4078-8148-6e1bce207b8c
author: mashamsft
ms.author: mathoma
manager: craigg
---
# Modify applications to expect bigint values from sysperfinfo.cntr_value
  sysperfinfo returns a `bigint` value for the cntr_value column.  
  
## Component  
 [!INCLUDE[ssDE](../../includes/ssde-md.md)]  
  
## Corrective Action  
 Modify applications that use sysperfinfo to ensure that they can handle the `bigint` values of the cntr_value column.  
  
> [!NOTE]  
>  sysperfinfo is a compatibility view. You should use the sys.dm_os_performance_counter dynamic management view instead.  
  
## See Also  
 [Database Engine Upgrade Issues](../../../2014/sql-server/install/database-engine-upgrade-issues.md)   
 [SQL Server 2014 Upgrade Advisor &#91;new&#93;](sql-server-2014-upgrade-advisor.md)  
  
  
