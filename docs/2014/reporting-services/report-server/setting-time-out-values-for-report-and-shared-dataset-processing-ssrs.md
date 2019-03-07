---
title: "Setting Time-out Values for Report and Shared Dataset Processing (SSRS) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
helpviewer_keywords: 
  - "time-outs [Reporting Services]"
  - "query time-outs [Reporting Services]"
  - "report processing [Reporting Services], time-outs"
  - "report execution time-outs [Reporting Services]"
ms.assetid: 0f9dc61d-d03c-4bbf-8090-7a53844350f8
author: markingmyname
ms.author: maghan
manager: kfile
---
# Setting Time-out Values for Report and Shared Dataset Processing (SSRS)
  You can specify time-out values to set limits on how system resources are used. Report server supports two time-out values:  
  
-   An embedded dataset query time-out value is the number of seconds that the report server waits for a response from the database. This value is defined in a report.  
  
-   A shared dataset query time-out value is the number of seconds that the report server waits for a response from the database. This value is part of the shared dataset definition and can be changed when you manage the shared dataset on the report server.  
  
-   A report execution time-out value is the maximum number of seconds that report processing can continue before it is stopped. This value is defined at the system level. You can vary this setting for individual reports.  
  
 Most time-out errors occur during query processing. If you are encountering time-out errors, try increasing the query time-out value. Make sure to adjust the report execution time-out value so that it is larger than the query time-out. The time period should be sufficient to complete both query and report processing.  
  
## Setting a Query Time-Out for an Embedded Dataset in a Report  
 Query time-out values are specified during report authoring when you define an embedded dataset. The time-out value is stored with the report, in the `Timeout` element of the report definition. By default, this value is set to 30 seconds. For more information, see [Report Embedded Datasets and Shared Datasets &#40;Report Builder and SSRS&#41;](../report-data/report-embedded-datasets-and-shared-datasets-report-builder-and-ssrs.md).  
  
 Users who have permission to modify the properties of a published report can reset this value by editing the report definition file.  
  
 You can also specify a query time-out value for data-driven subscriptions. The query time-out value is specified in the Data-Driven Subscription pages. The value you specify determines how long the report server waits for query processing to complete when retrieving data from the subscriber data source.  
  
## Setting a Query Time-Out for a Shared Dataset  
 Query time-out values are specified in seconds on the report server when you create or manage a shared dataset. By default, this value is set to 0 seconds, which is the equivalent of no time-out value. For more information, see [Manage Shared Datasets](../report-data/manage-shared-datasets.md).  
  
## Setting a Report Execution Time-Out  
 You can set the report execution time-out value to limit the amount of time that a report server uses to process a report. Report execution time-out values can be specified in Report Manager. You can set a default value for all reports in the Site Settings page, and then override that value in the Execution properties page for a specific report. By default, the value is set to 1800 seconds. For more information, see [Set Report Processing Properties](set-report-processing-properties.md).  
  
## How Report Execution Time-Out Values are Evaluated  
 The report server evaluates running jobs at 60 second intervals. At each 60 second interval, the report server compares actual process time against the report execution time-out value. If the processing time for a report exceeds the report execution time-out value, report processing will stop.  
  
 Note that if you specify a time-out value that is smaller than 60 seconds, the report may execute in full if processing starts and completes during the quiet part of the cycle when the report server is not evaluating running jobs. For example, if you set a time-out value of 10 seconds for a report that takes 20 seconds to run, the report will process in full if report execution starts early in the 60 second cycle.  
  
> [!NOTE]  
>  You can set the `RunningRequestsDbCycle` setting in the RSReportServer.config file to change the frequency of how often running jobs are evaluated.  
  
## See Also  
 [Set Processing Options &#40;Reporting Services in SharePoint Integrated Mode&#41;](../set-processing-options-reporting-services-in-sharepoint-integrated-mode.md)   
 [Reporting Services Report Server &#40;Native Mode&#41;](reporting-services-report-server-native-mode.md)   
 [Manage a Running Process](../subscriptions/manage-a-running-process.md)   
 [Report Manager  &#40;SSRS Native Mode&#41;](../report-manager-ssrs-native-mode.md)  
  
  
