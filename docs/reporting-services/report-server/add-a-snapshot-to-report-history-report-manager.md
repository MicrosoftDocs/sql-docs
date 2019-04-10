---
title: "Add a Snapshot to Report History (Report Manager) | Microsoft Docs"
ms.date: 03/01/2017
ms.prod: reporting-services
ms.prod_service: "reporting-services-sharepoint, reporting-services-native"
ms.technology: report-server


ms.topic: conceptual
helpviewer_keywords: 
  - "report history [Reporting Services], adding snapshots"
  - "historical data [Reporting Services]"
  - "snapshots [Reporting Services], adding report snapshots"
  - "adding snapshots to report history"
  - "report snapshots [Reporting Services], adding"
ms.assetid: 3aafb183-789e-46ac-966c-881dc549b31d
author: markingmyname
ms.author: maghan
---
# Add a Snapshot to Report History (Report Manager)
  Report history is a collection of report snapshots that you create over time. A report snapshot is a report that contains layout information and query results that were retrieved at a specific point in time. Unlike on-demand reports, which get up-to-date query results when you select the report, report snapshots are processed on a schedule and then saved to a report server. When you select a report snapshot for viewing, the report server retrieves the stored report from the report server database and shows the data and layout that were current for the report at the time the snapshot was created.  
  
 Report snapshots are not saved in a particular rendering format. Instead, report snapshots are rendered in a final viewing format (such as HTML) only when a user or an application requests it. Deferred rendering makes a snapshot portable. The report can be rendered in the correct format for the requesting device or Web browser.  
  
### To manually add snapshots to report history  (SSRS 2017)
1.  In Report Manager, navigate to the **Contents** page, and hover over the item that you want to view history for, and click the elipses (...).

2.  In the drop-down menu, click **View Report History**.  

3.  Click **New history snapshot**. A new snapshot is created and listed.  
  
    > [!NOTE]  
    >  In order to do this, the report history must be configured by the administrator to **Allow history to be created manually**. For more information, see [Limit Report History &#40;Report Manager&#41;](../../reporting-services/reports/limit-report-history-report-manager.md).  
  
4.  Click **Apply**.  

### To manually add snapshots to report history  
  
1.  In Report Manager, navigate to the **Contents** page, and hover over the item that you want to view history for, and click the drop-down arrow.  
  
2.  In the drop-down menu, click **View Report History**.  
  
3.  Click **New Snapshot**. A new snapshot is created in the **When Run** column.  
  
    > [!NOTE]  
    >  In order to do this, the report history must be configured by the administrator to **Allow history to be created manually**. For more information, see [Limit Report History &#40;Report Manager&#41;](../../reporting-services/reports/limit-report-history-report-manager.md).  
  
4.  Click **Apply**.  
  
### To automatically add all snapshots to report history  
  
1.  For a report that is already configured to run as a report execution snapshot, you can set additional properties to save a copy of the snapshot to report history each time the snapshot is refreshed.  
  
2.  In Report Manager, navigate to the **Contents** page, hover over the item that you want to view history for, and click the drop-down arrow.  
  
3.  In the drop-down menu, click **Manage**.  
  
4.  Click **Snapshot Options**.  
  
5.  Select the check box for **Store all report execution snapshots in history**.  
  
6.  Click **Apply**.  
  
### To automatically add snapshots to report history based on a schedule  
  
1.  In Report Manager, navigate to the **Contents** page, and hover over the item that you want to view history for, and click the drop-down arrow.  
  
2.  In the drop-down menu, click **Manage**.  
  
3.  Click **Snapshot Options**.  
  
4.  Select the check box for **Use the following schedule to add snapshots to report history**. Perform one of the following:  
  
    -   Select **Report-specific schedule**. Fill in the schedule details, select the start and end dates for the schedule, and then click **OK**.  
  
    -   Select **Shared schedule**. From the list, select the preferred schedule.  
  
5.  Click **Apply**.  
  
## See Also  
 [Configure Execution Properties for a Report  &#40;Report Manager&#41;](../../reporting-services/reports/configure-execution-properties-for-a-report-report-manager.md)   
 [Open and Close a Report &#40;Report Manager&#41;](../../reporting-services/reports/open-and-close-a-report-report-manager.md)   
 [Limit Report History &#40;Report Manager&#41;](../../reporting-services/reports/limit-report-history-report-manager.md)   
 [Schedules](../../reporting-services/subscriptions/schedules.md)   
 [Report Manager  &#40;SSRS Native Mode&#41;](https://msdn.microsoft.com/library/80949f9d-58f5-48e3-9342-9e9bf4e57896)  
  
  
