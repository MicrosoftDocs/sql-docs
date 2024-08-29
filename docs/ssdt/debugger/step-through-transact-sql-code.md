---
title: Step through Transact-SQL code
description: Learn how to use the Transact-SQL debugger to control which Transact-SQL statements are run in a Database Engine Query Editor window.
author: erinstellato-ms
ms.author: erinstellato
ms.reviewer: drskwier, maghan
ms.date: 08/28/2024
ms.service: sql
ms.subservice: ssdt
ms.topic: conceptual
---

# Step through Transact-SQL code

 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

The [!INCLUDE[tsql](../../includes/tsql-md.md)] debugger enables you to control which [!INCLUDE[tsql](../../includes/tsql-md.md)] statements are run in a [!INCLUDE[ssDE](../../includes/ssde-md.md)] Query Editor window. You can pause the debugger on individual statements and then view the state of the code elements at that point.  

## Breakpoints

A breakpoint signals the debugger to pause execution on a specific [!INCLUDE[tsql](../../includes/tsql-md.md)] statement. For more information about breakpoints, see [Transact-SQL Breakpoints](./transact-sql-breakpoints.md).  
  
## Controlling statement execution

In the [!INCLUDE[tsql](../../includes/tsql-md.md)] debugger, you can specify the following options for executing from the current statement in [!INCLUDE[tsql](../../includes/tsql-md.md)] code:

- Run to the next breakpoint.

- Step into the next statement.  

    If the next statement invokes a [!INCLUDE[tsql](../../includes/tsql-md.md)] stored procedure, function, or trigger, the debugger displays a new Query Editor window that contains the code of the module. The window is in debug mode, and execution pauses on the first statement in the module. You can then move through the module code, for example, by setting breakpoints or stepping through the code.

- Step over the next statement.

    The next statement is executed. If the statement invokes a stored procedure, function, or trigger, the module code runs until it finishes, returning the results to the calling code. If you're confident there are no errors in a stored procedure, you can step over it. Execution pauses on the statement that follows the call to the stored procedure, function, or trigger.

- Step out of a stored procedure, function, or trigger.  

    Execution pauses on the statement that follows the call to the stored procedure, function, or trigger.  

- Run from the current location to the current location of the pointer, and ignore all breakpoints.  

 The following table lists the various ways in which you can control how statements execute in the [!INCLUDE[tsql](../../includes/tsql-md.md)] debugger.  
  
|Action|Perform action:|  
|------------|---------------------|  
|Run all statements from the current statement to the next breakpoint|Select **Continue** on the **Debug** menu. Select the **Continue** button on the **Debug** toolbar. Press F5.|  
|Step into the next statement or module|Select **Step Into** on the **Debug** menu. Select the **Step Into** button on the **Debug** toolbar. Press F11.|  
|Step over the next statement or module|Select **Step Over** on the **Debug** menu. Select the **Step Over** button on the **Debug** toolbar. Press F10.|  
|Step out of a module|Select **Step Out** on the **Debug** menu. Select the **Step Out** button on the **Debug** toolbar. Press SHIFT+F11.|  
|Run to the current cursor location|Right-click in the Query Editor window, and then select **Run To Cursor**. Press CTRL+F10.|  
  
## Related content

- [Transact-SQL Debugger](./transact-sql-debugger.md)
- [Run the Transact-SQL Debugger](./run-transact-sql-debugger.md)
- [Debug Stored Procedures](debug-stored-procedures.md)
- [Transact-SQL Debugger Information](./transact-sql-debugger-information.md)
