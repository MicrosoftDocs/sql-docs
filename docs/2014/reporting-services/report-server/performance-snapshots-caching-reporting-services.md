---
title: "Performance, Snapshots, Caching (Reporting Services) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
helpviewer_keywords: 
  - "performance [Reporting Services]"
  - "Reporting Services, performance"
ms.assetid: 85afd00f-e8d7-4ef7-9174-2ff84d82f960
author: markingmyname
ms.author: maghan
manager: craigg
---
# Performance, Snapshots, Caching (Reporting Services)
  Report server performance is affected by a combination of factors that include hardware, number of concurrent users accessing reports, the amount of data in a report, and output format. To understand the performance factors that are specific to your installation and which remedies will produce the results you want, you will need to get baseline data and run tests. For more information about tools and guidelines, see the following publications on MSDN: [Reporting Services Performance Optimization](https://blogs.msdn.com/b/sqlcat/archive/2013/10/30/reporting-services-performance-and-optimization.aspx) and [Using Visual Studio 2005 to Perform Load Testing on a SQL Server 2005 Reporting Services Report Server](https://go.microsoft.com/fwlink/?LinkID=77519).  
  
 General principles to consider include the following:  
  
-   Report processing and rendering are memory intensive operations. When possible, choose a computer that has a lot of memory.  
  
-   Hosting the report server and the report server database on separate computers tends to provide better performance than hosting both on a single high-end computer.  
  
-   If all reports are processing slowly, consider a scale-out deployment where multiple report server instances support a single report server database. For best results, use load balancing software to distribute requests evenly across the deployment.  
  
-   If a single report is processing slowly, tune report dataset queries if the report must run on demand. You might also consider using shared datasets that you can cache, caching the report, or running the report as a snapshot.  
  
-   If all reports process slowly in a specific format (for example, while rendering to PDF), consider file share delivery, adding more memory, or choosing a different format.  
  
-   To find out how long it takes to process a report and other usage metrics, review the report server execution log. For more information, see [Report Server Execution Log and the ExecutionLog3 View](report-server-executionlog-and-the-executionlog3-view.md).  
  
-   For more information about how to mitigate performance issues by tuning memory management configuration settings, see [Configure Available Memory for Report Server Applications](../report-server/configure-available-memory-for-report-server-applications.md).  
  
## In This Section  
 [Monitoring Report Server Performance](monitoring-report-server-performance.md)  
 Describes the performance objects you can use to track the processing load on your server.  
  
 [Set Report Processing Properties](set-report-processing-properties.md)  
 Describes ways of configuring a report to run on demand, from cache, or on a schedule as a report snapshot.  
  
 [Configure Available Memory for Report Server Applications](../report-server/configure-available-memory-for-report-server-applications.md)  
 Describes how you can override default memory management behavior.  
  
 [Caching Reports &#40;SSRS&#41;](caching-reports-ssrs.md)  
 Describes report caching behavior on a report server.  
  
 [Cache Shared Datasets &#40;SSRS&#41;](cache-shared-datasets-ssrs.md)  
 Describes shared dataset caching behavior on a report server.  
  
 [Process Large Reports](process-large-reports.md)  
 Provides recommendations on how to configure and distribute a large report.  
  
 [Setting Time-out Values for Report and Shared Dataset Processing &#40;SSRS&#41;](setting-time-out-values-for-report-and-shared-dataset-processing-ssrs.md)  
 Explains how to set time outs on query and report processing.  
  
## See Also  
 [Manage a Running Process](../subscriptions/manage-a-running-process.md)   
 [Verifying a Report Run](verifying-a-report-run.md)  
  
  
