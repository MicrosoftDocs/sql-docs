---
title: Transact-SQL breakpoints
titleSuffix: T-SQL debugger
description: Breakpoints are used when debugging to pause execution as needed. Look here for a list of breakpoint tasks with links to articles that describe them.
author: erinstellato-ms
ms.author: erinstellato
ms.reviewer: drskwier, maghan
ms.date: 08/28/2024
ms.service: sql
ms.subservice: ssdt
ms.topic: conceptual
---

# Transact-SQL breakpoints

 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Breakpoints specify that the [!INCLUDE[tsql](../../includes/tsql-md.md)] debugger pause execution on a specific [!INCLUDE[tsql](../../includes/tsql-md.md)] statement, and you can then view the state of the code elements at that point.

## Breakpoints

When running the [!INCLUDE[tsql](../../includes/tsql-md.md)] debugger, you can toggle a breakpoint on specific statements. When execution reaches a statement with a breakpoint, the debugger pauses execution so you can view debugging information, such as the values present in variables and parameters.

You can manage breakpoints individually in the Query Editor window, or collectively by using the **Breakpoints** window. You can edit breakpoints to specify items such as specific conditions under which execution should pause, or the actions to be taken if the breakpoint is executed.

## Breakpoint tasks  
  
|Task Description|Article|  
|----------------------|-----------|  
|Describes how to specify the [!INCLUDE[tsql](../../includes/tsql-md.md)] statement on which you want the debugger to pause.|[Toggle a Breakpoint](./toggle-breakpoint.md)|  
|Describes how to temporarily deactivate a breakpoint, reactivate a breakpoint, and how to delete a breakpoint.|[Enable, Disable, and Delete Breakpoints](./enable-disable-delete-breakpoints.md)|  
|Describes how to specify a condition, which defines whether a breakpoint breaks based on the evaluation of a specified Transact-SQL expression.|[Specify a Breakpoint Condition](./specify-breakpoint-condition.md)|  
|Describes how to specify a hit count.  A break occurs only when the statement containing the breakpoint executes a specified number of times.|[Specify a Hit Count](./specify-hit-count.md)|  
|Describes how to specify a filter, which causes a breakpoint to break for only specified processes or threads.|[Specify a Breakpoint Filter](./specify-breakpoint-filter.md)|  
|Describes how to specify an action, which is a custom operation that is performed when the breakpoint statement is executed.|[Specify a Breakpoint Action](./specify-breakpoint-action.md)|  
  
## Related content

- [Transact-SQL Debugger Information](./transact-sql-debugger-information.md)
- [Toggle a Breakpoint](toggle-breakpoint.md)
