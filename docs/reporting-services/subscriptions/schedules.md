---
title: "Schedules | Microsoft Docs"
description: In this overview, learn how you can use shared schedules and report-specific schedules to control the processing and distribution of reports.
ms.date: 07/01/2016
ms.service: reporting-services
ms.subservice: subscriptions


ms.topic: conceptual
helpviewer_keywords: 
  - "schedules [Reporting Services]"
  - "schedules [Reporting Services], about schedules"
  - "published reports [Reporting Services], schedules"
  - "reports [Reporting Services], scheduling"
  - "subscriptions [Reporting Services], scheduling"
  - "automatic report processing"
ms.assetid: ecccd16b-eba9-4e95-b55d-f15c621e003f
author: maggiesMSFT
ms.author: maggies
---
# Schedules
  [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] provides **shared schedules** and **report-specific schedules** to help you control processing and distribution of reports. The difference between the two types of schedules is how they are defined, stored, and managed. The internal construction of the two types of schedules is the same. All schedules specify a type of recurrence: monthly, weekly, or daily. Within the recurrence type, you set the intervals and range for how often an event is to occur. The type of recurrence pattern and how those patterns are specified is the same whether you create a shared schedule or a report-specific schedule.
  
  -   Shared schedules are created as separate items. After they are created, you reference them when defining a subscription or some other scheduled operation.  
  
-   Report-specific schedules are created when you define a subscription or set report execution properties; filling out schedule information is part of defining a subscription or setting properties. To define a report-specific schedule, you open the report or subscription that uses it.  
  
 A shared schedule contains schedule and recurrence information that can be used by any number of published reports and subscriptions that run on a [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] report server. If you have many reports and subscriptions that run at the same time, you can create a shared schedule for those jobs. If you want to subsequently change the recurrence pattern or the end date, you can make the change in one place.  
  
 Shared schedules are easier to maintain and give you more flexibility in managing scheduled operations. For example, you can pause and resume shared schedules. Also, if you find that too many scheduled operations are running at the same time, you can create multiple shared schedules that run at different times and then adjust the schedule information until the processing load evens out across the report server.  
  
  
##  <a name="bkmk_whatyoucando"></a> What you can do with Schedules  
 You can use the [!INCLUDE[ssRSnoversion_md](../../includes/ssrsnoversion-md.md)] Web portal and [!INCLUDE[ssManStudioFull_md](../../includes/ssmanstudiofull-md.md)] in Native mode and SharePoint site administration pages in SharePoint mode to create and manage your schedules. You can:  
  
-   Schedule report delivery in a standard or data-driven subscription.  
  
-   Schedule report history so that new snapshots are added to report history at regular intervals.  
  
-   Schedule when to refresh the data of a report snapshot.  
  
-   Schedule when to refresh the data of a shared dataset  
  
-   Schedule the expiration of a cached report or shared dataset to occur at a predefined time so that it can be subsequently refreshed.  
  
 You can create a shared schedule if you want to use the same schedule information for many reports or subscriptions. Shared schedules are defined separately, and then referenced in reports, shared datasets, and subscriptions that need schedule information.  
  
 When you create a schedule, the report saves the schedule information in the report server database or for SharePoint mode, the service application database. The report server also creates a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent job that is used to trigger the schedule. Schedule processing is based on the local time of the report server that contains the schedule. The time format follows the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows operating system standard.  
  
 For details on how to create and manage schedules, see [Create, Modify, and Delete Schedules](../../reporting-services/subscriptions/create-modify-and-delete-schedules.md).  
  
> [!NOTE]  
>  Schedule operations are not available in every edition of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For a list of features that are supported by the editions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], see [Editions and supported features of SQL Server 2017](../../sql-server/editions-and-components-of-sql-server-2017.md).  
  
##  <a name="bkmk_compare"></a> Comparing Shared and Report-Specific Schedules  
 Both types of schedules yield the same output:.  
  
-   **Shared schedules** are portable, multipurpose items that contain ready-to-use schedule information. Because shared schedules are system-level items, creating a shared schedule requires system-level permissions. For this reason, a report server administrator or content manager typically creates the shared schedules that are available on your report server. Shared schedules are stored and managed on the report server by using the Web portal or SharePoint site settings.  
  
     In contrast with specific schedules that you define through report, shared dataset, or subscription properties, shared schedules are easier to manage and maintain for the following reasons:  
  
    -   Shared schedules can be managed from a central location, making it easier to compare schedule properties and adjust frequency and recurrence patterns if scheduled operations are running too close together or conflicting with other processes on your server.  
  
    -   Allows you to quickly adapt to changes in the computing environment. For example, suppose you have a set of reports that run at 4:00 A.M. after a data warehouse is refreshed. If the data refresh operation is rescheduled or is delayed, you can easily accommodate that change by updating the schedule information in a single shared schedule.  
  
    -   If you use only shared schedules, you know precisely when scheduled operations occur. This makes it easier to anticipate and accommodate server loads before performance issues occur. For example, if you decide to schedule computer backups at a specific hour, you can adjust shared schedules to run at different times.  
  
-   **Report-specific schedules** are defined in the context of an individual report, subscription, or report execution operation to determine cache expiration or snapshot updates. These schedules are created inline when you define a subscription or set report execution properties. You can create a report-specific schedule if a shared schedule does not provide the frequency or recurrence pattern that you need. To prevent a report from running, you must edit a report-specific schedule manually. Report-specific schedules can be created by individual users.  
  
##  <a name="bkmk_configuredatasources"></a> Configure the Data Sources  
 Before you can schedule data or subscription processing for a report, you must configure the report data source to use stored credentials or the unattended report processing account. If you use stored credentials, you can only store one set of credentials, and they will be used by all users who run the report. The credentials can be a Windows user account or a database user account.  
  
 The unattended report processing account is a special-purpose account that is configured on the report server. It is used by the report server to connect to remote computers when a scheduled operation requires the retrieval of an external file or processing. If you configure the account, you can use it to connect to external data sources that provide data to a report.  
  
 To specify stored credentials or the unattended report processing account, edit the data source properties of the report. If the report uses a shared data source, edit the shared data source instead.  
  
##  <a name="bkmk_credentials"></a> Store Credentials and Processing accounts  
 How you work with a schedule depends on tasks that are part of your role assignment. If you are using predefined roles, users who are Content Managers and System Administrators can create and manage any schedule. If you use custom role assignments, the role assignment must include tasks that support scheduled operations.  
  
|To do this|Include this task|Native Mode Predefined roles|SharePoint mode Groups|  
|----------------|-----------------------|----------------------------------|----------------------------|  
|Create, modify, or delete shared schedules|Manage shared schedules|System Administrator|Owners|  
|Select shared schedules|View shared schedules|System User|Members|  
|Create, modify, or delete report-specific schedules in a user-defined subscription|Manage individual subscriptions|Browser, Report Builder, My Reports, Content Manager|Visitors, Members|  
|Create, modify, or delete report-specific schedules for all other scheduled operations|Manage report history, manage all subscriptions, manage reports|Content Manager|Owners|  
  
 For more information about security in Native mode [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)], see [Predefined Roles](../../reporting-services/security/role-definitions-predefined-roles.md), [Granting Permissions on a Native Mode Report Server](../../reporting-services/security/granting-permissions-on-a-native-mode-report-server.md) and [Tasks and Permissions](../../reporting-services/security/tasks-and-permissions.md). For SharePoint mode, see [Compare Roles and Tasks in Reporting Services to SharePoint Groups and Permissions](../../reporting-services/security/reporting-services-roles-tasks-vs-sharepoint-groups-permissions.md)  
  
##  <a name="bkmk_how_scheduling_works"></a> How Scheduling and Delivery Processing Works  
 The Scheduling and Delivery Processor provides the following functionality:  
  
-   Maintains a queue of events and notifications in the report server database. In a scale-out deployment, the queue is shared across all of the report servers in the deployment.  
  
-   Calls the Report Processor to execute reports, process subscriptions, or clear a cached report. All report processing that occurs as a result of a schedule event is performed as a background process. SharePoint mode utilizes timer jobs to .  
  
-   Calls the delivery extension that is specified in a subscription so that the report can be delivered.  
  
 Other aspects of a scheduling and delivery operation are handled by other components and services that work with the Scheduling and Delivery Processor. Specifically, the Scheduling and Delivery Processor runs in the Report Server service and uses SQL Server Agent as a timer to generate scheduled events. The following step-by-step description explains how the scheduled operations work in a Reporting Services deployment:  
  
1.  A scheduled operation is defined when a user creates a schedule. The schedule defines a date and time that will be used to trigger a subscription for report delivery, refresh a snapshot, or expire a cache.  
  
2.  The report server saves the schedule information in the report server database.  
  
3.  The report server creates a corresponding job in SQL Server Agent that includes the schedule information provided. The jobs are created through a stored procedure, using the existing open connection to the report server database.  
  
4.  SQL Server Agent runs the job on the date and time specified in the schedule. The job creates an event that is added to a queue maintained by Reporting Services.  
  
5.  The event causes a report or subscription process to occur. Events are processed when they are detected in the queue, and the report is processed or delivered accordingly.  
  
     Before the events are processed, the Scheduling and Delivery Processor performs an authentication step to verify that the subscription owner has permission to view the report.  
  
 Reporting Services maintains an event queue for all scheduled operations. It polls the queue at regular intervals to check for new events. By default, the queue is scanned at 10 second intervals. You can change the interval by modifying the **PollingInterval**, **IsNotificationService**, and **IsEventService** configuration settings in the RSReportServer.config file. SharePoint mode also uses the RSreporserver.config for these settings and the values apply to all [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] service applications. For more information, see [RsReportServer.config Configuration File](../../reporting-services/report-server/rsreportserver-config-configuration-file.md).  
  
##  <a name="bkmk_serverdependencies"></a> Server Dependencies  
 The Scheduling and Delivery Processor requires that the Report Server service and SQL Server Agent are started. The Schedule and Delivery Processing feature must be enabled through the **ScheduleEventsAndReportDeliveryEnabled** property of the **Surface Area Configuration for Reporting Services** facet in Policy-Based Management. Both SQL Server Agent and the Report Server service must running in order for scheduled operations to occur.  
  
> [!NOTE]  
>  You can use the **Surface Area Configuration for Reporting Services** facet to stop scheduled operations on a temporary or permanent basis. Although you can create and deploy custom delivery extensions, by itself the Scheduling and Delivery Processor is not extensible. You cannot change how it manages events and notifications. For more information about turn off features, see the **Scheduled Events and Delivery** section of [Turn Reporting Services Features On or Off](../../reporting-services/report-server/turn-reporting-services-features-on-or-off.md).  
  
###  <a name="bkmk_stoppingagent"></a> Effects of Stopping the SQL Server Agent  
 Scheduled report processing uses SQL Server Agent by default. If you stop the service, no new processing requests are added to the queue unless you add them programmatically through the <xref:ReportService2010.ReportingService2010.FireEvent%2A> method. When you restart the service, the jobs that create report processing requests are resumed. The report server does not try to recreate report processing jobs that might have occurred in the past, while SQL Server Agent was offline. If you stop SQL Server Agent for a week, all scheduled operations are lost for that week.  
  
> [!NOTE]  
>  The functionality that SQL Server Agent provides to Reporting Services can be replaced with custom code that uses the <xref:ReportService2010.ReportingService2010.FireEvent%2A> method to add schedule events to the queue.  
  
###  <a name="bkmk_stoppingservice"></a> Effects of Stopping the Report Server Service  
 If you stop the Report Server service, SQL Server Agent continues to add report processing requests to the queue. Status information from SQL Server Agent indicates that the job succeeded. However, because the Report Server service is stopped, no report processing actually occurs. The requests will continue to accumulate in the queue until you restart the Report Server service. Once you restart the Report Server service, all report processing requests that are in the queue are processed in order.  
  
## See Also  
 [Create, Modify, and Delete Snapshots in Report History](../../reporting-services/report-server/create-modify-and-delete-snapshots-in-report-history.md)   
 [Subscriptions and Delivery &#40;Reporting Services&#41;](../../reporting-services/subscriptions/subscriptions-and-delivery-reporting-services.md)   
 [Data-Driven Subscriptions](../../reporting-services/subscriptions/data-driven-subscriptions.md)   
 [Caching Reports &#40;SSRS&#41;](../../reporting-services/report-server/caching-reports-ssrs.md)   
 [Report Server Content Management &#40;SSRS Native Mode&#41;](../../reporting-services/report-server/report-server-content-management-ssrs-native-mode.md)   
 [Cache Shared Datasets &#40;SSRS&#41;](../../reporting-services/report-server/cache-shared-datasets-ssrs.md)  
  
  
