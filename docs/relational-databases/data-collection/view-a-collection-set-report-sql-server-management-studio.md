---
description: "View a Collection Set Report (SQL Server Management Studio)"
title: "View a Collection Set Report (SSMS)"
ms.custom: ""
ms.date: "03/01/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: supportability
ms.topic: conceptual
f1_keywords: 
  - "sql13.swb.dc.reporthistory.calendar.f1"
helpviewer_keywords: 
  - "collection sets [SQL Server], viewing reports"
  - "reports [SQL Server], viewing collection set"
ms.assetid: c3b1e791-9aa1-4bba-9622-4954568e1820
author: MashaMSFT
ms.author: mathoma
---
# View a Collection Set Report (SQL Server Management Studio)
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  After you have configured the management data warehouse, you can view a collection set report in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)]. Reports are provided for the System Data collection sets that are installed during Setup. The reports include the following:  
  
-   Disk Usage Summary  
  
-   Query Statistics History  
  
-   Server Activity History  
  
 This procedure displays the report for the **Disk Usage** collection set. You can follow the same general procedure to view the reports for the other System Data collection sets.  
  
### To view a collection set report  
  
1.  The tables for a report are created the first time that the collected data is uploaded. If you try to view a report before this first upload, an error occurs and no report is displayed. To upload data for the Disk Usage collection set, in Object Explorer, expand the **Management** folder, expand **Data Collection**, expand **System Data Collection Sets**, right-click the **Disk Usage** collection set, and then click **Collect and Upload Now**.  
  
2.  To view the report, in Object Explorer, expand the **Management** folder, right-click **Data Collection**, point to **Reports**, point to **Management Data Warehouse**, and then click **Disk Usage Summary**.  
  
    > [!NOTE]  
    >  Some reports might display a calendar button under the data collection timeline. Click this button to access the **Data Collection Report Calendar**.  
  
#### Data Collection Report Calendar  
 Use this dialog box to specify the start date, start time, and duration of the data that you want to report on. For example, you may want to report on the disk usage activity of a server for a specific 12-hour period last Wednesday.  
  
 **Start date**  
 Enter a beginning date for the report data, or select one from the calendar.  
  
 **Start time**  
 Enter a beginning time for the report data or specify one by clicking the arrows.  
  
 **Duration**  
 Specify the time range to include in the report. The default value is 240 minutes. The possible values to select from are 15 minutes, 60 minutes, 240 minutes (4 hours), 720 minutes (12 hours), and 1440 minutes (24 hours).  
  
## See Also  
 [Data Collection](../../relational-databases/data-collection/data-collection.md)   
 [Custom Reports in Management Studio](../../ssms/object/custom-reports-in-management-studio.md)   
 [Configure the Management Data Warehouse &#40;SQL Server Management Studio&#41;](../../relational-databases/data-collection/configure-the-management-data-warehouse-sql-server-management-studio.md)  
  
  
