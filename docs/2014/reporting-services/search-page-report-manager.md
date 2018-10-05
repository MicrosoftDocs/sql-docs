---
title: "Search Page (Report Manager) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
ms.assetid: 51ffdc07-e1b9-4ed7-acb3-dd98d7cce273
author: markingmyname
ms.author: maghan
manager: craigg
---
# Search Page (Report Manager)
  Use the Search Results page to view the results of a search operation specified for a report, linked report, report model, shared data source, folder, or resource. Search results are listed alphabetically. You can sort by type, name, or description.  
  
 The following items are excluded from a search operation: report snapshots contained in report history, subscriptions, and shared schedules. Similarly, insufficient permission to view a folder or report excludes that item from a search.  
  
 You cannot search for text within a report or model. To search for specific text within a report, use the toolbar at the top of the report.  
  
## Navigation  
 Use the following procedure to navigate to this location in the user interface (UI).  
  
###### To open the Search Results page  
  
1.  Open Report Manager.  
  
2.  At the top of the page, type your search criteria in the **Search** box. Then press Enter or click the Search arrow.  
  
## Options  
 **Delete**  
 Click to remove an item from a report server database.  
  
> [!NOTE]  
>  This button is available only in **Details View**. However, you can hover over an item and use the menu to access the delete functionality in either **Details View** or in **List View**.  
  
 **Move**  
 Click to relocate an item. This opens the Move Items page, where you can select a different folder location.  
  
> [!NOTE]  
>  This button is available only in **Details View**. However, you can hover over an item and use the menu to access the move functionality in either **Details View** or in **List View**.  
  
 Search box  
 Type all or part of the name of an item that you want to locate, and then click **Go** to start the search. The longest string you can search for is 128 characters.  
  
 Item names or descriptions that contain the entire search string anywhere in the text value are included in the search results.  
  
 Boolean operators such as the plus character (+) are not supported.  
  
 **Details View**  
 Click to display the Search Results page in a list that contains additional information about items, such as the item type, name, description, the folder in which the item is located, and when it was last run. In **Details View**, you can use **Delete** and **Move** buttons to remove and relocate items in the folder.  
  
 Hover over an item and click the drop-down arrow to open the drop-down menu from which you can access and configure properties of the selected item.  
  
 **List View**  
 Click to display the Search Results page without details as in **Details View**. List View is the default view when the Search Results page opens.  
  
 Hover over an item and click the drop-down arrow to open the drop-down menu from which you can access and configure properties of the selected item.  
  
## See Also  
 [Report Manager  &#40;SSRS Native Mode&#41;](../../2014/reporting-services/report-manager-ssrs-native-mode.md)   
 [Report Manager F1 Help](../../2014/reporting-services/report-manager-f1-help.md)  
  
  
