---
title: "MSSQLSERVER_32044 | Microsoft Docs"
ms.custom: ""
ms.date: "04/04/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: supportability
ms.topic: "language-reference"
helpviewer_keywords: 
  - "32044 (Database Engine error)"
ms.assetid: f2d073be-d9a1-4837-8a38-028d3e3403bd
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# MSSQLSERVER_32044
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  
## Details  
  
|||  
|-|-|  
|Product Name|SQL Server|  
|Event ID|32044|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|SQLErrorNum32044|  
|Message Text|The alert for 'mirror commit overhead' has been raised. The current value of '%d' surpasses the threshold '%d'.|  
  
## Explanation  
This database mirroring event is issued on the principal server instance to indicate that the aggregate commit wait time reached or exceeded a user-specified threshold value because of database mirroring. The wait time is the product of the number of transactions and the time of each. For example, the following cases both produce 1000 milliseconds of wait time: 1000 transactions * 1 millisecond, and 1 transaction \* 1000 milliseconds. An increased commit wait time can be caused by a surge in the transaction count, delays in sending the log, or delays in flushing the log on the mirror server instance.  
  
The amount of mirror commit overhead is a performance metric that can help you evaluate the current performance impact of synchronous operation. This metric is relevant only in high-safety mode. Because high-safety mode is synchronous, the principal server instance waits to commit the transaction after it sends a log record to the mirror server instance until it receives confirmation that the mirror server instance has written the log record to disk. The log record remains on disk on the mirror server instance while it waits to be restored to the mirror database.  
  
## User Action  
Check the loads on the principal and mirror server instances and their network connection for the cause.  
  
## See Also  
[Database Mirroring &#40;SQL Server&#41;](~/database-engine/database-mirroring/database-mirroring-sql-server.md)  
[Use Warning Thresholds and Alerts on Mirroring Performance Metrics &#40;SQL Server&#41;](~/database-engine/database-mirroring/use-warning-thresholds-and-alerts-on-mirroring-performance-metrics-sql-server.md)  
  
