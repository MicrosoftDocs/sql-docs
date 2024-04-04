---
title: "SQL Server, Memory Broker Clerks object"
description: "Learn about the SQLServer:Memory Broker Clerks performance object, which provides counters for statistics related to memory broker clerks."
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: 12/04/2023
ms.service: sql
ms.subservice: performance
ms.topic: reference
helpviewer_keywords:
  - "SQLServer:Memory Broker Clerks"
---
# SQL Server, Memory Broker Clerks object
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
The **SQLServer:Memory Broker Clerks** performance object provides counters for statistics related to memory broker clerks.

This following table describes the SQL Server **Memory Broker Clerks** performance objects.

|**SQL Server Memory Broker Clerks counters**|Description|  
|-------------|-----------------|  
|**Internal benefit**|The internal value of memory for entry count pressure, in ms per page per ms, multiplied by 10 billion and truncated to an integer.|
|**Memory broker clerk size**|The size of the clerk, in pages.|
|**Periodic evictions (pages)**|The number of pages evicted from the broker clerk by last periodic eviction.|
|**Pressure evictions (pages/sec)**|The number of pages per second evicted from the broker clerk by memory pressure.|
|**Simulation benefit**|The value of memory to the clerk, in ms per page per ms, multiplied by 10 billion and truncated to an integer.|
|**Simulation size**|The current size of the clerk simulation, in pages.|

There is an instance of the counter for the buffer pool, and the columnstore object pool.

## Example

You begin to explore the query performance counters in this object using this T-SQL query on the [sys.dm_os_performance_counters](../system-dynamic-management-views/sys-dm-os-performance-counters-transact-sql.md) dynamic management view:

```sql
SELECT * FROM sys.dm_os_performance_counters
WHERE object_name LIKE '%Memory Broker Clerks%';
```  

## Related content

- [Monitor Resource Usage (Performance Monitor)](monitor-resource-usage-system-monitor.md)
