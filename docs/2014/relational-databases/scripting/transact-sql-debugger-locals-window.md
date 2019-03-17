---
title: "Locals Window | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
helpviewer_keywords: 
  - "Locals Window [Transact-SQL]"
ms.assetid: 59bea640-7823-4b4d-832c-f384d83cca2f
author: MightyPen
ms.author: genemi
manager: craigg
---
# Locals Window
  The **Locals** window displays information about the local expressions in the current scope of the [!INCLUDE[tsql](../../includes/tsql-md.md)] debugger. The scope is set to the current call stack frame that is selected in the **Call Stack** window. You must be in debug mode to display the local expressions.  
  
## Task List  
 **To access the Locals window**  
  
-   On the **Debug** menu, click **Windows**, and then click **Locals**.  
  
 **To change the value of an expression**  
  
-   Right-click the expression, and then select **Edit Value**.  
  
## Columns  
 **Name**  
 Is the name of the local expression. The [!INCLUDE[tsql](../../includes/tsql-md.md)] debugger lists variables, parameters, and the system functions that have names that start with @@.  
  
 **Value**  
 Displays the value that is currently assigned to the local expression. This column is blank when no value has been assigned to the expression.  
  
 If the length of an expression is larger than the width of the **Value** column, a tooltip displays the full value when you move the pointer over the **Value** cell for that expression.  
  
 A magnifying glass icon in a **Value** cell indicates that the [!INCLUDE[tsql](../../includes/tsql-md.md)] debugger visualizer is available. In the list, you can specify **Text Visualizer**, **XML Visualizer**, or **HTML Visualizer**. To start a debugger visualizer, click the magnifying glass icon. The [!INCLUDE[tsql](../../includes/tsql-md.md)] debugger opens a dialog box that displays the data in a format appropriate to the data type.  
  
 **Type**  
 Displays the data type of the expression.  
  
## See Also  
 [Transact-SQL Debugger](transact-sql-debugger.md)   
 [Transact-SQL Debugger Information](transact-sql-debugger-information.md)   
 [Watch Window](transact-sql-debugger-watch-window.md)   
 [Call Stack Window](transact-sql-debugger-call-stack-window.md)   
 [QuickWatch Dialog Box](transact-sql-debugger-quickwatch-dialog-box.md)   
 [Expressions &#40;Transact-SQL&#41;](/sql/t-sql/language-elements/expressions-transact-sql)  
