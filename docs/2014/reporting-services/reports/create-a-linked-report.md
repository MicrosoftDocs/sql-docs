---
title: "Create a Linked Report | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
helpviewer_keywords: 
  - "linked reports [Reporting Services], creating"
ms.assetid: 12be8341-cb57-45e8-a421-2bf66b50234d
author: markingmyname
ms.author: maghan
manager: kfile
---
# Create a Linked Report
  A linked report is a report server item that provides an access point to an existing report. Conceptually, it is similar to a program shortcut that you use to run a program or open a file.  
  
 A linked report is derived from an existing report and retains the original's report definition. A linked report always inherits report layout and data source properties of the original report. All other properties and settings can be different from those of the original report, including security, parameters, location, subscriptions, and schedules.  
  
 You can create a linked report when you want to create additional versions of an existing report. For example, you could use a single regional sales report to create region-specific reports for all of your sales territories.  
  
 Although linked reports are typically based on parameterized reports, a parameterized report is not required. You can create linked reports whenever you want to deploy an existing report with different settings.  
  
### To create a linked report  
  
1.  In Report Manager, navigate to the folder containing the report that you want to link to, and then open the options menu can click **Create Linked Report**.  
  
2.  Type a name for the new linked report. Optionally type a description.  
  
3.  To select a different folder for the report, click **Change Location**. Click the folder you want to use, or type the folder name in the **Location** box. [!INCLUDE[clickOK](../../../includes/clickok-md.md)] If you do not select a different folder, the linked report is created in the current folder (where the report it is based on is stored).  
  
4.  [!INCLUDE[clickOK](../../../includes/clickok-md.md)] The linked report opens.  
  
     A linked report's icon differs from other items managed by a report server. The following icon indicates a linked report:  
  
     ![Linked report icon](../media/hlp-16linked.gif "Linked report icon")  
  
## See Also  
 [Open and Close a Report &#40;Report Manager&#41;](../reports/open-and-close-a-report-report-manager.md)   
 [New Linked Report Page &#40;Report Manager&#41;](../new-linked-report-page-report-manager.md)   
 [Choose Item Location Page &#40;Report Manager&#41;](../choose-item-location-page-report-manager.md)   
 [General Properties Page, Reports &#40;Report Manager&#41;](../general-properties-page-reports-report-manager.md)   
 [Reporting Services Concepts &#40;SSRS&#41;](../reporting-services-concepts-ssrs.md)   
 [Report Manager  &#40;SSRS Native Mode&#41;](../report-manager-ssrs-native-mode.md)  
  
  
