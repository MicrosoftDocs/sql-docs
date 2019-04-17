---
title: "Monitor and Tune for Performance | Microsoft Docs"
ms.custom: ""
ms.date: "07/18/2016"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: performance
ms.topic: conceptual
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
ms.assetid: 87f23f03-0f19-4b2e-bfae-efa378f7a0d4
author: julieMSFT
ms.author: jrasnick
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Monitor and Tune for Performance
[!INCLUDE[appliesto-ss-asdb-xxxx-xxx-md](../../includes/appliesto-ss-asdb-xxxx-xxx-md.md)]
  The goal of monitoring databases is to assess how a server is performing. Effective monitoring involves taking periodic snapshots of current performance to isolate processes that are causing problems, and gathering data continuously over time to track performance trends.  
  
 Ongoing evaluation of the database performance helps you minimize response times and maximize throughput, yielding optimal performance. Efficient network traffic, disk I/O, and CPU usage are key to peak performance. You need to thoroughly analyze the application requirements, understand the logical and physical structure of the data, assess database usage, and negotiate tradeoffs between conflicting uses such as online transaction processing (OLTP) versus decision support.  
  
## Monitoring and tuning databases for performance  
 Microsoft [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and the Microsoft Windows operating system provide utilities to view the current condition of the database and track performance as conditions change. There are a variety of tools and techniques you can use to monitor [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Monitoring [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] helps you:  
  
-   Determine whether you can improve performance. For example, by monitoring the response times for frequently used queries, you can determine whether changes to the query or indexes on the tables are required.  
  
-   Evaluate user activity. For example, by monitoring users trying to connect to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], you can determine whether security is set up adequately and test applications or development systems. For example, by monitoring SQL queries as they are executed, you can determine whether they are written correctly and producing the expected results.  
  
-   Troubleshoot problems or debug application components, such as stored procedures.  
  
## Monitoring in a dynamic environment  
Changing conditions result in changing performance. In your evaluations, you can see performance changes as the number of users increases, user access and connection methods change, database contents grow, client applications change, data in the applications changes, queries become more complex, and network traffic rises. Using tools to monitor performance helps you associate changes in performance with changing conditions and complex queries. **Examples:**  
  
-   By monitoring the response times for frequently used queries, you can determine whether changes to the query or indexes on the tables where the queries execute are required.  
  
-   By monitoring [!INCLUDE[tsql](../../includes/tsql-md.md)] queries as they are executed, you can determine whether the queries are written correctly and producing the expected results.  
  
-   By monitoring users that try to connect to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], you can determine whether security is set up adequately and test applications or development systems.  
  
Response time is the length of time required for the first row of the result set to be returned to the user in the form of visual confirmation that a query is being processed. Throughput is the total number of queries handled by the server during a specified period of time.  
  
As the number of users increases, so does the competition for a server's resources, which in turn increases response time and decreases overall throughput.  
  
## Monitoring and performance tuning tasks  
  
|Topic| Task|  
|-----------|----------------------|  
|[Monitor SQL Server Components](../../relational-databases/performance/monitor-sql-server-components.md)|Required steps to monitor any SQL Server component, such as Activity Monitor, Extended Events, and Dynamic Management Views and Functions, etc.|  
|[Performance Monitoring and Tuning Tools](../../relational-databases/performance/performance-monitoring-and-tuning-tools.md)|Lists the monitoring and tuning tools available with SQL Server, such as Live Query Statistics, and the Database Engine Tuning Advisor.|  
|[Upgrading Databases by using the Query Tuning Assistant](../../relational-databases/performance/upgrade-dbcompat-using-qta.md)|Keep workload performance stability during the upgrade to newer database compatibility level.|  
|[Monitoring Performance by Using the Query Store](../../relational-databases/performance/monitoring-performance-by-using-the-query-store.md)|Use Query Store to automatically capture a history of queries, plans, and runtime statistics, and retain these for your review.|  
|[Establish a Performance Baseline](../../relational-databases/performance/establish-a-performance-baseline.md)|How to establish a performance baseline.|  
|[Isolate Performance Problems](../../relational-databases/performance/isolate-performance-problems.md)|Isolate database performance problems.|  
|[Identify Bottlenecks](../../relational-databases/performance/identify-bottlenecks.md)|Monitor and track server performance to identify bottlenecks.|  
|[Use DMVs to Determine Usage Statistics and Performance of Views](../../relational-databases/performance/use-dmvs-determine-usage-performance-views.md)|Covers methodology and scripts used to get information about the performance of queries.|  
|[Server Performance and Activity Monitoring](../../relational-databases/performance/server-performance-and-activity-monitoring.md)|Use [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and Windows performance and activity monitoring tools.|  
|[Monitor Resource Usage](../../relational-databases/performance-monitor/monitor-resource-usage-system-monitor.md)|Using System Monitor (also known as perfmon) to measure the performance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] using performance counters.|  

  
## See also  
 [Automated Administration Across an Enterprise](../../ssms/agent/automated-administration-across-an-enterprise.md)    
 [Compare and Analyze Execution Plans](../../relational-databases/performance/compare-and-analyze-execution-plans.md)    
 [Display and Save Execution Plans](../../relational-databases/performance/display-and-save-execution-plans.md)    
  
  
