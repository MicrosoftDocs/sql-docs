---
title: "SQL Server, Locks object"
description: Learn about the SQLServer:Locks object, which provides information about SQL Server locks on individual resource types. 
ms.custom: ""
ms.date: "07/13/2021"
ms.service: sql
ms.reviewer: ""
ms.subservice: performance
ms.topic: conceptual
helpviewer_keywords: 
  - "Locks object"
  - "SQLServer:Locks"
author: WilliamDAssafMSFT
ms.author: wiassaf
---
# SQL Server, Locks object
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  The **SQLServer:Locks** object in Microsoft [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] provides information about [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] locks on individual resource types. Locks are held on [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] resources, such as rows read or modified during a transaction, to prevent concurrent use of resources by different transactions. For example, if an exclusive (X) lock is held on a row within a table by a transaction, no other transaction can modify that row until the lock is released. Minimizing locks increases concurrency, which can improve performance. Multiple instances of the **Locks** object can be monitored at the same time, with each instance representing a lock on a resource type.  
  
 This table describes the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] **Locks** counters.  
  
|SQL Server Locks counters|Description|  
|-------------------------------|-----------------|  
|**Average Wait Time (ms)**|Average amount of wait time (in milliseconds) for each lock request that resulted in a wait.|  
|**Average Wait Time Base**|For internal use only.|
|**Lock Requests/sec**|Number of new locks and lock conversions per second requested from the lock manager.|  
|**Lock Timeouts (timeout > 0)/sec**|Number of lock requests per second that timed out, but excluding requests for NOWAIT locks.|  
|**Lock Timeouts/sec**|Number of lock requests per second that timed out, including requests for NOWAIT locks.|  
|**Lock Wait Time (ms)**|Total wait time (in milliseconds) for locks in the last second.|  
|**Lock Waits/sec**|Number of lock requests per second that required the caller to wait.|  
|**Number of Deadlocks/sec**|Number of lock requests per second that resulted in a deadlock.|  
  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] can lock these resources.  
  
|Item|Description|  
|----------|-----------------|  
|**_Total**|Information for all locks.|  
|**AllocUnit**|A lock on an allocation unit.|  
|**Application**|A lock on an application-specified resource.|  
|**Database**|A lock on a database, including all objects in the database.|  
|**Extent**|A lock on a contiguous group of 8 pages.|  
|**File**|A lock on a database file.|  
|**Heap/B-tree**|Heap or B-tree. A lock on a heap of data pages, or on the B-tree structure of an index.|  
|**Key**|A lock on a row in an index.|  
|**Metadata**|A lock on a piece of catalog information, also called metadata.|  
|**Object**|A lock on table, stored procedure, view, etc, including all data and indexes. The object can be anything that has an entry in **sys.all_objects**.|  
|**OIB**|Lock resource for online index build locks, specifically for a online index build LOB tracking table.|
|**Page**|A lock on an 8-kilobyte (KB) page in a database.|  
|**RID**|Row ID. A lock on a single row in a heap.|  
|**RowGroup**|Lock resource for a columnstore index rowgroup.|
|**Xact**|Lock resource for a transactions.|

[!INCLUDE [sql-b-tree](../../includes/sql-b-tree.md)]

## Example

You begin to explore the query performance counters in this object using this T-SQL query on the [sys.dm_os_performance_counters](../system-dynamic-management-views/sys-dm-os-performance-counters-transact-sql.md) dynamic management view:

```sql
SELECT * FROM sys.dm_os_performance_counters
WHERE object_name LIKE '%Locks%';
```  

## See also  
 [Monitor Resource Usage &#40;System Monitor&#41;](../../relational-databases/performance-monitor/monitor-resource-usage-system-monitor.md)  
  
  
