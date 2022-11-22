---
title: "Create Report History (Reporting Services in SharePoint Integrated Mode) | Microsoft Docs"
description: In Reporting Services in SharePoint Integrated Mode, learn how to create a report history, which is a collection of report snapshots that you create over time.
ms.date: 03/01/2017
ms.service: reporting-services
ms.subservice: report-server


ms.topic: conceptual
helpviewer_keywords: 
  - "report history [Reporting Services], SharePoint"
ms.assetid: e57ec746-05ae-4ff6-8e39-6cde87310daa
author: maggiesMSFT
ms.author: maggies
---
# Create Report History (Reporting Services in SharePoint Integrated Mode)
  Report history is a collection of report snapshots that you create over time. Each snapshot is a copy of the report as it existed when it was created. It includes the layout and data that was current for the report when the snapshot was created. Rendering information is not stored with the snapshot. When you open a snapshot in report history, it opens in HTML in the Report Viewer Web Part. After it is rendered, you can export it to other application formats.  
  
 To create report history, the report must be able to run unattended (that is, the report server must be able to run the report without user interaction). You must have Edit Items permission on the library that contains the report. To view or delete report history, you must have View Versions or Delete Versions permissions.  
  
### To create a snapshot or report history on demand  
  
1.  Point to the report.  
  
2.  Click to display the down arrow, and then select **View Report History**.  
  
3.  Click **New Snapshot**. If the button is not visible, it is because you do not have permission to create snapshots in report history.  
  
4.  To view the snapshot you just created, select it from the list. Each snapshot is identified by a timestamp that shows when the snapshot was created. You cannot rename the snapshot, move it to another location, or modify it.  
  
### To schedule report history  
  
1.  Point to the report.  
  
2.  Click to display the down arrow, and then select **Manage Processing Options**.  
  
3.  In **History Snapshot Options**, click **Create report history snapshots on a schedule**.  
  
4.  If you have a shared schedule that contains the schedule information you want to use, click **On a shared schedule** and select the schedule you want to use. Otherwise, click **On a custom schedule**, and then click **Configure** to specify options to create report history on a recurring schedule.  
  
### To create report history when data is refreshed in a report  
  
1.  Point to the report.  
  
2.  Click to display the down arrow, and then select **Manage Processing Options**.  
  
3.  In **History Snapshot Options**, click **Store all report data snapshots in report history**.  
  
## See Also  
 [Set Processing Options &#40;Reporting Services in SharePoint Integrated Mode&#41;](../../reporting-services/report-server-sharepoint/set-processing-options-reporting-services-in-sharepoint-integrated-mode.md)  
  
  
