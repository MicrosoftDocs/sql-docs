---
title: Fix Errors
description: Become familiar with the Error List pane and the types of errors it displays. See how to sort and filter error list information and identify error sources.
author: cwebb-msft
ms.author: chweb
ms.reviewer: vanto
ms.date: 09/09/2025
ms.service: sql
ms.subservice: ssdt
ms.topic: how-to
---

# How to: Fix errors

The Error List pane displays any deployment or build errors. Syntax and semantic errors caused by editing in either the Transact-SQL Editor or Table Designer also shows up in the list when you're editing database entities and its definitions. The Error List is dynamically updated as you edit scripts across different tabs. You can then follow the errors identified for further troubleshooting.

## Fix errors

1. Right-click the `Product` table (`Product.sql`) in **Solution Explorer** and select **View Designer**.

1. In the Columns Grid of the designer, right-click the `ShelfLife` column and select **Delete** to delete this column from the table.

1. In the **Error List** pane at the bottom of the screen, a warning, and an error similar to the following pop-up immediately.

   ```output
   Warning SQL71502: Function: [dbo].[GetProductsBySupplier] contains an unresolved reference to an object. Either the object does not exist or the reference is ambiguous because it could refer to any of the following objects: [dbo].[Product].[p]::[ShelfLife] or [dbo].[Product].[ShelfLife].Error SQL71501: Check Constraint: [dbo].[CK_Product_ShelfLife] has an unresolved reference to object [dbo].[Product].[ShelfLife].
   ```

1. You can right-click the **Error List** and use the contextual menus to sort results, filter which entries you want to display, and which columns of information you want to appear for each entry.

   Double-click the first warning identified and follow it to the script file that generated the warning. The problematic code section is highlighted. In the example, it's because the `ShelfLife` column is being used by both a `RETURN` and `SELECT` statement in a table-value function created previously.

1. In the Transact-SQL Editor, remove `ShelfLife` from the function.

1. Fix the second error in a similar manner by removing the check constraint.

1. The warning and error disappear from the **Error List** immediately after you fix the problems.

## Related content

- [Use Transact-SQL Editor to edit and execute scripts](use-transact-sql-editor-to-edit-and-execute-scripts.md)
