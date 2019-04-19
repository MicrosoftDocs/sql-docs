---
title: "Manage a Running Process | Microsoft Docs"
ms.date: 03/20/2017
ms.prod: reporting-services
ms.prod_service: "reporting-services-sharepoint, reporting-services-native"
ms.technology: subscriptions


ms.topic: conceptual
helpviewer_keywords: 
  - "report processing [Reporting Services], status information"
  - "jobs [Reporting Services]"
  - "viewing jobs"
  - "canceling jobs"
  - "user jobs [Reporting Services]"
  - "system jobs [Reporting Services]"
  - "report processing [Reporting Services], managing running processes"
  - "processes [Reporting Services]"
  - "scanning processes [Reporting Services]"
  - "status information [Reporting Services]"
  - "report processing [Reporting Services]"
  - "canceling subscriptions"
  - "report servers [Reporting Services], jobs"
  - "data-driven subscriptions"
  - "displaying jobs"
  - "subscriptions [Reporting Services], running processes"
ms.assetid: 473e574e-f1ff-4ef9-bda6-7028b357ac42
author: maggiesMSFT
ms.author: maggies
---
# Manage a Running Process
  [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] monitors the status of jobs that are running on the report server. At regular intervals, the report server does a scan of in-progress jobs and writes the status information to the report server database or the service application databases for SharePoint mode. A job is in progress if any of the following processes are underway: query execution on a remote or local database server, report processing, and report rendering.  
  
 You can manage both *user jobs* and *system jobs*.  
  
-   User jobs are initiated by an individual user or subscription. This includes running a report on demand, requesting a report history snapshot, manually creating a report snapshot, and processing a standard subscription.  
  
-   System jobs are initiated by the report server. System jobs include scheduled report execution snapshots, scheduled report history snapshots, and data-driven subscriptions.  
  
 Report processing time and resource use varies considerably depending on the report, the query complexity, the amount of data, and the rendering format that is specified for the report. Reports that have simple queries against a local data source will often complete in milliseconds and never require management or tuning. In contrast, a large report that is rendered in PDF or Excel might require significant processing time depending on hardware resources, delivery options, and whether other processes are running concurrently. On a report server, most long-running processes are report rendering operations and processes that are waiting for query processing to conclude. Occasionally, you might need to cancel a report process if you want to take a computer offline, or stop a running job that is taking too long to complete.  
  
 The following processes can be cancelled:  
  
-   On-demand report processing.  
  
-   Scheduled report processing.  
  
-   Standard subscriptions owned by individual users.  
  
 Canceling a job only cancels the processes that are running on the report server. Because the report server does not manage data processing that occurs on other computers, you must manually cancel query processes that are subsequently orphaned on other systems. Consider specifying query time-out values to automatically shut down queries that are taking too long to execute. For more information, see [Setting Time-out Values for Report and Shared Dataset Processing &#40;SSRS&#41;](../../reporting-services/report-server/setting-time-out-values-for-report-and-shared-dataset-processing-ssrs.md). For more information about temporarily pausing a report, see [Disable or Pause Report and Subscription Processing](../../reporting-services/subscriptions/disable-or-pause-report-and-subscription-processing.md).  
  
> [!NOTE]  
>  In rare circumstances, you may need to restart the server to cancel a process. For SharePoint mode, you may need to restart the application pool hosting the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] service application. For more information, see [Start and Stop the Report Server Service](../../reporting-services/report-server/start-and-stop-the-report-server-service.md).  
  
 In this Topic:  
  
-   [View and Cancel Jobs (Native Mode)](#bkmk_native)  
  
-   [View and Cancel Jobs (SharePoint Mode)](#bkmk_sharepoint)  
  
-   [Managing Jobs Programmatically](#bkmk_programmatically)  
  
##  <a name="bkmk_native"></a> View and Cancel Jobs (Native Mode)  
 You can use [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] to view or cancel a job that is running on the report server. You must refresh the page to retrieve a list of jobs that are currently running or to get up-to-date job status from the report server database. When you connect to a report server in [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)], you can open a Jobs folder to view a list of reports that are currently processing on the report server computer. Status information for each job is displayed in the Job Properties page. You can view status information for all jobs by opening the Cancel Report Server Jobs dialog box.  
  
 You can use [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] to view or cancel a job that is running on the report server. You must refresh the page to retrieve a list of jobs that are currently running or to get up-to-date job status from the report server database. When you connect to a report server in [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)], you can open a Jobs folder to view a list of reports that are currently processing on the report server computer. Status information for each job is displayed in the Job Properties page. You can view status information for all jobs by opening the Cancel Report Server Jobs dialog box.  
  
 You cannot use [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] to list or cancel model generation, model processing, or data-driven subscriptions. Reporting a Services does not provide a way to cancel model generation or processing. However, you can cancel data-driven subscriptions using the instructions provided in this topic.  
  
### How to Cancel Report Processing or Subscription  
  
1.  In [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)], connect to the report server. For instructions, see [Connect to a Report Server in Management Studio](../../reporting-services/tools/connect-to-a-report-server-in-management-studio.md).  
  
2.  Open the **Jobs** folder.  
  
3.  Right-click the report and then click **Cancel Jobs**.  
  
### How to Cancel a Data-driven Subscription  
  
1.  Open the RSReportServer.config file in a text editor.  
  
2.  Find **IsNotificationService**.  
  
3.  Set it to **False**.  
  
4.  Save the file.  
  
5.  In Report Manager, delete the data-driven subscription from the Subscriptions tab of the report or from **My Subscriptions**.  
  
6.  After you delete the subscription, in the RSReportServer.config file, find **IsNotificationService** and set it to **True**.  
  
7.  Save the file.  
  
### Configuring Frequency Settings for Retrieving Job Status  
 A running job is stored in the report server temporary database. You can modify configuration settings in the RSReportServer.config file to control how often the report server scans for in-progress jobs and the interval after which the status of a running job changes from new to running. The **RunningRequestsDbCycle** setting specifies how often the report server scans for running processes. By default, status information is recorded every 60 seconds. The **RunningRequestsAge** setting specifies the interval at which a job is transitioned from new to running.  
  
##  <a name="bkmk_sharepoint"></a> View and Cancel Jobs (SharePoint Mode)  
 Management of jobs in a SharePoint mode deployment is completed using SharePoint Central Administration, for each [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] service application.  
  
#### To manage jobs in SharePoint mode  
  
1.  In SharePoint Central Administration, click **Manage service applications**.  
  
2.  Find and click the name of your [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] service application to open the manage application page.  
  
3.  Click **Manage Jobs**  
  
4.  Click the **Job Id** to see the details of the job.  
  
5.  Or click the box for your job and click **Delete** to cancel the job. Deleting the job does not delete the subscription.  
  
##  <a name="bkmk_programmatically"></a> Managing Jobs Programmatically  
 You can manage jobs programmatically or by using a script. For more information, see <xref:ReportService2010.ReportingService2010.ListJobs%2A>, <xref:ReportService2010.ReportingService2010.CancelJob%2A>.  
  
## See Also  
 [Cancel Report Server Jobs &#40;Management Studio&#41;](../../reporting-services/tools/cancel-report-server-jobs-management-studio.md)   
 [Job Properties &#40;Management Studio&#41;](../../reporting-services/tools/job-properties-management-studio.md)   
 [Modify a Reporting Services Configuration File &#40;RSreportserver.config&#41;](../../reporting-services/report-server/modify-a-reporting-services-configuration-file-rsreportserver-config.md)   
 [RsReportServer.config Configuration File](../../reporting-services/report-server/rsreportserver-config-configuration-file.md)   
 [Report Manager  &#40;SSRS Native Mode&#41;](https://msdn.microsoft.com/library/80949f9d-58f5-48e3-9342-9e9bf4e57896)   
 [Monitoring Report Server Performance](../../reporting-services/report-server/monitoring-report-server-performance.md)  
  
  
