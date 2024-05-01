---
title: "Move or delete an item (Report Manager)"
description: A Report Manager report server stores reports and related items in folders. You can move or delete items. Report server maintains references to items you move.
author: maggiesMSFT
ms.author: maggies
ms.date: 03/01/2017
ms.service: reporting-services
ms.subservice: report-server
ms.topic: conceptual
ms.custom: updatefrequency5
helpviewer_keywords:
  - "moving items"
  - "items [Reporting Services], moving"
---
# Move or delete an item (Report Manager)
  Reports and report-related items that you publish to a report server are stored in folders. You can move items to a different folder and the report server automatically maintains the references to those items. Before you delete an item, consider whether other items depend on it.  
  
## Move an item  
 You can move report server items to different folder locations in the report server folder hierarchy. When you move an item, all properties, including security settings, move with the item to the new location. When you move a folder, all the items in the folder move with it.  
  
 In Report Manager, the items that you can move are indicated in the folder hierarchy. The following table shows the icon for each movable item.  
  
|Icon|Moveable item|  
|----------|-------------------|  
|:::image type="icon" source="../../reporting-services/report-server/media/hlp-16doc.gif":::|Report|  
|:::image type="icon" source="../../reporting-services/report-server/media/hlp-16linked.gif":::|Linked report|  
|:::image type="icon" source="../../reporting-services/report-server/media/hlp-16folder.gif":::|Folder|  
|:::image type="icon" source="../../reporting-services/report-server/media/hlp-16file.gif":::|Generic resource|  
|:::image type="icon" source="../../reporting-services/report-data/media/hlp-16datasource.png":::|Shared data source|  
||Shared dataset|  
  
 Not all items that you work with can be moved. You can't move items that are associated with a report, such as subscriptions or report history. Those items move with their associated reports. Similarly, you can't move items that exist outside of the folder hierarchy, such as shared schedules. You can't move items if you lack permission to do so. Permission to move an item is conveyed when the following tasks are selected in your role assignment for the item in question: "Manage reports," "Manage models," "Manage folders," and "Manage data sources."  
  
#### Move an item from within the Contents page  
  
1.  Start [Report Manager  &#40;SSRS native mode&#41;](../web-portal-ssrs-native-mode.md).  
  
1.  In Report Manager, navigate to the **Contents** page, and locate the item that you want to move.  
  
1.  Hover over the item, and select the arrow.  
  
1.  In the menu, choose **Move**.  
  
1.  Select **OK**.
  
1.  For **Location**, specify the folder you want to move the item to. You can enter the fully qualified folder name or use the tree control to navigate to the folder.  
  
1.  Select **OK**.
  
 Alternatively, you can navigate to the object you want to move, select **Properties**, and then select **Move** at the top of the page.  
  
## Delete an item  
 Before you delete an item, determine if the item is used by other items. For example, if you delete a shared data source, reports and models that use that data source no longer runs. If you delete a report, subscriptions and report history associated with that report are also deleted. To find dependent items for an item, see [Dependent Items page &#40;Report Manager&#41;](../web-portal-ssrs-native-mode.md).  
  
### Delete a report or item  
  
1.  Start [Report Manager  &#40;SSRS native mode&#41;](../web-portal-ssrs-native-mode.md).  
  
1.  In Report Manager, navigate to the **Contents** page, and locate the item that you want to delete.  
  
1.  Hover over the item, and select the arrow.  
  
1.  In the menu, choose **Delete**.  
  
1.  Select **OK**.
  
## Related content 
 [Contents page &#40;Report Manager&#41;](/previous-versions/sql/sql-server-2016/ms186470(v=sql.130))   
 [Find, view, and manage reports &#40;Report Builder and SSRS &#41;](../../reporting-services/report-builder/finding-viewing-and-managing-reports-report-builder-and-ssrs.md)  
  
