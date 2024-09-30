---
title: "Create report history (Reporting Services in SharePoint integrated mode)"
description: In Reporting Services in SharePoint integrated mode, learn how to create a report history, which is a collection of report snapshots that you create over time.
author: maggiesMSFT
ms.author: maggies
ms.date: 09/25/2024
ms.service: reporting-services
ms.subservice: report-server
ms.topic: conceptual
ms.custom:
  - updatefrequency5
helpviewer_keywords:
  - "report history [Reporting Services], SharePoint"
---
# Create report history (Reporting Services in SharePoint integrated mode)
  Report history is a collection of report snapshots that you create over time. Each snapshot is a copy of the report as it existed when it was created. It includes the layout and data that was current for the report when the snapshot was created. Rendering information isn't stored with the snapshot. When you open a snapshot in report history, it opens in HTML in the Report Viewer Web Part. After the snapshot is rendered, you can export it to other application formats.  
  
 To create report history, the report must be able to run unattended (that is, the report server must be able to run the report without user interaction). You must have Edit Items permission on the library that contains the report. To view or delete report history, you must have View Versions or Delete Versions permissions.  
  
### Create a snapshot or report history on demand  
  
1.  Point to the report.  
  
1.  Choose the down arrow, and then select **View Report History**.  
  
1.  Select **New Snapshot**. If the button isn't visible, it's because you don't have permission to create snapshots in report history.  
  
1.  To view the snapshot you created, select it from the list. You can identify each snapshot by a timestamp that shows when the snapshot was created. You can't rename the snapshot. Move it to another location, or modify it.  
  
### Schedule report history  
  
1.  Point to the report.  
  
1.  Choose the down arrow, and then select **Manage Processing Options**.  
  
1.  In **History Snapshot Options**, select **Create report history snapshots on a schedule**.  
  
1.  If you have a shared schedule that contains the schedule information you want to use, choose **On a shared schedule** and select the schedule you want to use. Otherwise, choose **On a custom schedule**, and then select **Configure** to specify options to create report history on a recurring schedule.  
  
### Create report history when data is refreshed in a report  
  
1.  Point to the report.  
  
1.  Choose the down arrow, and then select **Manage Processing Options**.  
  
1.  In **History Snapshot Options**, select **Store all report data snapshots in report history**.  
  
## Related content

- [Set processing options &#40;Reporting Services in SharePoint integrated mode&#41;](../../reporting-services/report-server-sharepoint/set-processing-options-reporting-services-in-sharepoint-integrated-mode.md)
