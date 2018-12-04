---
title: "New Schedule: Edit Schedule Page (Report Manager) | Microsoft Docs"
ms.custom: ""
ms.date: "05/24/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
ms.assetid: 52a4d250-e185-4116-a29c-d809940a00fb
author: markingmyname
ms.author: maghan
manager: craigg
---
# New Schedule: Edit Schedule Page (Report Manager)
  Use the New Schedule / Edit Schedule page to create a schedule for a report. Schedules are used with subscriptions, to refresh cached reports, and to create snapshots as standalone items or in report history.  
  
> [!NOTE]  
>  This feature is not available in every edition of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]. For a list of features that are supported by the editions of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)], see [Features Supported by the Editions of SQL Server 2014](../../2014/getting-started/features-supported-by-the-editions-of-sql-server-2014.md).  
  
 You can create schedules only for reports that can run unattended. Running a report unattended requires that you store report data source credentials in the report server database. For more information, see [Data Sources Properties Page &#40;Report Manager&#41;](../../2014/reporting-services/data-sources-properties-page-report-manager.md).  
  
 Not all frequency combinations can be supported in a single schedule. For example, if you want to run a report at 12:00 P.M. and 4:00 P.M. every Friday, you must create two daily schedules that specify a Friday run date, one with a start time of 12:00 P.M. and another with a start time of 4:00 P.M.  
  
 Schedule processing is based on the local time of the report server that hosts and processes the schedule.  
  
## Navigation  
 Use the following procedures to navigate to this location in the user interface (UI).  
  
### To open the New Schedule or Edit Schedule page from the Execution properties page of a report  
  
1.  Open Report Manager, and locate the report for which you want configure a schedule.  
  
2.  Hover over the report, and click the drop-down arrow.  
  
3.  In the drop-down menu, click **Manage**. This opens the General properties page for the model.  
  
4.  Select the **Execution** tab.  
  
5.  Select the **Render this report from a report execution snapshot option**. Then select **Use the following schedule to add snapshots to report history**, and select **Report-specific schedule**. Then click **Configure**.  
  
### To open the New Schedule or Edit Schedule page from the History properties page of a report  
  
1.  Open Report Manager, and locate the report for which you want configure a schedule.  
  
2.  Hover over the report, and click the drop-down arrow.  
  
3.  In the drop-down menu, click **Manage**. This opens the General properties page for the model.  
  
4.  Select the **History** tab.  
  
5.  Select **Use the following schedule to add snapshots to report history**, and select **Report-specific schedule**. Then click **Configure**.  
  
### To open the New Schedule or Edit Schedule page from the Subscriptions page  
  
1.  Open Report Manager, and locate the report for which you want configure a schedule.  
  
2.  Hover over the report, and click the drop-down arrow.  
  
3.  In the drop-down menu,  
  
    -   Click **Manage**. This opens the General properties page for the report. Then select the **Subscriptions** tab.  
  
    -   Click **Subscribe**. This opens the **Subscriptions** properties page for the report.  
  
4.  In the toolbar, click **New Subscription** or select an existing subscription to edit.  
  
5.  Under **Subscription Processing Options**, click **New Schedule**.  
  
## Options  
 **Schedule details**  
 Select options that determine when and how often a report runs. Frequency options are layered. The first set of options specifies a category of frequency (hourly, daily, weekly, and so on). The second set of options that appears is based on your initial selection.  
  
-   **Hour** defines a schedule that runs at hourly intervals. Use the **Start and end dates** section to specify the day on which to run the schedule.  
  
-   **Day** defines a schedule that runs on the days you select at a specific hour and minute. You can specify days in the following ways: Every \<*day*>, Every weekday, and Every \<*number*> day. Choosing one approach voids the others, even if the other days appear to be selected.  
  
-   **Week** defines a schedule that runs at weekly intervals at a specific hour and minute. The interval can be a complete week (for example, every two weeks), or days within a week.  
  
-   **Month** defines a schedule that runs on a monthly basis. Within a month, you can choose a day based on a pattern (for example, the last Sunday of every month) or specific calendar dates (such as 1 and 15 to indicate the first and fifteenth day of every month). Using commas and hyphens, you can specify multiple days and ranges, for example, 1, 5, 7-12, 21.  
  
-   **Once** defines a schedule that runs only once. Use the **Start and end dates** section to specify the day on which to run the schedule. This schedule expires as soon as it is processed.  
  
 **Start and end dates**  
 Specify a start date that determines when the schedule takes effect and an end date that determines when the schedule expires.  
  
 Schedules expire without notification. After the end date, they no longer run. Expired schedules are not deleted. Schedules can only be deleted manually. That way, if you choose to continue the schedule, you can extend the end date.  
  
## See Also  
 [Report Manager  &#40;SSRS Native Mode&#41;](../../2014/reporting-services/report-manager-ssrs-native-mode.md)   
 [Create, Modify, and Delete Schedules](subscriptions/create-modify-and-delete-schedules.md)   
 [Report Manager F1 Help](../../2014/reporting-services/report-manager-f1-help.md)  
  
  
