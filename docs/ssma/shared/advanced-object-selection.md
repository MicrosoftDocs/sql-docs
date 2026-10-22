---
title: Advanced Object Selection
description: Learn how to filter and select database objects using the Advanced Object Selection dialog box in SSMA.
author: cwebb-msft
ms.author: chweb
ms.date: 04/16/2026
ms.service: sql
ms.subservice: ssma
ms.topic: concept-article
ms.collection:
  - sql-migration-content
ai-usage: ai-assisted
---
# Advanced Object Selection

The **Advanced Object Selection** dialog box lets you filter database objects by using strings and substrings in the object name, and then select or deselect those objects. SQL Server Migration Assistant (SSMA) performs conversion and migration operations on selected objects.

To access this dialog box, right-click in a metadata explorer, and then select **Advanced Object Selection**.

When you first open the dialog box, select **Show Subcategories Items** to display all objects that have metadata loaded into the project. You can then enter strings to filter the items. For example, enter the string "company" to show all items with names that include that string.

Before you use this dialog box, you might want to force SSMA to load all metadata by either converting schemas or saving the project.

## Options

#### Check All Items

Adds a check mark next to all items. These items are immediately selected in the metadata explorer.

#### Uncheck All Items

Removes the check mark next to all items. These items are immediately cleared in the metadata explorer.

#### List View Mode

Displays filtered items in a list.

#### Tree View Mode

Displays filtered items in a tree.

#### Show Icons Only Button

Displays items using icons only (without text).

#### Show Icons and Text Button

Displays items using icons and text.

## Related content

- [SQL Server Migration Assistant](../sql-server-migration-assistant.md)
