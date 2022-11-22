---
title: "Add a Snapshot to Report History - Reporting Services | Microsoft Docs"
description: Learn details about how to manually add a snapshot to report history in SQL Server Reporting Services (SSRS).
ms.service: reporting-services
ms.subservice: reporting-services
ms.topic: conceptual
author: maggiesMSFT 
ms.author: maggies
ms.reviewer: ""
ms.custom: ""
ms.date: 06/26/2019
---

# Add a Snapshot to Report History

Report history is a collection of report snapshots that you create over time. A report snapshot is a report that contains layout information and query results that were retrieved at a specific point in time. Unlike on-demand reports, which get up-to-date query results when you select the report, report snapshots are processed on a schedule and then saved to a report server. When you select a report snapshot for viewing, the report server retrieves the stored report from the report server database and shows the data and layout that were current for the report at the time the snapshot was created.  
  
Report snapshots are not saved in a particular rendering format. Instead, report snapshots are rendered in a final viewing format (such as HTML) only when a user or an application requests it. Deferred rendering makes a snapshot portable. The report can be rendered in the correct format for the requesting device or Web browser.  
  
## To manually add snapshots to report history
  
::: moniker range="=sql-server-2016"

1. In Report Manager, navigate to the **Contents** page, and hover over the item that you want to view history for, and click the drop-down arrow.
  
2. In the drop-down menu, click **View Report History**.  
  
3. Click **New Snapshot**. A new snapshot is created in the **When Run** column.  
    > [!NOTE]
    > To enable creating snapshots, the administrator must configure the report history to **Allow history to be created manually**. For more information, see [Limit Report History &#40;Report Manager&#41;](../reports/limit-report-history-report-manager.md).

4. Click **Apply**.
  
## To automatically add all snapshots to report history  
  
1. For a report that is already configured to run as a report execution snapshot, you can set additional properties to save a copy of the snapshot to report history each time the snapshot is refreshed.  
  
2. In Report Manager, navigate to the **Contents** page, hover over the item that you want to view history for, and click the drop-down arrow.  
  
3. In the drop-down menu, click **Manage**.  
  
4. Click **Snapshot Options**.  
  
5. Select the check box for **Store all report execution snapshots in history**.  
  
6. Click **Apply**.  
  
## To automatically add snapshots to report history based on a schedule  
  
1. In Report Manager, navigate to the **Contents** page, and hover over the item that you want to view history for, and click the drop-down arrow.  
  
2. In the drop-down menu, click **Manage**.  
  
3. Click **Snapshot Options**.  
  
4. Select the check box for **Use the following schedule to add snapshots to report history**. Perform one of the following:  
  
    - Select **Report-specific schedule**. Fill in the schedule details, select the start and end dates for the schedule, and then click **OK**.  

    - Select **Shared schedule**. From the list, select the preferred schedule.  

5. Click **Apply**.  
  
## See also

- [Configure Execution Properties for a Report  &#40;Report Manager&#41;](../../reporting-services/reports/configure-execution-properties-for-a-report-report-manager.md)
- [Limit Report History &#40;Report Manager&#41;](../../reporting-services/reports/limit-report-history-report-manager.md)
- [Schedules](../../reporting-services/subscriptions/schedules.md)   
- [Report Manager  &#40;SSRS Native Mode&#41;](../web-portal-ssrs-native-mode.md)

::: moniker-end

::: moniker range=">=sql-server-2017"

1. In the web portal, navigate to the item that you want to view history for and right-click it.  
  
2. In the drop-down menu, select **Manage**.  
  
3. Select the **History snapshots** tab.  
  
4. On the **History snapshots** page, select the **New history snapshot**. A new snapshot is created and appears below with the current date and time in the **Created** column.  
  
    > [!NOTE]
    > To enable creating snapshots, the administrator must configure the report history to **Allow history to be created manually**. For more information, see [Limit Report History (web portal)](../../reporting-services/reports/limit-report-history-report-manager.md).

## To add snapshots via a schedule to report history

1. In the web portal, navigate to the item that you want to view history for and right-click it.  
  
2. In the drop-down menu, select **Manage**.  
  
3. Select the **History snapshots** tab.  
  
4. On the **History snapshots** page, select the **Schedule and settings** button.  
  
5. In the **Schedule** section, select one or both of the options below if at least one choice is not already selected:
    - **Create history snapshots on a schedule**.  
    - **Allow people to create snapshots manually**.  
  
6. In the **Advanced** section, select **Retain all history snapshots**.  
  
7. Optionally select the checkbox for **Save cache snapshots in report history as well**.  
  
8.  Select **Apply** to save the settings.  

    > [!NOTE]  
    > To enable creating snapshots, the administrator must configure the report history to **Allow history to be created manually**. For more information, see [Limit Report History (web portal)](../../reporting-services/reports/limit-report-history-report-manager.md).

9.  Click **Apply**.

## To automatically add all snapshots to report history  
  
1. For a report that is already configured to run as a report execution snapshot, you can set additional properties to save a copy of the snapshot to report history each time the snapshot is refreshed.  
  
2. In the web portal, navigate to the item that you want to view history for and right-click it.  
  
3. In the drop-down menu, select **Manage**.  
  
4. Select the **History snapshots** tab.  
  
5. On the **History snapshots** page, select the **Schedule and settings** button.  
  
6. In the **Schedule** section, select one or both of the options below if at least one choice is not already selected:
    - **Create history snapshots on a schedule**.  
    - **Allow people to create snapshots manually**.  
  
7. In the **Advanced** section, select **Retain all history snapshots**.  
  
8. Optionally select the checkbox for **Save cache snapshots in report history as well**.  
  
9. Select **Apply** to save the settings.  
  
## To automatically add snapshots to report history based on a schedule  
  
1. In the web portal, navigate to the item that you want to view history for and right-click it.  
  
2. In the drop-down menu, select **Manage**.  
  
3. Select the **History snapshots** tab.  
  
4. On the **History snapshots** page, select the **Schedule and settings** button.  
  
5. Select the check box for **Use the following schedule to add snapshots to report history**. Perform one of the following:  
  
    - Select **Report-specific schedule**. Fill in the schedule details, select the start and end dates for the schedule, and then click **OK**.  

    - Select **Shared schedule**. From the list, select the preferred schedule.  

5. Click **Apply**.  
  
## See also

- [Configure Execution Properties for a Report (web portal)](../../reporting-services/reports/configure-execution-properties-for-a-report-report-manager.md)
- [Limit Report History (web portal)](../../reporting-services/reports/limit-report-history-report-manager.md)
- [Schedules](../../reporting-services/subscriptions/schedules.md)   
- [the web portal  &#40;SSRS Native Mode&#41;](../web-portal-ssrs-native-mode.md)

::: moniker-end