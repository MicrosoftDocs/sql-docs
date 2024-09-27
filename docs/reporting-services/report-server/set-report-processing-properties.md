---
title: "Set report processing properties"
description: Learn about report execution properties in Report Server that control how reports are processed and how to set them for each report by using the web portal.
author: maggiesMSFT
ms.author: maggies
ms.date: 09/25/2024
ms.service: reporting-services
ms.subservice: report-server
ms.topic: conceptual
ms.custom:
  - updatefrequency5
helpviewer_keywords:
  - "on-demand reports"
  - "report processing [Reporting Services], execution properties"
  - "snapshots [Reporting Services], running reports from"
  - "cached reports [Reporting Services]"
  - "report snapshots [Reporting Services], running reports from"
  - "report execution snapshots [Reporting Services]"
---
# Set report processing properties
  Report execution properties control how a report is processed. Execution properties must be set for each report individually.  
  
 To set report execution properties, navigate to the report in the web portal, right-click the report, and select **Manage** from the menu.
  
## Report execution modes  
 You can run a report either on demand or as a snapshot. The following section describes each approach.  
  
### Run reports on demand 
 You can specify that a report query a data source each time a user runs the report, resulting in on-demand reports that contain the most up-to-date data. A new instance of the report is created for each user who opens or requests the report; each new instance contains the results of a new query. With this approach, if 10 users open the report at the same time, 10 queries are sent to the data source for processing.  
  
### Running reports on demand from cache 
 To enhance performance, you can specify a report (and data) to be cached temporarily when a user runs the report. The cached copy is then available to other users who access the same report. With this approach, if 10 users open the report, only the first request results in report processing. The report is later cached, and the remaining nine users view the cached report.  
  
 Cached reports are removed from the cache at intervals that you define. You can specify intervals in minutes, or you can schedule a specific date and time to empty the cache. For more information, see [Cache reports &#40;SSRS&#41;](../../reporting-services/report-server/caching-reports-ssrs.md).  
  
### Running reports from snapshots  
 A report snapshot is a report that contains layout information and data that is retrieved at a specific point in time. You can run a report as a report snapshot to prevent the report from being run at arbitrary times (for example, during a scheduled backup). A report snapshot is created and later refreshed on a schedule, allowing you to time exactly when report and data processing occurs. You should run a report as a snapshot if a report is based on queries that take a long time to run. Or, run a report as a snapshot if the report is based on queries that use data from a data source that you prefer no one access during certain hours.  
  
 A report snapshot is stored in a report server database, where the report is then retrieved when a user or process, such as a subscription, requests the report. When a report snapshot is updated, the snapshot is overwritten with a new instance. The report server doesn't save earlier versions of a report snapshot unless you specifically set options to add it to report history. For more information, see [Create, modify, and delete snapshots in report history](../../reporting-services/report-server/create-modify-and-delete-snapshots-in-report-history.md).  
  
 Not all reports can be configured to run as a snapshot. You can't create a snapshot for a report that prompts users for credentials or uses Windows integrated security to get data for the report. If you want to run a parameterized report as a snapshot, you must specify a default parameter to use when creating the snapshot. In contrast with reports that run on demand, it isn't possible to specify a different parameter value for a report snapshot when the report is open. Choosing a different parameter value would result in a new report processing request, which isn't allowed.  
  
 In some cases, configuring an on-demand report to run as a snapshot can deactivate subscriptions. The following condition causes a report server to deactivate existing subscriptions that were defined when the report was configured to run on demand:  
  
-   The report uses query parameters, and you select a specific value as the default parameter to meet the requirements for running the report as a snapshot.  
  
-   Existing subscriptions are configured to use parameter values that differ from the default parameter value that you specified for the snapshot.  
  
 When this condition exists, the report server will disable the subscription the next time the subscription is scheduled to run. To reactivate the subscription, open and then save the subscription. When you open the subscription, the report server updates the subscription parameter values to those values specified for the snapshot. For more information about subscriptions, see [Subscriptions and delivery &#40;Reporting Services&#41;](../../reporting-services/subscriptions/subscriptions-and-delivery-reporting-services.md).  
  
## Related content

- [Set processing options &#40;Reporting Services in SharePoint integrated mode&#41;](../../reporting-services/report-server-sharepoint/set-processing-options-reporting-services-in-sharepoint-integrated-mode.md)
- [Reporting Services concepts &#40;SSRS&#41;](../../reporting-services/reporting-services-concepts-ssrs.md)
- [Create, modify, and delete snapshots in report history](create-modify-and-delete-snapshots-in-report-history.md)
- [Specify credential and connection information for report data sources](../../reporting-services/report-data/specify-credential-and-connection-information-for-report-data-sources.md)
