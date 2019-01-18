---
title: "Set Processing Options (Reporting Services in SharePoint Integrated Mode) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
helpviewer_keywords: 
  - "SharePoint integration [Reporting Services], content management"
  - "snapshots [Reporting Services], creating"
ms.assetid: 453b19a1-739a-4b67-aeea-2069b52204e1
author: markingmyname
ms.author: maghan
manager: craigg
---
# Set Processing Options (Reporting Services in SharePoint Integrated Mode)
  You can set processing options on a [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] report to determine when data processing occurs. You can also set a time-out value for report processing, and options that determine whether report history is enabled for the current report.  
  
-   You can run a report as a report snapshot to prevent the report from being run at arbitrary times (for example, during a scheduled backup). A report snapshot is usually created and subsequently refreshed on a schedule, allowing you to time exactly when report and data processing will occur. If a report is based on queries that take a long time to run, or on queries that use data from a data source that you prefer no one access during certain hours, you should run the report as a snapshot.  
  
-   A report server can cache a copy of a processed report and return that copy when a user opens the report. Caching is a performance-enhancement technique. Caching can shorten the time required to retrieve a report if the report is large or accessed frequently.  
  
-   Report history is a collection of previously run copies of a report. You can use report history to maintain a record of a report over time. Report history is not intended for reports that contain confidential or personal data. For this reason, report history can include only those reports that query a data source using a single set of credentials (either stored credentials or credentials used for unattended report execution) that are available to all users who run a report.  
  
    > [!NOTE]  
    >  [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] integration with SharePoint uses the check out and check in content management features of SharePoint to save updates to [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] content types. This includes the creation of report snapshots. Therefore if you have enabled versioning on a document library, you will see the report version updated when a new report history snapshot is created. This is a side-effect of updating snapshots. When a snapshot is updated it causes the LastExecution property of the report to change and that will cause a change in the version of the report.  
  
-   You can specify time-out values to set limits on how system resources are used.  
  
||  
|-|  
|**[!INCLUDE[applies](../includes/applies-md.md)]**  [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] SharePoint mode|  
  
 **In this topic:**  
  
-   [To set data refresh options](#bkmk_set_data_refresh)  
  
-   [To set report caching options](#bkmk_set_report_caching)  
  
-   [To set processing time-out values](#bkmk_set_processing)  
  
-   [To set report history options and limits](#bkmk_set_report_history)  
  
-   [Set database timeout](#bkmk_set_database_timeout)  
  
##  <a name="bkmk_set_data_refresh"></a> To set data refresh options  
  
1.  Point to a report in the library.  
  
2.  Click the down arrow, and select **Manage processing options**.  
  
3.  In **Data Refresh Options**, click **Use snapshot data**. If you see "This report can not run from a snapshot because one or more of the data sources credentials are not stored", the report is not configured to run unattended and you must modify the data sources to use stored credentials before setting this option.  
  
4.  In **Data Snapshot Options**, select **Schedule data processing**.  
  
5.  Select **On a shared schedule** if you have an existing shared schedule that you want to use, otherwise click **On a custom schedule**, and then click **Configure**.  
  
6.  Select frequency, schedule, and start and end dates, and then click **OK**.  
  
7.  In **Data Snapshot Options**, select **Create or update the snapshot when this page is saved** if you want to immediately create snapshot data to use with the report, without waiting for the scheduled data processing to occur.  
  
##  <a name="bkmk_set_report_caching"></a> To set report caching options  
  
1.  Point to a report in the library.  
  
2.  Click the down arrow, and select **Manage processing options**.  
  
3.  In **Data Refresh Options**, click **Use cached data**. If you see "This report can not be cached because one or more of the data sources credentials are not stored", the report is not configured to run unattended and you must modify the data sources to use stored credentials before setting this option.  
  
4.  In **Cache Options**, specify how the cache will expire:  
  
    -   Enter a number of minutes after which the cache will expire.  
  
    -   Use a shared schedule to clear the cache at times specified in the schedule.  
  
    -   Create a custom schedule to clear the cache at a time that you specify.  
  
##  <a name="bkmk_set_processing"></a> To set processing time-out values  
  
1.  Point to a report in the library.  
  
2.  Click the down arrow, and select **Manage processing options**.  
  
3.  In **Processing Time-out**, select **Use site default setting** if you want to use the value specified at the report server level. Otherwise, select **Do not time out report processing** or **Limit report processing in seconds** if you want to override that value with no time-out or different time-out values.  
  
##  <a name="bkmk_set_report_history"></a> To set report history options and limits  
  
1.  Point to a report in the library.  
  
2.  Click the down arrow, and select **Manage processing options**.  
  
3.  In **History Snapshot Options**, specify how and when data processing occurs and is saved.  
  
4.  In **History Snapshot Limits**, select **Use site default setting** if you want to use the value specified at the report server level. Otherwise, select **Do not limit the number of snapshots** or **Limit number of snapshots** to specify a custom value.  
  
##  <a name="bkmk_set_database_timeout"></a> Set database timeout  
  
1.  Use Windows PowerShell to set the database timeout of a SharePoint report server. For more information, see the "Get and set Properties of the Reporting Service Application Database" section of [PowerShell cmdlets for Reporting Services SharePoint Mode](../../2014/reporting-services/powershell-cmdlets-for-reporting-services-sharepoint-mode.md).  
  
## See Also  
 [Set Report Processing Properties](report-server/set-report-processing-properties.md)   
 [Caching Reports &#40;SSRS&#41;](report-server/caching-reports-ssrs.md)   
 [Setting Time-out Values for Report and Shared Dataset Processing &#40;SSRS&#41;](report-server/setting-time-out-values-for-report-and-shared-dataset-processing-ssrs.md)  
  
  
