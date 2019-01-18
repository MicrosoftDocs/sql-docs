---
title: "General Properties Page, Reports (Report Manager) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
ms.assetid: 66c99d28-ab41-45f0-bf02-ed560293595d
author: markingmyname
ms.author: maghan
manager: craigg
---
# General Properties Page, Reports (Report Manager)
  Use the General properties page for reports to rename, delete, move, or replace the report definition. You can also use this page to create a linked report. Details about who created or modified the report and when the changes took place are indicated at the top of the page.  
  
## Navigation  
 Use the following procedure to navigate to this location in the user interface (UI).  
  
###### To open the General properties page for a report  
  
1.  Open Report Manager, and locate the report for which you want to view or configure properties.  
  
2.  Hover over the report, and click the drop-down arrow.  
  
3.  In the drop-down menu, click **Manage**. This opens the General properties page for the report.  
  
## Options  
 **Name**  
 Specify a name for the report. A name must contain at least one alphanumeric character. It can also include spaces and certain symbols. Do not use the characters ; ? : \@ & = + , $ * \< >  
  
 " or / when specifying a name.  
  
 **Description**  
 Type a description of the report. This description appears in the Contents page to users who have permission to access the report.  
  
 **Hide in list view**  
 Select this option to hide the report from users who are using list view mode in Report Manager. List view mode is the default view format when browsing the report server folder hierarchy. In list view, item names and descriptions flow across the page. The alternate format is details view. Details view omits descriptions, but includes other information about the item. Although you can hide an item in list view, you cannot hide an item in details view. If you want to restrict access to an item, you must create a role assignment.  
  
 **Apply**  
 Click to save your changes.  
  
 **Delete**  
 Click to remove the report from the report server database. Deleting a report deletes all associated report history and report-specific schedules and subscriptions. If the report is associated with linked reports, the linked reports are invalidated.  
  
 **Move**  
 Click to relocate a report within the report server folder hierarchy. Clicking this button opens the Move Items page, on which you can browse through folders for a new folder location. For more information, see [Move Items Page &#40;Report Manager&#41;](../../2014/reporting-services/move-items-page-report-manager.md).  
  
 **Create Linked Report**  
 Click to open the New Linked Report page. For more information about this page and linked reports, see [New Linked Report Page &#40;Report Manager&#41;](../../2014/reporting-services/new-linked-report-page-report-manager.md).  
  
 **Save**  
 Click to extract a read-only copy of the report definition. Depending on the file associations defined on your computer, the file will open in [!INCLUDE[vsprvs](../includes/vsprvs-md.md)] or a different application. In most cases, the report opens as an XML file.  
  
 The copy that you open is identical to the original report definition that was initially published to the report server. Any properties that were set on the report after it was published (such as parameters and data source properties) are not reflected in the file that you open.  
  
 You can modify the report definition and save it as a new file in a shared folder, and upload the report definition to the report server as a new item. Modifications that you make to the report definition while it is open in [!INCLUDE[vsprvs](../includes/vsprvs-md.md)] (or another application) are not saved directly to the report server. You must upload the file to publish the modified report to the report server.  
  
 **Replace**  
 Click to replace the report definition that is used in the current report with a different one from an .rdl file located on the file system. If you update a report definition, you must reset the data source settings after the update is complete.  
  
 **Change Link**  
 Click to select a different report definition for the linked report. This option appears if the report is a linked report. If the report is a linked report, you can set this property to replace the report definition.  
  
## See Also  
 [Report Manager  &#40;SSRS Native Mode&#41;](../../2014/reporting-services/report-manager-ssrs-native-mode.md)   
 [Report Manager F1 Help](../../2014/reporting-services/report-manager-f1-help.md)  
  
  
