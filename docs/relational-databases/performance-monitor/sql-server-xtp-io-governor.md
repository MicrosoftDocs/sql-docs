---
title: "SQL Server XTP IO Governor object"
description: Learn about the SQL Server XTP IO Governor performance object, which contains counters related to the In-Memory OLTP IO Rate Governor.
ms.custom: ""
ms.date: "07/13/2021"
ms.service: sql
ms.reviewer: ""
ms.subservice: performance
ms.topic: conceptual
helpviewer_keywords: 
  - "SQL Server 2016 XTP IO Governor"
  - "SQL Server 2017 XTP IO Governor"
  - "SQL Server XTP IO Governor"
author: WilliamDAssafMSFT
ms.author: wiassaf
---
# SQL Server XTP IO Governor object
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

The **SQL Server XTP IO Governor** performance object contains counters related to the In-Memory OLTP IO Rate Governor.

This table describes the **SQL Server XTP IO Governor** counters.

|Counter|Description|  
|-------------|-----------------|  
|**Insufficient Credits Waits/sec**|Number of waits due to insufficient credits in the rate objects (per second).|
|**Io Issued/sec**|Number of Io issued per second by flush threads.|
|**Log Blocks/sec**|Number of log blocks processed by controller per second.|
|**Missed Credit Slots**|Number of credit slots missed because of wait for credits from rate object.|
|**Stale Rate Object Waits/sec**|Number of waits due to stale rate objects (per second).|
|**Total Rate Objects Published**|Total number of Rate objects published.|
 
## Example

You begin to explore the query performance counters in this object using this T-SQL query on the [sys.dm_os_performance_counters](../system-dynamic-management-views/sys-dm-os-performance-counters-transact-sql.md) dynamic management view:

```sql
SELECT * FROM sys.dm_os_performance_counters
WHERE object_name LIKE '%XTP IO Governor%';
```  

## See also  
- [In-Memory OLTP and Memory-Optimization](../in-memory-oltp/overview-and-usage-scenarios.md)
- [SQL Server XTP &#40;In-Memory OLTP&#41; Performance Counters](../../relational-databases/performance-monitor/sql-server-xtp-in-memory-oltp-performance-counters.md)