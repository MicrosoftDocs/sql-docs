---
title: "Call Stack Window | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
helpviewer_keywords: 
  - "Call Stack Window [Transact-SQL]"
ms.assetid: ddb0b19c-87cd-4883-bcb8-ec09ffb30369
author: MightyPen
ms.author: genemi
manager: craigg
---
# Call Stack Window
  The **Call Stack** window displays the modules on the call stack, and the data types and values of any parameters that are passed to the modules. [!INCLUDE[tsql](../../includes/tsql-md.md)] modules include stored procedures, functions, and triggers. To display the call stack, you must be in debug mode.  
  
## Task List  
 **To access the Call Stack window**  
  
-   On the **Debug** menu, click **Windows**, and then click **Call Stack**.  
  
 **To change the current Call Stack frame**  
  
 You can use either of the following procedures to make a stack frame the current frame:  
  
-   Right-click the stack frame, and then click **Switch To Frame**.  
  
-   Double-click the stack frame.  
  
 **To view the source of a frame other than the current frame**  
  
-   Right-click the stack frame, and then click **Go To Source Code**.  
  
## Stack Frames  
 Each row in the **Call Stack** window is called a stack frame and represents either a call from a [!INCLUDE[tsql](../../includes/tsql-md.md)] script file to a module or a call from one module to another. The bottom stack frame in the display indicates the line in the [!INCLUDE[ssDE](../../includes/ssde-md.md)] Query Editor window that made the first call into the stack. The top row indicates the line on which the debugger paused execution, and is identified by a yellow arrow in the left margin of the window. Each intermediate row indicates the module and the line number of the source code that called the next higher stack frame.  
  
 All expressions in the **Locals**, **Watch**, and **QuickWatch** windows are evaluated based on the current stack frame. The Query Editor window displays the code for the current frame. By default, the current stack frame is the frame in which the [!INCLUDE[tsql](../../includes/tsql-md.md)] debugger paused execution. When you change the current stack frame to another frame, the expressions in the **Locals**, **Watch**, and **QuickWatch** windows are reevaluated in the context of the new frame, and the source code of the new frame is displayed in the Query Editor window.  
  
## Columns  
 **Name**  
 Displays information about a module on the call stack.  
  
 For the bottom row in the call stack, **Name** lists the Query Editor source window and line number of the first call into the stack. For the other rows, **Name** has the format **Module(Instance.Database)(ParmList) LineNumber**.  
  
 **Module**  
 Is the name of the stored procedure, function, or stored procedure that called to the next frame.  
  
 **Instance.Database**  
 Is the instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)] and the database that is holding the module.  
  
 **ParmList**  
 Indicates the data type, name, and value for each parameter that is passed in during the call to the module.  
  
 **LineNumber**  
 For all rows except the top row, **LineNumber** indicates which line in the module called to the frame. For the top row, **LineNumber** indicates the line on which the debugger is currently focused.  
  
 **Language**  
 Displays **Transact-SQL** for [!INCLUDE[tsql](../../includes/tsql-md.md)].  
  
## See Also  
 [Transact-SQL Debugger](transact-sql-debugger.md)   
 [Transact-SQL Debugger Information](transact-sql-debugger-information.md)   
 [Step Through Transact-SQL Code](step-through-transact-sql-code.md)  
