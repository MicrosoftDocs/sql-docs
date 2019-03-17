---
title: "Move or Delete an Item (Report Manager) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
helpviewer_keywords: 
  - "moving items"
  - "items [Reporting Services], moving"
ms.assetid: 980a66c7-a18b-4af7-8954-45726fa517d6
author: markingmyname
ms.author: maghan
manager: kfile
---
# Move or Delete an Item (Report Manager)
  Reports and report-related items that you publish to a report server are stored in folders. You can move items to a different folder and references to those items are maintained automatically by the report server. Before you delete an item, consider whether other items depend on it.  
  
## Move an Item  
 You can move report server items to different folder locations in the report server folder hierarchy. When you move an item, all properties (including security settings) move with the item to the new location. When you move a folder, all the items in the folder move with it.  
  
 In Report Manager, the items that you can move are indicated in the folder hierarchy. The following table shows the icon for each movable item.  
  
|Icon|Moveable item|  
|----------|-------------------|  
|![Report icon](../media/hlp-16doc.gif "Report icon")|Report|  
|![Linked report icon](../media/hlp-16linked.gif "Linked report icon")|Linked report|  
|![Folder icon](../media/hlp-16folder.gif "Folder icon")|Folder|  
|![generic resource icon](../media/hlp-16file.gif "generic resource icon")|Generic resource|  
|![Shared data source icon](../media/hlp-16datasource.png "Shared data source icon")|Shared data source|  
||Shared dataset|  
  
 Not all items that you work with can be moved. You cannot move items that are associated with a report, such as subscriptions or report history. Those items move with their associated reports. Similarly, you cannot move items, such as shared schedules, that exist outside of the folder hierarchy. You cannot move items if you lack permission to do so. Permission to move an item is conveyed when the following tasks are selected in your role assignment for the item in question: "Manage reports," "Manage models", "Manage folders," and "Manage data sources."  
  
#### To move an item from within the Contents page  
  
1.  Start [Report Manager  &#40;SSRS Native Mode&#41;]../report-manager-ssrs-native-mode.md).  
  
2.  In Report Manager, navigate to the **Contents** page, and locate the item that you want to move.  
  
3.  Hover over the item, and click the drop-down arrow.  
  
4.  In the drop-down menu, click **Move**.  
  
5.  [!INCLUDE[clickOK](../../../includes/clickok-md.md)]  
  
6.  For **Location**, specify the folder you want to move the item to. You can type the fully qualified folder name or use the tree control to navigate to the folder.  
  
7.  [!INCLUDE[clickOK](../../../includes/clickok-md.md)]  
  
 Alternatively, you can navigate to the object you want to move, click **Properties**, and then click **Move** at the top of the page.  
  
## Delete an item  
 Before you delete an item, determine if it is used by other items. For example, if you delete a shared data source, reports and models that use that data source will no longer run. If you delete a report, subscriptions and report history associated with that report are also deleted. To find dependent items for an item, see [Dependent Items Page &#40;Report Manager&#41;]../dependent-items-page-report-manager.md).  
  
#### To delete a report or item  
  
1.  Start [Report Manager  &#40;SSRS Native Mode&#41;]../report-manager-ssrs-native-mode.md).  
  
2.  In Report Manager, navigate to the **Contents** page, and locate the item that you want to delete.  
  
3.  Hover over the item, and click the drop-down arrow.  
  
4.  In the drop-down menu, click **Delete**.  
  
5.  [!INCLUDE[clickOK](../../../includes/clickok-md.md)]  
  
## See Also  
 [Contents Page &#40;Report Manager&#41;]../contents-page-report-manager.md)   
 [Finding, Viewing, and Managing Reports &#40;Report Builder and SSRS &#41;](../report-builder/finding-viewing-and-managing-reports-report-builder-and-ssrs.md)  
  
  
