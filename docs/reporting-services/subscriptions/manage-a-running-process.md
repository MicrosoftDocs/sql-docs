---
title: "Manage a running process"
description: Learn how to manage a running process, such as a user job or a system job. You can view a job, cancel a job, or manage a job programmatically.
author: maggiesMSFT
ms.author: maggies
ms.date: 09/25/2024
ms.service: reporting-services
ms.subservice: subscriptions
ms.topic: conceptual
ms.custom:
  - updatefrequency5
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
---
# Manage a running process
  [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] monitors the status of jobs that are running on the report server. At regular intervals, the report server does a scan of in-progress jobs and writes the status information to the report server database or the service application databases for SharePoint mode. A job is in progress if any of the following processes are underway: query execution on a remote or local database server, report processing, and report rendering.  
  
 You can manage both *user jobs* and *system jobs*.  
  
-   An individual user or subscription can initiate user jobs. This process includes:
    - Running a report on demand 
    - Requesting a report history snapshot
    - Manually creating a report snapshot
    - Processing a standard subscription  
  
-   The report server initiates system jobs. System jobs include scheduled report execution snapshots, scheduled report history snapshots, and data-driven subscriptions.  
  
 Report processing time and resource use vary considerably depending on the report, the query complexity, the amount of data, and the rendering format that is specified for the report. Reports that have simple queries against a local data source often complete in milliseconds and never require management or tuning. In contrast, a large report that is rendered in PDF or Excel might require significant processing time. The time depends on hardware resources, delivery options, and whether other processes are running concurrently. On a report server, most long-running processes are report rendering operations and processes that are waiting for query processing to conclude. Occasionally, you might need to cancel a report process if you want to take a computer offline, or stop a running job that is taking too long to complete.  
  
 The following processes can be canceled:  
  
-   On-demand report processing.  
  
-   Scheduled report processing.  
  
-   Standard subscriptions owned by individual users.  
  
 Canceling a job only cancels the processes that are running on the report server. Sometimes the report server doesn't manage data processing that occurs on other computers. So, you must manually cancel query processes that are orphaned on other systems. Consider specifying query time-out values to automatically stop queries that are taking too long to execute. For more information, see [Set time-out values for report and shared dataset processing &#40;SSRS&#41;](../../reporting-services/report-server/setting-time-out-values-for-report-and-shared-dataset-processing-ssrs.md). For more information about temporarily pausing a report, see [Disable or pause report and subscription processing](../../reporting-services/subscriptions/disable-or-pause-report-and-subscription-processing.md).  
  
> [!NOTE]  
>  In rare circumstances, you may need to restart the server to cancel a process. For SharePoint mode, you may need to restart the application pool hosting the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] service application. For more information, see [Start and stop the Report Server service](../../reporting-services/report-server/start-and-stop-the-report-server-service.md).  
  
 In this article:  
  
-   [View and cancel jobs (native mode)](#bkmk_native)  
  
-   [View and cancel jobs (SharePoint mode)](#bkmk_sharepoint)  
  
-   [Manage jobs programmatically](#bkmk_programmatically)  
  
##  <a name="bkmk_native"></a> View and cancel jobs (native mode)  
 You can use [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] to view or cancel a job that is running on the report server. You must refresh the page to retrieve a list of jobs that are currently running or to get up-to-date job status from the report server database. When you connect to a report server in [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)], you can open a Jobs folder to view a list of reports that are currently processing on the report server computer. Status information for each job is displayed in the Job Properties page. You can view status information for all jobs by opening the Cancel Report Server Jobs dialog box.  
  
 You can use [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] to view or cancel a job that is running on the report server. You must refresh the page to retrieve a list of jobs that are currently running or to get up-to-date job status from the report server database. When you connect to a report server in [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)], you can open a Jobs folder to view a list of reports that are currently processing on the report server computer. Status information for each job is displayed in the Job Properties page. You can view status information for all jobs by opening the Cancel Report Server Jobs dialog box.  
  
 You can't use [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] to list or cancel model generation, model processing, or data-driven subscriptions. Reporting a service doesn't provide a way to cancel model generation or processing. However, you can cancel data-driven subscriptions by using the instructions provided in this article.  
  
### How to cancel report processing or subscription  
  
1.  In [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)], connect to the report server. For instructions, see [Connect to a Report Server in Management Studio](../../reporting-services/tools/connect-to-a-report-server-in-management-studio.md).  
  
2.  Open the **Jobs** folder.  
  
3.  Right-click the report and then select **Cancel Jobs**.  
  
### How to cancel a data-driven subscription  
  
1.  Open the RSReportServer.config file in a text editor.  
  
2.  Find **IsNotificationService**.  
  
3.  Set it to **False**.  
  
4.  Save the file.  
  
5.  In Report Manager, delete the data-driven subscription from the Subscriptions tab of the report or from **My Subscriptions**.  
  
6.  After you delete the subscription, in the RSReportServer.config file, find **IsNotificationService** and set it to **True**.  
  
7.  Save the file.  
  
### Configure frequency settings to retrieve job status  
 A running job is stored in the report server temporary database. You can modify configuration settings in the RSReportServer.config file to control how often the report server scans for in-progress jobs and the interval after which the status of a running job changes from new to running. The **RunningRequestsDbCycle** setting specifies how often the report server scans for running processes. By default, status information is recorded every 60 seconds. The **RunningRequestsAge** setting specifies the interval at which a job is transitioned from new to running.  
  
##  <a name="bkmk_sharepoint"></a> View and cancel jobs (SharePoint mode)  
 Management of jobs in a SharePoint mode deployment is completed by using SharePoint Central Administration, for each [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] service application.  
  
#### Manage jobs in SharePoint mode  
  
1.  In SharePoint Central Administration, select **Manage service applications**.  
  
2.  Find and select the name of your [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] service application to open the page to manage the application.  
  
3.  Select **Manage Jobs**  
  
4.  Select the **Job Id** to see the details of the job.  
  
5.  Or choose the box for your job and select **Delete** to cancel the job. Deleting the job doesn't delete the subscription.  
  
##  <a name="bkmk_programmatically"></a> Manage jobs programmatically  
 You can manage jobs programmatically or by using a script. For more information, see <xref:ReportService2010.ReportingService2010.ListJobs%2A>, <xref:ReportService2010.ReportingService2010.CancelJob%2A>.  
  
## Related content

- [Cancel report server jobs &#40;Management Studio&#41;](../../reporting-services/tools/cancel-report-server-jobs-management-studio.md)
- [Job properties &#40;Management Studio&#41;](../../reporting-services/tools/job-properties-management-studio.md)
- [Modify a Reporting Services configuration file &#40;RSreportserver.config&#41;](../../reporting-services/report-server/modify-a-reporting-services-configuration-file-rsreportserver-config.md)
- [RsReportServer.config configuration file](../../reporting-services/report-server/rsreportserver-config-configuration-file.md)
- [Report manager &#40;SSRS native mode&#41;](../web-portal-ssrs-native-mode.md)
- [Monitor report server performance](../../reporting-services/report-server/monitoring-report-server-performance.md)
