---
title: "Set Report Processing Properties | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
helpviewer_keywords: 
  - "on-demand reports"
  - "report processing [Reporting Services], execution properties"
  - "snapshots [Reporting Services], running reports from"
  - "cached reports [Reporting Services]"
  - "report snapshots [Reporting Services], running reports from"
  - "report execution snapshots [Reporting Services]"
ms.assetid: b5cbc453-5986-423e-af44-1f243ef3edb1
author: maggiesMSFT
ms.author: maggies
manager: kfile
---
# Set Report Processing Properties
  Report execution properties control how a report is processed. Execution properties must be set for each report individually.  
  
 To set report execution properties, open the report in Report Manager, and then navigate to the Execution properties page. You can also set properties using [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)]. For more information, see [Processing Options Properties Page &#40;Report Manager&#41;](../processing-options-properties-page-report-manager.md).  
  
## Report Execution Modes  
 You can run a report either on demand or as a snapshot. The following section describes each approach.  
  
### Running Reports On Demand  
 You can specify that a report query a data source each time a user runs the report, resulting in on-demand reports that contain the most up-to-date data. A new instance of the report is created for each user who opens or requests the report; each new instance contains the results of a new query. With this approach, if ten users open the report at the same time, ten queries are sent to the data source for processing.  
  
### Running Reports On Demand From Cache  
 To enhance performance, you can specify a report (and data) to be cached temporarily when a user runs the report. The cached copy is subsequently available to other users who access the same report. With this approach, if ten users open the report, only the first request results in report processing. The report is subsequently cached, and the remaining nine users view the cached report.  
  
 Cached reports are removed from the cache at intervals that you define. You can specify intervals in minutes, or you can schedule a specific date and time to empty the cache. For more information, see [Caching Reports &#40;SSRS&#41;](caching-reports-ssrs.md).  
  
### Running Reports From Snapshots  
 A report snapshot is a report that contains layout information and data that is retrieved at a specific point in time. You can run a report as a report snapshot to prevent the report from being run at arbitrary times (for example, during a scheduled backup). A report snapshot is usually created and subsequently refreshed on a schedule, allowing you to time exactly when report and data processing will occur. If a report is based on queries that take a long time to run, or on queries that use data from a data source that you prefer no one access during certain hours, you should run the report as a snapshot.  
  
 A report snapshot is stored in a report server database, where it is subsequently retrieved when a user or process (such as a subscription) requests the report. When a report snapshot is updated, it is overwritten with a new instance. The report server does not save earlier versions of a report snapshot unless you specifically set options to add it to report history. For more information, see [Create, Modify, and Delete Snapshots in Report History](create-modify-and-delete-snapshots-in-report-history.md).  
  
 Not all reports can be configured to run as a snapshot. You cannot create a snapshot for a report that prompts users for credentials or uses Windows integrated security to get data for the report. If you want to run a parameterized report as a snapshot, you must specify a default parameter to use when creating the snapshot. In contrast with reports that run on demand, it is not possible to specify a different parameter value for a report snapshot when the report is open. Choosing a different parameter value would result in a new report processing request, which is not allowed.  
  
 In some cases, configuring an on-demand report to run as a snapshot can deactivate subscriptions. The following condition will cause a report server to deactivate existing subscriptions that were defined when the report was configured to run on demand:  
  
-   The report uses query parameters, and you select a specific value as the default parameter to meet the requirements for running the report as a snapshot.  
  
-   Existing subscriptions are configured to use parameter values that differ from the default parameter value that you specified for the snapshot.  
  
 When this condition exists, the report server will disable the subscription the next time the subscription is scheduled to run. To reactivate the subscription, open and then save the subscription. When you open the subscription, the report server updates the subscription parameter values to those specified for the snapshot. For more information about subscriptions, see [Subscriptions and Delivery &#40;Reporting Services&#41;](../subscriptions/subscriptions-and-delivery-reporting-services.md).  
  
## See Also  
 [Set Processing Options &#40;Reporting Services in SharePoint Integrated Mode&#41;](../set-processing-options-reporting-services-in-sharepoint-integrated-mode.md)   
 [Configure Execution Properties for a Report  &#40;Report Manager&#41;](../reports/configure-execution-properties-for-a-report-report-manager.md)   
 [Reporting Services Concepts &#40;SSRS&#41;](../reporting-services-concepts-ssrs.md)   
 [How to: Add a Snapshot to Report History](add-a-snapshot-to-report-history-report-manager.md)   
 [Specify Credential and Connection Information for Report Data Sources](../report-data/specify-credential-and-connection-information-for-report-data-sources.md)  
  
  
