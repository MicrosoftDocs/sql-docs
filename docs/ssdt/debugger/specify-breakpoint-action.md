---
title: Specify a breakpoint action
titleSuffix: T-SQL debugger
description: Learn now to specify a What Hit action - a custom task for the Transact-SQL debugger to perform when a breakpoint is hit and certain other conditions are satisfied.
author: erinstellato-ms
ms.author: erinstellato
ms.reviewer: drskwier, maghan, randolphwest
ms.date: 08/29/2024
ms.service: sql
ms.subservice: ssms
ms.topic: conceptual
---

# Specify a breakpoint action

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

A breakpoint action specifies a custom task that the [!INCLUDE [tsql](../../includes/tsql-md.md)] debugger performs for a breakpoint. If the specified hit count is reached and a specified breakpoint condition is satisfied, the debugger performs the action specified for the breakpoint.

## Action considerations

The default action for a breakpoint is to break execution when both the hit count and breakpoint condition are satisfied. The primary use of an action in the [!INCLUDE [tsql](../../includes/tsql-md.md)] debugger is to print information to the debugger **Output** window.

The message is specified in the **Show a message in the Output Window:** box, and is specified as a text string that includes expressions containing information from the [!INCLUDE [tsql](../../includes/tsql-md.md)] being debugged. Expressions include:

- A [!INCLUDE [tsql](../../includes/tsql-md.md)] expression contained in curly braces (`{}`). The expressions can include [!INCLUDE [tsql](../../includes/tsql-md.md)] variables, parameters, and built-in functions. Examples include `{@MyVariable}`, `{@NameParameter}`, `{@@SPID}`, or `{SERVERPROPERTY('ProcessID')}`.

- One of the following keywords:

  - `$ADDRESS` returns the name of the stored procedure or user-defined function where the breakpoint is set. If the breakpoint is set in the editor window, `$ADDRESS` returns the name of the script file being edited. `$ADDRESS` and `$FUNCTION` return the same information in the [!INCLUDE [tsql](../../includes/tsql-md.md)] debugger.

  - `$CALLER` returns the name of the unit of [!INCLUDE [tsql](../../includes/tsql-md.md)] code that called a stored procedure or function. If the breakpoint is in the editor window, `$CALLER` returns `<No caller available>`. If the breakpoint is in a stored procedure or user-defined function called from the code in the editor window, `$CALLER` returns the name of the file being edited. If the breakpoint is in a stored procedure or user-defined function called from another stored procedure or function, `$CALLER` returns the name of the calling procedure or function.

  - `$CALLSTACK` returns the call stack of functions in the chain that called the current stored procedure or user-defined function. If the breakpoint is in the editor window, `$CALLSTACK` returns the name of the script file being edited.

  - `$FUNCTION` returns the name of the stored procedure or user-defined function where the breakpoint is set. If the breakpoint is set in the editor window, `$FUNCTION` returns the name of the script file being edited.

  - `$PID` and `$PNAME` return the ID and name of the operating system process running the instance of the Database Engine where the [!INCLUDE [tsql](../../includes/tsql-md.md)] is running. `$PID` returns the same ID as `SERVERPROPERTY('ProcessID')`, except that `$PID` is a hexadecimal value while `SERVERPROPERTY('ProcessID')` is a decimal value.

  - `$TID` and `$TNAME` return the ID and name of the operating system thread running the [!INCLUDE [tsql](../../includes/tsql-md.md)] batch. The thread is one associated with the process running the instance of the Database Engine. `$TID` returns the same value as `SELECT kpid FROM sys.sysprocesses WHERE spid = @@SPID`, except that `$TID` is a hexadecimal value while `kpid` is a decimal value.

- You can also use the backslash character (`\`) as an escape character to allow curly braces and backslashes in the message: `\{`, `\}`, and `\\`.

### Specify an action

1. In the editor window, right-click the breakpoint glyph, and then select **Actions** on the shortcut menu.

   -or-

   In the **Breakpoints** window, right-click the breakpoint glyph, and then select **Settings** on the shortcut menu.

1. In the **Breakpoint Settings** dialog box, select the **Actions** option.

1. In the **Show a message in the Output Window:** dialog, enter an expression.

1. Select **Continue code execution** if you don't want the breakpoint to pause execution. This option is active only if you select the **Actions** option.

1. Select **Close** to implement the changes.

## Related content

- [Specify a breakpoint condition](specify-breakpoint-condition.md)
- [Specify a hit count](specify-hit-count.md)
- [Specify a breakpoint filter](specify-breakpoint-filter.md)
