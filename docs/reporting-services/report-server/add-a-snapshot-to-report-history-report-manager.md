---
title: "Add a snapshot to report history - Reporting Services"
description: Learn details about how to manually add a snapshot to report history in SQL Server Reporting Services (SSRS).
author: maggiesMSFT
ms.author: maggies
ms.date: 06/26/2019
ms.service: reporting-services
ms.subservice: reporting-services
ms.topic: conceptual
ms.custom: updatefrequency5
---

# Add a snapshot to report history

Report history is a collection of report snapshots that you create over time. A report snapshot is a report that contains layout information and query results retrieved at a specific point in time. Unlike on-demand reports, which get up-to-date query results when you select the report, report snapshots are processed on a schedule and then saved to a report server. When you select a report snapshot for viewing, the report server retrieves the stored report from the report server database. Then, it shows the data and layout that were current for the report at the time the snapshot was created.  
  
Report snapshots aren't saved in a particular rendering format. Instead, report snapshots are rendered in a final viewing format (such as HTML) only when a user or an application requests it. Deferred rendering makes a snapshot portable. The report can be rendered in the correct format for the requesting device or Web browser.  
  
## Manually add snapshots to report history
  
::: moniker range="=sql-server-2016"

1. In Report Manager, navigate to the **Contents** page. Hover over the item that you want to view history for, and select the arrow.
  
1. In the menu, choose **View Report History**.  
  
1. Select **New Snapshot**. A new snapshot is created in the **When Run** column.  
    > [!NOTE]
    > To enable creating snapshots, the administrator must configure the report history to **Allow history to be created manually**. For more information, see [Limit Report History &#40;Report Manager&#41;](../reports/limit-report-history-report-manager.md).

1. Select **Apply**.
  
## Automatically add all snapshots to report history  
  
1. For a report that is already configured to run as a report execution snapshot, you can set other properties to save a copy of the snapshot to report history each time you refresh the snapshot.  
  
1. In Report Manager, navigate to the **Contents** page. Hover over the item that you want to view history for, and select the arrow.  
  
1. In the menu, choose **Manage**.  
  
1. Select **Snapshot Options**.  
  
1. Select the **Store all report execution snapshots in history** checkbox.  
  
1. Select **Apply**.  
  
## Automatically add snapshots to report history based on a schedule  
  
1. In Report Manager, navigate to the **Contents** page. Hover over the item that you want to view history for, and select the arrow.  
  
1. In the menu, choose **Manage**.  
  
1. Select **Snapshot Options**.  
  
1. Select the **Use the following schedule to add snapshots to report history** checkbox. Perform one of the following actions:  
  
    - Select **Report-specific schedule**. Fill in the schedule details, select the start and end dates for the schedule, and then choose **OK**.  

    - Select **Shared schedule**. From the list, choose the schedule you prefer.  

1. Select **Apply**.  
  
## Related content

- [Configure execution properties for a report  &#40;report manager&#41;](../../reporting-services/reports/configure-execution-properties-for-a-report-report-manager.md)
- [Limit report history &#40;report manager&#41;](../../reporting-services/reports/limit-report-history-report-manager.md)
- [Schedules](../../reporting-services/subscriptions/schedules.md)   
- [Report manager  &#40;SSRS native mode&#41;](../web-portal-ssrs-native-mode.md)

::: moniker-end

::: moniker range=">=sql-server-2017"

1. In the web portal, navigate to the item that you want to view history for and right-click it.  
  
1. In the menu, choose **Manage**.  
  
1. Select the **History snapshots** tab.  
  
1. On the **History snapshots** page, select the **New history snapshot**. A new snapshot appears with the current date and time in the **Created** column.  
  
    > [!NOTE]
    > To enable creating snapshots, the administrator must configure the report history to **Allow history to be created manually**. For more information, see [Limit report history (web portal)](../../reporting-services/reports/limit-report-history-report-manager.md).

## Add snapshots via a schedule to report history

1. In the web portal, navigate to the item that you want to view history for and right-click it.  
  
1. In the menu, choose **Manage**.  
  
1. Select the **History snapshots** tab.  
  
1. On the **History snapshots** page, select **Schedule and settings**.  
  
1. In the **Schedule** section, select one or both of the following options if at least one choice isn't selected:
    - **Create history snapshots on a schedule**
    - **Allow people to create snapshots manually**  
  
1. In the **Advanced** section, select **Retain all history snapshots**.  
  
1. Optionally, select the **Save cache snapshots in report history as well** checkbox.  
  
1. Select **Apply** to save the settings.  

    > [!NOTE]  
    > To enable creating snapshots, the administrator must configure the report history to **Allow history to be created manually**. For more information, see [Limit report history (web portal)](../../reporting-services/reports/limit-report-history-report-manager.md).

1.  Select **Apply**.

## Automatically add all snapshots to report history  
  
1. For a report that is already configured to run as a report execution snapshot, you can set other properties to save a copy of the snapshot to report history each time you refresh the snapshot.  
  
1. In the web portal, navigate to the item that you want to view history for and right-click it.  
  
1. In the menu, choose **Manage**.  
  
1. Select the **History snapshots** tab.  
  
1. On the **History snapshots** page, select **Schedule and settings**.  
  
1. In the **Schedule** section, select one or both of the following options if at least one choice isn't selected:
    - **Create history snapshots on a schedule**
    - **Allow people to create snapshots manually**  
  
1. In the **Advanced** section, select **Retain all history snapshots**.  
  
1. Optionally, select the **Save cache snapshots in report history as well** checkbox.  
  
1. Select **Apply** to save the settings.  
  
## Automatically add snapshots to report history based on a schedule  
  
1. In the web portal, navigate to the item that you want to view history for and right-click it.  
  
1. In the menu, choose **Manage**.  
  
1. Select the **History snapshots** tab.  
  
1. On the **History snapshots** page, select **Schedule and settings**.  
  
1. Select the **Use the following schedule to add snapshots to report history** checkbox. Perform one of the following actions:  
  
    - Select **Report-specific schedule**. Fill in the schedule details, choose the start and end dates for the schedule, and then select **OK**.  

    - Select **Shared schedule**. From the list, choose the preferred schedule.  

1. Select **Apply**.  
  
## Related content

- [Configure execution properties for a report (web portal)](../../reporting-services/reports/configure-execution-properties-for-a-report-report-manager.md)
- [Limit report history (web portal)](../../reporting-services/reports/limit-report-history-report-manager.md)
- [Schedules](../../reporting-services/subscriptions/schedules.md)   
- [The web portal  &#40;SSRS native mode&#41;](../web-portal-ssrs-native-mode.md)

::: moniker-end
