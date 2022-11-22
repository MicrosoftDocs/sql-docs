---
title: "SQL Server XTP Garbage Collection object"
description: Learn about the SQL Server XTP Garbage Collection performance object, which contains counters related to the In-Memory OLTP engine's garbage collector.
ms.custom: ""
ms.date: "07/13/2021"
ms.service: sql
ms.reviewer: ""
ms.subservice: performance
ms.topic: conceptual
helpviewer_keywords: 
  - "SQL Server 2016 XTP Garbage Collection"
  - "SQL Server 2017 XTP Garbage Collection"
  - "SQL Server XTP Garbage Collection"
author: WilliamDAssafMSFT
ms.author: wiassaf
---
# SQL Server XTP Garbage Collection object
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  The **SQL Server XTP Garbage Collection** performance object contains counters related to the In-Memory OLTP engine's garbage collector.  
  
 This table describes the **SQL Server XTP Garbage Collection** counters.  
  
|Counter|Description|  
|-------------|-----------------|  
|**Dusty corner scan retries/sec (GC-issued)**|The number of scan retries due to write conflicts during dusty corner sweeps issued by the garbage collector (on average), per second. This is a very low-level counter, not intended for customer use.|  
|**Main GC work items/sec**|The number of work items processed by the main GC thread.|  
|**Parallel GC work item/sec**|The number of times a parallel thread has executed a GC work item.|  
|**Rows processed/sec**|The number of rows processed by the garbage collector (on average), per second.|  
|**Rows processed/sec (first in bucket and removed)**|The number of rows processed by the garbage collector that were first in the corresponding hash bucket, and were able to be removed immediately (on average), per second.|  
|**Rows processed/sec (first in bucket)**|The number of rows processed by the garbage collector that were first in the corresponding hash bucket (on average), per second.|  
|**Rows processed/sec (marked for unlink)**|The number of rows processed by the garbage collector that were already marked for unlink (on average), per second.|  
|**Rows processed/sec (no sweep needed)**|The number of rows processed by the garbage collector that will not require a dusty corner sweep (on average), per second.|  
|**Sweep expired rows removed/sec**|The number of expired rows removed during dusty corner sweeps (on average), per second.|  
|**Sweep expired rows touched/sec**|The number of expired rows touched during dusty corner sweeps (on average), per second.|  
|**Sweep expiring rows touched/sec**|The number of expiring rows touched during dusty corner sweeps (on average), per second.|  
|**Sweep rows touched/sec**|The number of rows touched during dusty corner sweeps (on average), per second.|  
|**Sweep scans started/sec**|The number of dusty corner sweep scans started (on average), per second.|  
  
  
## Example

You begin to explore the query performance counters in this object using this T-SQL query on the [sys.dm_os_performance_counters](../system-dynamic-management-views/sys-dm-os-performance-counters-transact-sql.md) dynamic management view:

```sql
SELECT * FROM sys.dm_os_performance_counters
WHERE object_name LIKE '%XTP Garbage Collection%';
```  

## See also  
 - [In-Memory OLTP and Memory-Optimization](../in-memory-oltp/overview-and-usage-scenarios.md)
 - [SQL Server XTP &#40;In-Memory OLTP&#41; Performance Counters](../../relational-databases/performance-monitor/sql-server-xtp-in-memory-oltp-performance-counters.md)  
  
