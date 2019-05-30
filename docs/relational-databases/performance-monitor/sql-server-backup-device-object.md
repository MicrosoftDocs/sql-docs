---
title: "SQL Server, Backup Device Object | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: performance
ms.topic: conceptual
helpviewer_keywords: 
  - "SQLServer:Backup Device"
  - "Backup Device object"
ms.assetid: 52e7febf-d5e0-4674-945b-aacc40a9ad6e
author: julieMSFT
ms.author: jrasnick
manager: craigg
---
# SQL Server, Backup Device Object
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]

  The **Backup Device** object provides counters to monitor Microsoft [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] backup devices used for backup and restore operations. Monitor backup devices when you want to determine the throughput or the progress and performance of your backup and restore operations on a per device basis. To monitor the throughput of the entire database backup or restore operation, use the **Backup/Restore Throughput/sec** counter of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] **Databases** object. For more information, see [SQL Server, Databases Object](../../relational-databases/performance-monitor/sql-server-databases-object.md).  
  
 This table describes the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] **Backup Device** counter.  
  
|SQL Server Backup Device counters|Description|  
|---------------------------------------|-----------------|  
|**Device Throughput Bytes/sec**|Throughput of read and write operations (in bytes per second) for a backup device used when backing up or restoring databases. This counter exists only while the backup or restore operation is executing.|  
  
## See Also  
 [Backup Devices &#40;SQL Server&#41;](../../relational-databases/backup-restore/backup-devices-sql-server.md)   
 [Monitor Resource Usage &#40;System Monitor&#41;](../../relational-databases/performance-monitor/monitor-resource-usage-system-monitor.md)  
  
  
