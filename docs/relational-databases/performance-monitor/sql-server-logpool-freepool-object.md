---
title: "SQL Server, LogPool FreePool object"
description: Learn about the SQLServer:LogPool FreePool performance object, wich provides counters for statistics for the free pool inside the Log Pool.
ms.custom: ""
ms.date: "07/13/2021"
ms.service: sql
ms.reviewer: ""
ms.subservice: performance
ms.topic: conceptual
helpviewer_keywords: 
  - "SQLServer:LogPool FreePool"
author: WilliamDAssafMSFT
ms.author: wiassaf
---
# SQL Server, LogPool FreePool object
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
The **SQLServer:LogPool FreePool** performance object provides counters for statistics for the free pool inside the Log Pool.

This following table describes the SQL Server **LogPool FreePool** performance objects.

|**SQL Server LogPool FreePool counters**|Description|  
|-------------|-----------------|  
|**Free Buffer Refills/sec**|Number of buffers being allocated for refill, per second.|
|**Free List Length**|Length of the free list.|

There is one instance of the counter for each category of log pool.

  
## Example

You begin to explore the query performance counters in this object using this T-SQL query on the [sys.dm_os_performance_counters](../system-dynamic-management-views/sys-dm-os-performance-counters-transact-sql.md) dynamic management view:

```sql
SELECT * FROM sys.dm_os_performance_counters
WHERE object_name LIKE '%LogPool FreePool%';
```  

## See also  
[Monitor Resource Usage (System Monitor)](../../relational-databases/performance-monitor/monitor-resource-usage-system-monitor.md)

