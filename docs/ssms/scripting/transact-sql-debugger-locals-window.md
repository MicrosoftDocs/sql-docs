---
title: "Locals Window | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.technology: scripting
ms.reviewer: ""
ms.topic: conceptual
helpviewer_keywords: 
  - "Locals Window [Transact-SQL]"
ms.assetid: 59bea640-7823-4b4d-832c-f384d83cca2f
author: stevestein
ms.author: sstein
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Transact-SQL Debugger - Locals Window
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]
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
 [Transact-SQL Debugger](../../relational-databases/scripting/transact-sql-debugger.md)   
 [Transact-SQL Debugger Information](../../relational-databases/scripting/transact-sql-debugger-information.md)   
 [Watch Window](../../relational-databases/scripting/transact-sql-debugger-watch-window.md)   
 [Call Stack Window](../../relational-databases/scripting/transact-sql-debugger-call-stack-window.md)   
 [QuickWatch Dialog Box](../../relational-databases/scripting/transact-sql-debugger-quickwatch-dialog-box.md)   
 [Expressions &#40;Transact-SQL&#41;](../../t-sql/language-elements/expressions-transact-sql.md)  
