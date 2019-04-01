---
title: "Use an Expression in a Data Flow Component | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "integration-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "components [Integration Services], data flow"
  - "expressions [Integration Services], data flow components"
ms.assetid: 9181b998-d24a-41fb-bb3c-14eee34f910d
author: janinezhang
ms.author: janinez
manager: craigg
---
# Use an Expression in a Data Flow Component
  This procedure describes how to add an expression to the Conditional Split transformation or to the Derived Column transformation. The Conditional Split transformation uses expressions to define the conditions that direct data rows to a transformation output, and the Derived Column transformation uses expressions to define values assigned to columns.  
  
 To implement an expression in a transformation, the package must already include at least one Data Flow task and a source. For information about adding items to packages, see the following topics:  
  
-   [Add or Delete a Task or a Container in a Control Flow](control-flow/add-or-delete-a-task-or-a-container-in-a-control-flow.md)  
    
  
-   [Add or Delete a Component in a Data Flow](data-flow/add-or-delete-a-component-in-a-data-flow.md)  
  
-   [Connect Components in a Data Flow](data-flow/connect-components-in-a-data-flow.md)  
  
### To create an expression  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)], open the [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] project that contains the package you want.  
  
2.  In Solution Explorer, double-click the package to open it.  
  
3.  In [!INCLUDE[ssIS](../includes/ssis-md.md)] Designer, click the **Control Flow** tab, and then click the Data Flow task that contains the data flow in which you want to implement an expression.  
  
4.  Click the **Data Flow** tab, and drag either a Conditional Split or Derived Column transformation from the **Toolbox** to the design surface.  
  
5.  Drag the green connector from the source or a transformation to the Conditional Split or Derived Column transformation.  
  
6.  Double-click the transformation to open its dialog box.  
  
7.  In the left pane, expand **Variables** to display system and user-defined variables, and expand **Columns** to display the transformation input columns.  
  
8.  In the right pane, expand **Mathematical Functions**, **String Functions**, **Date/Time Functions**, **NULL Functions**, **Type Casts**, and **Operators** to access the functions, the casts, and the operators that the expression grammar provides.  
  
9. Depending on the transformation, do one of the following to build an expression:  
  
    -   In the **Conditional Split Transformation Editor** dialog box, drag variables, columns, functions, operators, and casts to the **Condition** column. Alternatively, you can type an expression directly in the **Condition** column.  
  
    -   In the **Derived Column Transformation Editor** dialog box, drag variables, columns, functions, operators, and casts to the **Expression** column. Alternatively, you can type an expression directly in the **Expression** column.  
  
        > [!NOTE]  
        >  When you remove the focus from the **Condition** column or the **Expression** column, the expression text might be highlighted to indicate that the expression syntax is incorrect.  
  
10. Click **OK** to exit the dialog box.  
  
    > [!NOTE]  
    >  If the expression is not valid, an alert appears describing the syntax errors in the expression.  
  
## See Also  
 [Integration Services &#40;SSIS&#41; Expressions](expressions/integration-services-ssis-expressions.md)   
 [Conditional Split Transformation](data-flow/transformations/conditional-split-transformation.md)   
 [Derived Column Transformation](data-flow/transformations/derived-column-transformation.md)   
 [Data Flow Task](control-flow/data-flow-task.md)   
 [Data Flow](data-flow/data-flow.md)  
  
  
