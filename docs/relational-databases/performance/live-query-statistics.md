---
title: "Live Query Statistics | Microsoft Docs"
ms.custom: 
  - "SQL2016_New_Updated"
ms.date: "10/28/2015"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "query statistics [SQL Server] live query stats"
  - "live query statistics"
  - "debugging [SQL Server], live query stats"
  - "statistics [SQL Server], live query statistics"
ms.assetid: 07f8f594-75b4-4591-8c29-d63811d7753e
caps.latest.revision: 16
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# Live Query Statistics
  [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] provides the ability to view the live execution plan of an active query. This live query plan provides real-time insights into the query execution process as the controls flow from one query plan operator to another. The live query plan displays the overall query progress and operator-level run-time execution statistics such as the number of rows produced, elapsed time, operator progress, etc. Because this data is available in real time without needing to wait for the query to complete, these execution statistics are extremely useful for debugging query performance issues. This feature is available beginning with [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)], however it can work with [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)].  
  
||  
|-|  
|**Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] ([!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [current version](http://go.microsoft.com/fwlink/p/?LinkId=299658)).|  
  
> [!WARNING]  
>  This feature is primarily intended for troubleshooting purposes. Using this feature can moderately slow the overall query performance. This feature can be used with the [Transact-SQL Debugger](../../relational-databases/scripting/configure-firewall-rules-before-running-the-tsql-debugger.md).  
  
#### To view live query statistics  
  
1.  To view the live query execution plan, on the tools menu click the **Live Query Statistics** icon.  
  
     ![Live Query Stats button on toolbar](../../relational-databases/performance/media/livequerystatstoolbar.png "Live Query Stats button on toolbar")  
  
     You can also view access the live query execution plan by right clicking on a selected query in [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] and then click **Include Live Query Statistics**.  
  
     ![Live Query Stats button on popup menu](../../relational-databases/performance/media/livequerystatsmenu.png "Live Query Stats button on popup menu")  
  
2.  Now execute the query. The live query plan displays the overall query progress and the run-time execution statistics (e.g. elapsed time, progress, etc.) for the query plan operators. The query progress information and execution statistics are periodically updated while query execution is in progress. Use this information to understand the overall query execution process and to debug long running queries, queries that run indefinitely, queries that cause tempdb overflow, and timeout issues.  
  
     ![Live Query Stats button in showplan](../../relational-databases/performance/media/livequerystatsplan.png "Live Query Stats button in showplan")  
  
 The live execution plan can also be accessed from the **Activity Monitor** by right-clicking on the queries in the **Active Expensive Queries** table.  
  
 ![Live Query Stats button in Activity Monitor](../../relational-databases/performance/media/livequerystatsactmon.png "Live Query Stats button in Activity Monitor")  
  
## Remarks  
 The statistics profile infrastructure must be enabled before live query statistics can capture information about the progress of queries. Specifying **Include Live Query Statistics** in [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] enables the statistics infrastructure for the current query session. 
 
Until [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)], there are two other ways to enable the statistics infrastructure which can be used to view the live query statistics from other sessions (such as from Activity Monitor):  
  
-   Execute `SET STATISTICS XML ON;` or `SET STATISTICS PROFILE ON;` in the target session.  
  
 or  
  
-   Enable the **query_post_execution_showplan** extended event. This is a server wide setting that enable live query statistics on all sessions. To enable extended events, see [Monitor System Activity Using Extended Events](../../relational-databases/extended-events/monitor-system-activity-using-extended-events.md).  

Starting with [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] SP1, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] includes a lightweight version of the statistics profile infrastructure. There are two ways to enable the lightweight statistics infrastructure which can be used to view the live query statistics from other sessions (such as from Activity Monitor):

-   Using global trace flag 7412.  
  
 or  
  
-   Enable the **query_thread_profile** extended event. This is a server wide setting that enable live query statistics on all sessions. To enable extended events, see [Monitor System Activity Using Extended Events](../../relational-databases/extended-events/monitor-system-activity-using-extended-events.md).
  
 > [!NOTE]
 > Natively compiled stored procedures are not supported.  
  
## Permissions  
 Requires the database level **SHOWPLAN** permission to populate the **Live Query Statistics** results page, the server level **VIEW SERVER STATE** permission to see the live statistics, and requires any permissions necessary to execute the query.  
  
## See Also  
 [Monitor and Tune for Performance](../../relational-databases/performance/monitor-and-tune-for-performance.md)   
 [Performance Monitoring and Tuning Tools](../../relational-databases/performance/performance-monitoring-and-tuning-tools.md)   
 [Open Activity Monitor &#40;SQL Server Management Studio&#41;](../../relational-databases/performance-monitor/open-activity-monitor-sql-server-management-studio.md)   
 [Activity Monitor](../../relational-databases/performance-monitor/activity-monitor.md)   
 [Monitoring Performance By Using the Query Store](../../relational-databases/performance/monitoring-performance-by-using-the-query-store.md)   
 [sys.dm_exec_query_statistics_xml](../../relational-databases/system-dynamic-management-views/sys-dm-exec-query-statistics-xml-transact-sql.md)   
 [sys.dm_exec_query_profiles](../../relational-databases/system-dynamic-management-views/sys-dm-exec-query-profiles-transact-sql.md)   
 [Trace flags](../../t-sql/database-console-commands/dbcc-traceon-trace-flags-transact-sql.md)
