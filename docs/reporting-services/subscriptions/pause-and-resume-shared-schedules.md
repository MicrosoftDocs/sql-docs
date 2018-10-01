---
title: "Pause and Resume Shared Schedules | Microsoft Docs"
ms.date: 03/14/2017
ms.prod: reporting-services
ms.prod_service: "reporting-services-sharepoint, reporting-services-native"
ms.technology: subscriptions


ms.topic: conceptual
helpviewer_keywords: 
  - "pausing schedules"
  - "report-specific schedules [Reporting Services]"
  - "shared schedules [Reporting Services], resuming"
  - "resuming schedules"
  - "continuing schedules"
  - "schedules [Reporting Services], resuming"
  - "schedules [Reporting Services], pausing"
ms.assetid: e416be75-5234-4aa6-a3de-77f60f25169a
author: markingmyname
ms.author: maghan
---
# Pause and Resume Shared Schedules
  You can pause and resume a shared schedule that is in use. Pausing a shared schedule provides a way to temporarily freeze a schedule that is used to trigger report processing and subscriptions. Only shared schedules can be paused and resumed. You cannot pause report-specific schedules.  
  
 You cannot pause and resume report processing that is in progress. You can only pause and resume schedules that are in the scheduling queue of SQL Server Agent service. A job that is in progress is outside the scope of the scheduling engine. For more information, see [Manage a Running Process](../../reporting-services/subscriptions/manage-a-running-process.md)  
  
 While a shared schedule is paused, any operations that would have occurred are allowed to lapse. After you resume a shared schedule, report and subscription processing occurs at the next scheduled time, using the local time of the server. The native mode report server or SharePoint service applications, do not make up scheduled operations that would have occurred had the schedule not been paused.  
  
 In this Topic:  
  
-   [Pause and Resume Shared Schedules (Native Mode)](#bkmk_native)  
  
-   [Pause and Resume Shared Schedules (SharePoint mode)](#bkmk_sharepoint)  
  
##  <a name="bkmk_native"></a> Pause and Resume Shared Schedules (Native Mode)  
 To pause and resume a shared schedule, use the Schedules page in Report Manager. You cannot use [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)]; it does not provide options for pausing and resuming schedules. For more information, see [Create, Modify, and Delete Schedules](../../reporting-services/subscriptions/create-modify-and-delete-schedules.md).  
  
#### To pause or resume a shared schedule  
  
1.  From Report Manager Click, **Site Settings**.  
  
2.  Click **Schedules**.  
  
3.  Select the schedule, and click **Pause** or **Resume** in the ribbon. If a Schedule is currently paused, the **Status** column will contain **Paused**.  
  
##  <a name="bkmk_sharepoint"></a> Pause and Resume Shared Schedules (SharePoint mode)  
 To pause and resume a shared schedule, use the Site Settings page or PowerShell. Schedules are managed per SharePoint site.  
  
#### To pause or resume a shared schedule  
  
1.  Click **Site Actions**.  
  
2.  Click **Site Settings**.  
  
3.  In the Reporting Services section, click **Manage Shared Schedules**.  
  
4.  Select the schedule, and click **Pause Selected Schedules** or **Run Selected Schedules**. If a Schedule is currently paused, the **Status** column will contain **Paused**.  
  
## See Also  
 [Schedules](../../reporting-services/subscriptions/schedules.md)   
 [Create, Modify, and Delete Schedules](../../reporting-services/subscriptions/create-modify-and-delete-schedules.md)   
 [Change Time Zones and Clock Settings on a Report Server](../../reporting-services/subscriptions/change-time-zones-and-clock-settings-on-a-report-server.md)   
 [Manage a Running Process](../../reporting-services/subscriptions/manage-a-running-process.md)  
  
  
