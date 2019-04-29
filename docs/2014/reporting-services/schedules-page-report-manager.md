---
title: "Schedules Page (Report Manager) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
ms.assetid: ef19d96e-9f00-4434-950e-152dda9c1ced
author: maggiesMSFT
ms.author: maggies
manager: kfile
---
# Schedules Page (Report Manager)
  Use the Schedules page to create, modify, delete, pause, or resume shared schedules. A shared schedule is a named schedule that you can create and manage separately from reports, subscriptions, and other processes that consume schedule information. Users can select shared schedules that you provide.  
  
 To delete, pause, or resume a shared schedule, select the check box next to the shared schedule that you want to modify.  
  
> [!NOTE]  
>  This feature is not available in every edition of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]. For a list of features that are supported by the editions of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)], see [Features Supported by the Editions of SQL Server 2014](../../2014/getting-started/features-supported-by-the-editions-of-sql-server-2014.md).  
  
## Navigation  
 Use the following procedure to navigate to this location in the user interface (UI).  
  
### To open the Schedules page  
  
1.  Open Report Manager.  
  
2.  At the top of the page, in the right-hand corner, click **Site Settings**. This opens the General Properties page of the site.  
  
3.  Select the **Schedules** tab.  
  
## Options  
 **New Schedule**  
 Click to open the Scheduling page, which is used to specify frequency information.  
  
 **Delete**  
 Click to remove a shared schedule.  
  
 **Pause**  
 Click to stop a shared schedule from running temporarily. Pausing a schedule prevents subscriptions and other scheduled processes from running.  
  
 **Resume**  
 Click to reinstate a shared schedule. Lapsed processes that were scheduled to run while the schedule was paused are not made up.  
  
 **Schedule**  
 Shows the shared schedules that are currently defined. Click a shared schedule to view or edit frequency information.  
  
 **Creator**  
 Shows the name of the user who created the shared schedule.  
  
 **Last Run, Next Run**  
 Shows when the shared schedule was last run and when it will run next.  
  
 **Status**  
 Shows whether a shared schedule is paused or active.  
  
## See Also  
 [Create, Modify, and Delete Schedules](subscriptions/create-modify-and-delete-schedules.md)   
 [Report Manager F1 Help](../../2014/reporting-services/report-manager-f1-help.md)  
  
  
