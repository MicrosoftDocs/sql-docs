---
title: "Change time zones and clock settings on a report server"
description: Change time zones & clock settings for a report server. You can't set a report server time zone, so set the computer's time zone or SharePoint region settings.
author: maggiesMSFT
ms.author: maggies
ms.date: 09/25/2024
ms.service: reporting-services
ms.subservice: subscriptions
ms.topic: conceptual
ms.custom:
  - updatefrequency5
helpviewer_keywords:
  - "time zones [Reporting Services]"
  - "clocks [Reporting Services]"
  - "schedules [Reporting Services], clock settings"
  - "schedules [Reporting Services], time zones"
---

# Change time zones and clock settings on a report server
  A report server always uses the local time of the computer on which the server is installed. You can't configure it to use a different time zone. If a client application points to a report server in a different time zone, the report server time zone is used to execute a scheduled operation. In Report Manager and SharePoint management pages, the time zone is indicated on each scheduling page so that you know exactly when a scheduled operation occurs. For example, the page for creating custom schedules notes, "Times are expressed in (UTC-08:00) Pacific time (US and Canada)."
  The report server creates a SQL Server Agent job that is used to trigger the schedule. When the Report Server and the SQL Server Agent are located on separate servers, the time zone must be the same on all servers.
  
> [!NOTE]
> The Report Server web portal shows all times it displays in the client's time zone regardless of the Report Server's clock settings.

## Change the time zone (native mode)  
 If you change the time zone on a computer hosting a report server, you must restart the Report Server service in order for the time zone change to take effect.  
  
 Timestamp values of existing report history snapshots are synchronized to the new time zone setting. If you generated a report history snapshot at 9:00 A.M., and then reset the time zone ahead one time zone, the timestamp on the generated snapshot changes from 9:00 A.M. to 10:00 A.M.  
  
 Schedules retain existing settings, except that they're mapped to the new time zone. For example, if a schedule runs at 2:00 A.M. Pacific Standard Time and you change the time zone to East Australia Standard Time, the schedule runs at 2:00 A.M. East Australia Standard Time.  
  
 Property timestamp values (for example, the time at which a folder or linked report item is created) aren't synchronized to a new time zone setting. If you create an item on June 25 at 9:00 A.M., and then reset the time zone or clock, the timestamp remains June 25 at 9:00 A.M.  
  
## Change the time zone (SharePoint mode)  
 The time zone configuration for [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] SharePoint mode is managed as part of the SharePoint regional settings. For more information, see [Regional settings (SharePoint Server 2010 (/previous-versions/office/sharepoint-server-2010/cc824907(v=office.14))](/previous-versions/office/sharepoint-server-2010/cc824907(v=office.14)).  
  
## Change the clock settings  
 Changing the computer clock has no effect on existing timestamp values. For example, if you move the clock forward an hour, the timestamps of report history snapshots don't change. There might be a delay of 10 seconds before the Scheduling and Delivery Processor uses the new setting. The actual delay might vary if you modified polling interval settings in the configuration files.  

## Related content

- [Start and stop the report server service](../../reporting-services/report-server/start-and-stop-the-report-server-service.md)
- [Schedules](../../reporting-services/subscriptions/schedules.md)
