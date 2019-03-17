---
title: "Transact-SQL Debugger Information | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
helpviewer_keywords: 
  - "Transact-SQL debugger, Locals Window"
  - "Transact-SQL debugger, Watch Window"
  - "Transact-SQL debugger, Threads Window"
  - "Transact-SQL debugger, Call Stack Window"
  - "Transact-SQL debugger, QuickWatch"
  - "Transact-SQL debugger, viewing information"
ms.assetid: b99819cc-f388-41a1-b304-36e78ce24147
author: MightyPen
ms.author: genemi
manager: craigg
---
# Transact-SQL Debugger Information
  Every time that the debugger pauses execution on a specific [!INCLUDE[tsql](../../includes/tsql-md.md)] statement, you can use the various debugger windows to view the current execution state.  
  
## Debugger Windows  
 In debugger mode, the debugger opens two windows at the bottom of the main [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] window. The debugger displays all its information in these two windows. Each of the debugger windows has tabs that you can select to control which set of information is displayed in the window. The left debugger window contains the **Locals**, **Watch1**, **Watch2**, **Watch3**, and **Watch4** tabs. The right debugger window contains the **Call Stack**, **Threads**, **Breakpoints**, **Command Window**, and **Output** tabs.  
  
> [!NOTE]  
>  The previous descriptions apply to the default locations of the debugger windows. You can drag a tab to move it from one window to another, or you can undock a tab to create a new window that you can put wherever you want.  
  
 By default, not all of these tabs or windows are active. You can open a particular window by using either of the following ways:  
  
-   On the **Debug** menu, click **Windows**, and then select the window you want.  
  
-   On the **Debug** toolbar, click **Breakpoints**, and then select the window you want.  
  
## Transact-SQL Expressions  
 Expressions are [!INCLUDE[tsql](../../includes/tsql-md.md)] clauses that evaluate to a single, scalar value, such as variables or parameters. The left debugger window can display the data values that are currently assigned to expressions in up to five tabs or windows: **Locals, Watch1**, **Watch2**, **Watch3**, and **Watch4**.  
  
 The **Locals** window displays information about the local variables in the current scope of the [!INCLUDE[tsql](../../includes/tsql-md.md)] debugger. The set of expressions that are listed in the **Locals** window changes as the debugger runs through the different parts of the code.  
  
 The expressions in the **QuickWatch** and the four **Watch** windows are not limited to just listing the identifier of a variable. You can specify a [!INCLUDE[tsql](../../includes/tsql-md.md)] expression that evaluates to a single value, such as adding a number to a variable, or a SELECT statement that evaluates to a single value. Examples include:  
  
-   The name of a variable, such as @IntegerCounter.  
  
-   An arithmetic operation on a variable, such as @IntegerCounter + 1.  
  
-   A string operation on two character variables, such as @FirstName + @LastName.  
  
-   A SELECT statement that returns a single value, such as SELECT CharCol FROM MyTable WHERE PrimaryKey = 1.  
  
 You can use the **QuickWatch** window to view the value of a [!INCLUDE[tsql](../../includes/tsql-md.md)] expression, and then save that expression to a **Watch** window. To select an expression in **QuickWatch**, either select or enter the name of the expression in the **Expression** box.  
  
 The four **Watch** windows display information about variables and expressions that you have selected. The set of expressions that are listed in the **Watch** windows does not change until you either add or delete expressions from the list.  
  
 To add an expression to a **Watch** window, you can either select **Add Watch** in the **QuickWatch** dialog box, or enter the name of the expression in the **Name** column of an empty row in a **Watch** window.  
  
 You can set the data values for variables in the **Locals**, **Watch**, or **QuickWatch** windows by right-clicking the row and then selecting **Edit Value**. The **Value** columns in the **Locals** window, **Watch** window, and **QuickWatch** dialog box all support text, XML, and HTML data visualizers. The visualizers are represented by a magnifying glass data tip on the right side end of the **Values** column. You can use the visualizers to view text, XML, or HTML data values in displays that match the data types, for example, viewing XML files in a browser window.  
  
 In debug mode, when you move the mouse pointer over an identifier, a **Quick Info** pop up is displayed with the name of the expression and its current value. For more information, see [Quick Info &#40;IntelliSense&#41;](quick-info-intellisense.md).  
  
## Breakpoints  
 You can use the **Breakpoints** window to view and manage the currently set breakpoints. For more information, see [Step Through Transact-SQL Code](step-through-transact-sql-code.md).  
  
## Call Stacks  
 The **Call Stack** window displays the current execution location, and information about how execution passed from the original editor window through any [!INCLUDE[tsql](../../includes/tsql-md.md)] modules (functions, stored procedures, or triggers) to reach the current execution location. Each row in the **Call Stack** window is called a stack frame and represents any one of the following items:  
  
-   The current execution location.  
  
-   A call from one module to another.  
  
-   A call from an editor window to a [!INCLUDE[tsql](../../includes/tsql-md.md)] module.  
  
 The order of the stack is the reverse of that in which the modules were called. The current execution location is at the top of the stack and the original call at the bottom. A yellow arrow on the left margin of the stack frame identifies the frame in which the debugger paused execution.  
  
 The **Name** column records the following information:  
  
-   The source module that contains the line of code that called down to the next level.  
  
-   The line of code that called the next module on the stack.  
  
-   If the call went to a stored procedure or function that took parameters, the names, data types, and values of all the parameters are also listed.  
  
 The expressions in the **Locals**, **Watch**, and **QuickWatch** windows are evaluated for the current stack frame. By default, the current stack frame is the top frame in the stack, where the debugger paused execution. When you specify another stack frame as the current frame, the expressions in the **Locals**, **Watch**, and **QuickWatch** windows are reevaluated for the new stack frame. You can change the current stack frame by either by double-clicking a frame or clicking a frame and selecting **Switch To Frame**. At that point, the expressions in the **Locals**, **Watch**, and **QuickWatch** windows are reevaluated for the new frame. Whenever the current stack frame is not the top frame in the stack, a green arrow on the left margin of the stack frame identifies the current stack frame.  
  
 When you right-click a stack frame and select **Go To Source Code**, the code for that frame is displayed in a Query Editor window. However, that frame is not made the current frame, and the contents of the **Locals**, **Watch**, and **QuickWatch** windows are not changed.  
  
## System Information and Transact-SQL Results  
 The debugger lists its status and event messages in the **Output** window. This includes information such as when the debugger attaches to other processes or when debugger threads end.  
  
 While in debug mode, the **Results** and **Messages** tabs are still active in the Query Editor. The **Results** tab continues to display the result sets from the [!INCLUDE[tsql](../../includes/tsql-md.md)] statements that are executed during a debugging session. The **Messages** tab continues to display system messages, such as *xx* Rows Affected and the output of PRINT and RAISERROR statements.  
  
## See Also  
 [Locals Window](transact-sql-debugger-locals-window.md)   
 [Watch Window](transact-sql-debugger-watch-window.md)   
 [QuickWatch Dialog Box](transact-sql-debugger-quickwatch-dialog-box.md)   
 [Breakpoints Window](transact-sql-debugger-breakpoints-window.md)   
 [Call Stack Window](transact-sql-debugger-call-stack-window.md)   
 [Threads Window](transact-sql-debugger-threads-window.md)   
 [Output Window](transact-sql-debugger-output-window.md)   
 [Transact-SQL Debugger](transact-sql-debugger.md)  
  
  
