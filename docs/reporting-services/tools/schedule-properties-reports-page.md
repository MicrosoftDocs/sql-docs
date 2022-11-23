---
title: "Schedule Properties (Reports Page) | Microsoft Docs"
description: Learn about the Reporting Services schedule properties page in SQL Server Management Studio that lists all reports for a specific shared schedule.
ms.date: 06/30/2016
ms.service: reporting-services
ms.subservice: tools


ms.topic: conceptual
f1_keywords: 
  - "sql13.swb.reportserver.scheduleproperties.reports.f1"
ms.assetid: 7db728bd-4b08-43ef-a49a-e8dcdd37cf89
author: maggiesMSFT
ms.author: maggies
---
# Schedule Properties (Reports Page)
  Use the [!INCLUDE[ssRSnoversion_md](../../includes/ssrsnoversion-md.md)] schedule properties page in [!INCLUDE[ssManStudioFull_md](../../includes/ssmanstudiofull-md.md)] to view a list of all reports that use the specific shared schedule. Schedules can be used to refresh report snapshots, generate report history, trigger a subscription, or expire a cached copy of the report. To find out how the schedule is used, view the property and subscription information of the report.  
  
 Although this page shows each report that uses the shared schedule, it does not indicate how many times the shared schedule is used within that single report. For example, suppose 20 different subscribers to the Company Sales report all use the same shared schedule to trigger subscription processing. In this case, the Company Sales report will only appear once in this list, even though the report has 20 references to the shared schedule.  
  
 To open the schedule properties page:
 1. Start [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)].
 2. Connect to a report server.
 3. Open the **Shared Schedules** folder.
 4. Right-click a shared schedule, select **Properties**.
 5. click **Reports**.  
  
  You can also manage shared schedules from the **Site Settings** of the [!INCLUDE[ssRSnoversion_md](../../includes/ssrsnoversion-md.md)] Web Portal.
  
> [!NOTE]  
>  This feature is not available in every edition of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For a list of features that are supported by the editions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], see [Editions and supported features of SQL Server 2017](../../sql-server/editions-and-components-of-sql-server-2017.md).  
  
## Options  
 **Folder**  
 Specifies the path of the report.  
  
 **Report**  
 Specifies the name of the report that uses the schedule.  
  
## See Also  
 [Create, Modify, and Delete Schedules](../../reporting-services/subscriptions/create-modify-and-delete-schedules.md)   
 [Schedules](../../reporting-services/subscriptions/schedules.md)   
 [Report Server in Management Studio F1 Help](../../reporting-services/tools/report-server-in-management-studio-f1-help.md)   
 [Connect to a Report Server in Management Studio](../../reporting-services/tools/connect-to-a-report-server-in-management-studio.md)   
 [Configure General Properties for a Report (Report Manager)](../reports/configure-execution-properties-for-a-report-report-manager.md)  
  
