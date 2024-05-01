---
title: "Performance, snapshots, caching (Reporting Services)"
description: Learn how to get baseline data and run tests to understand performance factors specific to your installation and to produce the results you want.
author: maggiesMSFT
ms.author: maggies
ms.date: 03/01/2017
ms.service: reporting-services
ms.subservice: report-server
ms.topic: conceptual
ms.custom: updatefrequency5
helpviewer_keywords:
  - "performance [Reporting Services]"
  - "Reporting Services, performance"
---
# Performance, snapshots, caching (Reporting Services)
  A combination of factors affects report server performance. These factors include hardware, number of concurrent users accessing reports, the amount of data in a report, and output format. It's important to understand the performance factors that are specific to your installation and the remedies produce the results you want. To do so, you need to get baseline data and run tests. For more information about tools and guidelines, see [Reporting Services performance optimization](/archive/blogs/sqlcat/reporting-services-performance-and-optimization) and [Use Visual Studio 2005 to perform load testing on a SQL Server 2005 Reporting Services report server](/previous-versions/sql/sql-server-2005/administrator/aa964139(v=sql.90)).  
  
 General principles to consider include:  
  
-   Report processing and rendering are memory intensive operations. When possible, choose a computer that has sufficient memory.  
  
-   Hosting the report server and the report server database on separate computers tends to provide better performance than hosting both on a single high-end computer.  
  
-   If all reports are processing slowly, consider a scale-out deployment where multiple report server instances support a single report server database. For best results, use load balancing software to distribute requests evenly across the deployment.  
  
-   If a single report is processing slowly, tune report dataset queries if the report must run on demand. You might also consider using shared datasets that you can cache, caching the report, or running the report as a snapshot.  
  
-   If all reports process slowly in a specific format, like while rendering to PDF, consider file share delivery, adding more memory, or choosing a different format.  
  
-   To find out how long it takes to process a report and other usage metrics, review the report server execution log. For more information, see [Report server ExecutionLog and the ExecutionLog3 view](../../reporting-services/report-server/report-server-executionlog-and-the-executionlog3-view.md).  
  
-   For more information about how to mitigate performance issues by tuning memory management configuration settings, see [Configure available memory for report server applications](../../reporting-services/report-server/configure-available-memory-for-report-server-applications.md).  
  
## In this section  
 [Monitor report server performance](../../reporting-services/report-server/monitoring-report-server-performance.md)  
 Describes the performance objects you can use to track the processing load on your server.  
  
 [Set report processing properties](../../reporting-services/report-server/set-report-processing-properties.md)  
 Describes ways of configuring a report to run on demand, from cache, or on a schedule as a report snapshot.  
  
 [Configure available memory for report server applications](../../reporting-services/report-server/configure-available-memory-for-report-server-applications.md)  
 Describes how you can override default memory management behavior.  
  
 [Cache reports &#40;SSRS&#41;](../../reporting-services/report-server/caching-reports-ssrs.md)  
 Describes report caching behavior on a report server.  
  
 [Cache shared datasets &#40;SSRS&#41;](../../reporting-services/report-server/cache-shared-datasets-ssrs.md)  
 Describes shared dataset caching behavior on a report server.  
  
 [Process large reports](../../reporting-services/report-server/process-large-reports.md)  
 Provides recommendations on how to configure and distribute a large report.  
  
 [Set time-out values for report and shared dataset processing &#40;SSRS&#41;](../../reporting-services/report-server/setting-time-out-values-for-report-and-shared-dataset-processing-ssrs.md)  
 Explains how to set time outs on query and report processing.  
  
## Related content  
 [Manage a running process](../../reporting-services/subscriptions/manage-a-running-process.md)   
 [Verify a report run](../../reporting-services/report-server/verifying-a-report-run.md)  
  
