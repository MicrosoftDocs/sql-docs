---
title: "Create, modify, and delete schedules"
description: Create, modify & delete Reporting Services shared schedules. For native mode, use the web portal or Management Studio. For SharePoint mode, use the service app.
author: maggiesMSFT
ms.author: maggies
ms.date: 09/25/2024
ms.service: reporting-services
ms.subservice: subscriptions
ms.topic: conceptual
ms.custom:
  - updatefrequency5
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
---
# Create, modify, and delete schedules
  Use this article to learn about how to create, modify, and delete [!INCLUDE[ssRSnoversion_md](../../includes/ssrsnoversion-md.md)] shared schedules.  To manage shared schedules for native mode, use the Schedules page in the web portal or the **Shared Schedules** folder in [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)]. For SharePoint mode use, the management pages for the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] service application.

 Use one of the following methods to determine if a shared schedule is actively used:

-   **Web portal:** On the **Schedules** tab of the **Site Settings**, review the values in the Last Run date, Next Run date, and Status fields. If a schedule no longer runs because it expired, the expiration date appears in the Status field. For more information, see [Web portal (SSRS native mode)](../../reporting-services/web-portal-ssrs-native-mode.md).

-   **SQL Server Management Studio:** Viewing the **Reports** page of a given shared schedule. This page lists all reports and shared datasets that use the shared schedule. For more information, see [Reporting Services in SQL Server Management Studio](../../reporting-services/tools/reporting-services-in-sql-server-management-studio-ssrs.md).

-  **Logs:** Viewing the report execution log files or trace logs to determine whether reports were run at the times specified by the schedule. For more information, see [Reporting Services log files and sources](../../reporting-services/report-server/reporting-services-log-files-and-sources.md).

## When you delete a shared schedule
Shared schedules must be deleted manually using the Schedules page in the web portal or the Shared Schedules folder in [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)]. If you delete a shared schedule that's in use, all references to it are replaced with report-specific schedules.

If you delete a shared schedule that is used by multiple reports and subscriptions, the report server creates individual schedules for each report and subscription that previously used the shared schedule. Each new individual schedule contains the date, time, and recurrence pattern that was specified in the shared schedule. [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] doesn't provide central management of individual schedules. When you delete a shared schedule, you have to maintain the schedule information for each individual item.

**Note:**  If you aren't sure if a shared schedule is used, consider deleting it in [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] instead of the Web portal. [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] provides the same shared schedule management features as Report Manager. But, it provides another Reports page that shows you the name of each report that uses the schedule.

 Deleting a schedule and causing it to expire are different. An expiration date is used to stop a schedule but doesn't delete it. Because schedules are used to automate so many features, they're never deleted automatically. Expired schedules provide evidence to report server administrators as to why an automated process suddenly stopped. Without the presence of the expired schedule, a report server administrator can misdiagnose the problem or spend unnecessary time trying to troubleshoot a fully functional process.

 ## When you delete a report specific schedule
Report and subscription-specific schedules are deleted when you delete the report or subscription, or when you choose a different approach to run the report or subscription. For example, choosing **Always run this report with the most recent data** deletes a report-specific schedule that you created to run a report as a report execution snapshot.

A report-specific schedule that expired remains attached to the report. You can determine if a schedule expired by checking its end date. An expired shared schedules remains in the Shared Schedules list. The Status field indicates whether the schedule expired. You can reinstate the schedule by extending the end date, or you can remove the schedule reference if you no longer need it.

## <a name="bkmk_native"></a> Create, delete, or modify a shared schedule (web portal)
 Creating and modifying a schedule consists of setting frequency options that determine when the schedule runs.

 You can create or modify a schedule at any time. However, if a schedule starts to run before you complete your modifications, the earlier version of the schedule is used. The revised schedule doesn't take effect until you save it.

 If you're modifying a shared schedule, you can pause it before you make changes. The changes take effect when you resume the schedule.

1. In the web portal, select **Settings** :::image type="icon" source="../../reporting-services/subscriptions/media/ssrs-portal-settings-gear.png"::: in the toolbar.  

   >[!NOTE]  
   >If **Settings** is not available, you do not have permission to access site settings.  

1. Select **Site settings** from the menu.
1. Select the **Schedules** tab.
1. Select **+ New schedule**. To modify an existing schedule, select the name of the schedule.
1. Type a descriptive name for the schedule.
1. Select **Hour**, **Day**, **Week**, or **Month**. Select **Once** to create a schedule that runs one time only. Other options appear when you specify the basis of your schedule.
1. Optionally select a date to start the schedule. The default is the current day. You can postpone the schedule start time by choosing a later date.
1. Optionally, select a date to end the schedule. The schedule stops running on this date, but isn't deleted.
1. Select a time for the schedule to run.
1. Select **OK**.

### Delete a shared schedule (web portal)

1. In the web portal, select **Settings** :::image type="icon" source="../../reporting-services/subscriptions/media/ssrs-portal-settings-gear.png"::: in the toolbar.
2. Select **Site settings** from the menu.
3. Select the **Schedules** tab.
4. Select the checkbox next to the shared schedule you want to delete, and then choose **Delete**.

### Create, delete, or modify a shared schedule (Management Studio)
 A shared schedule contains schedule and recurrence information. Any number of published reports and subscriptions that run on a [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] report server can use that information. If you have many reports and subscriptions that run at the same time, you can create a shared schedule for those jobs. If you want to change the recurrence pattern or the end date, you can make the change in one place.

 Shared schedules are easier to maintain and give you more flexibility in managing scheduled operations. For example, you can pause and resume shared schedules. Also, if you find that too many scheduled operations are running at the same time, you can create multiple shared schedules. These schedules can run at different times. Then, you can adjust the schedule information until the processing load evens out across the report server.

#### Create or modify a shared schedule (Management Studio)

1.  Start [!INCLUDE[ssManStudioFull_md](../../includes/ssmanstudiofull-md.md)] and connect to a report server instance.
2.  In Object Explorer, expand a report server node.
3.  Right-click the **Shared Schedules** folder, and then select **New Schedule**. The General page of the **New Shared Schedule** dialog box is displayed.

     To modify an existing shared schedule, expand the **Shared Schedules** folder, right-click the schedule you want to modify, and then select **Properties**.

4.  Type a descriptive name for the schedule.
5.  Optionally select a date to start the schedule. The default is the current day.
6.  Optionally select a date to end the schedule. The schedule stops running on this date, but isn't deleted.
7.  To configure a recurring schedule, select **Hour**, **Day**, **Week**, or **Month**. Other options are displayed. Use these other options to configure schedule frequency, based on your preferred hour, day, week, or month.

     Or, to specify a one-time (nonrecurring) schedule, select **Once**, and then specify a **Start time**.

8.  Select **OK**.

##### To delete a shared schedule (Management Studio)

1.  In Object Explorer, expand a report server node.
2.  To verify the shared schedule isn't currently used by reports, Expand the **Shared Schedules** folder, right-click the schedule and select **Properties**.
3. Select the **Reports** tab to view the list of reports currently using the schedule.
Select **Cancel**.
4.  Expand the **Shared Schedules** folder, right-click the schedule you want to delete, and then select **Delete**. The **Delete Catalog Items** dialog box appears.
5.  Select **OK**.

 If you delete a shared schedule that is used by multiple reports and subscriptions, the report server creates individual schedules for each report and subscription that previously used the shared schedule. Each new individual schedule contains the date, time, and recurrence pattern that was specified in the shared schedule.

##  <a name="bkmk_sharepoint"></a>Create and manage shared schedules (SharePoint mode)
 You must be a site administrator to create, modify, or delete shared schedules on a SharePoint site.

 You can identify a specific schedule by its descriptive name. If a name isn't specified, a default name is created based on facts about the schedule, such as its recurrence pattern or dates and times when it runs.

> [!NOTE]
> Creating shared schedules requires [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent service.

### Create shared schedules (SharePoint mode)
1.  Select **Site Actions**.
2.  Select **Site Settings**.
3.  In the Reporting Services section, select **Manage Shared Schedules**.
4.  Select **Add Schedule** to open the **Schedule Properties** page.
5.  Enter a descriptive name for the schedule. On the application pages used to work with [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] reports, this name appears in drop-down lists in schedule definition pages throughout the site. Avoid long names that are hard to read. Do follow a naming convention that puts the most description information at the beginning of the name.
6.  Choose a frequency. Depending on the frequency you choose, the schedule options that appear on the page might change to support that frequency (for example, if you choose **Month**, the name of each month appears on the page).
7.  Define the schedule. Not all schedule combinations can be supported in a single schedule.
8.  Set a start and end date.
9.  Select **OK**.

### Delete shared schedules (SharePoint mode)
 All schedules, whether shared or report specific, must be deleted manually. When you delete a shared schedule that's in use, all references to it are replaced with unspecified custom schedules. An unspecified custom schedule is a custom schedule that doesn't have date or time information.

1.  Select **Site Actions**.
2.  Select **Site Settings**.
3.  In the Reporting Services section, select **Manage Shared Schedules**.
4.  Select the schedule, and select **Delete**.


## Related content

- [Schedules](../../reporting-services/subscriptions/schedules.md)
- [Pause and resume shared schedules](../../reporting-services/subscriptions/pause-and-resume-shared-schedules.md)
- [Caching reports (SSRS)](../../reporting-services/report-server/caching-reports-ssrs.md)
- [Create, modify, and delete snapshots in report history](../report-server/create-modify-and-delete-snapshots-in-report-history.md)
