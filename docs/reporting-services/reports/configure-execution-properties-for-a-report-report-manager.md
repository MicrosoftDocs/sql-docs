---
title: "Configure Execution Properties for a Report - Reporting Services | Microsoft Docs"
description: Learn how to set report processing options to specify when to retrieve report data to avoid the overhead of retrieving the same data each time a report is requested. 
ms.date: 06/26/2019
ms.service: reporting-services
ms.subservice: reports


ms.topic: conceptual
helpviewer_keywords: 
  - "report execution properties [Reporting Services]"
  - "reports [Reporting Services], properties"
  - "reports [Reporting Services], execution options"
ms.assetid: 73cc8dcc-ef80-40d7-9739-d33bba0eb28a
author: maggiesMSFT
ms.author: maggies
---
# Configure Execution Properties for a Report
  You can set report processing options to specify when data is retrieved for a report. It is useful to schedule data processing for a report if the external data source is refreshed at specific times (for example, a data warehouse that is refreshed daily or weekly) and you want to avoid the overhead of retrieving the same data each time a report is requested. Scheduling data processing is also useful if you want to control the processing load on the external database server or when you want to provide consistent results for multiple users who must work with identical sets of data. With volatile data, an on-demand report can produce different results from one minute to the next. A report snapshot, by contrast, allows you to make valid comparisons against other reports or analytical tools that contain data from the same point in time.  
  
 A report snapshot is a report that contains layout instructions and query results that were retrieved at a specific point in time. Unlike on-demand reports, which get up-to-date query results when you select the report, report snapshots are processed on a schedule and then saved to a report server. When you select a report snapshot for viewing, the report server retrieves the stored report from the report server database and shows the data and layout that were current for the report at the time the snapshot was created.  
  
 Report snapshots are not saved in a particular rendering format. Instead, report snapshots are rendered in a final viewing format (such as HTML) only when a user or an application requests it. Deferred rendering makes a snapshot portable. The report can be rendered in the correct format for the requesting device or Web browser.  

::: moniker range="=sql-server-2016"
  
## To configure report processing options  
  
1.  Start [Report Manager  &#40;SSRS Native Mode&#41;](../web-portal-ssrs-native-mode.md).  
  
2.  Navigate to and open the report for which you want to set processing options.  
  
 Hover over the report, and click the drop-down arrow.  
  
1.  In the drop-down menu, click **Manage** and then select the **Processing Options** tab.  
  
2.  Click **Render this report from an execution snapshot, and then select one of the following options:**  
  
    -   If you want to create a snapshot, select **Use the following schedule to create report execution snapshots**, and then either define a report-specific schedule or select from the **Shared schedule** list.  
  
    -   If you want to create a snapshot immediately, select **Create a report snapshot when you click the Apply button on this page**.  
  
3.  Click **Apply**.  
  
## See Also  
 [Set Report Processing Properties](../../reporting-services/report-server/set-report-processing-properties.md)   
 [Contents Page &#40;Report Manager&#41;](/previous-versions/sql/sql-server-2016/ms186470(v=sql.130))   
 [Report Server Content Management &#40;SSRS Native Mode&#41;](../../reporting-services/report-server/report-server-content-management-ssrs-native-mode.md)   
 [Processing Options Properties Page &#40;Report Manager&#41;](/previous-versions/sql/sql-server-2016/ms178821(v=sql.130))  
  
::: moniker-end

::: moniker range=">=sql-server-2017"
  
## To configure report execution properties  
  
From [the web portal of a report server (SSRS Native Mode)](../../reporting-services/web-portal-ssrs-native-mode.md):  
  
1. Navigate to the report for which you want to configure the execution properties.  
  
2. Right-click the report and select **Manage** from the drop-down menu.

3. Select the **History snapshots** tab to display the **History snapshots** page.  
  
4. Select **Schedules and settings** button, and check **Create history snapshots on a schedule** if it is not already checked.
  
5. Select either a **Shared schedule** or a **Report-specific schedule** as desired.  
  
6. In the **Advanced** section, select the desired **Retention** policy for the history snapshots.  
  
7. Select **Apply**.  
  
   >[!NOTE]
   >If you want to create a snapshot immediately, select the **New history snapshot** button instead of the **Schedules and settings** button, and a report snapshot will be created immediately.  
  
## See also  
 [Set Report Processing Properties](../../reporting-services/report-server/set-report-processing-properties.md)   
 [Report Server Content Management (SSRS Native Mode)](../../reporting-services/report-server/report-server-content-management-ssrs-native-mode.md)   
 [Set Report Processing Properties](../../reporting-services/report-server/set-report-processing-properties.md)   

::: moniker-end