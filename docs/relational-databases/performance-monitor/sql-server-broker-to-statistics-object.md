---
title: "SQL Server, Broker TO Statistics object"
description: Learn about the SQLServer:Broker TO Statistics performance object, which reports information about Service Broker request transmission objects.
ms.custom: ""
ms.date: "07/12/2021"
ms.service: sql
ms.reviewer: ""
ms.subservice: performance
ms.topic: conceptual
helpviewer_keywords: 
  - "Broker Transmission Object object"
  - "SQL Server: Broker Transmission Object"
  - "SQL Server:Broker TO Statistics"
  - "Broker TO Statistics"
author: WilliamDAssafMSFT
ms.author: wiassaf
---
# SQL Server, Broker TO Statistics object
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  The SQLServer:Broker TO Statistics performance object reports information about how many times [!INCLUDE[ssSB](../../includes/sssb-md.md)] dialogs request transmission objects, and how often transmission objects are written to `tempdb`.  
  
 Transmission objects record the state of message transmissions for a [!INCLUDE[ssSB](../../includes/sssb-md.md)] dialog. They are stored in memory. To free memory, [!INCLUDE[ssSB](../../includes/sssb-md.md)] periodically writes batches of inactive transmission objects to work tables in `tempdb`.  
  
 The following table lists the counters that this object contains.  
  
|**SQL Server Broker TO Statistics** counters|Description|  
|----------------------------------------------|-----------------|  
|**Avg. Length of Batched Writes**|The average number of transmission objects saved in a batch.|  
|**Avg. Time To Write Batch (ms)**|The average number of milliseconds required to save a batch of transmission objects.|  
|**Avg. Time to Write Batch Base**|For internal use only.|
|**Avg. Time Between Batches (ms)**|The average number of milliseconds between writes of transmission object batches.|  
|**Avg. Time Between Batches Base**|For internal use only.| 
|**Transmission Obj Gets/Sec**|The number of times per second that dialogs requested transmission objects.|  
|**Transmission Obj Set Dirty/Sec**|The number of times per second that transmission objects were marked as dirty. Transmission objects are marked as dirty by the first modification that causes the in-memory copy to differ from the copy stored in `tempdb`. Transmission objects are modified when [!INCLUDE[ssSB](../../includes/sssb-md.md)] has to record a change in the state of message transmissions for the dialog.|  
|**Transmission Obj Writes/Sec**|The number of times per second that a batch of transmission objects were written to `tempdb` work tables. Large numbers of writes could indicate that [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] memory is being stressed.|  

  
## Example

You begin to explore the query performance counters in this object using this T-SQL query on the [sys.dm_os_performance_counters](../system-dynamic-management-views/sys-dm-os-performance-counters-transact-sql.md) dynamic management view:

```sql
SELECT * FROM sys.dm_os_performance_counters
WHERE object_name LIKE '%Broker TO Statistics%';
```  
  
## See also  
 [SQL Server, Access Methods Object](../../relational-databases/performance-monitor/sql-server-access-methods-object.md)   
 [SQL Server, Memory Manager Object](../../relational-databases/performance-monitor/sql-server-memory-manager-object.md)   
 [Monitor Resource Usage &#40;System Monitor&#41;](../../relational-databases/performance-monitor/monitor-resource-usage-system-monitor.md)  
  
  
