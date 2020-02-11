---
title: "MSSQLSERVER_32042 | Microsoft Docs"
ms.custom: ""
ms.date: "03/08/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: supportability
ms.topic: conceptual
helpviewer_keywords: 
  - "32042 (Database Engine error)"
ms.assetid: 53a51c7a-dcd4-4c15-b4d2-6aaa9dce76da
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# MSSQLSERVER_32042
    
## Details  
  
|||  
|-|-|  
|Product Name|SQL Server|  
|Event ID|32042|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|SQLErrorNum32042|  
|Message Text|The alert for 'unsent log' has been raised. The current value of '%d' surpasses the threshold '%d'.|  
  
## Explanation  
 This database mirroring event is issued on the principal server instance to indicate that the amount of unsent log has reached a user-specified threshold value. Typically, this event occurs because the performance of the system has changed. Either the bandwidth between the two systems has decreased, or the load has increased.  
  
 The amount of unsent log is a performance metric that can help you evaluate the potential for data loss in terms of the number of kilobytes (KB) of unsent log. This metric is particularly relevant for high-performance mode sessions. However, this metric is also a relevant for high-safety mode session when mirroring is paused or suspended because the partners become disconnected.  
  
## User Action  
 Check the loads on the principal and mirror server instances and their network connection for the cause.  
  
## See Also  
 [Database Mirroring &#40;SQL Server&#41;](../../database-engine/database-mirroring/database-mirroring-sql-server.md)   
 [Use Warning Thresholds and Alerts on Mirroring Performance Metrics &#40;SQL Server&#41;](../../database-engine/database-mirroring/use-warning-thresholds-and-alerts-on-mirroring-performance-metrics-sql-server.md)  
  
  
