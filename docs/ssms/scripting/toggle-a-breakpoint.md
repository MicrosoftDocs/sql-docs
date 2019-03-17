---
title: "Toggle a Breakpoint | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.technology: scripting
ms.reviewer: ""
ms.topic: conceptual
ms.assetid: c477ab89-a1cd-4f2c-aa7c-40525041100f
author: stevestein
ms.author: sstein
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Toggle a Breakpoint
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]
  The act of setting a breakpoint on a [!INCLUDE[tsql](../../includes/tsql-md.md)] statement is called toggling a breakpoint.  
  
## Breakpoints  
 Once the breakpoint has been set, it is represented by an icon in the grey bar to the left of the statement. The icon is called a breakpoint glyph. [!INCLUDE[tsql](../../includes/tsql-md.md)] breakpoints are applied to a complete [!INCLUDE[tsql](../../includes/tsql-md.md)] statement. When a breakpoint is toggled on, the debugger highlights the associated [!INCLUDE[tsql](../../includes/tsql-md.md)] statement.  
  
 If there are multiple [!INCLUDE[tsql](../../includes/tsql-md.md)] statements on a line, you can toggle a breakpoint for each statement. Clicking in the grey bar on the left of the window toggles a breakpoint on the first statement on the line. You can toggle a breakpoint in a subsequent statement by highlighting any part of the statement, or moving the cursor into the statement, and then either pressing F9 or clicking **Toggle Breakpoint** on the **Debug** menu. If you have multiple breakpoints on a line, there is only one breakpoint glyph in the grey bar on the left.  
  
 After a breakpoint has been toggled, you can perform various actions on the breakpoint, such as editing its properties or temporarily disabling it. For more information, see [Transact-SQL Breakpoints](../../relational-databases/scripting/transact-sql-breakpoints.md).  
  
## Toggle a Breakpoint  
 **To toggle a breakpoint on a Transact-SQL statement**  
  
1.  Click the gray bar to the left side of the [!INCLUDE[tsql](../../includes/tsql-md.md)] statement.  
  
2.  Alternatively, either highlight any part of the statement or move the cursor to the statement, and then perform either action:  
  
    -   Press F9.  
  
    -   On the **Debug** menu, click **Toggle Breakpoint**.  
  
  
