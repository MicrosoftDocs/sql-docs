---
title: "How to: View and Edit Data in a Table | Microsoft Docs"
ms.custom: 
  - "SSDT"
ms.date: "02/09/2017"
ms.prod: "sql"
ms.technology: ssdt
ms.reviewer: ""
ms.topic: conceptual
f1_keywords: 
  - "SQL.DATA.TOOLS.QUERYRESULTS.F1"
  - "sql.data.tools.queryresults.executequerydeletingrow"
ms.assetid: bb67ce83-a87a-4e14-84cd-9a5930fe74c8
author: "markingmyname"
ms.author: "maghan"
manager: "craigg"
---
# How to: View and Edit Data in a Table
You can view, edit, and delete data in an existing table by using a visual Data Editor.  
  
> [!WARNING]  
> The following procedures uses entities created in previous procedures in the [Connected Database Development](../ssdt/connected-database-development.md) section.  
  
### To edit data in a table visually using the Data Editor  
  
1.  Right-click the **Products** table in **SQL Server Object Explorer**, and select **View Data**.  
  
2.  The Data Editor launches. Notice the rows we added to the table in previous procedures.  
  
3.  Right-click the **Fruits** table in SQL Server Object Explorer, and select **View Data**.  
  
4.  In the Data Editor, type **1** for **Id** and **True** for **Perishable**, then either press ENTER or TAB to shift focus away from the new row to commit it to the database.  
  
5.  Repeat the above step to enter **2**, **False** and **3**, **False** to the table.  
  
    Notice that as you are editing a row, you can always revert the changes to a cell by pressing ESC.  
  
6.  You can view your edits as a script by clicking the **Script** button on the toolbar. Alternatively, you can use the **Script to File** button to save them in a .sql script file to be executed later.  
  
7.  Right-click the **Trade** database in **SQL Server Object Explorer**, and select **New Query**. In the editor, type `select * from dbo.PerishableFruits` and press the **Execute Query** button to return the data represented by the `PerishableFruits` view.  
  
