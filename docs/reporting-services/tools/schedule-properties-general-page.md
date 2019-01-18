---
title: "Schedule Properties (General Page) | Microsoft Docs"
ms.date: 06/11/2016
ms.prod: reporting-services
ms.prod_service: "reporting-services-sharepoint, reporting-services-native"
ms.technology: tools


ms.topic: conceptual
f1_keywords: 
  - "sql13.swb.reportserver.scheduleproperties.general.f1"
ms.assetid: 20e43966-6caf-4972-a2e2-0d9131ac8f51
author: markingmyname
ms.author: maghan
---
# Schedule Properties (General Page)
  Use the [!INCLUDE[ssRSnoversion_md](../../includes/ssrsnoversion-md.md)] page in [!INCLUDE[ssManStudioFull_md](../../includes/ssmanstudiofull-md.md)] to view or modify a shared schedule. Shared schedules can be used in place of report-specific or subscription-specific schedules. Changes to the schedule are applied after you save the schedule. Editing a schedule has no effect on jobs that are currently in progress. If you edit a schedule while it is being used, all currently processing reports and subscriptions triggered from that schedule will be allowed to finish.  
  
 Not all frequency combinations can be supported in a single schedule. For example, if you want to run a report at 12:00 P.M. and 4:00 P.M. every Friday, you must create two daily schedules that specify a Friday run date, one with a start time of 12:00 P.M. and another with a start time of 4:00 P.M.  
  
 Schedule processing is based on the local time of the report server that hosts and processes the schedule.  
  
 To open this page:
 1) Start [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)].
 2) Connect to a report server.
 3) Expand the **Shared Schedules** folder.
 4) Right-click a shared schedule, and select **Properties**.  
  
> [!NOTE]  
>This feature is not available in every edition of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and this page does not appear when you are running an edition which does not have this feature. For a list of features that are supported by the editions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], see [Editions and supported features of SQL Server 2017](../../sql-server/editions-and-components-of-sql-server-2017.md).  
  
## Options  
 **Name**  
 Specifies the name for the shared schedule.  
  
 **Begin running this schedule on**  
 Specifies a start date for this schedule.  
  
 **Stop this schedule on**  
 Specifies an expiration date for this schedule.  
  
 **Type**  
 Specifies whether the recurrence pattern is based primarily on hours, days, weeks, months, or only runs once.  
  
 **Hour (Recurrence Pattern)**  
 Specifies options for running a scheduled operation in intervals of an hour (for example, to run a report every 6 hours). You can specify the interval in hours and minutes.  
  
 **Day (Recurrence Pattern)**  
 Specifies options for running a scheduled operation in intervals of days (for example, to run a report every 2 days). You can specify the interval in days and at the hour and minute you want the schedule to run.  
  
 **Week (Recurrence Pattern)**  
 Specifies options for running a scheduled operation in intervals of a week or when the pattern that you want to repeat is based on weeks (for example, to run a report every other week). You can specify a weekly schedule to the day, hour, and minute that you want the schedule to run.  
  
 **Month (Recurrence Pattern)**  
 Specifies options for running a scheduled operation in intervals of a month or when the pattern that you want to repeat is based on months. You can specify a monthly schedule to the day, hour, and minute that you want the schedule to run. You can omit specific months from the schedule.  
  
 **Once**  
 Specifies a schedule that runs only once, on a specific date and time.  
  
## See Also  
 [Report Server in Management Studio F1 Help](../../reporting-services/tools/report-server-in-management-studio-f1-help.md)   
 [Connect to a Report Server in Management Studio](../../reporting-services/tools/connect-to-a-report-server-in-management-studio.md)   
 [Create, Modify, and Delete Schedules](../../reporting-services/subscriptions/create-modify-and-delete-schedules.md)   
 [Schedules](../../reporting-services/subscriptions/schedules.md)  
  
  

