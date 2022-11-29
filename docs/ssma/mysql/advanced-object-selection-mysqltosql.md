---
description: "Advanced Object Selection  (MySQLToSQL)"
title: "Advanced Object Selection  (MySQLToSQL) | Microsoft Docs"
ms.service: sql
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.subservice: ssma
ms.topic: conceptual
ms.assetid: 390ef0c2-107c-4443-9495-80f35f22d168
author: cpichuka 
ms.author: cpichuka 
f1_keywords: 
    - "ssma.mysql.multichecktreeview.f1"
---
# Advanced Object Selection  (MySQLToSQL)
The **Advanced Object Section** dialog box lets you filter database objects by using strings and substrings in the object name, and then select or deselect those objects. SSMA performs conversion and migration operations on selected objects.  
  
To access this dialog box, right-click in a metadata explorer, and then select **Advanced Object Selection**.  
  
When you first open the dialog box, click **Show Subcategories Items** to display all objects that have metadata loaded into the project. You can then enter strings to filter the items. For example, enter the string "company" to show all items with names that include that string.  
  
Before you use this dialog box, you might want to force SSMA to load all metadata by either converting schemas or saving the project.  
  
## Options  
**Check All Items**  
Adds a check mark next to all items. These items will be immediately selected in the metadata explorer.  
  
**Uncheck All Items**  
Removes the check mark next to all items. These items will be immediately cleared in the metadata explorer.  
  
**List View Mode**  
Displays filtered items in a list.  
  
**Table View Mode**  
Displays filtered items in a table.  
  
**Displayed Only Loaded Items**  
Toggles the display of categories or items. When this button is selected, SSMA shows all the items that match the filter criteria and those that were previously loaded. When this button is not selected, SSMA shows the category folders.  
  
**Filter**  
Enter the string you want to use to filter items. For example, to find all available items that contain the string "ID" in the item name, enter the string "ID" in the **Filter** box.  
  
If items match the filter criteria, the categories or items will appear as you type the string. To see the matching items, we recommend that you click the **Displayed Only Loaded Items** button.  
  
**Clear Filter**  
Clears the **Filter** box.  
  
