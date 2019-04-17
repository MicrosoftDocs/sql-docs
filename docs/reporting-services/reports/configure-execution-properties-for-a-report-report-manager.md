---
title: "Configure Execution Properties for a Report  (Report Manager) | Microsoft Docs"
ms.date: 03/01/2017
ms.prod: reporting-services
ms.prod_service: "reporting-services-sharepoint, reporting-services-native"
ms.technology: reports


ms.topic: conceptual
helpviewer_keywords: 
  - "report execution properties [Reporting Services]"
  - "reports [Reporting Services], properties"
  - "reports [Reporting Services], execution options"
ms.assetid: 73cc8dcc-ef80-40d7-9739-d33bba0eb28a
author: markingmyname
ms.author: maghan
---
# Configure Execution Properties for a Report  (Report Manager)
  You can set report processing options to specify when data is retrieved for a report. It is useful to schedule data processing for a report if the external data source is refreshed at specific times (for example, a data warehouse that is refreshed daily or weekly) and you want to avoid the overhead of retrieving the same data each time a report is requested. Scheduling data processing is also useful if you want to control the processing load on the external database server or when you want to provide consistent results for multiple users who must work with identical sets of data. With volatile data, an on-demand report can produce different results from one minute to the next. A report snapshot, by contrast, allows you to make valid comparisons against other reports or analytical tools that contain data from the same point in time.  
  
 A report snapshot is a report that contains layout instructions and query results that were retrieved at a specific point in time. Unlike on-demand reports, which get up-to-date query results when you select the report, report snapshots are processed on a schedule and then saved to a report server. When you select a report snapshot for viewing, the report server retrieves the stored report from the report server database and shows the data and layout that were current for the report at the time the snapshot was created.  
  
 Report snapshots are not saved in a particular rendering format. Instead, report snapshots are rendered in a final viewing format (such as HTML) only when a user or an application requests it. Deferred rendering makes a snapshot portable. The report can be rendered in the correct format for the requesting device or Web browser.  
  
### To configure report processing options  
  
1.  Start [Report Manager  &#40;SSRS Native Mode&#41;](https://msdn.microsoft.com/library/80949f9d-58f5-48e3-9342-9e9bf4e57896).  
  
2.  Navigate to and open the report for which you want to set processing options.  
  
 Hover over the report, and click the drop-down arrow.  
  
1.  In the drop-down menu, click **Manage** and then select the **Processing Options** tab.  
  
2.  Click **Render this report from an execution snapshot, and then select one of the following options:**  
  
    -   If you want to create a snapshot, select **Use the following schedule to create report execution snapshots**, and then either define a report-specific schedule or select from the **Shared schedule** list.  
  
    -   If you want to create a snapshot immediately, select **Create a report snapshot when you click the Apply button on this page**.  
  
3.  Click **Apply**.  
  
## See Also  
 [Set Report Processing Properties](../../reporting-services/report-server/set-report-processing-properties.md)   
 [Open and Close a Report &#40;Report Manager&#41;](../../reporting-services/reports/open-and-close-a-report-report-manager.md)   
 [Contents Page &#40;Report Manager&#41;](https://msdn.microsoft.com/library/6b16869b-158a-4934-9c85-bee934b35378)   
 [Report Server Content Management &#40;SSRS Native Mode&#41;](../../reporting-services/report-server/report-server-content-management-ssrs-native-mode.md)   
 [Processing Options Properties Page &#40;Report Manager&#41;](https://msdn.microsoft.com/library/28f07c70-7132-4d15-9505-4fdf31dc9cc0)  
  
  
