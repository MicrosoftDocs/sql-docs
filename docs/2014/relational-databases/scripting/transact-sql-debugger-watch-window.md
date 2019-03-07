---
title: "Watch Window | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
helpviewer_keywords: 
  - "Watch Window [Transact-SQL]"
ms.assetid: 23f3baa4-14c2-4262-92f7-3f43fcfa0436
author: MightyPen
ms.author: genemi
manager: craigg
---
# Watch Window
  The **Watch** window displays information about the expressions that you have selected. There can be up to four watch windows: **Watch 1**, **Watch 2, Watch 3**, and **Watch 4**. The expressions are evaluated within the scope of the current call stack frame that is selected in the **Call Stack** window. You must be in debug mode to watch variables and expressions.  
  
## Task List  
 **To access the Watch windows**  
  
-   On the **Debug** menu, click **Windows**, click **Watch**, and then click **Watch 1**, **Watch 2, Watch 3**, or **Watch 4**.  
  
 **To change the value of an expression**  
  
-   Right-click the expression, and then select **Edit Value**.  
  
## Columns  
 **Name**  
 Are the expressions that are listed by the [!INCLUDE[tsql](../../includes/tsql-md.md)] debugger. The following expressions are supported:  
  
-   Variables.  
  
-   Parameters.  
  
-   System functions whose name starts with @@.  
  
-   Expressions built by applying operators to one or more variables, parameters, or system functions, such as @IntegerCounter + 1, or FirstName + LastName.  
  
-   Transact-SQL statements that return a single value, such as: SELECT CharacterCol FROM MyTable WHERE PrimaryKey = 1.  
  
 **Value**  
 Displays the value that is returned after the [!INCLUDE[tsql](../../includes/tsql-md.md)] debugger evaluates the expression specified in **Name**.  
  
 If the length of an expression is larger than the width of the **Value** column, a tooltip displays the full value when you move the pointer over the **Value** cell for that expression.  
  
 A magnifying glass icon in a **Value** cell indicates that the [!INCLUDE[tsql](../../includes/tsql-md.md)] debugger visualizer is available. In the list, you can specify **Text Visualizer**, **XML Visualizer**, or **HTML Visualizer**. To start a debugger visualizer, click the magnifying glass icon. The [!INCLUDE[tsql](../../includes/tsql-md.md)] debugger opens a dialog that displays the data in a format appropriate to the data type.  
  
 **Type**  
 Displays the data type of the expression.  
  
## See Also  
 [Transact-SQL Debugger](transact-sql-debugger.md)   
 [Transact-SQL Debugger Information](transact-sql-debugger-information.md)   
 [Locals Window](transact-sql-debugger-locals-window.md)   
 [Call Stack Window](transact-sql-debugger-call-stack-window.md)   
 [QuickWatch Dialog Box](transact-sql-debugger-quickwatch-dialog-box.md)   
 [Expressions &#40;Transact-SQL&#41;](/sql/t-sql/language-elements/expressions-transact-sql)  
