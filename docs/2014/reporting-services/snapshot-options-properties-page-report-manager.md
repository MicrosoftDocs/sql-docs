---
title: "Snapshot Options Properties Page (Report Manager) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
ms.assetid: f6641f59-5267-4f57-8957-63b93d1a9679
author: markingmyname
ms.author: maghan
manager: craigg
---
# Snapshot Options Properties Page (Report Manager)
  Use the Snapshot Options properties page to schedule report snapshots to be added to report history, and to set limits on the number of report snapshots that are stored in report history.  
  
> [!NOTE]  
>  This feature is not available in every edition of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]. For a list of features that are supported by the editions of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)], see [Additional Database Services](../../2014/getting-started/features-supported-by-the-editions-of-sql-server-2014.md#Add_DBServices).  
  
## Navigation  
 Use the following procedure to navigate to this location in the user interface (UI).  
  
### To open the Snapshot Options properties page for a report  
  
1.  Open Report Manager, and locate the report for which you want to configure report snapshot properties.  
  
2.  Hover over the report, and click the drop-down arrow.  
  
3.  In the drop-down menu, click **Manage**. This opens the General properties page for the report.  
  
4.  Select the **Snapshot Options** tab.  
  
## Options  
 **Allow report history to be created manually**  
 Select this check box to add snapshots to report history as needed. Selecting this check box causes the **New Snapshot** button to appear on the History page.  
  
 **Store all report execution snapshots in report history**  
 Select this check box to copy a report snapshot that you generate based on report execution properties to report history. You can set report execution properties to run a report from a generated snapshot. By setting this report history property, you can keep a record of all reports snapshots that are generated over time by placing copies of them in report history.  
  
 **Use the following schedule to add snapshots to report history**  
 Select this check box to add snapshots to report history on a scheduled basis. You can create a schedule that is used exclusively for this purpose, or you can select a predefined shared schedule if one contains the schedule information you want.  
  
 **Select the number of snapshots to keep**  
 Select from the following options to control the number of reports that are kept in report history. Report history settings can vary for each report.  
  
-   Select **Use default setting** to retain the default setting. The report server administrator controls a master setting for report history storage. If you choose this option, the number of snapshots that are retained is obtained from this master setting.  
  
-   Select **Keep an unlimited number of snapshots in report history** to retain all report history snapshots. You must manually delete snapshots to reduce the size of report history.  
  
-   Select **Limit the copies of report history** to retain a set number of snapshots. When the limit is reached, older copies are removed from report history to make room for newer copies.  
  
 Report history is stored in the report server database. If you have large reports or numerous reports for which you want to maintain history, consider limiting the amount of report history to help manage the disk space requirements of the report server database.  
  
 **Apply**  
 Click to save your changes.  
  
## See Also  
 [Add a Snapshot to Report History &#40;Report Manager&#41;](report-server/add-a-snapshot-to-report-history-report-manager.md)   
 [Report Manager  &#40;SSRS Native Mode&#41;](../../2014/reporting-services/report-manager-ssrs-native-mode.md)   
 [Create, Modify, and Delete Snapshots in Report History](report-server/create-modify-and-delete-snapshots-in-report-history.md)   
 [Report Manager F1 Help](../../2014/reporting-services/report-manager-f1-help.md)  
  
  
