---
description: "Object Explorer Details Pane"
title: "Object Explorer Details Pane"
ms.custom: seo-lt-2019
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: ssms
ms.topic: conceptual
f1_keywords: 
  - "sql13.swb.summary.general.f1"
  - "sql13.swb.summary.report.f1"
helpviewer_keywords: 
  - "object search [SQL Server], Object Explorer"
  - "Object Explorer"
  - "object search [SQL Server]"
  - "searching objects [SQL Server]"
ms.assetid: b963e3c2-dc9e-4d38-bd28-2e00fe9e0e47
author: "markingmyname"
ms.author: "maghan"
---
# Object Explorer Details Pane
[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]
Object Explorer Details, a component of [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], provides a tabular view of all the objects in the server and presents a user interface to manage them. The capabilities of Object Explorer vary slightly depending on the type of server, but generally include the development features for databases and management features for all server types.  
  
The Object Explorer Details pane is visible in the [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] by default. If you cannot see Object Explorer, on the **View** menu, click **Object Explorer Details** or press **F7**.  
  
> [!NOTE]  
> [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] presents dates formatted with the Microsoft Windows Regional and Language Options in effect when [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] was started. Restart [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] to reflect newer settings.  
  
## Object Explorer Details  
Object Explorer Details can be used to navigate through folders and objects on your [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance. On 32-bit operating systems, Object Explorer can only display 64,000 objects. An icon must be selected to access additional objects.  
  
Object Explorer Details includes a toolbar which contains the icons described in the following table. Icons are only available when appropriate.  
  
|Icon|Action|  
|--------|----------|  
|**Back**|Moves to the previous items displayed in Object Explorer Details. Re-runs a search when the previous display is the result of a search operation.|  
|**Forward**|Moves to the next screen after a **Back** operation is selected.|  
|**Up**|Moves to the parent object or folder.|  
|**Synchronize**|Sets the focus of Object Explorer to the selected object in Object Explorer Details.|  
|**Filter**|When available, shows a configurable subset of objects.|  
|**Refresh**|Refreshes the display in Object Explorer Details.|  
|**Search**|Provides an area to enter a search term for certain database objects.|  
  
### Column Header Selections  
Object Explorer Details has selectable columns. You can right-click in any column header and check the items that you want to display. Your selections will be persisted across the different objects you navigate through. Selections for each user are retained when you leave and restart SQL Server Management Studio.  
  
> [!CAUTION]  
> Showing all columns for some object types (such as Databases) might slow the display rendering slightly for large sets of objects.  
  
### Sorting  
Clicking one time on a column header will sort by that column. Clicking again on the same header reverse-sorts by that column. Sort selections are maintained for each user across objects and folders, and on [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] restart.  
  
### Filtering  
Certain lists of objects displayed in Object Explorer Detail can be filtered using the **Filter** icon on the Object Explorer Details toolbar. The icon will be enabled when filtering is possible.  
  
### Details Pane  
The very bottom area of Object Explorer Details contains a panel that displays certain details of the selected object. Multiple object selections show only the count of the objects. When items are selected in the panel, click the **Copy** icon to copy the displayed text to the clipboard.  
  
The down arrow key moves the selection to the next item in the list. The up arrow key moves the selection to the previous item in the list. Holding the arrow key down will speed through the items. The selection stops at the bottom or top of the property list.  
  
Typing the first character of a property name moves the selection to the next property that begins with that character.  
  
When properties are arranged in multiple columns, use the left arrow and right arrow keys to move to adjacent columns.  
  
To copy property values to the clipboard, use the following keyboard key combinations:  
  
-   Ctrl+C copies the property value.  
  
-   Ctrl+Shift+C copies the property name and the property value with a tab delimiter.  
  
Use the right-click menu in the Object Explorer Details property pane to copy all properties or all properties and names to the clipboard.  
  
To display a property value when the field width does not completely display a property value, hover the mouse cursor over the value or set UI focus on the property value to activate a tool tip with the entire property value. Accessibility screen readers will report the full property value when the user focuses on the long property value.  
  
If the number of properties in the property pane exceeds that which will fit in the content area, a scroll bar will appear on the right side of the property pane. Use the scroll bar to adjust the view of property values in the content area.  
  
### Multiple Object Selection  
Object Explorer Details supports multiple object selection. For example, if you select Tables in Object Explorer, then in the Object Explorer Details window if you hold down the **Ctrl** key, you can select several tables, right-click them, and then select **Delete** or **Script Table AS** to act on all the selected tables immediately. The standard copy commands will copy the displayed text to the clipboard.  
  
## SQL Server Object Search  
Wildcards  
  
-   Standard wildcard characters are supported. For example, searching for **dm_os%counters** returns both dm_os_memory_cache_counters and dm_os_performance_counters. For more information, see [How to: Search with Wildcards](../scripting/search-text-with-wildcards.md).  
  
Search Scope  
  
-   Each search is scoped to the current focus in the Object Explorer tree. For example, if the Object Explorer focus is on a database, search for **dm_os%counters** returns dm_os_memory_cache_counters and dm_os_performance_counters in that database. If the Object Explorer focus is on the Databases node, all databases are searched and multiple instances of the dynamic views are returned.  
  
Large Sets  
  
-   Searching large object sets can take a long time and slow server performance.  
  
## See Also  
[Object Explorer](../../ssms/object/object-explorer.md)  
