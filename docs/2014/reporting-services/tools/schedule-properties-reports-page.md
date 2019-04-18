---
title: "Schedule Properties (Reports Page) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
f1_keywords: 
  - "sql12.swb.reportserver.scheduleproperties.reports.f1"
ms.assetid: 7db728bd-4b08-43ef-a49a-e8dcdd37cf89
author: maggiesMSFT
ms.author: maggies
manager: kfile
---
# Schedule Properties (Reports Page)
  Use this page to view a list of all reports that use this shared schedule. Schedules can be used to refresh report snapshots, generate report history, trigger a subscription, or expire a cached copy of the report. To find out how the schedule is used, view the property and subscription information of the report.  
  
 Although this page shows each report that uses the shared schedule, it does not indicate how many times the shared schedule is used within that single report. For example, suppose 20 different subscribers to the Company Sales report all use the same shared schedule to trigger subscription processing. In this case, the Company Sales report will only appear once in this list, even though the report has 20 references to the shared schedule.  
  
 To open this page, start [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], connect to a report server, open the **Shared Schedules** folder, right-click a shared schedule, select **Properties**, and then click **Reports**.  
  
> [!NOTE]  
>  This feature is not available in every edition of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For a list of features that are supported by the editions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], see [Features Supported by the Editions of SQL Server 2012](https://go.microsoft.com/fwlink/?linkid=232473) (https://go.microsoft.com/fwlink/?linkid=232473).  
  
## Options  
 **Folder**  
 Specifies the path of the report.  
  
 **Report**  
 Specifies the name of the report that uses the schedule.  
  
## See Also  
 [Create, Modify, and Delete Schedules](../subscriptions/create-modify-and-delete-schedules.md)   
 [Schedules](../subscriptions/schedules.md)   
 [Report Server in Management Studio F1 Help](report-server-in-management-studio-f1-help.md)   
 [Connect to a Report Server in Management Studio](connect-to-a-report-server-in-management-studio.md)   
 [Configure General Properties for a Report &#40;Report Manager&#41;](../configure-general-properties-for-a-report-report-manager.md)  
  
  
