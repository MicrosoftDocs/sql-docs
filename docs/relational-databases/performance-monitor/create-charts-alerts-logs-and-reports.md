---
title: "Create Charts, Alerts, Logs, and Reports | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: performance
ms.topic: conceptual
helpviewer_keywords: 
  - "System Monitor [SQL Server], charts and reports"
  - "charts [SQL Server]"
  - "reports [SQL Server]"
  - "reports [SQL Server], creating"
  - "Windows System Monitor [SQL Server], charts and reports"
  - "logs [SQL Server], System Monitor"
  - "System Monitor [SQL Server], logs"
  - "Windows System Monitor [SQL Server], logs"
ms.assetid: c9162b37-e5dc-43d1-a3aa-1e9ebc69fecc
author: julieMSFT
ms.author: jrasnick
manager: craigg
---
# Create Charts, Alerts, Logs, and Reports
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  System Monitor lets you create charts, alerts, logs, and reports to monitor an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
## Charts  
 Charts can monitor the current performance of selected objects and counters; for example, the CPU usage or disk I/O. You can add to a chart various combinations of System Monitor objects and counters. You also can add [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows objects and counters to a chart.  
  
 Each chart represents a subset of information you want to monitor. For example, one chart can track memory usage statistics and a second chart can track disk I/O statistics.  
  
 Using a chart can be useful for the following tasks:  
  
-   Investigating why a computer or application is slow or inefficient.  
  
-   Continually monitoring systems to find intermittent performance problems.  
  
-   Discovering why you need to increase capacity.  
  
-   Displaying a trend as a line chart.  
  
-   Displaying a comparison as a histogram chart.  
  
 Charts are useful for short-term, real-time monitoring of a local or remote computer (for example, when you want to monitor an event as it occurs).  
  
## Alerts  
 Using alerts, System Monitor tracks specific events and notifies you of these events as requested. An alert log can monitor the current performance of selected counters and instances for objects in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. When a counter exceeds a given value, the log records the date and time of the event. An event can also generate a network alert. You can have a specified program run the first time or every time an event occurs. For example, an alert can send a network message to all system administrators that the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is getting low on disk space.  
  
## Logs  
 Logs allow you to record information on the current activity of selected objects and computers for later viewing and analysis. You can collect data from multiple systems into a single log file. For example, you can create different logs to accumulate information about the performance of selected objects on various computers for future analysis. You can save these selections under a file name and reuse them when you want to create another log of similar information for comparison.  
  
 Log files provide a wealth of information for troubleshooting or planning. Whereas charts, alerts, and reports on current activity provide instant feedback, log files enable you to track counters over a long period of time. Thus, you can examine information more thoroughly and document system performance.  
  
## Reports  
 Reports allow you to display constantly changing counter and instance values for selected objects. Values appear in columns for each instance. You can adjust report intervals, print snapshots, and export data. Use reports when you need to display the raw numbers.  
  
 For more information about creating charts, alerts, logs, and reports, or about Windows objects and counters, see the Windows documentation.  
  
## See Also  
 [Monitor Resource Usage &#40;System Monitor&#41;](../../relational-databases/performance-monitor/monitor-resource-usage-system-monitor.md)  
  
  
