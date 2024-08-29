---
title: Specify a breakpoint condition
titleSuffix: T-SQL debugger
description: Learn how to establish a breakpoint condition to control whether the debugger performs an action when the breakpoint and hit count are reached.
author: erinstellato-ms
ms.author: erinstellato
ms.reviewer: drskwier, maghan
ms.date: 08/28/2024
ms.service: sql
ms.subservice: ssdt
ms.topic: conceptual
---

# Specify a breakpoint condition

 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

A breakpoint condition is a [!INCLUDE[tsql](../../includes/tsql-md.md)] expression that the debugger evaluates when the breakpoint is reached. If the condition is satisfied and any specified hit count reached, the debugger either breaks or performs the action specified for the breakpoint.  

## Specifying conditions

The expression specified must be a valid Transact-SQL expression that evaluates to a Boolean value. For more information, see [Expressions (Transact-SQL)](../../t-sql/language-elements/expressions-transact-sql.md).  
  
 If you specify a breakpoint condition with invalid syntax, a warning message appears immediately. If you specify a condition with valid syntax but invalid semantics, a warning message is displayed the first time the breakpoint is hit. In either case, the debugger breaks execution when the invalid breakpoint is hit.  
  
### Specify a condition
  
1. In the editor window, right-click the breakpoint glyph, and then select **Conditions...** on the shortcut menu.  
  
     -or-  
  
     In the **Breakpoints** window, right-click the breakpoint glyph, and then select **Settings** on the shortcut menu.  
  
1. In the **Breakpoint Settings** dialog box, select the **Conditions** option and select **Conditional Expression** from the drop-down.

1. Enter a valid Boolean expression for the **Conditional Expression**.  
  
1. Select **Is true** if you want to break when the expression evaluates to **true**, or select **When changed** if you want to break when the value of the expression changes.  
  
    > [!NOTE]  
    > The debugger does not evaluate the Boolean expression until the first time the breakpoint is reached. If you choose **When changed**, the debugger does not consider the first evaluation to be a change, so the debugger will not break on the first evaluation.  
  
## Related content

- [Specify a Hit Count](./specify-hit-count.md)
- [Specify a Breakpoint Action](./specify-breakpoint-action.md)
- [Specify a Breakpoint Filter](specify-breakpoint-filter.md)
