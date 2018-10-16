---
title: "Step Through Transact-SQL Code | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
helpviewer_keywords: 
  - "Transact-SQL debugger, debugging code"
  - "Transact-SQL debugger, step over"
  - "Transact-SQL debugger, step out"
  - "Transact-SQL debugger, step into"
ms.assetid: e09079b8-c4c9-42b4-821b-4ce81a98a086
author: MightyPen
ms.author: genemi
manager: craigg
---
# Step Through Transact-SQL Code
  The [!INCLUDE[tsql](../../includes/tsql-md.md)] debugger enables you to control which [!INCLUDE[tsql](../../includes/tsql-md.md)] statements are run in a [!INCLUDE[ssDE](../../includes/ssde-md.md)] Query Editor window. You can pause the debugger on individual statements and then view the state of the code elements at that point.  
  
## Breakpoints  
 A breakpoint signals the debugger to pause execution on a specific [!INCLUDE[tsql](../../includes/tsql-md.md)] statement. For more information about breakpoints, see Using Transact-SQL Breakpoints.  
  
## Controlling Statement Execution  
 In the [!INCLUDE[tsql](../../includes/tsql-md.md)] debugger, you can specify the following options for executing from the current statement in [!INCLUDE[tsql](../../includes/tsql-md.md)] code:  
  
-   Run to the next breakpoint.  
  
-   Step into the next statement.  
  
     If the next statement invokes a [!INCLUDE[tsql](../../includes/tsql-md.md)] stored procedure, function, or trigger, the debugger displays a new Query Editor window that contains the code of the module. The window is in debug mode, and execution pauses on the first statement in the module. You can then move through the module code, for example, by setting breakpoints or stepping through the code.  
  
-   Step over the next statement.  
  
     The next statement is executed. However, if the statement invokes a stored procedure, function, or trigger, the module code runs until it finishes, and the results are returned to the calling code. If you are sure there are no errors in a stored procedure, you can step over it. Execution pauses on the statement that follows the call to the stored procedure, function, or trigger.  
  
-   Step out of a stored procedure, function, or trigger.  
  
     Execution pauses on the statement that follows the call to the stored procedure, function, or trigger.  
  
-   Run from the current location to the current location of the pointer, and ignore all breakpoints.  
  
 The following table lists the various ways in which you can control how statements execute in the [!INCLUDE[tsql](../../includes/tsql-md.md)] debugger.  
  
|Action|Procedure|  
|------------|---------------|  
|Run all statements from the current statement to the next breakpoint|On the **Debug** menu, click **Continue**.<br /><br /> On the **Debug** toolbar, click the **Continue** button.|  
|Step into the next statement or module|On the **Debug** menu, click **Step Into**.<br /><br /> On the **Debug** toolbar, click the **Step Into** button.<br /><br /> Press F11.|  
|Step over the next statement or module|On the **Debug** menu, click **Step Over**.<br /><br /> On the **Debug** toolbar, click the **Step Over** button.<br /><br /> Press F10.|  
|Step out of a module|On the **Debug** menu, click **Step Out**.<br /><br /> On the **Debug** toolbar, click the **Step Out** button.<br /><br /> Press SHIFT+F11.|  
|Run to the current cursor location|Right-click in the Query Editor window, and then click **Run To Cursor**.<br /><br /> Press CTRL+F10.|  
  
## See Also  
 [Transact-SQL Debugger Information](transact-sql-debugger-information.md)  
  
  
