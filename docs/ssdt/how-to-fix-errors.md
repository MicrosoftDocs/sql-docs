---
title: "How to: Fix Errors | Microsoft Docs"
ms.custom: 
  - "SSDT"
ms.date: "02/09/2017"
ms.prod: "sql"
ms.technology: ssdt
ms.reviewer: ""
ms.topic: conceptual
ms.assetid: 0d504e00-4ff0-4fdf-b874-85280bbd8668
author: "markingmyname"
ms.author: "maghan"
manager: "craigg"
---
# How to: Fix Errors
The Error List pane displays any deployment or build errors. Syntax and semantic errors caused by editing in either the Transact\-SQL Editor or Table Designer also shows up in the list when you are editing database entities and its definitions. The Error List is dynamically updated as you edit scripts across different tabs. You can then follow the errors identified for further troubleshooting.  
  
> [!WARNING]  
> The following procedures use entities created in procedures in the [Connected Database Development](../ssdt/connected-database-development.md) and [Project-Oriented Offline Database Development](../ssdt/project-oriented-offline-database-development.md) sections.  
  
### To fix errors  
  
1.  Right-click the **Product** table (Product.sql) in **Solution Explorer** and select **View Designer**.  
  
2.  In the Columns Grid of the designer, right-click the **ShelflLife** column and select **Delete** to delete this column from the table.  
  
3.  Notice that in the **Error List** pane at the bottom of the screen, a warning, and an error similar to the followings pop up immediately.  
  
**Warning SQL71502: Function: [dbo].[GetProductsBySupplier] contains an unresolved reference to an object. Either the object does not exist or the reference is ambiguous because it could refer to any of the following objects: [dbo].[Product].[p]::[ShelfLife] or [dbo].[Product].[ShelfLife].Error SQL71501: Check Constraint: [dbo].[CK_Product_ShelfLife] has an unresolved reference to object [dbo].[Product].[ShelfLife].**  
  
4.  You can right-click the **Error List** and use the contextual menus to sort results, filter which entries you want to display, and which columns of information you want to appear for each entry.  
  
    Double-click the first warning identified and follow it to the script file that generated the warning. The problematic code section is highlighted. In the example, it is because the `ShelfLife` column is being used by both a `RETURN` and `SELECT` statement in a table-value function created previously.  
  
5.  In the Transact\-SQL Editor, remove `ShelfLife` from the function.  
  
6.  Fix the second error in a similar manner by removing the check constraint.  
  
7.  Notice that the warning and error disappear from the **Error List** immediately after you fix the problems.  
  
## See Also  
[Use Transact-SQL Editor to Edit and Execute Scripts](../ssdt/use-transact-sql-editor-to-edit-and-execute-scripts.md)  
  
