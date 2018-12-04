---
title: "SQL Server, Database Replica | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: performance
ms.topic: conceptual
helpviewer_keywords: 
  - "Availability Groups [SQL Server], monitoring"
  - "SQLServer:Database Replica"
  - "performance counters [SQL Server], AlwaysOn Availability Groups"
  - "Availability Groups [SQL Server], performance counters"
ms.assetid: a5f6bdce-2b13-4924-aaeb-b50b57d624d8
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# SQL Server, Database Replica
  The **SQLServer:Database Replica** performance object contains performance counters that report information about the secondary databases of an AlwaysOn availability group in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)]. This object is valid only on an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] that hosts a secondary replica.  
  
|Counter Name|Description|View on...|  
|------------------|-----------------|--------------|  
|**File Bytes Received/sec**|Amount of FILESTREAM data received by the secondary replica for the secondary database in the last second.|Secondary replica|  
|**Log Bytes Received/sec**|Amount of log records received by the secondary replica for the database in the last second.|Secondary replica|  
|**Log remaining for undo**|The amount of log in kilobytes remaining to complete the undo phase.|Secondary replica|  
|**Log Send Queue**|Amount of log records in the log files of the primary database, in kilobytes, that has not yet been sent to the secondary replica. This value is sent to the secondary replica from the primary replica. Queue size does not include FILESTREAM files that are sent to a secondary.|Secondary replica|  
|**Mirrored Write Transaction/sec**|Number of transactions that were written to the primary database and then waited to commit until the log was sent to the secondary database, in the last second.|Primary replica|  
|**Recovery Queue**|Amount of log records in the log files of the secondary replica that has not yet been redone.|Secondary replica|  
|**Redo blocked/sec**|Number of times the redo thread is blocked on locks held by readers of the database.|Secondary replica|  
|**Redo Bytes Remaining**|The amount of log in kilobytes remaining to be redone to finish the reverting phase.|Secondary replica|  
|**Redone Bytes/sec**|Amount of log records redone on the secondary database in the last second.|Secondary replica|  
|**Total Log requiring undo**|Total kilobytes of log that must be undone.|Secondary replica|  
|**Transaction Delay**|Delay in waiting for unterminated commit acknowledgement, in milliseconds.|Primary replica|  
  
## See Also  
 [Monitor Resource Usage &#40;System Monitor&#41;](monitor-resource-usage-system-monitor.md)   
 [SQL Server, Availability Replica](sql-server-availability-replica.md)   
 [SQL Server, Databases Object](sql-server-databases-object.md)   
 [AlwaysOn Availability Groups (SQL Server)](../../database-engine/availability-groups/windows/always-on-availability-groups-sql-server.md)  
  
  
