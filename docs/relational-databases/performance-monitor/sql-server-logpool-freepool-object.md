---
title: "SQL Server, LogPool FreePool object"
description: "Learn about the SQLServer:LogPool FreePool performance object, wich provides counters for statistics for the free pool inside the Log Pool."
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: 12/04/2023
ms.service: sql
ms.subservice: performance
ms.topic: reference
helpviewer_keywords:
  - "SQLServer:LogPool FreePool"
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

## Related content

- [Monitor Resource Usage (Performance Monitor)](monitor-resource-usage-system-monitor.md)
