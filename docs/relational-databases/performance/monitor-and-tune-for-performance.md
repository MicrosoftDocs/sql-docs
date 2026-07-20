---
title: "Monitor and Tune for Performance"
description: Learn about monitoring databases to assess server performance, using periodic snapshots and gathering data continuously to track performance trends.
author: rwestMSFT
ms.author: randolphwest
ms.date: 09/03/2025
ms.service: sql
ms.subservice: performance
ms.topic: concept-article
ms.custom:
  - ignite-2025
helpviewer_keywords:
  - "instances of SQL Server, monitoring performance"
  - "monitoring server performance [SQL Server]"
  - "Database Engine [SQL Server], performance"
  - "monitoring performance [SQL Server], about performance"
  - "server performance [SQL Server]"
  - "monitoring performance [SQL Server]"
  - "database performance [SQL Server], about performance"
  - "tuning databases [SQL Server], about performance"
  - "status information [SQL Server], performance monitoring"
  - "database monitoring [SQL Server], about performance"
  - "monitoring [SQL Server], queries performance"
  - "server performance [SQL Server], about performance"
  - "tuning databases [SQL Server]"
  - "database performance [SQL Server]"
  - "monitoring [SQL Server], server performance"
  - "database monitoring [SQL Server]"
  - "monitoring server performance [SQL Server], about monitoring server performance"
monikerRange: "=azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"
---
# Monitor and Tune for Performance

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance FabricSQLDB](../../includes/applies-to-version/sql-asdb-asdbmi-fabricsqldb.md)]

The goal of monitoring databases is to assess how a server is performing. Effective monitoring involves taking periodic snapshots of current performance to isolate processes that are causing problems, and gathering data continuously over time to track performance trends.

Ongoing evaluation of the database performance helps you minimize response times and maximize throughput, yielding optimal performance. Efficient network traffic, disk I/O, and CPU usage are key to peak performance. You need to thoroughly analyze the application requirements, understand the logical and physical structure of the data, assess database usage, and negotiate tradeoffs between conflicting uses such as online transaction processing (OLTP) versus decision support.

<a id="monitoring-and-tuning-databases-for-performance"></a>

## Monitor and tuning databases for performance

Microsoft [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] and the Microsoft Windows operating system provide utilities to view the current condition of the database and track performance as conditions change. There are a variety of tools and techniques you can use to monitor [!INCLUDE [msCoName](../../includes/msconame-md.md)] [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. Monitoring [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] helps you:

- Determine whether you can improve performance. For example, by monitoring the response times for frequently used queries, you can determine whether changes to the query or indexes on the tables are required.

- Evaluate user activity. For example, by monitoring users trying to connect to an instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], you can determine whether security is set up adequately and test applications or development systems. For example, by monitoring SQL queries as they are executed, you can determine whether they are written correctly and producing the expected results.

- Troubleshoot problems or debug application components, such as stored procedures.

<a id="monitoring-in-a-dynamic-environment"></a>

## Monitor in a dynamic environment

Changing conditions result in changing performance. In your evaluations, you can see performance changes as the number of users increases, user access and connection methods change, database contents grow, client applications change, data in the applications changes, queries become more complex, and network traffic rises. Using tools to monitor performance helps you associate changes in performance with changing conditions and complex queries. **Examples:**

- By monitoring the response times for frequently used queries, you can determine whether changes to the query or indexes on the tables where the queries execute are required.

- By monitoring [!INCLUDE [tsql](../../includes/tsql-md.md)] queries as they are executed, you can determine whether the queries are written correctly and producing the expected results.

- By monitoring users that try to connect to an instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], you can determine whether security is set up adequately and test applications or development systems.

Response time is the length of time required for the first row of the result set to be returned to the user in the form of visual confirmation that a query is being processed. Throughput is the total number of queries handled by the server during a specified period of time.

As the number of users increases, so does the competition for a server's resources, which in turn increases response time and decreases overall throughput.

<a id="monitoring-and-performance-tuning-tasks"></a>

## Monitor and performance tuning tasks

| Topic | Task |
| --- | --- |
| [Monitor SQL Server Components](monitor-sql-server-components.md) | Required steps to monitor any SQL Server component, such as Activity Monitor, Extended Events, and Dynamic Management Views and Functions, etc. |
| [Performance monitoring and tuning tools](performance-monitoring-and-tuning-tools.md) | Lists the monitoring and tuning tools available with SQL Server, such as Live Query Statistics, and the Database Engine Tuning Advisor. |
| [Upgrade databases using the Query Tuning Assistant](upgrade-dbcompat-using-qta.md) | Keep workload performance stability during the upgrade to newer database compatibility level. |
| [Monitor performance by using the Query Store](monitoring-performance-by-using-the-query-store.md) | Use Query Store to automatically capture a history of queries, plans, and runtime statistics, and retain these for your review. |
| [Establish a Performance Baseline](establish-a-performance-baseline.md) | How to establish a performance baseline. |
| [Isolate Performance Problems](isolate-performance-problems.md) | Isolate database performance problems. |
| [Identify Bottlenecks](identify-bottlenecks.md) | Monitor and track server performance to identify bottlenecks. |
| [Use DMVs to Determine Usage Statistics and Performance of Views](use-dmvs-determine-usage-performance-views.md) | Covers methodology and scripts used to get information about the performance of queries. |
| [Server Performance and Activity Monitoring](server-performance-and-activity-monitoring.md) | Use [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] and Windows performance and activity monitoring tools. |
| [Monitor Resource Usage (Performance Monitor)](../performance-monitor/monitor-resource-usage-system-monitor.md) | Using System Monitor (also known as perfmon) to measure the performance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] using performance counters. |

## Related content

- [Automated Administration Across an Enterprise](/ssms/agent/automated-administration-across-an-enterprise)
- [Compare and Analyze Execution Plans](compare-and-analyze-execution-plans.md)
- [Display and save execution plans](display-and-save-execution-plans.md)
