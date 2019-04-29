---
title: "Create, Modify, and Delete Schedules | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
helpviewer_keywords: 
  - "report-specific schedules [Reporting Services]"
  - "shared schedules [Reporting Services]"
  - "modifying schedules"
  - "removing schedules"
  - "shared schedules [Reporting Services], creating"
  - "shared schedules [Reporting Services], modifying"
  - "schedules [Reporting Services], deleting"
  - "deleting schedules"
  - "schedules [Reporting Services], creating"
  - "schedules [Reporting Services], modifying"
  - "shared schedules [Reporting Services], deleting"
ms.assetid: 05da5f3d-9222-43a9-893b-aa10f0f690f8
author: maggiesMSFT
ms.author: maggies
manager: kfile
---
# Create, Modify, and Delete Schedules
  Use this topic to learn about how to create, modify, and delete schedules.  
  
 In this topic:  
  
-   [Overview of Managing Shared Schedules](#bkmk_overview)  
  
-   [Create and Manage Shared Schedules (SharePoint Mode)](#bkmk_sharepoint)  
  
-   [Create and Manage Shared Schedules (Native Mode)](#bkmk_native)  
  
##  <a name="bkmk_overview"></a> Overview of Managing Shared Schedules  
 To manage shared schedules for native mode, use the Schedules page in Report Manager or the Shared Schedules folder in [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)]. For SharePoint mode use, the management pages for the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] service application.  
  
 You can view all the shared schedules that are defined for the report server, pause and resume schedules (on Report Manager only), and select schedules to modify or delete. The Shared Schedules page summarizes the following information about the state of each schedule: frequency, owner, expiration date, and status.  
  
 You can tell whether a shared schedule is actively used by:  
  
-   Inspecting the values in the Last Run date, Next Run date, and Status fields on the Shared Schedules page. If a schedule no longer runs because it has expired, the expiration date appears in the Status field.  
  
-   Viewing the Reports page of a given Shared Schedule. This page lists all reports and shared datasets that use the shared schedule.  
  
-   Viewing the report execution log files or trace logs to determine whether reports have been run at the times specified by the schedule. For more information, see [Reporting Services Log Files and Sources](../report-server/reporting-services-log-files-and-sources.md).  
  
##  <a name="bkmk_sharepoint"></a> Create and Manage Shared Schedules (SharePoint Mode)  
 A shared schedule is a multipurpose schedule that provides ready-to-use schedule information to any number of reports or subscriptions. You create a shared schedule once, and then reference it in a subscription or property page when you need schedule information. Shared schedules can be centrally managed, paused, and resumed. In contrast, you must edit a custom schedule manually to prevent a report or subscription from running.  
  
 You must be a site administrator to create, modify, or delete shared schedules on a SharePoint site.  
  
 You can identify a specific schedule by its descriptive name. If a name is not specified, a default name is created based on facts about the schedule, such as its recurrence pattern or dates and times when it runs.  
  
> [!NOTE]  
>  Creating shared schedules requires [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent service.  
  
### Create Shared Schedules (SharePoint)  
  
##### To create shared schedules  
  
1.  Click **Site Actions**.  
  
2.  Click **Site Settings**.  
  
3.  In the Reporting Services section, click **Manage Shared Schedules**.  
  
4.  Click **Add Schedule** to open the Schedule Properties page.  
  
5.  Enter a descriptive name for the schedule. On the application pages used to work with [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] reports, this name will appear in drop-down lists in schedule definition pages throughout the site. Avoid long names that are hard to read. Do follow a naming convention that puts the most description information at the beginning of the name.  
  
6.  Choose a frequency. Depending on the frequency you choose, the schedule options that appear on the page might change to support that frequency (for example, if you choose **Month**, the name of each month will appear on the page).  
  
7.  Define the schedule. Not all schedule combinations can be supported in a single schedule.  
  
8.  Set a start and end date.  
  
9. Click **OK**.  
  
### Delete Shared Schedules (SharePoint)  
 All schedules, whether shared or report specific, must be deleted manually. If you delete a shared schedule that is in use, all references to it are replaced with unspecified custom schedules (that is, a custom schedule that does not have date or time information).  
  
 Deleting a schedule and causing it to expire are different. An expiration date is used to stop a schedule but does not delete it. Because schedules are used to automate report server operations, they are never deleted automatically. Expired schedules provide evidence to report server administrators as to why an automated process has suddenly stopped. Without the presence of the expired schedule, a report server administrator might misdiagnose the problem or spend unnecessary time trying to troubleshoot a fully functional process.  
  
 A custom schedule that has expired remains attached to the report. You can determine if a schedule has expired by checking its end date. An expired shared schedule remains in the Shared Schedules list. The Status field indicates whether the schedule has expired. You can reinstate the schedule by extending the end date, or you can remove the schedule reference if you no longer need it.  
  
##### To delete a shared schedule  
  
1.  Click **Site Actions**.  
  
2.  Click **Site Settings**.  
  
3.  In the Reporting Services section, click **Manage Shared Schedules**.  
  
4.  Select the schedule, and click **Delete**.  
  
##  <a name="bkmk_native"></a> Create and Manage Shared Schedules (Native Mode)  
 Shared schedules must be deleted manually using the Schedules page in Report Manager or the Shared Schedules folder in [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)]. If you delete a shared schedule that is in use, all references to it are replaced with report-specific schedules.  
  
 Report and subscription-specific schedules are deleted when you delete the report or subscription, or when you choose a different approach to run the report or subscription. For example, choosing **Always run this report with the most recent data** will delete a report-specific schedule that you created to run a report as a report execution snapshot.  
  
 Deleting a schedule and causing it to expire are different. An expiration date is used to stop a schedule but does not delete it. Because schedules are used to automate so many features, they are never deleted automatically. Expired schedules provide evidence to report server administrators as to why an automated process has suddenly stopped. Without the presence of the expired schedule, a report server administrator can misdiagnose the problem or spend unnecessary time trying to troubleshoot a fully functional process.  
  
 A report-specific schedule that has expired remains attached to the report. You can determine if a schedule has expired by checking its end date. An expired shared schedules remains in the Shared Schedules list. The Status field indicates whether the schedule has expired. You can reinstate the schedule by extending the end date, or you can remove the schedule reference if you no longer need it.  
  
### Create, Delete, or Modify a Shared Schedule (Report Manager)  
 Creating and modifying a schedule consists of setting frequency options that determine when the schedule runs.  
  
-   Shared schedules are created as separate items. After they are created, you reference them when defining a subscription or some other scheduled operation.  
  
-   Report-specific schedules are created when you define a subscription or set report execution properties; filling out schedule information is part of defining a subscription or setting properties. To define a report-specific schedule, you open the report or subscription that uses it.  
  
 A shared schedule contains schedule and recurrence information that can be used by any number of published reports and subscriptions that run on a [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] report server. If you have many reports and subscriptions that run at the same time, you can create a shared schedule for those jobs. If you want to subsequently change the recurrence pattern or the end date, you can make the change in one place.  
  
 Shared schedules are easier to maintain and give you more flexibility in managing scheduled operations. For example, you can pause and resume shared schedules. Also, if you find that too many scheduled operations are running at the same time, you can create multiple shared schedules that run at different times and then adjust the schedule information until the processing load evens out across the report server.  
  
 You can create or modify a schedule at any time. However, if a schedule starts to run before you have completed your modifications, the earlier version of the schedule is used. The revised schedule does not take effect until you save it.  
  
 If you are modifying a shared schedule, you can pause it before you make changes. The changes take effect when you resume the schedule.  
  
##### To create or modify a shared schedule (Report Manager)  
  
1.  Start [Report Manager  &#40;SSRS Native Mode&#41;](../report-manager-ssrs-native-mode.md).  
  
2.  In Report Manager, click **Site Settings**on the global toolbar.  
  
3.  Click **schedules**.  
  
4.  Click **New Schedule**. To modify an existing schedule, click the name of the schedule.  
  
5.  Type a descriptive name for the schedule.  
  
6.  Select **Hour**, **Day**, **Week**, or **Month**. Click **Once** to create a schedule that runs one time only. Additional options appear when you specify the basis of your schedule.  
  
7.  Optionally select a date to start the schedule. The default is the current day. You can postpone the schedule start time by choosing a later date.  
  
8.  Optionally, select a date to end the schedule. The schedule stops running on this date, but is not deleted.  
  
9. [!INCLUDE[clickOK](../../includes/clickok-md.md)]  
  
##### To delete a shared schedule (Report Manager)  
  
1.  In Report Manager, click **Site Settings**on the global toolbar.  
  
    > [!NOTE]  
    >  If **Site Settings** is not available, you do not have permission to access site settings.  
  
2.  In the **Other** section on the page, click **Manage shared schedules**.  
  
3.  Select the check box next to the schedule you want to delete, and then click **Delete**.  
  
 If you delete a shared schedule that is used by multiple reports and subscriptions, the report server will create individual schedules for each report and subscription that previously used the shared schedule. Each new individual schedule will contain the date, time, and recurrence pattern that was specified in the shared schedule. Note that [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] does not provide central management of individual schedules. If you delete a shared schedule, you will now have to maintain the schedule information for each individual item.  
  
 If you are not sure whether a shared schedule is used, consider deleting it in [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] instead. [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] provides the same shared schedule management features as Report Manager, but it provides an additional Reports page that shows you the name of each report that uses the schedule.  
  
### Create, Delete, or Modify a Shared Schedule (Management Studio)  
 A shared schedule contains schedule and recurrence information that can be used by any number of published reports and subscriptions that run on a [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] report server. If you have many reports and subscriptions that run at the same time, you can create a shared schedule for those jobs. If you want to subsequently change the recurrence pattern or the end date, you can make the change in one place.  
  
 Shared schedules are easier to maintain and give you more flexibility in managing scheduled operations. For example, you can pause and resume shared schedules. Also, if you find that too many scheduled operations are running at the same time, you can create multiple shared schedules that run at different times and then adjust the schedule information until the processing load evens out across the report server.  
  
##### To create or modify a shared schedule (Management Studio)  
  
1.  Start SQL Server Management Studio and connect to a report server instance.  
  
2.  In Object Explorer, expand a report server node.  
  
3.  Right-click the Shared Schedules folder, and then click **New Schedule**. The General page of the **New Shared Schedule** dialog box is displayed.  
  
     To modify an existing shared schedule, expand the Shared Schedules folder, right-click the schedule you want to modify, and then click **Properties**.  
  
4.  Type a descriptive name for the schedule.  
  
5.  Optionally select a date to start the schedule. The default is the current day.  
  
6.  Optionally select a date to end the schedule. The schedule stops running on this date, but is not deleted.  
  
7.  To configure a recurring schedule, select **Hour**, **Day**, **Week**, or **Month**. Additional options are displayed. Use these additional options to configure schedule frequency, based on your preferred hour, day, week, or month.  
  
     Or, to specify a one-time (non-recurring) schedule, select **Once**, and then specify a **Start time**.  
  
8.  [!INCLUDE[clickOK](../../includes/clickok-md.md)]  
  
##### To delete a shared schedule (Management Studio)  
  
1.  In Object Explorer, expand a report server node.  
  
2.  Expand the Shared Schedules folder, right-click the schedule you want to delete, and then click **Delete**. The **Delete Catalog Items** dialog box appears.  
  
3.  [!INCLUDE[clickOK](../../includes/clickok-md.md)]  
  
 If you delete a shared schedule that is used by multiple reports and subscriptions, the report server will create individual schedules for each report and subscription that previously used the shared schedule. Each new individual schedule will contain the date, time, and recurrence pattern that was specified in the shared schedule. Note that [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] does not provide central management of individual schedules. If you delete a shared schedule, you will now have to maintain the schedule information for each individual item. Before deleting a shared schedule, use the [Reports Page](../tools/schedule-properties-reports-page.md) to determine which reports are currently using the shared schedule.  
  
## See Also  
 [Schedules](schedules.md)   
 [Pause and Resume Shared Schedules](pause-and-resume-shared-schedules.md)   
 [Cache a Report &#40;Report Manager&#41;](../report-server/cache-a-report-report-manager.md)   
 [Add a Snapshot to Report History &#40;Report Manager&#41;](../report-server/add-a-snapshot-to-report-history-report-manager.md)  
  
  
