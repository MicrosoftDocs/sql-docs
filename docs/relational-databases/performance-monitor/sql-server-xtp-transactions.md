---
title: "SQL Server XTP Transactions object"
description: Learn about the SQL Server XTP Transactions performance object, which contains counters related to transactions involving In-Memory OLTP in SQL Server.
ms.custom: ""
ms.date: "07/13/2021"
ms.service: sql
ms.reviewer: ""
ms.subservice: performance
ms.topic: conceptual
helpviewer_keywords: 
  - "SQL Server 2016 XTP Transactions"
  - "SQL Server 2017 XTP Transactions"
  - "SQL Server XTP Transactions"
author: WilliamDAssafMSFT
ms.author: wiassaf
---
# SQL Server XTP Transactions object
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  The **SQL Server XTP Transactions** performance object contains counters related to transactions involving In-Memory OLTP in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
 This table describes the **SQL Server XTP Transactions** counters.  
  
|Counter|Description|  
|-------------|-----------------|  
|**Cascading aborts/sec**|The number of transactions that rolled back to due a commit dependency rollback (on average), per second.|  
|**Commit dependencies taken/sec**|The number of commit dependencies taken by transactions (on average), per second.|  
|**Read-only transactions prepared/sec**|The number of read-only transactions that were prepared for commit processing, per second.|  
|**Save point refreshes/sec**|The number of times a savepoint was "refreshed", (on average), per second. A savepoint refresh is when an existing savepoint is reset to the current point in the transaction's lifetime.|  
|**Save point rollbacks/sec**|The number of times a transaction rolled back to a save point (on average), per second.|  
|**Save points created/sec**|The number of save points created (on average), per second.|  
|**Transaction validation failure/sec**|The number of transactions that failed validation processing (on average), per second.|  
|**Transactions aborted by user/sec**|The number of transactions that were aborted by the user (on average), per second.|  
|**Transactions aborted/sec**|The number of transactions that aborted (both by the user and the system, on average), per second.|  
|**Transactions created/sec**|The number of transactions created in the system (on average), per second.<br /><br /> XTP transactions are counted differently than disk-based transactions (as reflected in Databases:Transactions/sec). For example, Transactions created/sec counts read/only transactions, while Databases:Transactions/sec does not.|  
  
 
## Example

You begin to explore the query performance counters in this object using this T-SQL query on the [sys.dm_os_performance_counters](../system-dynamic-management-views/sys-dm-os-performance-counters-transact-sql.md) dynamic management view:

```sql
SELECT * FROM sys.dm_os_performance_counters
WHERE object_name LIKE '%XTP Transactions%';
``` 

## See also  
- [In-Memory OLTP and Memory-Optimization](../in-memory-oltp/overview-and-usage-scenarios.md)
- [Databases Object](sql-server-databases-object.md)
- [SQL Server XTP &#40;In-Memory OLTP&#41; Performance Counters](../../relational-databases/performance-monitor/sql-server-xtp-in-memory-oltp-performance-counters.md)
  
