---
title: Transact-SQL debugger
description: Learn how to step through code and view the execution state using the Transact-SQL debugger, and how to do various other debugging tasks. 
titleSuffix: T-SQL debugger
ms.service: sql
ms.subservice: ssms
ms.topic: conceptual
helpviewer_keywords: 
  - "Transact-SQL debugger, introduction"
ms.assetid: 6e914699-0d85-46c2-aa2d-3e339ac2c4ce
author: markingmyname
ms.author: maghan
ms.reviewer: drskwier
ms.custom: seo-lt-2019
ms.date: 10/16/2021
monikerRange: ">= sql-server-2016" 
---

# Transact-SQL debugger

 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

The [!INCLUDE[tsql](../../includes/tsql-md.md)] debugger helps you find errors in [!INCLUDE[tsql](../../includes/tsql-md.md)] code by investigating the run-time behavior of the code. After you set the [!INCLUDE[ssDE](../../includes/ssde-md.md)] Query Editor window to debug mode, you can pause execution on specific lines of code and inspect information and data that is used by or returned by those [!INCLUDE[tsql](../../includes/tsql-md.md)] statements.

[!INCLUDE[ssms-old-versions](../../includes/ssms-old-versions.md)]  T-SQL debugging is also available in [SQL Server Data Tools](../../ssdt/sql-server-data-tools.md) for Visual Studio.

## Stepping through Transact-SQL Code

The [!INCLUDE[tsql](../../includes/tsql-md.md)] debugger provides the following options that you can use to navigate through [!INCLUDE[tsql](../../includes/tsql-md.md)] code when the [!INCLUDE[ssDE](../../includes/ssde-md.md)] Query Editor window is in debug mode:

- Set breakpoints on individual [!INCLUDE[tsql](../../includes/tsql-md.md)] statements.

    A breakpoint specifies a point at which you want execution to pause so you can examine data. When you start the debugger, it pauses on the first line of code in the Query Editor window. To run to the first breakpoint that you have set, you can use the **Continue** feature. You can also use the **Continue** feature to run to the next breakpoint from any location at which the window is currently paused. You can edit breakpoints to specify actions such as the conditions under which the breakpoint should pause execution, information to print to the **output** window, and change the location of the breakpoint.  

- Step into the next statement.  

    This option enables you to navigate through a set of statements one by one, and to observe their behavior as you go.  

- Step either into or over a call to a stored procedure or function.  

    If you're sure there are no errors in a stored procedure, you can step over it. The procedure is executed in full, and the results are returned to the code.  

    If you want to debug a stored procedure or function, you can step into the module. SQL Server Management Studio opens a new [!INCLUDE[ssDE](../../includes/ssde-md.md)] Query Editor window that is populated with the source code for the module, places the window into debug mode, and then pauses execution on the first statement in the module. You can then navigate through the module code, for example, by setting breakpoints or stepping through the code.  

For more information about how the debugger enables you to navigate code, see [Step Through Transact-SQL Code](./step-through-transact-sql-code.md).  

## Viewing debugger information

Each time the debugger pauses execution on a specific [!INCLUDE[tsql](../../includes/tsql-md.md)] statement, you can use the following debugger windows to view the current execution state:  

- **Locals** and **Watch.** These windows display currently allocated [!INCLUDE[tsql](../../includes/tsql-md.md)] expressions. Expressions are [!INCLUDE[tsql](../../includes/tsql-md.md)] clauses that evaluate to a single, scalar expression. The [!INCLUDE[tsql](../../includes/tsql-md.md)] debugger supports viewing expressions that reference [!INCLUDE[tsql](../../includes/tsql-md.md)] variables, parameters, or the built-in functions that have names that start with @@. These windows also display the data values that are currently assigned to the expressions.  

- **QuickWatch.** This window displays the value of a [!INCLUDE[tsql](../../includes/tsql-md.md)] expression, and enables saving that expression to a **Watch** window.  

- **Breakpoints.** This window displays the currently set breakpoints and enables you to manage them.  

- **Call Stack.** This window displays the current execution location. And also provides information about how execution passed from the original Query Editor window through any functions, stored procedures, or triggers to reach the current execution location.  

- **Output.** This window displays various messages and program data, such as system messages from the debugger.  

- **Results** and **Messages.** These tabs on the Query Editor window display the results of previously executed [!INCLUDE[tsql](../../includes/tsql-md.md)] statements.  

## Transact-SQL debugger tasks  

|Task Description|Topic|  
|----------------------|-----------|  
|Describes how to configure the [!INCLUDE[tsql](../../includes/tsql-md.md)] debugger for remote debugging.|[Configure firewall rules before running the TSQL Debugger](./configure-firewall-rules-before-running-the-tsql-debugger.md)|  
|Describes how to start, stop, and control the operation of the debugger.|[Run the Transact-SQL Debugger](./run-the-transact-sql-debugger.md)|  
|Describes how to use the [!INCLUDE[tsql](../../includes/tsql-md.md)] debugger to step through code.|[Step Through Transact-SQL Code](./step-through-transact-sql-code.md)|  
|Describes how to use the debugger to view [!INCLUDE[tsql](../../includes/tsql-md.md)] data, such as parameters and variables, and system information.|[Transact-SQL Debugger Information](./transact-sql-debugger-information.md)|  

## See also

[Query and Text Editors &#40;SQL Server Management Studio&#41;](../f1-help/database-engine-query-editor-sql-server-management-studio.md)