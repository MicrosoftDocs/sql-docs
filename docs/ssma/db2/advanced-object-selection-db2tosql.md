---
title: "Advanced Object Selection (Db2ToSQL)"
description: Learn how to filter database objects by using strings and substrings in the object name (Db2ToSQL).
author: cpichuka
ms.author: cpichuka
ms.reviewer: randolphwest
ms.date: 09/24/2024
ms.service: sql
ms.subservice: ssma
ms.topic: conceptual
ms.collection:
  - sql-migration-content
f1_keywords:
  - "ssma.db2.multichecktreeview.f1"
---
# Advanced Object Selection (Db2ToSQL)

The **Advanced Object Section** dialog box lets you filter database objects by using strings and substrings in the object name, and then select or deselect those objects. SQL Server Migration Assistant (SSMA) performs conversion and migration operations on selected objects.

To access this dialog box, right-click in a metadata explorer, and then select **Advanced Object Selection**.

When you first open the dialog box, select **Show Subcategories Items** to display all objects that have metadata loaded into the project. You can then enter strings to filter the items. For example, enter the string `company` to show all items with names that include that string.

Before you use this dialog box, you might want to force SSMA to load all metadata by either converting schemas or saving the project.

## Options

#### Check All Items

Adds a check mark next to all items. These items are immediately selected in the metadata explorer.

#### Uncheck All Items

Removes the check mark next to all items. These items are immediately cleared in the metadata explorer.

#### List View Mode

Displays filtered items in a list.

#### Table View Mode

Displays filtered items in a table.

#### Displayed Only Loaded Items

Toggles the display of categories or items. When this button is selected, SSMA shows all the items that match the filter criteria and the criteria that were previously loaded. When this button isn't selected, SSMA shows the category folders.

#### Filter

Enter the string you want to use to filter items. For example, to find all available items that contain the string "ID" in the item name, enter the string `ID` in the **Filter** box.

If items match the filter criteria, the categories or items appear as you type the string. To see the matching items, we recommend that you select the **Displayed Only Loaded Items** button.

#### Clear Filter

Clears the **Filter** box.
