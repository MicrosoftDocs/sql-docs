---
title: "SQL Server, Locks Object | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: performance
ms.topic: conceptual
helpviewer_keywords: 
  - "Locks object"
  - "SQLServer:Locks"
ms.assetid: ace04f0d-3993-4444-8317-ca39d7087e49
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# SQL Server, Locks Object
  The **SQLServer:Locks** object in Microsoft [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] provides information about [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] locks on individual resource types. Locks are held on [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] resources, such as rows read or modified during a transaction, to prevent concurrent use of resources by different transactions. For example, if an exclusive (X) lock is held on a row within a table by a transaction, no other transaction can modify that row until the lock is released. Minimizing locks increases concurrency, which can improve performance. Multiple instances of the **Locks** object can be monitored at the same time, with each instance representing a lock on a resource type.  
  
 This table describes the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] **Locks** counters.  
  
|SQL Server Locks counters|Description|  
|-------------------------------|-----------------|  
|**Average Wait Time (ms)**|Average amount of wait time (in milliseconds) for each lock request that resulted in a wait.|  
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
|**Heap/BTree**|Heap or BTree (HOBT). A lock on a heap of data pages, or on the BTree structure of an index.|  
|**Key**|A lock on a row in an index.|  
|**Metadata**|A lock on a piece of catalog information, also called metadata.|  
|**Object**|A lock on table, stored procedure, view, etc, including all data and indexes. The object can be anything that has an entry in **sys.all_objects**.|  
|**Page**|A lock on an 8-kilobyte (KB) page in a database.|  
|**RID**|Row ID. A lock on a single row in a heap.|  
  
## See Also  
 [Monitor Resource Usage &#40;System Monitor&#41;](monitor-resource-usage-system-monitor.md)  
  
  
