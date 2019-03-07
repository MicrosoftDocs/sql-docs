---
title: "Limit Report History (Report Manager) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
helpviewer_keywords: 
  - "viewing report history"
  - "report history [Reporting Services], viewing history"
  - "report history [Reporting Services], configuring history"
  - "historical data [Reporting Services]"
  - "displaying report history"
ms.assetid: 8e255792-d9ef-496f-a26c-9e969c1209a0
author: markingmyname
ms.author: maghan
manager: kfile
---
# Limit Report History (Report Manager)
  Report history is a collection of report snapshots that you create over time. You can create report history on demand, or schedule how often a snapshot is created and added to report history.  
  
 Report history is stored in the report server database. If report snapshots contain a large amount of data, you might consider limiting report history to minimize the affect of snapshot retention on database size.  
  
### To configure report history for a report server  
  
1.  In Report Manager, click **Site Settings** on the global toolbar.  
  
2.  Select **Keep an unlimited number of snapshots in report history** if you want to keep all report history indefinitely. Otherwise, select **Limit the copies of report history** to specify the maximum number of snapshots that can be kept for any given report.  
  
3.  Click **Apply**.  
  
### To configure report history for a specific report  
  
1.  In Report Manager, navigate to the report for which you want to configure history, and then click the report to open it.  
  
2.  Click the **Properties** tab.  
  
3.  Click the **History** tab.  
  
4.  Select the options for your report and click **Apply**. For details about each option, see [Snapshot Options Properties Page &#40;Report Manager&#41;](../snapshot-options-properties-page-report-manager.md).  
  
## See Also  
 [Add a Snapshot to Report History &#40;Report Manager&#41;](../report-server/add-a-snapshot-to-report-history-report-manager.md)   
 [Report Manager  &#40;SSRS Native Mode&#41;](../report-manager-ssrs-native-mode.md)  
  
  
