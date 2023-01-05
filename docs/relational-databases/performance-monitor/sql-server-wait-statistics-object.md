---
title: "SQL Server, Wait Statistics object"
description: Learn about the SQLServer:Wait Statistics performance object, which contains performance counters that report information about wait status.
ms.custom: ""
ms.date: "07/13/2021"
ms.service: sql
ms.reviewer: ""
ms.subservice: performance
ms.topic: conceptual
helpviewer_keywords: 
  - "Wait Statistics object"
  - "SQLServer:Wait Statistics"
author: WilliamDAssafMSFT
ms.author: wiassaf
---
# SQL Server, Wait Statistics object

 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  The **SQLServer:Wait Statistics** performance object contains performance counters that report information about broad categorizations of waits. 
  
 The table below lists the counters that the Wait Statistics object contains.  
  
|SQL Server Wait Statistics counters|Description|  
|-----------------------------------------|-----------------|  
|**Lock waits**|Statistics for processes waiting on a lock.|  
|**Log buffer waits**|Statistics for processes waiting for log buffer to be available.|  
|**Log write waits**|Statistics for processes waiting for log buffer to be written.|  
|**Memory grant queue waits**|Statistics for processes waiting for memory grant to become available.|  
|**Network IO waits**|Statistics relevant to wait on network I/O.|  
|**Non-Page latch waits**|Statistics relevant to non-page latches.|  
|**Page IO latch waits**|Statistics relevant to page I/O latches.|  
|**Page latch waits**|Statistics relevant to page latches, not including I/O latches.|  
|**Thread-safe memory objects waits**|Statistics for processes waiting on thread-safe memory allocators.|  
|**Transaction ownership waits**|Statistics relevant to processes synchronizing access to transaction.|  
|**Wait for the worker**|Statistics relevant to processes waiting for worker to become available.|  
|**Workspace synchronization waits**|Statistics relevant to processes synchronizing access to workspace.|  
  
 Each counter in the object contains the following instances:  
  
|Item|Description|  
|----------|-----------------|  
|**Average wait time (ms)**|Average time for the selected type of wait.|  
|**Cumulative wait time (ms) per second**|Aggregated wait time per second, for the selected type of wait.|  
|**Waits in progress**|Number of processes currently waiting on the following type.|  
|**Waits started per second**|Number of waits started per second of the selected type of wait.|  
  
  
## Example

You begin to explore the query performance counters in this object using this T-SQL query on the [sys.dm_os_performance_counters](../system-dynamic-management-views/sys-dm-os-performance-counters-transact-sql.md) dynamic management view:

```sql
SELECT * FROM sys.dm_os_performance_counters
WHERE object_name LIKE '%Wait Statistics%';
```  

## See also  
 - [sys.dm_os_wait_stats (Transact-SQL)](../../relational-databases/system-dynamic-management-views/sys-dm-os-wait-stats-transact-sql.md) 
 - [Monitor Resource Usage &#40;System Monitor&#41;](../../relational-databases/performance-monitor/monitor-resource-usage-system-monitor.md)  
 - [Monitoring performance by using the Query Store](../../relational-databases/performance/monitoring-performance-by-using-the-query-store.md) 
  
