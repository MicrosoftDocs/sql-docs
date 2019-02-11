---
title: "Choose Link Page (Report Manager) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
ms.assetid: a89a555d-efa3-45d6-951e-db78ec6a2c8e
author: markingmyname
ms.author: maghan
manager: kfile
---
# Choose Link Page (Report Manager)
  Use the Choose Link page to choose a different report upon which to base the currently selected linked report. Linked reports are based on other reports already published to a report server. A linked report uses the layout and data of the base report, but has separate property pages so that you can customize parameter properties, security settings, name, description, and location.  
  
 Through the Choose Link page, you can choose a different published report to use with the linked report. Other settings of the linked report (such as security and parameter settings) are unaffected by changes to the link information. The report server will not validate your selection, so be sure to choose a report that has the same parameters as those you specified on the linked report.  
  
## Navigation  
 Use the following procedure to navigate to this location in the user interface (UI).  
  
###### To open the Choose Link page  
  
1.  Open Report Manager, and locate the linked report you want to change.  
  
2.  Hover over the linked report, and click the drop-down arrow.  
  
3.  In the drop-down menu, click **Manage**. This opens the **General** properties page for the linked report.  
  
4.  On the **General** tab, on the properties page, click **Change Link**.  
  
## Options  
 **Location**  
 Specify the full name of the published report, including the folder path and report name. You can type the full name of the report or use the tree view to navigate to the report you want to use.  
  
 **Tree view**  
 Shows all of the folders in the report server folder hierarchy. To use the tree view to fill in the **Location** field, click the name of the report.  
  
## See Also  
 [General Properties Page, Reports &#40;Report Manager&#41;](../../2014/reporting-services/general-properties-page-reports-report-manager.md)   
 [New Linked Report Page &#40;Report Manager&#41;](../../2014/reporting-services/new-linked-report-page-report-manager.md)   
 [Report Manager F1 Help](../../2014/reporting-services/report-manager-f1-help.md)  
  
  
