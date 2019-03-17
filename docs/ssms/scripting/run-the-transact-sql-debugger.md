---
title: "Run the Transact-SQL Debugger | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.technology: scripting
ms.reviewer: ""
ms.topic: conceptual
helpviewer_keywords: 
  - "Transact-SQL debugger, sysadmin requirement"
  - "Transact-SQL debugger, supported versions"
  - "Query Editor [Database Engine], right-click menu"
  - "debugging [SQL Server], T-SQL debugger"
  - "Transact-SQL debugger, Query Editor shortcut menu"
  - "Transact-SQL debugger, stopping"
  - "Transact-SQL debugger, Debug menu"
  - "debugging [SQL Server]"
  - "Transact-SQL debugger, Debug toolbar"
  - "Transact-SQL debugger, keyboard shortcuts"
  - "Transact-SQL debugger, starting"
ms.assetid: 386f6d09-dbec-4dc7-9e8a-cd9a4a50168c
author: stevestein
ms.author: sstein
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Run the Transact-SQL Debugger
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]
  You can start the [!INCLUDE[tsql](../../includes/tsql-md.md)] debugger after you open a [!INCLUDE[ssDE](../../includes/ssde-md.md)] Query Editor window. Then, you can run your [!INCLUDE[tsql](../../includes/tsql-md.md)] code in debug mode until you stop the debugger. You can set options to customize how the debugger runs.  
  
## Starting and Stopping the Debugger  
 The requirements to start the [!INCLUDE[tsql](../../includes/tsql-md.md)] debugger are as follows:  
  
-   If your [!INCLUDE[ssDE](../../includes/ssde-md.md)] Query Editor is connected to an instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)] on another computer, you must have configured the debugger for remote debugging. For more information, see [Configure firewall rules before running the TSQL Debugger](../../relational-databases/scripting/configure-firewall-rules-before-running-the-tsql-debugger.md).  
  
-   [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] must be running under a Windows account that is a member of the sysadmin fixed server roll.  
  
-   The [!INCLUDE[ssDE](../../includes/ssde-md.md)] Query Editor window must be connected by using either a Windows Authentication or [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication login that is a member of the sysadmin fixed server role.  
  
-   The [!INCLUDE[ssDE](../../includes/ssde-md.md)] Query Editor window must be connected to an instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)] from [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] Service Pack 2 (SP2) or later. You cannot run the debugger when the Query Editor window is connected to an instance that is in single-user mode.  
  
 We recommend that [!INCLUDE[tsql](../../includes/tsql-md.md)] code be debugged on a test server, not a production server, for the following reasons:  
  
-   Debugging is a highly privileged operation. Therefore, only members of the sysadmin fixed server role are allowed to debug in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
-   Debugging sessions often run for long periods of time while you investigate the operations of several [!INCLUDE[tsql](../../includes/tsql-md.md)] statements. Locks, such as update locks, that are acquired by the session might be held for extended periods, until the session is ended or the transaction committed or rolled back.  
  
 Starting the [!INCLUDE[tsql](../../includes/tsql-md.md)] debugger puts the Query Editor window into debug mode. When the Query Editor window enters debug mode, the debugger pauses at the first line of code. You can then step through the code, pause the execution on specific [!INCLUDE[tsql](../../includes/tsql-md.md)] statements, and use the debugger windows to view the current execution state. You can start the debugger by either clicking the **Debug** button on the **Query** toolbar or by clicking **Start Debugging** on the **Debug** menu.  
  
 The Query Editor window stays in debug mode until either the last statement in the Query Editor window finishes or you stop debug mode. You can stop debug mode and statement execution by using any one of the following methods:  
  
-   On the **Debug** menu, click **Stop Debugging**.  
  
-   On the **Debug** toolbar, click the **Stop Debugging** button.  
  
-   On the **Query** menu, click **Cancel Executing Query**.  
  
-   On the **Query** toolbar, click the **Cancel Executing Query** button.  
  
 You can also stop debug mode and allow for the remaining [!INCLUDE[tsql](../../includes/tsql-md.md)] statements to finish executing by clicking **Detach All** on the **Debug** menu.  
  
## Controlling the Debugger  
 You can control how the [!INCLUDE[tsql](../../includes/tsql-md.md)] debugger operates by using the following menu commands, toolbars, and shortcuts:  
  
-   The **Debug** menu and the **Debug** toolbar. Both the **Debug** menu and **Debug** toolbar are inactive until the focus is placed in an open Query Editor window. They remain active until the current project is closed.  
  
-   The debugger keyboard shortcuts.  
  
-   The Query Editor shortcut menu. The shortcut menu is displayed when you right-click a line in a Query Editor window. When the Query Editor window is in debug mode, the shortcut menu displays debugger commands that apply to the selected line or string.  
  
-   Menu items and context commands in the windows that are opened by the debugger, such as the **Watch** or **Breakpoints** windows.  
  
 The following table shows the debugger menu commands, toolbar buttons, and keyboard shortcuts.  
  
|Debug menu command|Editor shortcut command|Toolbar button|Keyboard shortcut|Action|  
|------------------------|-----------------------------|--------------------|-----------------------|------------|  
|**Windows/Breakpoints**|Not available|**Breakpoints**|CTRL+ALT+B|Display the **Breakpoints** window in which you can view and manage breakpoints.|  
|**Windows/Watch/Watch1**|Not available|**Breakpoints/Watch/Watch1**|CTRL+ALT+W, 1|Display the **Watch1** window.|  
|**Windows/Watch/Watch2**|Not available|**Breakpoints/Watch/Watch2**|CTRL+ALT+W, 2|Display the **Watch2** window.|  
|**Windows/Watch/Watch3**|Not available|**Breakpoints/Watch/Watch3**|CTRL+ALT+W, 3|Display the **Watch3** window.|  
|**Windows/Watch/Watch4**|Not available|**Breakpoints/Watch/Watch4**|CTRL+ALT+W, 4|Display the **Watch4** window.|  
|**Windows/Locals**|Not available|**Breakpoints/Locals**|CTRL+ALT+V, L|Display the **Locals** window.|  
|**Windows/Call Stack**|Not available|**Breakpoints/Call Stack**|CTRL+ALT+C|Display the **Call Stack** window.|  
|**Windows/Threads**|Not available|**Breakpoints/Threads**|CTRL+ALT+H|Display the **Threads** window.|  
|**Continue**|Not available|**Continue**|ALT+F5|Run to the next breakpoint. **Continue** is not active until you are focused on a Query Editor window that is in debug mode.|  
|**Start Debugging**|Not available|**Start Debugging**|ALT+F5|Put a Query Editor window into debug mode and run to the first breakpoint. If you are focused on a Query Editor window that is in debug mode, **Start Debugging** is replaced by **Continue**.|  
|**Break All**|Not available|**Break All**|CTRL+ALT+BREAK|This feature not used by the [!INCLUDE[tsql](../../includes/tsql-md.md)] debugger.|  
|**Stop Debugging**|Not available|**Stop Debugging**|SHIFT+F5|Take a Query Editor window out of debug mode and return it to regular mode.|  
|**Detach All**|Not available|Not available|Not available|Stops debug mode, but executes the remaining statements in the Query Editor window.|  
|**Step Into**|Not available|**Step Into**|F11|Run the next statement, and also open a new Query Editor window in debug mode if the next statement runs a stored procedure, trigger, or function.|  
|**Step Over**|Not available|**Step Over**|F10|Same as **Step Into**, except that no functions, stored procedures, or triggers are debugged.|  
|**Step Out**|Not available|**Step Out**|SHIFT+F11|Execute the remaining code in a trigger, function, or stored procedure without pausing for any breakpoints. Regular debug mode resumes when control is returned to the code that called the module.|  
|Not available|**Run To** Cursor|Not available|CTRL+F10|Execute all code from the last stop location to the current cursor location without stopping at any breakpoints.|  
|**QuickWatch**|**QuickWatch**|Not available|CTRL+ALT+Q|Display the **QuickWatch** window.|  
|**Toggle Breakpoint**|**Breakpoint/Insert Breakpoint**|Not available|F9|Position a breakpoint on the current or selected [!INCLUDE[tsql](../../includes/tsql-md.md)] statement.|  
|Not available|**Breakpoint/Delete Breakpoint**|Not available|Not available|Delete the breakpoint from the selected line.|  
|Not available|**Breakpoint/Disable Breakpoint**|Not available|Not available|Disable the breakpoint on the selected line. The breakpoint remains on the line of code, but will not stop execution until it is reenabled.|  
|Not available|**Breakpoint/Enable Breakpoint**|Not available|Not available|Enable the breakpoint on the selected line.|  
|**Delete All Breakpoints**|Not available|Not available|CTRL+SHIFT+F9|Delete all breakpoints.|  
|**Disable All Breakpoints**|Not available|Not available|Not available|Disable all breakpoints.|  
|Not available|**Add Watch**|Not available|Not available|Add the selected expression to the **Watch** window.|  
  
## See Also  
 [Transact-SQL Debugger](../../relational-databases/scripting/transact-sql-debugger.md)   
 [Step Through Transact-SQL Code](../../relational-databases/scripting/step-through-transact-sql-code.md)   
 [Transact-SQL Debugger Information](../../relational-databases/scripting/transact-sql-debugger-information.md)   
 [Database Engine Query Editor &#40;SQL Server Management Studio&#41;](../../relational-databases/scripting/database-engine-query-editor-sql-server-management-studio.md)   
 [Live Query Statistics](../../relational-databases/performance/live-query-statistics.md)  
  
  
