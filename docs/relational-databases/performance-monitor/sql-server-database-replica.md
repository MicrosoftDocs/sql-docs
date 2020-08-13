---
title: "SQL Server, Database Replica | Microsoft Docs"
description: Learn about the SQLServer:Database Replica performance object, which contains performance counters about secondary databases of an Always On availability group.
ms.custom: ""
ms.date: "08/24/2016"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: performance
ms.topic: conceptual
helpviewer_keywords: 
  - "Availability Groups [SQL Server], monitoring"
  - "SQLServer:Database Replica"
  - "performance counters [SQL Server], AlwaysOn Availability Groups"
  - "Availability Groups [SQL Server], performance counters"
ms.assetid: a5f6bdce-2b13-4924-aaeb-b50b57d624d8
author: julieMSFT
ms.author: jrasnick
---
# SQL Server, Database Replica

 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  The **SQLServer:Database Replica** performance object contains performance counters that report information about the secondary databases of an Always On availability group in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)]. This object is valid only on an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] that hosts a secondary replica.  
  
|Counter Name|Description|View on...|  
|------------------|-----------------|--------------|  
|**File Bytes Received/sec**|Amount of FILESTREAM data received by the secondary replica for the secondary database in the last second.|Secondary replica|  
|**Log Apply Pending Queue**|Number of log blocks that are waiting to be applied to the database replica.|Secondary replica|
|**Log Apply Ready Queue**|Number of log blocks that are waiting and ready to be applied to the database replica.|Secondary replica|
|**Log Bytes Received/sec**|Amount of log records received by the secondary replica for the database in the last second.|Secondary replica|  
|**Log remaining for undo**|The amount of log, in kilobytes, remaining to complete the undo phase.|Secondary replica|  
|**Log Send Queue**|Amount of log records in the log files of the primary database, in kilobytes, that haven't been sent to the secondary replica. This value is sent to the secondary replica from the primary replica. Queue size doesn't include FILESTREAM files that are sent to a secondary.|Secondary replica|  
|**Mirrored Write Transaction/sec**|Number of transactions that were written to the primary database and then waited to commit until the log was sent to the secondary database, in the last second.|Primary replica|  
|**Recovery Queue**|Amount of log records in the log files of the secondary replica that have not been redone.|Secondary replica|  
|**Redo blocked/sec**|Number of times the redo thread was blocked on locks held by readers of the database.|Secondary replica|  
|**Redo Bytes Remaining**|The amount of log, in kilobytes, remaining to be redone to finish the reverting phase.|Secondary replica|  
|**Redone Bytes/sec**|Amount of log records redone on the secondary database in the last second.|Secondary replica|  
|**Total Log requiring undo**|Total kilobytes of log that must be undone.|Secondary replica|  
|**Transaction Delay**|Delay in waiting for unterminated commit acknowledgment for all the current transactions, in milliseconds. Divide by *Mirrored Write Transaction/sec* to get *Avg Transaction Delay*. For more information, see [SQL Server 2012 AlwaysOn – Part 12 – Performance Aspects and Performance Monitoring II](https://blogs.msdn.microsoft.com/saponsqlserver/2013/04/24/sql-server-2012-alwayson-part-12-performance-aspects-and-performance-monitoring-ii/)|Primary replica|  
  
## See Also
  
 [Monitor Resource Usage &#40;System Monitor&#41;](../../relational-databases/performance-monitor/monitor-resource-usage-system-monitor.md)   
 [SQL Server, Availability Replica](../../relational-databases/performance-monitor/sql-server-availability-replica.md)   
 [SQL Server, Databases Object](../../relational-databases/performance-monitor/sql-server-databases-object.md)   
 [Always On Availability Groups &#40;SQL Server&#41;](../../database-engine/availability-groups/windows/always-on-availability-groups-sql-server.md)  
  