---
title: "Monitor and Tune for Performance | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
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
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Monitor and Tune for Performance
  The goal of monitoring databases is to assess how a server is performing. Effective monitoring involves taking periodic snapshots of current performance to isolate processes that are causing problems, and gathering data continuously over time to track performance trends.  
  
 Ongoing evaluation of the database performance helps you minimize response times and maximize throughput, yielding optimal performance. Efficient network traffic, disk I/O, and CPU usage are key to peak performance. You need to thoroughly analyze the application requirements, understand the logical and physical structure of the data, assess database usage, and negotiate tradeoffs between conflicting uses such as online transaction processing (OLTP) versus decision support.  
  
## Benefits of Monitoring and Tuning Databases for Performance  
 Microsoft [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and the Microsoft Windows operating system provide utilities that allow you to view the current condition of the database and to track performance as conditions change. There are a variety of tools and techniques that can be used to monitor [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Understanding how to monitor [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] can help you:  
  
-   Determine whether you can improve performance. For example, by monitoring the response times for frequently used queries, you can determine whether changes to the query or indexes on the tables are required.  
  
-   Evaluate user activity. For example, by monitoring users trying to connect to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], you can determine whether security is set up adequately and test applications or development systems. For example, by monitoring SQL queries as they are executed, you can determine whether they are written correctly and producing the expected results.  
  
-   Troubleshoot any problems or debug application components, such as stored procedures.  
  
### Monitoring in a Dynamic Environment  
 Monitoring is important because [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] provides a service in a dynamic environment. Changing conditions result in changing performance. In your evaluations, you can see performance changes as the number of users increases, user access and connection methods change, database contents grow, client applications change, data in the applications changes, queries become more complex, and network traffic rises. By using [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] tools to monitor performance, you can associate some changes in performance with changing conditions and complex queries. The following scenarios provide examples:  
  
-   By monitoring the response times for frequently used queries, you can determine whether changes to the query or indexes on the tables where the queries execute are required.  
  
-   By monitoring [!INCLUDE[tsql](../../includes/tsql-md.md)] queries as they are executed, you can determine whether the queries are written correctly and producing the expected results.  
  
-   By monitoring users that try to connect to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], you can determine whether security is set up adequately and test applications or development systems.  
  
 Response time is the length of time required for the first row of the result set to be returned to the user in the form of visual confirmation that a query is being processed. Throughput is the total number of queries handled by the server during a specified period of time.  
  
 As the number of users increases, so does the competition for a server's resources, which in turn increases response time and decreases overall throughput.  
  
## Monitoring and Tuning Performance Tasks  
  
|Task Description|Topic|  
|----------------------|-----------|  
|[Monitor SQL Server Components](monitor-sql-server-components.md)|Provides the necessary steps required to effectively monitor any component of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].|  
|[Performance Monitoring and Tuning Tools](performance-monitoring-and-tuning-tools.md)|Lists the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] monitoring and tuning tools.|  
|[Establish a Performance Baseline](establish-a-performance-baseline.md)|Provides information about how to establish a performance baseline.|  
|[Isolate Performance Problems](isolate-performance-problems.md)|Describes how to isolate database performance problems.|  
|[Identify Bottlenecks](identify-bottlenecks.md)|Describes how to monitor and track server performance to identify bottlenecks.|  
|[Server Performance and Activity Monitoring](server-performance-and-activity-monitoring.md)|Describes how to use [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and Windows performance and activity monitoring tools.|  
|[Display and Save Execution Plans](display-and-save-execution-plans.md)|Describes how to display and save execution plans to a file in XML format.|  
|[Monitoring Performance By Using the Query Store](monitoring-performance-by-using-the-query-store.md)|The Query Store automatically captures a history of queries, plans, and runtime statistics, and retains these for your review.|  
  
## See Also  
 [Automated Administration Across an Enterprise](../../ssms/agent/automated-administration-across-an-enterprise.md)   
 [Database Engine Tuning Advisor](database-engine-tuning-advisor.md)   
 [Monitor Resource Usage &#40;System Monitor&#41;](../performance-monitor/monitor-resource-usage-system-monitor.md)   
 [SQL Server Profiler](../../tools/sql-server-profiler/sql-server-profiler.md)  
  
  
