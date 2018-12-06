---
title: "MSSQLSERVER_32040 | Microsoft Docs"
ms.custom: ""
ms.date: "04/04/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: supportability
ms.topic: "language-reference"
helpviewer_keywords: 
  - "32040 (Database Engine error)"
ms.assetid: 709219b1-f8b2-4696-8923-dd2e91492eb8
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# MSSQLSERVER_32040
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  
## Details  
  
|||  
|-|-|  
|Product Name|SQL Server|  
|Event ID|32040|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|SQLErrorNum32040|  
|Message Text|The alert for 'oldest unsent transaction' has been raised. The current value of '%d' surpasses the threshold '%d'.|  
  
## Explanation  
This database mirroring event is issued on the principal server instance to indicate that the age of the oldest unsent transaction has reached a user-specified threshold value. Typically, this event occurs because the performance of the system has changed. Either the bandwidth between the two systems has decreased, or the load has increased.  
  
The age of the oldest unsent transaction is a performance metric that can help you evaluate the potential for data loss as measured by the number of minutes of unsent transactions. This metric is especially relevant for high-performance mode sessions. However, this metric is also relevant for high-safety mode session when mirroring is paused or suspended because the partners become disconnected.  
  
## User Action  
Check the loads on the principal and mirror server instances and their network connection for the cause.  
  
## See Also  
[Database Mirroring &#40;SQL Server&#41;](~/database-engine/database-mirroring/database-mirroring-sql-server.md)  
[Use Warning Thresholds and Alerts on Mirroring Performance Metrics &#40;SQL Server&#41;](~/database-engine/database-mirroring/use-warning-thresholds-and-alerts-on-mirroring-performance-metrics-sql-server.md)  
  
