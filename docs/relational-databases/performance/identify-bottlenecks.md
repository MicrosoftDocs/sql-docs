---
title: "Identify Bottlenecks"
description: Learn about the causes of bottlenecks, such as insufficient resources and malfunctioning/incorrectly configured resources in SQL Server.
author: rwestMSFT
ms.author: randolphwest
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: performance
ms.topic: concept-article
ms.custom:
  - ignite-2025
helpviewer_keywords:
  - "resource bottlenecks [SQL Server]"
  - "database monitoring [SQL Server], bottlenecks"
  - "performance [SQL Server], bottlenecks"
  - "tuning databases [SQL Server], bottlenecks"
  - "monitoring server performance [SQL Server], bottlenecks"
  - "monitoring performance [SQL Server], bottlenecks"
  - "database performance [SQL Server], bottlenecks"
  - "server performance [SQL Server], bottlenecks"
  - "bottlenecks [SQL Server]"
  - "identifying bottlenecks [SQL Server]"
monikerRange: "=azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"
---
# Identify Bottlenecks
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance FabricSQLDB](../../includes/applies-to-version/sql-asdb-asdbmi-fabricsqldb.md)]
  Simultaneous access to shared resources causes bottlenecks. In general, bottlenecks are present in every software system and are inevitable. However, excessive demands on shared resources cause poor response time and must be identified and tuned.  
  
 Causes of bottlenecks include:  
  
-   Insufficient resources, requiring additional or upgraded components.  
  
-   Resources of the same type among which workloads are not distributed evenly; for example, one disk is being monopolized.  
  
-   Malfunctioning resources.  
  
-   Incorrectly configured resources.  
  
## Analyzing Bottlenecks  
 Excessive durations for various events are indicators of bottlenecks that can be tuned.  
  
 For example:  
  
-   Some other component may prevent the load from reaching this component thereby increasing the time to complete the load.  
  
-   Client requests may take longer due to network congestion.  
  
 Following are five key areas to monitor when tracking server performance to identify bottlenecks.  
  
|Possible bottleneck area|Effects on the server|  
|------------------------------|---------------------------|  
|Memory usage|Insufficient memory allocated or available to Microsoft [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] degrades performance. Data must be read from the disk rather than directly from the data cache. Microsoft Windows operating systems perform excessive paging by swapping data to and from the disk as the pages are needed.|  
|CPU utilization|A chronically high CPU utilization rate may indicate that [!INCLUDE[tsql](../../includes/tsql-md.md)] queries need to be tuned or that a CPU upgrade is needed.|  
|Disk input/output (I/O)|[!INCLUDE[tsql](../../includes/tsql-md.md)] queries can be tuned to reduce unnecessary I/O; for example, by employing indexes.|  
|User connections|Too many users may be accessing the server simultaneously causing performance degradation.|  
|Blocking locks|Incorrectly designed applications can cause locks and hamper concurrency, thus causing longer response times and lower transaction throughput rates.|  
  
## Related content

- [Monitor CPU Usage](../performance-monitor/monitor-cpu-usage.md)
- [Monitor Disk Usage](../performance-monitor/monitor-disk-usage.md)
- [Monitor memory usage](../performance-monitor/monitor-memory-usage.md)
- [SQL Server, General Statistics object](../performance-monitor/sql-server-general-statistics-object.md)
- [SQL Server, Locks object](../performance-monitor/sql-server-locks-object.md)
