---
title: Transact-SQL debugger information
titleSuffix: T-SQL debugger
description: Learn how to view Transact-SQL debugger output, which includes information such as call stacks, threads, breakpoints, code, variables, and commands.
author: erinstellato-ms
ms.author: erinstellato
ms.reviewer: drskwier, maghan
ms.date: 08/28/2024
ms.service: sql
ms.subservice: ssdt
ms.topic: conceptual
---

# Transact-SQL debugger information

 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Every time that the debugger pauses execution on a specific [!INCLUDE[tsql](../../includes/tsql-md.md)] statement, you can use the various debugger windows to view the current execution state.

## Debugger windows  

In debugger mode, the debugger opens windows next to the Query Editor window. The debugger displays its information in the selected windows. Each of the debugger windows has tabs that you can select to control which set of information is displayed in the window. The **Call Stack**, **Breakpoints**, **Exception Settings**, and **Output** tabs are contained in one window. The  **Watch1**, **Watch2**, **Watch3**, and **Watch4** tabs are contained in one window. The **Threads** and **Locals** windows are displayed separately.
  
> [!NOTE]  
> The previous descriptions apply to the default locations of the debugger windows. You can drag a tab to move it from one window to another, or you can undock a tab to create a new window for selected tabs.  
  
 By default, not all of these tabs or windows are active. To open a particular window, on the **Debug** menu, select **Windows**, and then select the window you want to view. 
  
## Transact-SQL expressions

 Expressions are [!INCLUDE[tsql](../../includes/tsql-md.md)] clauses that evaluate to a single, scalar value, such as variables or parameters. The debugger window can display the data values that are currently assigned to expressions in up to five tabs or windows: **Locals, Watch1**, **Watch2**, **Watch3**, and **Watch4**.  
  
 The **Locals** window displays information about the local variables in the current scope of the [!INCLUDE[tsql](../../includes/tsql-md.md)] debugger. The set of expressions that are listed in the **Locals** window changes as the debugger runs through the different parts of the code.  
  
 The expressions in the four **Watch** windows aren't limited to simply listing the identifier of a variable. You can specify a [!INCLUDE[tsql](../../includes/tsql-md.md)] expression that evaluates to a single value, such as adding a number to a variable, or a SELECT statement that evaluates to a single value. Examples include:  
  
- The name of a variable, such as @IntegerCounter.  
  
- An arithmetic operation on a variable, such as @IntegerCounter + 1.  
  
- A string operation on two character variables, such as @FirstName + @LastName.
  
- A SELECT statement that returns a single value, such as SELECT CharCol FROM MyTable WHERE PrimaryKey = 1.  

 The four **Watch** windows display information about selected variables and expressions. The set of expressions that are listed in the **Watch** windows doesn't change until you either add or delete expressions from the list.  
  
 To add an expression to a **Watch** window, enter the name of the expression in the **Name** column of an empty row in a **Watch** window. You can also select **QuickWatch** from the **Debug** menu, enter an expression, then select **Add Watch**.
  
 You can set the data values for variables in the **Locals**, **Watch**, or **QuickWatch** windows by right-clicking the row and then selecting **Edit Value**. The **Value** columns in the **Locals** window, **Watch** window, and **QuickWatch** dialog box all support text, XML, and HTML data visualizers. The visualizers are represented by a magnifying glass data tip on the right side end of the **Values** column. You can use the visualizers to view text, XML, or HTML data values in displays that match the data types, for example, viewing XML files in a browser window.  
  
 In debug mode, when you move the mouse pointer over an identifier, a **Quick Info** pop up is displayed with the name of the expression and its current value. For more information, see [Quick Info (IntelliSense)](../../ssms/scripting/quick-info-intellisense.md).  
  
## Breakpoints

 You can use the **Breakpoints** window to view and manage breakpoints. For more information, see [Step Through Transact-SQL Code](./step-through-transact-sql-code.md).  
  
## Call stacks

 The **Call Stack** window displays the current execution location, and information about how execution passed from the original editor window through any [!INCLUDE[tsql](../../includes/tsql-md.md)] modules (functions, stored procedures, or triggers) to reach the current execution location. Each row in the **Call Stack** window is called a stack frame and represents any one of the following items:  
  
- The current execution location.  
  
- A call from one module to another.  
  
- A call from an editor window to a [!INCLUDE[tsql](../../includes/tsql-md.md)] module.  
  
 The order of the stack is the reverse of the order in which the modules were called. The current execution location is at the top of the stack and the original call at the bottom. A yellow arrow on the left margin of the stack frame identifies the frame in which the debugger paused execution.  
  
 The **Name** column records the following information:  
  
- The source module that contains the line of code that called down to the next level.  
  
- The line of code that called the next module on the stack.  
  
- The names, data types, and values of all the parameters are listed if the call went to a stored procedure or function that takes parameters.
  
 The expressions in the **Locals** and **Watch**, and **QuickWatch** windows are evaluated for the current stack frame. By default, the current stack frame is the top frame in the stack, where the debugger paused execution. When you specify another stack frame as the current frame, the expressions in the **Locals**, **Watch**, and **QuickWatch** windows are reevaluated for the new stack frame. You can change the current stack frame by either by double-clicking a frame or clicking a frame and selecting **Switch To Frame**. At that point, the expressions in the **Locals**, **Watch**, and **QuickWatch** windows are reevaluated for the new frame. Whenever the current stack frame isn't the top frame in the stack, a green arrow on the left margin of the stack frame identifies the current stack frame.  
  
 When you right-click a stack frame and select **Go To Source Code**, the code for that frame is displayed in a Query Editor window. However, that frame isn't made the current frame, and the contents of the **Locals**, **Watch**, and **QuickWatch** windows aren't changed.  
  
## System information and Transact-SQL results

 The debugger lists its status and event messages in the **Output** window. The window includes information such as when the debugger attaches to other processes or when debugger threads end.  
  
 While in debug mode, the **Results** and **Messages** tabs are still active in the Query Editor. The **Results** tab continues to display the result sets from the [!INCLUDE[tsql](../../includes/tsql-md.md)] statements that are executed during a debugging session. The **Messages** tab continues to display system messages, such as the number of rows affected and the output of PRINT and RAISERROR statements.  
  
## Related content

- [Transact-SQL Debugger](./transact-sql-debugger.md)
- [Transact-SQL Breakpoints](transact-sql-breakpoints.md)
