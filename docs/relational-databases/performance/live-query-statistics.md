---
title: "Live Query Statistics"
description: Learn how to view the live execution plan of an active query in SQL Server Management Studio. Use the execution statistics to debug query performance issues.
author: rwestMSFT
ms.author: randolphwest
ms.date: 04/07/2026
ms.service: sql
ms.subservice: performance
ms.topic: how-to
ms.custom:
  - ignite-2025
helpviewer_keywords:
  - "query statistics [SQL Server] live query stats"
  - "live query statistics"
  - "debugging [SQL Server], live query stats"
  - "statistics [SQL Server], live query statistics"
  - "query profiling"
  - "lightweight query profiling"
  - "lightweight profiling"
monikerRange: "=azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"
---
# Live query statistics

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance FabricSQLDB](../../includes/applies-to-version/sql-asdb-asdbmi-fabricsqldb.md)]

[!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] provides the ability to view the live execution plan of an active query. This live query plan provides real-time insights into the query execution process as the controls flow from one [query plan operator](../showplan-logical-and-physical-operators-reference.md) to another. The live query plan displays the overall query progress and operator-level run-time execution statistics such as the number of rows produced, elapsed time, operator progress, and more. 

Because you can access this data in real time without needing to wait for the query to complete, these execution statistics are extremely useful for debugging query performance problems. 

Internally, live query statistics use the [sys.dm_exec_query_profiles](../system-dynamic-management-views/sys-dm-exec-query-profiles-transact-sql.md) DMV.

> [!WARNING]  
> This feature is primarily intended for troubleshooting purposes. Using this feature can moderately slow the overall query performance, especially in [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)]. For more information, see [Query Profiling Infrastructure](query-profiling-infrastructure.md).  
> You can use this feature with the [Transact-SQL debugger](../../ssdt/debugger/configure-firewall-rules-before-running-tsql-debugger.md).  

<a id="to-view-live-query-statistics-for-one-query"></a>

## View live query statistics for one query

1. To view the live query execution plan, on the tools menu, select the **Include Live Query Statistics** icon.

     :::image type="content" source="../../relational-databases/performance/media/livequerystatstoolbar.png" alt-text="Screenshot from SQL Server Management Studio, showing the Live Query Stats button on toolbar." lightbox="../../relational-databases/performance/media/livequerystatstoolbar.png":::  

     You can also access the live query execution plan by right-clicking on a selected query in [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] and then selecting **Include Live Query Statistics**.    

     :::image type="content" source="../../relational-databases/performance/media/livequerystatsmenu.png" alt-text="Screenshot from SQL Server Management Studio, showing the Live Query Stats button on popup menu.":::  

1. Execute the query. The live query plan displays the overall query progress and the run-time execution statistics (for example, elapsed time or progress) for the query plan operators. The query progress information and execution statistics are periodically updated while query execution is in progress. Use this information to understand the overall query execution process and to debug long running queries, queries that run indefinitely, queries that cause `tempdb` overflow, and timeouts.      

     :::image type="content" source="../../relational-databases/performance/media/livequerystatsplan.png" alt-text="Screenshot from SQL Server Management Studio, showing the Live Query Stats button in showplan." lightbox="../../relational-databases/performance/media/livequerystatsplan.png":::  

<a id="to-view-live-query-statistics-for-any-query"></a>

## View live query statistics for any query

You can also access the live execution plan from **[Activity Monitor](../performance-monitor/activity-monitor.md)** by right-clicking any query in the **Processes** or **Active Expensive Queries** table.  

:::image type="content" source="../../relational-databases/performance/media/livequerystatsactmon.png" alt-text="Screenshot of Live Query Stats button in Activity Monitor.":::  

## Remarks

You must enable the statistics profile infrastructure before live query statistics can capture information about the progress of queries. Depending on the version, the overhead can be significant. For more information about this overhead, see [Query Profiling Infrastructure](query-profiling-infrastructure.md).

## Permissions

- To populate the **Live Query Statistics** results page, you need the database level `SHOWPLAN` permission, and any permissions necessary to execute the query.
- On [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], you need the server level `VIEW SERVER STATE` permission to see the live statistics.  
- On [!INCLUDE[ssSDS](../../includes/sssds-md.md)] Premium Tiers, you need the `VIEW DATABASE STATE` permission in the database to see the live statistics. On [!INCLUDE[ssSDS](../../includes/sssds-md.md)] Standard and Basic Tiers, you need the **Server admin** or **Microsoft Entra admin** account to see the live statistics.

[!INCLUDE [entra-id](../../includes/entra-id.md)]

## Related content

- [Execution plan overview](execution-plans.md)
- [Query processing architecture guide](../query-processing-architecture-guide.md)
- [Monitor and Tune for Performance](monitor-and-tune-for-performance.md)
- [Performance monitoring and tuning tools](performance-monitoring-and-tuning-tools.md)
- [Open Activity Monitor in SQL Server Management Studio (SSMS)](../performance-monitor/open-activity-monitor-sql-server-management-studio.md)
- [Activity Monitor](../performance-monitor/activity-monitor.md)
- [Monitor performance by using the Query Store](monitoring-performance-by-using-the-query-store.md)
- [sys.dm_exec_query_statistics_xml (Transact-SQL)](../system-dynamic-management-objects/sys-dm-exec-query-statistics-xml-transact-sql.md)
- [sys.dm_exec_query_profiles (Transact-SQL)](../system-dynamic-management-objects/sys-dm-exec-query-profiles-transact-sql.md)
- [Logical and physical showplan operator reference](../showplan-logical-and-physical-operators-reference.md)
- [Query Profiling Infrastructure](query-profiling-infrastructure.md)
