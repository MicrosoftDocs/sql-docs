---
description: "MSSQLSERVER_32043"
title: "MSSQLSERVER_32043 | Microsoft Docs"
ms.custom: ""
ms.date: "04/04/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: supportability
ms.topic: "reference"
helpviewer_keywords: 
  - "32043 (Database Engine error)"
ms.assetid: a0c48ae3-4c8c-419c-afb5-579fcefac01d
author: MashaMSFT
ms.author: mathoma
---
# MSSQLSERVER_32043
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  
## Details  
  
| Attribute | Value |  
| :-------- | :---- |  
|Product Name|SQL Server|  
|Event ID|32043|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|SQLErrorNum32043|  
|Message Text|The alert for 'unrestored log' has been raised. The current value of '%d' surpasses the threshold '%d'.|  
  
## Explanation  
This database mirroring event is issued on the mirror server instance to indicate that the amount of unrestored log reached a user-specified threshold value. Typically, this event occurs because the performance of the system has changed. Either the bandwidth between the two systems has decreased, or the load has increased.  
  
An unrestored log is a log that has been received by the mirror server instance and written to disk but is waiting to be restored to the mirror database. The amount of unrestored log in kilobytes (KB) is a performance metric that can help you evaluate the current failover time. The time that is required to apply the unrestored log is the main factor in failover time, along with a short additional time that is required to recover the database and bring it online.  
  
> [!NOTE]  
> For an automatic failover, the time for the system to notice the failure is independent of the failover time.  
  
## User Action  
Check the loads on the principal and mirror server instances and their network connection for the cause.  
  
## See Also  
[Database Mirroring &#40;SQL Server&#41;](~/database-engine/database-mirroring/database-mirroring-sql-server.md)  
[Use Warning Thresholds and Alerts on Mirroring Performance Metrics &#40;SQL Server&#41;](~/database-engine/database-mirroring/use-warning-thresholds-and-alerts-on-mirroring-performance-metrics-sql-server.md)  
  
