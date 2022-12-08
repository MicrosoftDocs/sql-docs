---
title: Transact-SQL Breakpoints
description: When you debug you can use breakpoints to pause execution as needed. Look here for a list of breakpoint tasks with links to articles that describe them.
titleSuffix: T-SQL debugger
ms.service: sql
ms.subservice: ssms
ms.topic: conceptual
helpviewer_keywords: 
  - "Transact-SQL debugger, breakpoints"
ms.assetid: c234430f-bd94-4d0d-9e74-2bf11681fa50
author: markingmyname
ms.author: maghan
ms.reviewer: ""
ms.custom: seo-lt-2019
ms.date: 12/04/2019
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---

# Transact-SQL Breakpoints

 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Breakpoints specify that the [!INCLUDE[tsql](../../includes/tsql-md.md)] debugger pause execution on a specific [!INCLUDE[tsql](../../includes/tsql-md.md)] statement, you can then view the state of the code elements at that point.

[!INCLUDE[ssms-old-versions](../../includes/ssms-old-versions.md)]

## Breakpoints

When running the [!INCLUDE[tsql](../../includes/tsql-md.md)] debugger, you can toggle a breakpoint on specific statements. When execution reaches a statement with a breakpoint, the debugger pauses execution so you can view debugging information, such as the values present in variables and parameters.

You can manage breakpoints individually in the editor window, or collectively by using the **Breakpoints** window. You can edit breakpoints to specify items such as specific conditions under which execution should pause, or the actions to be taken if the breakpoint is executed.

## Breakpoint Tasks  
  
|Task Description|Topic|  
|----------------------|-----------|  
|Describes how to specify the [!INCLUDE[tsql](../../includes/tsql-md.md)] statement on which you want the debugger to pause.|[Toggle a Breakpoint](./toggle-a-breakpoint.md)|  
|Describes how to temporarily deactivate a breakpoint, and later reactivate it. Also describes how to delete a breakpoint.|[Enable, Disable, and Delete Breakpoints](./enable-disable-and-delete-breakpoints.md)|  
|Describes how to specify a condition, which defines whether breakpoint breaks based on the evaluation of a specified Transact-SQL expression.|[Specify a Breakpoint Condition](./specify-a-breakpoint-condition.md)|  
|Describes how to specify a hit count, which causes a breakpoint to break only when the statement containing the breakpoint has been executed a specified number of times.|[Specify a Hit Count](./specify-a-hit-count.md)|  
|Describes how to specify a filter, which causes a breakpoint to break for only specified processes or threads.|[Specify a Breakpoint Filter](./specify-a-breakpoint-filter.md)|  
|Describes how to specify a **When Hit** action, which is a custom operation that is performed when the breakpoint statement is executed. An example would be to print a message.|[Specify a Breakpoint Action](./specify-a-breakpoint-action.md)|  
|Describes how to edit the location of a breakpoint.|[Edit a Breakpoint Location](./edit-a-breakpoint-location.md)|  
  
## See Also  
 [Transact-SQL Debugger Information](./transact-sql-debugger-information.md)  
  
