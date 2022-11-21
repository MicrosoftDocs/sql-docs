---
title: "SQL Server, Backup Device object"
description: Learn about the Backup Device object, which provides counters to monitor Microsoft SQL Server backup devices used for backup and restore operations.
ms.custom: ""
ms.date: "07/12/2021"
ms.service: sql
ms.reviewer: ""
ms.subservice: performance
ms.topic: conceptual
helpviewer_keywords: 
  - "SQLServer:Backup Device"
  - "Backup Device object"
author: WilliamDAssafMSFT
ms.author: wiassaf
---
# SQL Server, Backup Device object
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  The **Backup Device** object provides counters to monitor Microsoft [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] backup devices used for backup and restore operations. Monitor backup devices when you want to determine the throughput or the progress and performance of your backup and restore operations on a per device basis. To monitor the throughput of the entire database backup or restore operation, use the **Backup/Restore Throughput/sec** counter of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] **Databases** object. For more information, see [SQL Server, Databases Object](../../relational-databases/performance-monitor/sql-server-databases-object.md).  
  
> [!NOTE]
>  The SQL Server Backup Device counter are not currently visible from `sys.dm_os_performance_counters`. On Windows, the counters can be viewed from [System Monitor](../../relational-databases/performance/start-system-monitor-windows.md).

 This table describes the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] **Backup Device** counter.  
  
|**SQL Server Backup Device** counters|Description|  
|---------------------------------------|-----------------|  
|**Device Throughput Bytes/sec**|Throughput of read and write operations (in bytes per second) for a backup device used when backing up or restoring databases. This counter exists only while the backup or restore operation is executing.|  
  
## See also  
 - [Backup Devices &#40;SQL Server&#41;](../../relational-databases/backup-restore/backup-devices-sql-server.md)   
 - [Monitor Resource Usage &#40;System Monitor&#41;](../../relational-databases/performance-monitor/monitor-resource-usage-system-monitor.md)  
  
  
