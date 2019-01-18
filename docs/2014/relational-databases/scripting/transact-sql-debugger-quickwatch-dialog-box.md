---
title: "QuickWatch Dialog Box | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
f1_keywords: 
  - "vs.debug.quickwatch"
helpviewer_keywords: 
  - "QuickWatch Dialog [Transact-SQL]"
ms.assetid: d6bbb373-1452-41f2-bdc5-86ae689c3dc0
author: MightyPen
ms.author: genemi
manager: craigg
---
# QuickWatch Dialog Box
  Use the **QuickWatch** dialog box to quickly view the data type and value of one [!INCLUDE[tsql](../../includes/tsql-md.md)] expression, such as a variable or parameter, when you are debugging [!INCLUDE[tsql](../../includes/tsql-md.md)] code. To watch multiple expressions, you can also add the expression to a **Watch** window.  
  
## Task List  
 **To access the QuickWatch dialog box**  
  
-   On the **Debug** menu, click **QuickWatch**.  
  
 **To view the information about an expression**  
  
1.  In the **Expression** list box, type or select the expression that you want. The following [!INCLUDE[tsql](../../includes/tsql-md.md)] expressions are supported:  
  
    -   Variables.  
  
    -   Parameters.  
  
    -   System functions whose name starts with @@.  
  
    -   Expressions built by applying operators to one or more variables, parameters, or system functions, such as @IntegerCounter + 1, or FirstName + LastName.  
  
    -   Transact-SQL statements that return a single value, such as: SELECT CharacterCol FROM MyTable WHERE PrimaryKey = 1.  
  
2.  Click **Reevaluate**.  
  
 **To add the QuickWatch expression to a Watch window**  
  
-   Click **Add Watch**.  
  
 **To change the value of the QuickWatch expression**  
  
-   Right-click the expression, and then select **Edit Value**.  
  
## Options  
 **Expression list**  
 Displays the currently selected expression. The drop-down list contains a set of expressions that you can select to display. The expressions in the list are those available in the scope of the stack frame that is currently selected in the **Call Stack** window. To display a different expression, either enter the expression or select it from list. The [!INCLUDE[tsql](../../includes/tsql-md.md)] debugger supports the following expressions: variables, parameters, and the system functions that have names that start with @@.  
  
 **Value grid**  
 Displays the properties of the expression that is currently being watched.  
  
 **Name**  
 Is the [!INCLUDE[tsql](../../includes/tsql-md.md)] expression being watched.  
  
 **Value**  
 Displays the value that is currently assigned to the expression. A blank is displayed when the expression currently has no value.  
  
 If the length of an expression is larger than the width of the **Value** column, a tooltip displays the full value when you move the pointer over the **Value** cell for that expression.  
  
 A magnifying glass icon in a **Value** cell indicates that the [!INCLUDE[tsql](../../includes/tsql-md.md)] debugger visualizer is available. In the list, you can specify **Text Visualizer**, **XML Visualizer**, or **HTML Visualizer**. To start a debugger visualizer, click the magnifying glass icon. The [!INCLUDE[tsql](../../includes/tsql-md.md)] debugger opens a dialog box that displays the data in a format appropriate to the data type.  
  
 **Type**  
 Displays the data type of the expression.  
  
## See Also  
 [Transact-SQL Debugger](transact-sql-debugger.md)   
 [Transact-SQL Debugger Information](transact-sql-debugger-information.md)   
 [Watch Window](transact-sql-debugger-watch-window.md)   
 [Locals Window](transact-sql-debugger-locals-window.md)   
 [Call Stack Window](transact-sql-debugger-call-stack-window.md)   
 [Expressions &#40;Transact-SQL&#41;](/sql/t-sql/language-elements/expressions-transact-sql)  
  
  
