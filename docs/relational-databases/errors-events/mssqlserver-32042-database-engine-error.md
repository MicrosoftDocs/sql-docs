---
title: "MSSQLSERVER_32042 | Microsoft Docs"
ms.custom: ""
ms.date: "04/04/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
helpviewer_keywords: 
  - "32042 (Database Engine error)"
ms.assetid: 53a51c7a-dcd4-4c15-b4d2-6aaa9dce76da
caps.latest.revision: 16
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
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
[Database Mirroring &#40;SQL Server&#41;](../Topic/Database%20Mirroring%20(SQL%20Server).md)  
[Use Warning Thresholds and Alerts on Mirroring Performance Metrics &#40;SQL Server&#41;](../Topic/Use%20Warning%20Thresholds%20and%20Alerts%20on%20Mirroring%20Performance%20Metrics%20(SQL%20Server).md)  
  
