---
title: "SQL Server, CLR object"
description: Learn about the SQLServer:CLR object, which provides counters to monitor common language runtime execution in Microsoft SQL Server.
ms.custom: ""
ms.date: "07/12/2021"
ms.service: sql
ms.reviewer: ""
ms.subservice: performance
ms.topic: conceptual
helpviewer_keywords: 
  - "SQLServer:CLR"
  - "CLR object [SQL Server]"
author: WilliamDAssafMSFT
ms.author: wiassaf
---
# SQL Server, CLR object
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  The **SQLServer:CLR** object provides counters to monitor common language runtime (CLR) execution in [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
 The following table describes the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] **CLR** counters.  
  
|**CLR** counters|Description|  
|------------------|-----------------|  
|**CLR Execution**|Total execution time in CLR (microseconds)|  
  
## Example

You begin to explore the query performance counters in this object using this T-SQL query on the [sys.dm_os_performance_counters](../system-dynamic-management-views/sys-dm-os-performance-counters-transact-sql.md) dynamic management view:

```sql
SELECT * FROM sys.dm_os_performance_counters
WHERE object_name LIKE '%CLR%';
```  

## See also  
 [Monitor Resource Usage &#40;System Monitor&#41;](../../relational-databases/performance-monitor/monitor-resource-usage-system-monitor.md)  
  
  
