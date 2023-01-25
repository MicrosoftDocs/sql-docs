---
title: Outline and Add Snippets to Transact-SQL Script
description: Learn about code snippets that SSDT provides. See how to insert snippets into applications, and find out how to hide and expand code in the Transact-SQL Editor.
ms.service: sql
ms.subservice: ssdt
ms.topic: conceptual
ms.assetid: 543e7ce7-8639-4281-8a91-85314755e5de
author: markingmyname
ms.author: maghan
ms.custom: seo-lt-2019
ms.date: 02/09/2017
---

# How to: Outline and Add Snippets to Transact-SQL Script

SQL Server Data Tools includes a code library consisting of code snippets that are ready to be inserted in your application. Each snippet performs a complete scripting task such as creating a function, table, trigger, index, view, user-defined data type, etc. You can insert a snippet into your source code with a few mouse clicks. These snippets increase your productivity by reducing the amount of time you spend typing.  
  
When you need to browse for an appropriate snippet, you can use the snippet picker, which gives you categorized lists of snippets to choose from. Once you have added the snippet to your code, there may be parts of it that need customization, such as replacing variable names with more appropriate names, or putting in the actual logic of a stored procedure. You will notice that the inserted snippet code has one or more replacement points highlighted in the code for this purpose. If you rest your mouse pointer over the replacement point, a ToolTip appears that explains how you can change the code.  
  
By default, all text is displayed in the Transact\-SQL Editor, but you can choose to hide some code from view. The Transact\-SQL Editor allows you to select a region of code and make it collapsible, so that it appears under a plus sign (+).You can then expand or hide the region by clicking the plus sign (+) next to the symbol. Outlined code is not deleted; it is just hidden from view.  
  
> [!WARNING]  
> The following procedures utilize entities created in previous procedures in the [Connected Database Development](../ssdt/connected-database-development.md) and [Project-Oriented Offline Database Development](../ssdt/project-oriented-offline-database-development.md) sections.  
  
### To insert snippets  
  
1.  Right-click the **TradeDev** project in **Solution Explorer** and select **Add**, then **Script**. In the **Add New Item** dialog box, click **Add**.  
  
2.  Right-click the Transact\-SQL editor and select **Insert Snippet**. The code snippet picker appears.  
  
3.  Double-click **Table** in the code snippet picker, then double-click **Create Table**.  
  
4.  Notice that replacement points are highlighted in yellow. Hover your mouse over `Sample_Table`, and an infotip displays a description of the replacement. Double-click `Sample_Table` and change it to `Shipper2`.  
  
5.  Use the TAB key to move to the next replacement point, which is `column_1`. Rename it to `Id`. Follow the same steps to rename `column_2` to `name` and change its data type to `nvarchar(128)` and allow `NULL`.  
  
### To outline code  
  
1.  Notice the **-** sign next to the CREATE TABLE statement. Click the **-** sign next to a section in the script to hide it.  
  
2.  Right-click the Transact\-SQL Editor and select **Outlining**, then **Stop Outlining** to remove the outline information without affecting your underlying code in the editor.  
  
3.  To start outlining your code again, right-click the Transact\-SQL Editor and select **Outlining**, then **Start Automatic Outlining**. You can also select **Toggle All Outlining** to switch the expand/hide sections.  
  
