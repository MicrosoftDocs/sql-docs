---
title: Toggle a breakpoint
titleSuffix: T-SQL debugger
description: Learn how to toggle a breakpoint to highlight the associated Transact-SQL statement, and to perform various actions on the statement (such as editing).
author: erinstellato-ms
ms.author: erinstellato
ms.reviewer: drskwier, maghan
ms.date: 08/28/2024
ms.service: sql
ms.subservice: ssdt
ms.topic: conceptual
---

# Toggle a breakpoint

 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

The act of setting a breakpoint on a [!INCLUDE[tsql](../../includes/tsql-md.md)] statement is called toggling a breakpoint.  

## Breakpoints

Once the breakpoint is set, it's represented by an icon in the grey bar to the left of the statement. The icon is called a breakpoint glyph. [!INCLUDE[tsql](../../includes/tsql-md.md)] breakpoints are applied to a complete [!INCLUDE[tsql](../../includes/tsql-md.md)] statement. When a breakpoint is toggled on, the debugger highlights the associated [!INCLUDE[tsql](../../includes/tsql-md.md)] statement.  
  
 If there are multiple [!INCLUDE[tsql](../../includes/tsql-md.md)] statements on a line, you can toggle a breakpoint for each statement. Selecting the grey bar on the left of the window toggles a breakpoint on the first statement on the line. You can toggle a breakpoint in a subsequent statement by highlighting any part of the statement, or moving the cursor into the statement, and then either pressing F9 or selecting **Toggle Breakpoint** on the **Debug** menu. If you have multiple breakpoints on a line, there's only one breakpoint glyph in the grey bar on the left.  
  
 After a breakpoint is toggled, you can perform various actions on the breakpoint, such as editing its properties or temporarily disabling it. For more information, see [Transact-SQL Breakpoints](./transact-sql-breakpoints.md).  
  
## Toggle a breakpoint on a Transact-SQL statement
  
1. Select the gray bar to the left side of the [!INCLUDE[tsql](../../includes/tsql-md.md)] statement.  
  
1. Alternatively, either highlight any part of the statement or move the cursor to the statement, and then perform either action:  
  
    - Select F9.  
  
    - On the **Debug** menu, select **Toggle Breakpoint**.

