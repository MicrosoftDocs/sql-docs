---
title: Outline and Add Snippets to Transact-SQL Script
description: Learn about code snippets that SSDT provides. See how to insert snippets into applications, and find out how to hide and expand code in the Transact-SQL Editor.
author: cwebb-msft
ms.author: chweb
ms.reviewer: vanto
ms.date: 09/09/2025
ms.service: sql
ms.subservice: ssdt
ms.topic: how-to
---

# How to: Outline and add snippets to Transact-SQL script

SQL Server Data Tools includes a code library consisting of code snippets that are ready to be inserted in your application. Each snippet performs a complete scripting task such as creating a function, table, trigger, index, view, user-defined data type, etc. You can insert a snippet into your source code with your pointing device. These snippets increase your productivity by reducing the amount of time you spend typing.

When you need to browse for an appropriate snippet, you can use the snippet picker, which gives you categorized lists of snippets to choose from. Once you have added the snippet to your code, there might be parts of it that need customization, such as replacing variable names with more appropriate names, or putting in the actual logic of a stored procedure. You notice that the inserted snippet code has one or more replacement points highlighted in the code for this purpose. If you rest your mouse pointer over the replacement point, a ToolTip appears that explains how you can change the code.

By default, all text is displayed in the Transact-SQL Editor, but you can choose to hide some code from view. The Transact-SQL Editor allows you to select a region of code and make it collapsible, so that it appears under a plus sign (**+**). You can then expand or hide the region by selecting the plus sign (**+**) next to the symbol. Outlined code isn't deleted; it's hidden from view.

> [!WARNING]  
> The following procedure uses entities created in previous procedures in the [SQL database projects](../tools/sql-database-projects/sql-database-projects.md) sections.

## Insert snippets

1. Right-click the `TradeDev` project in **Solution Explorer** and select **Add**, then **Script**. In the **Add New Item** dialog box, select **Add**.

1. Right-click the Transact-SQL editor and select **Insert Snippet**. The code snippet picker appears.

1. Double-click **Table** in the code snippet picker, then double-click **Create Table**.

1. Replacement points are highlighted in yellow. Hover your mouse over `Sample_Table`, and an infotip displays a description of the replacement. Double-click `Sample_Table` and change it to `Shipper2`.

1. Use the TAB key to move to the next replacement point, which is `column_1`. Rename it to `Id`. Follow the same steps to rename `column_2` to `name` and change its data type to **nvarchar (128)** and allow `NULL`.

## Outline code

1. Notice the **-** sign next to the `CREATE TABLE` statement. Select the **-** sign next to a section in the script to hide it.

1. Right-click the Transact-SQL Editor and select **Outlining**, then **Stop Outlining** to remove the outline information without affecting your underlying code in the editor.

1. To start outlining your code again, right-click the Transact-SQL Editor and select **Outlining**, then **Start Automatic Outlining**. You can also select **Toggle All Outlining** to switch the expand/hide sections.
