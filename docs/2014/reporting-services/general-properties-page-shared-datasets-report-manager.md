---
title: "General Properties Page, Shared Datasets (Report Manager) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
ms.assetid: 10798e41-24c3-4e69-893b-7ee6af7fc958
author: maggiesMSFT
ms.author: maggies
manager: kfile
---
# General Properties Page, Shared Datasets (Report Manager)
  Use the Shared Dataset page to view and manage properties for a shared dataset item.  
  
 From Report Manager, you cannot view or change the shared dataset definition, including the query. To change the definition, you must use an authoring tool to open and modify the definition and then save it to the report server.  
  
 With a shared dataset, you can manage the settings for a dataset separately from reports, components, and other catalog items that use it.  
  
## Navigation  
 Use the following procedure to navigate to this location in the user interface (UI).  
  
###### To open the Shared Dataset properties page for a shared dataset  
  
1.  Open Report Manager, and locate shared dataset for which you want to configure properties.  
  
2.  Hover over the shared dataset, and click the drop-down arrow.  
  
3.  In the drop-down list, click **Manage**. The General properties page opens.  
  
## Options  
 **Name**  
 Type a name for the shared dataset that is used to identify the item within the report server folder hierarchy.  
  
 **Description**  
 Provide information about the shared dataset. This description appears on the Contents page.  
  
 **Hide in list view**  
 Hide the shared dataset from users who are using list view mode in Report Manager. List view mode is the default view format when browsing the report server folder hierarchy. In list view, item names and descriptions flow across the page. The alternative format is details view. Details view omits descriptions, but includes other information about the item. Although you can hide an item in list view, you cannot hide an item in details view. If you want to restrict access to an item, you must create a role assignment.  
  
 **Query execution timeout**  
 Type the number of seconds until the query times out. If it is 0, the query does not time out.  
  
 **Apply**  
 Save your changes.  
  
 **Delete**  
 Remove the shared dataset from the report server database. Deleting a shared dataset deactivates any reports or cached versions. To reactivate a report, you must open each one in a report authoring tool, and specify a dataset with the same name and the same field collection. Alternatively, you can update each data region reference to use a different dataset with the same field collection.  
  
 **Move**  
 Relocate a shared dataset within the report server folder hierarchy. Clicking this button opens the Move Items page, on which you can browse through folders for a new folder location. For more information, see [Move Items Page &#40;Report Manager&#41;](../../2014/reporting-services/move-items-page-report-manager.md).  
  
 **Download**  
 Extract a copy of the shared dataset definition. Depending on the file associations defined on your computer, the file will open in Visual Studio or a different application. In most cases, the shared dataset opens as an XML file.  
  
 **Replace**  
 Replace the shared dataset definition with a different one from an .rsd file located on the file system.  
  
## See Also  
 [Report Manager  &#40;SSRS Native Mode&#41;](../../2014/reporting-services/report-manager-ssrs-native-mode.md)   
 [Contents Page &#40;Report Manager&#41;](../../2014/reporting-services/contents-page-report-manager.md)   
 [Report Manager F1 Help](../../2014/reporting-services/report-manager-f1-help.md)   
 [Cache Refresh Options &#40;Report Manager&#41;](../../2014/reporting-services/cache-refresh-options-report-manager.md)   
 [Caching Page, Shared Datasets &#40;Report Manager&#41;](../../2014/reporting-services/caching-page-shared-datasets-report-manager.md)   
 [Manage Shared Datasets](report-data/manage-shared-datasets.md)   
 [Cache Shared Datasets &#40;SSRS&#41;](report-server/cache-shared-datasets-ssrs.md)  
  
  
