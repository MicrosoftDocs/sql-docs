---
title: "SQL Server XTP Cursors object"
description: Learn about the SQL Server XTP Cursors performance object, which contains counters related to internal In-Memory OLTP engine cursors. 
ms.custom: ""
ms.date: "07/13/2021"
ms.service: sql
ms.reviewer: ""
ms.subservice: performance
ms.topic: conceptual
helpviewer_keywords: 
  - "SQL Server 2016 XTP Cursors"
  - "SQL Server 2017 XTP Cursors"
  - "SQL Server XTP Cursors"
author: WilliamDAssafMSFT
ms.author: wiassaf
---
# SQL Server XTP Cursors object
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  The **SQL Server XTP Cursors** performance object contains counters related to internal In-Memory OLTP engine cursors. Cursors are the low-level building blocks the In-Memory OLTP engine uses to process [!INCLUDE[tsql](../../includes/tsql-md.md)] queries. As such, you do not typically have direct control over them.  
  
 This table describes the **SQL Server XTP Cursors** counters.  
  
|Counter|Description|  
|-------------|-----------------|  
|**Cursor deletes/sec**|The number of cursor deletes (on average), per second.|  
|**Cursor inserts/sec**|The number of cursor inserts (on average), per second.|  
|**Cursor scans started /sec**|The number of cursor scans started (on average), per second.|  
|**Cursor unique violations/sec**|The number of unique-constraint violations (on average), per second.|  
|**Cursor updates/sec**|The number of cursor updates (on average), per second.|  
|**Cursor write conflicts/sec**|The number of write-write conflicts to the same row version (on average), per second.|  
|**Dusty corner scan retries/sec (user-issued)**|The number of scan retries due to write conflicts during dusty corner sweeps issued by a user's full-table scan (on average), per second. This is a very low-level counter, not intended for customer use.|  
|**Expired rows removed/sec**|The number of expired rows removed by cursors (on average), per second.|  
|**Expired rows touched/sec**|The number of expired rows touched by cursors (on average), per second.|  
|**Rows returned/sec**|The number of rows returned by cursors (on average), per second.|  
|**Rows touched/sec**|The number of rows touched by cursors (on average), per second.|  
|**Tentatively-deleted rows touched/sec**|The number of expiring rows touched by cursors (on average), per second. A row is expiring if the transaction that deleted it is still active (i.e. has not yet committed or aborted.)|  

  
## Example

You begin to explore the query performance counters in this object using this T-SQL query on the [sys.dm_os_performance_counters](../system-dynamic-management-views/sys-dm-os-performance-counters-transact-sql.md) dynamic management view:

```sql
SELECT * FROM sys.dm_os_performance_counters
WHERE object_name LIKE '%XTP Cursors%';
```  
  
## See also  
 - [In-Memory OLTP and Memory-Optimization](../in-memory-oltp/overview-and-usage-scenarios.md)
 - [SQL Server XTP &#40;In-Memory OLTP&#41; Performance Counters](../../relational-databases/performance-monitor/sql-server-xtp-in-memory-oltp-performance-counters.md)  
  
