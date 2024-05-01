---
title: "Pause and resume shared schedules"
description: In this article, learn how to pause and resume a shared schedule that's in use but not in progress. You can pause and resume in native mode or SharePoint mode.
author: maggiesMSFT
ms.author: maggies
ms.date: 03/14/2017
ms.service: reporting-services
ms.subservice: subscriptions
ms.topic: conceptual
ms.custom: updatefrequency5
helpviewer_keywords:
  - "pausing schedules"
  - "report-specific schedules [Reporting Services]"
  - "shared schedules [Reporting Services], resuming"
  - "resuming schedules"
  - "continuing schedules"
  - "schedules [Reporting Services], resuming"
  - "schedules [Reporting Services], pausing"
---
# Pause and resume shared schedules
  You can pause and resume a shared schedule that is in use. Pausing a shared schedule provides a way to temporarily freeze a schedule that is used to trigger report processing and subscriptions. Only shared schedules can be paused and resumed. You can't pause report-specific schedules.  
  
 You can't pause and resume report processing that is in progress. You can only pause and resume schedules that are in the scheduling queue of SQL Server Agent service. A job that is in progress is outside the scope of the scheduling engine. For more information, see [Manage a running process](../../reporting-services/subscriptions/manage-a-running-process.md)  
  
 While a shared schedule is paused, operations are allowed to lapse. After you resume a shared schedule, report and subscription processing occurs at the next scheduled time, using the local time of the server. The native mode report server or SharePoint service applications don't make up scheduled operations that were paused.  
  
 In this article:  
  
-   [Pause and resume shared schedules (native mode)](#bkmk_native)  
  
-   [Pause and resume shared schedules (SharePoint mode)](#bkmk_sharepoint)  
  
##  <a name="bkmk_native"></a> Pause and resume shared schedules (native mode)  
 To pause and resume a shared schedule, use the Schedules page in Report Manager. You can't use [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)]; it doesn't provide options for pausing and resuming schedules. For more information, see [Create, modify, and delete schedules](../../reporting-services/subscriptions/create-modify-and-delete-schedules.md).  
  
### Pause or resume a shared schedule  
  
1.  From Report Manager select, **Site Settings**.  
  
2.  Select **Schedules**.  
  
3.  Choose the schedule, and select **Pause** or **Resume** in the ribbon. If a Schedule is currently paused, the **Status** column contains **Paused**.  
  
##  <a name="bkmk_sharepoint"></a> Pause and resume shared schedules (SharePoint mode)  
 To pause and resume a shared schedule, use the Site Settings page or PowerShell. Schedules are managed per SharePoint site.  
  
### Pause or resume a shared schedule  
  
1.  Select **Site Actions**.  
  
2.  Select **Site Settings**.  
  
3.  In the Reporting Services section, select **Manage Shared Schedules**.  
  
4.  Choose the schedule, and select **Pause Selected Schedules** or **Run Selected Schedules**. If a Schedule is currently paused, the **Status** column contains **Paused**.  
  
## Related content 
 [Schedules](../../reporting-services/subscriptions/schedules.md)   
 [Create, modify, and delete schedules](../../reporting-services/subscriptions/create-modify-and-delete-schedules.md)   
 [Change time zones and clock settings on a report server](../../reporting-services/subscriptions/change-time-zones-and-clock-settings-on-a-report-server.md)   
 [Manage a running process](../../reporting-services/subscriptions/manage-a-running-process.md)  
  
  
