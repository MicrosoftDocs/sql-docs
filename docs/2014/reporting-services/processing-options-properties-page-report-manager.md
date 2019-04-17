---
title: "Processing Options Properties Page (Report Manager) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
ms.assetid: 28f07c70-7132-4d15-9505-4fdf31dc9cc0
author: markingmyname
ms.author: maghan
manager: kfile
---
# Processing Options Properties Page (Report Manager)
  Use the Processing Options properties page to set report execution properties for the currently selected report. These options determine when data processing for the report occurs. You can set these options to retrieve report data during off-peak hours. If you have a report that is accessed frequently, you can temporarily cache copies of it to eliminate wait time if multiple users are accessing the same report within minutes of each other.  
  
> [!NOTE]  
>  The Report history, Execution snapshots and Caching features are not available in every edition of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]. For a list of features that are supported by the editions of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)], see [Features Supported by the Editions of SQL Server 2014](../../2014/getting-started/features-supported-by-the-editions-of-sql-server-2014.md).  
  
## Navigation  
 Use the following procedure to navigate to this location in the user interface (UI).  
  
###### To open the Processing Options properties page  
  
1.  Open Report Manager, and locate the report for which you want to set processing properties.  
  
2.  Hover over the report, and click the drop-down arrow.  
  
3.  In the drop-down menu, click **Manage**. This opens the General properties page for the report.  
  
4.  Select the **Processing Options** tab.  
  
## Options  
 **Always run this report with the most recent data**  
 Use this option when you want to retrieve report data when the user selects the report. If a cached copy of the report is available, it is returned to the user; otherwise, data retrieval and rendering occurs when a user selects the report.  
  
 Select **Do not cache temporary copies of this report** to always run the report with the most recent data. Each user who opens the report triggers a query against the data source that contains data used in the report.  
  
 Select **Cache a temporary copy of the report**to place a temporary copy of the report in a cache when a user first opens the report. Subsequent users who run the report within the caching period receive the cached copy of the report. Caching usually improves performance because the report is returned from the cache instead of being processed again.  
  
 Cached reports must eventually expire. Specify the number of minutes to save the cached copy of the report. As soon as a temporary copy expires, it is no longer returned from cache. The next time a user opens the report, the report server processes the report again and places a copy of the refreshed report back in the cache.  
  
 You can also use a schedule to expire a cached report using a frequency other than minutes. For example, to expire a cached report at the end of the day, you can pick a specific hour at night after which the copy expires.  
  
 **Render this report from a report execution snapshot**  
 Use this option to retrieve a report that has been stored as a snapshot at a time that you schedule. When you choose this option, you can schedule data processing to occur during off-peak hours. Unlike cached copies that are created when a user opens the report, a snapshot is created and subsequently refreshed on a schedule. Snapshots do not expire; they remain in service until they are replaced by newer versions.  
  
 Snapshots that are generated as a result of report execution settings have the same characteristics as report history snapshots. The difference is that there is only one report execution snapshot and potentially many report history snapshots. Report history snapshots are accessed from the History page of the report, which stores many instances of a report as it existed at different points in time. In contrast, users access report execution snapshots from folders the same way that they access live reports. In the case of report execution snapshots, no visual cue exists to indicate to users that the report is a snapshot.  
  
 Select the related option of **Create a report snapshot when you click Apply on this page** to create a report snapshot when you click Apply. This generates the report snapshot right away to make it available before the scheduled start time.  
  
 **Report Execution Timeout**  
 Specify whether report processing times out after a certain number of seconds. If you choose the default setting, the timeout setting that is specified in the Site Settings page is used for this report.  
  
 This value applies to report processing on a report server. It does not set a timeout for data processing on the database server that provides the data for your report. However, the value that you specify must be enough to complete both data processing and report processing. The count for report processing begins when the report is selected and ends when the report opens.  
  
## See Also  
 [Set Report Processing Properties](report-server/set-report-processing-properties.md)   
 [Caching Reports &#40;SSRS&#41;](report-server/caching-reports-ssrs.md)   
 [Create, Modify, and Delete Schedules](subscriptions/create-modify-and-delete-schedules.md)   
 [Report Manager F1 Help](../../2014/reporting-services/report-manager-f1-help.md)  
  
  
