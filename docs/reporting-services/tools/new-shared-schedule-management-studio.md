---
title: "New shared schedule (Management Studio)"
description: Learn how to create a new shared schedule to run published reports and subscriptions by using the options on the New Schedule page in SQL Server Management Studio.
author: maggiesMSFT
ms.author: maggies
ms.date: 09/25/2024
ms.service: reporting-services
ms.subservice: tools
ms.topic: conceptual
ms.custom:
  - updatefrequency5
f1_keywords:
  - "sql13.swb.reportserver.newschedule.f1"
---
# New shared schedule (Management Studio)
  Use this page to create a shared schedule to run published reports and subscriptions. Shared schedules can be used in place of report-specific or subscription-specific schedules. Centralized schedule information and the ability to pause and resume scheduled operations are two key features that distinguish shared schedules from item-specific schedules.  
  
 Not all frequency combinations can be supported in a single schedule. For example, if you want to run a report at 12:00 P.M. and 4:00 P.M. every Friday, you must create two daily schedules. These schedules should specify a Friday run date, with one having a start time of 12:00 P.M. and the other with a start time of 4:00 P.M.  
  
 Schedule processing is based on the local time of the report server that hosts and processes the schedule.  
  
 To open this page, start [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], connect to a report server, right-click **Shared Schedule**, and select **New Schedule**. To save the schedule, SQL Server Agent service must be running.  
  
> [!NOTE]  
>  This feature is not available in every edition of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For a list of features that are supported by the editions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], see [Features supported by the editions of SQL Server 2012](/previous-versions/sql/sql-server-2012/cc645993(v=sql.110)) (https://go.microsoft.com/fwlink/?linkid=232473).  
  
## Options  
 **Name**  
 Enter a name for the shared schedule. This name appears in drop-down lists when users select a shared schedule for reports and subscriptions. Be sure to provide a descriptive name that fits easily within a list and that easily distinguishes one shared schedule from another. A name must contain at least one alphanumeric character. It can also include spaces and some symbols. Don't use the following characters when specifying a name:  
  
` ; ? : \@ & = + , $ / * < > " /  `
  
 **Begin running this schedule on**  
 Specify a start date for this schedule.  
  
 **Stop this schedule on**  
 Specify an expiration date for this schedule.  
  
 **Type**  
 Specifies whether the recurrence pattern is based primarily on hours, days, weeks, or months.  
  
 **Hour (Recurrence Pattern)**  
 Select options to run a scheduled operation in intervals of an hour (for example, to run a report every 6 hours). You can specify the interval in hours and minutes.  
  
 **Day (Recurrence Pattern)**  
 Select options to run a scheduled operation in intervals of days (for example, to run a report every 2 days). You can specify the interval in days and at the hour and minute you want the schedule to run.  
  
 **Week (Recurrence Pattern)**  
 Select options to run a scheduled operation in intervals of a week or when the pattern that you want to repeat is based on weeks (for example, to run a report every other week). You can specify a weekly schedule to the day, hour, and minute that you want the schedule to run.  
  
 **Month (Recurrence Pattern)**  
 Select options to run a scheduled operation in intervals of a month or when the pattern that you want to repeat is based on months. You can specify a monthly schedule to the day, hour, and minute that you want the schedule to run. You can omit specific months from the schedule.  
  
 **Once**  
 Select this option to create a schedule that runs only once, on a specific date and time.  
  
## Related content

- [Report server in Management Studio F1 Help](../../reporting-services/tools/report-server-in-management-studio-f1-help.md)
- [Connect to a report server in Management Studio](../../reporting-services/tools/connect-to-a-report-server-in-management-studio.md)
- [Create, modify, and delete schedules](../../reporting-services/subscriptions/create-modify-and-delete-schedules.md)
- [Schedules](../../reporting-services/subscriptions/schedules.md)
