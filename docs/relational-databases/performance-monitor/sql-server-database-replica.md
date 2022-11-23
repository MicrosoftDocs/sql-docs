---
title: "SQL Server, Database Replica object"
description: Learn about the SQLServer:Database Replica performance object, which contains performance counters about secondary databases of an Always On availability group.
ms.custom: ""
ms.date: "07/12/2021"
ms.service: sql
ms.reviewer: ""
ms.subservice: performance
ms.topic: conceptual
helpviewer_keywords: 
  - "Availability Groups [SQL Server], monitoring"
  - "SQLServer:Database Replica"
  - "performance counters [SQL Server], AlwaysOn Availability Groups"
  - "Availability Groups [SQL Server], performance counters"
author: WilliamDAssafMSFT
ms.author: wiassaf
---
# SQL Server, Database Replica object

 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  The **SQLServer:Database Replica** performance object contains performance counters that report information about the secondary databases of an Always On availability group in [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)]. This object is valid only on an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] that hosts a secondary replica.  
  
|Counter Name|Description|View on...|  
|------------------|-----------------|--------------|  
|**Database Flow Control Delay**|Duration spent in database flow control wait in microseconds. **Applies to**: SQL Server 2014 Service Pack 2 and later, SQL Server 2016 Service Pack 1 and later||
|**Database Flow Controls/sec**|The number of database flow control per sec. **Applies to**: SQL Server 2014 Service Pack 2 and later, SQL Server 2016 Service Pack 1 and later||
|**File Bytes Received/sec**|Amount of FILESTREAM data received by the secondary replica for the secondary database in the last second.|Secondary replica|  
|**Group Commit Time**| Number of microseconds all transactions group commit waited. **Applies to**: SQL Server 2014 Service Pack 2 and later, SQL Server 2016 Service Pack 1 and later||
|**Group Commits/Sec**| Number of times transactions waited for group commit. **Applies to**: SQL Server 2014 Service Pack 2 and later, SQL Server 2016 Service Pack 1 and later||
|**Log Apply Pending Queue**|Number of log blocks that are waiting to be applied to the database replica.|Secondary replica|
|**Log Apply Ready Queue**|Number of log blocks that are waiting and ready to be applied to the database replica.|Secondary replica|
|**Log Bytes Compressed/sec**| The amount of log in bytes compressed per sec. **Applies to**: SQL Server 2014 Service Pack 2 and later, SQL Server 2016 Service Pack 1 and later||
|**Log Bytes Decompressed/sec**| The amount of log in bytes decompressed per sec. **Applies to**: SQL Server 2014 Service Pack 2 and later, SQL Server 2016 Service Pack 1 and later||
|**Log Bytes Received/sec**|Amount of log records received by the secondary replica for the database in the last second.|Secondary replica|  
|**Log Compression Cache hits/sec**| The number of log block compression hits per sec. **Applies to**: SQL Server 2014 Service Pack 2 and later, SQL Server 2016 Service Pack 1 and later||
|**Log Compression Cache misses/sec**| The number of log block compression cache misses per sec. **Applies to**: SQL Server 2014 Service Pack 2 and later, SQL Server 2016 Service Pack 1 and later||
|**Log Compressions/sec**| The number of log blocks compressed per sec. **Applies to**: SQL Server 2014 Service Pack 2 and later, SQL Server 2016 Service Pack 1 and later||
|**Log Decompressions/sec**| The number of log blocks decompressed per sec. **Applies to**: SQL Server 2014 Service Pack 2 and later, SQL Server 2016 Service Pack 1 and later||
|**Log remaining for undo**|The amount of log, in kilobytes, remaining to complete the undo phase.|Secondary replica|  
|**Log Send Queue**|Amount of log records in the log files of the primary database, in kilobytes, that haven't been sent to the secondary replica. This value is sent to the secondary replica from the primary replica. Queue size doesn't include FILESTREAM files that are sent to a secondary.|Secondary replica|  
|**Mirrored Write Transaction/sec**|Number of transactions that were written to the primary database and then waited to commit until the log was sent to the secondary database, in the last second.|Primary replica|  
|**Recovery Queue**|Amount of log records in kilobytes in the log files of the secondary replica that have not been redone.|Secondary replica|  
|**Redo blocked/sec**|Number of times the redo thread was blocked on locks held by readers of the database.|Secondary replica|  
|**Redo Bytes Remaining**|The amount of log, in kilobytes, remaining to be redone to finish the reverting phase.|Secondary replica|  
|**Redone Bytes/sec**|Amount of log records redone on the secondary database in the last second.|Secondary replica|  
|**Redones/sec**| Amount of log records redone in the last second to catch up the database replica|Secondary replica|
|**Total Log requiring undo**|Total kilobytes of log that must be undone.|Secondary replica|  
|**Transaction Delay**|Total delay in waiting for unterminated commit acknowledgment for all the current transactions, in milliseconds. Divide by *Mirrored Write Transaction/sec* to get *Avg Transaction Delay*. For more information, see [Monitor performance for Always On availability groups](../../database-engine/availability-groups/windows/monitor-performance-for-always-on-availability-groups.md)|Primary replica|  
  
## Example

You begin to explore the query performance counters in this object using this T-SQL query on the [sys.dm_os_performance_counters](../system-dynamic-management-views/sys-dm-os-performance-counters-transact-sql.md) dynamic management view:

```sql
SELECT * FROM sys.dm_os_performance_counters
WHERE object_name LIKE '%Database Replica%';
```  

## See also
  
 [Monitor Resource Usage &#40;System Monitor&#41;](../../relational-databases/performance-monitor/monitor-resource-usage-system-monitor.md)   
 [SQL Server, Availability Replica](../../relational-databases/performance-monitor/sql-server-availability-replica.md)   
 [SQL Server, Databases Object](../../relational-databases/performance-monitor/sql-server-databases-object.md)   
 [Always On Availability Groups &#40;SQL Server&#41;](../../database-engine/availability-groups/windows/always-on-availability-groups-sql-server.md)  
