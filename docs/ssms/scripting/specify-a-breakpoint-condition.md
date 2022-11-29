---
title: Specify a Breakpoint Condition
description: Learn how to establish a breakpoint condition to control whether the debugger performs a breakpoint action when the breakpoint is hit and the hit count is reached. 
titleSuffix: T-SQL debugger
ms.service: sql
ms.subservice: ssms
ms.topic: conceptual
helpviewer_keywords: 
  - "Transact-SQL debugger, breakpoint conditions"
ms.assetid: b43d8a2b-99a3-4fb7-8848-99c042ea7ef7
author: markingmyname
ms.author: maghan
ms.custom: seo-lt-2019
ms.reviewer: ""
ms.date: 12/04/2019
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---

# Specify a Breakpoint Condition

 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

A breakpoint condition is a [!INCLUDE[tsql](../../includes/tsql-md.md)] expression that is evaluated by the debugger when the breakpoint is reached. If the condition is satisfied and any specified hit count reached, the debugger either breaks or performs the action specified for the breakpoint.  

[!INCLUDE[ssms-old-versions](../../includes/ssms-old-versions.md)]

## Specifying Conditions

The expression specified must be a valid Transact-SQL expression that evaluates to a Boolean value. For more information, see [Expressions &#40;Transact-SQL&#41;](../../t-sql/language-elements/expressions-transact-sql.md).  
  
 If you specify a breakpoint condition with invalid syntax, a warning message appears immediately. If you specify a condition with valid syntax but invalid semantics, a warning message is displayed the first time the breakpoint is hit. In either case, the debugger breaks execution when the invalid breakpoint is hit.  
  
#### To Specify a Condition
  
1. In the editor window, right-click the breakpoint glyph, and then click **Condition** on the shortcut menu.  
  
     -or-  
  
     In the **Breakpoints** window, right-click the breakpoint glyph, and then click **Condition** on the shortcut menu.  
  
2. In the **Breakpoint Condition** dialog box, enter a valid Boolean expression in the **Condition** box.  
  
3. Choose **Is true** if you want to break when the expression evaluates to **true**, or choose **Has changed** if you want to break when the value of the expression has changed.  
  
    > [!NOTE]  
    >  The debugger does not evaluate the Boolean expression until the first time the breakpoint is reached. If you choose **Has changed**, the debugger does not consider the first evaluation to be a change, so the debugger will not break on the first evaluation.  
  
## See Also

- [Specify a Hit Count](./specify-a-hit-count.md)
- [Specify a Breakpoint Action](./specify-a-breakpoint-action.md)