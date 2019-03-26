---
title: "Derive Column Values by Using the Derived Column Transformation | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
helpviewer_keywords: 
  - "columns [Integration Services]"
  - "derived columns"
  - "columns [Integration Services], values"
  - "Derived Column transformation"
ms.assetid: 28b07746-fc6f-42b2-b741-9de6fac3f29c
author: janinezhang
ms.author: janinez
manager: craigg
---
# Derive Column Values by Using the Derived Column Transformation
  To add and configure a Derived Column transformation, the package must already include at least one Data Flow task and one source.  
  
 The Derived Column transformation uses expressions to update the values of existing or to add values to new columns. When you choose to add values to new columns, the **Derived Column Transformation Editor** dialog box evaluates the expression and defines the metadata of the columns accordingly. For example, if an expression concatenates two columns-each with the DT_WSTR data type and a length of 50-with a space between the two column values, the new column has the DT_WSTR data type and a length of 101. You can update the data type of new columns. The only requirement is that data type be compatible with the inserted data. For example, the **Derived Column Transformation Editor** dialog box generates a validation error when you assign a date value to a column with an integer data type. Depending on the data type that you selected, you can specify the length, precision, scale, and code page of the column.  
  
### To derive column values  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../../../includes/ssbidevstudiofull-md.md)], open the [!INCLUDE[ssISnoversion](../../../includes/ssisnoversion-md.md)] project that contains the package you want.  
  
2.  In Solution Explorer, double-click the package to open it.  
  
3.  Click the **Data Flow** tab, and then, from the **Toolbox**, drag the Derived Column transformation to the design surface.  
  
4.  Connect the Derived Column transformation to the data flow by dragging the connector from the source or the previous transformation to the Derived Column transformation.  
  
5.  Double-click the Derived Column transformation.  
  
6.  In the **Derived Column Transformation Editor** dialog box, build the expressions to use as conditions by dragging variables, columns, functions, and operators to the **Expression** column in the grid. Alternatively, you can type the expression in the **Expression** column.  
  
    > [!NOTE]  
    >  If the expression is not valid, the expression text is highlighted and a ToolTip on the column describes the errors.  
  
7.  In the **Derived Column** list, select **\<add as new column>** to write the evaluation result of the expression to a new column, or select an existing column to update with the evaluation result.  
  
     If you chose to use a new column, the **Derived Column Transformation Editor** dialog box evaluates the expression and assigns a data type to the column, depending on the data type, length, precisions, scale, and code page.  
  
8.  If using a new column, select a data type in the **Data Type** list. Depending on the selected data type, optionally update the values in the **Length**, **Precision**, **Scale**, and **Code Page** columns. Metadata of existing columns cannot be changed.  
  
9. Optionally, modify the values in the **Derived Column Name** column.  
  
10. To configure the error output, click **Configure Error Output**. For more information, see [Debugging Data Flow](../../../integration-services/troubleshooting/debugging-data-flow.md).  
  
11. Click **OK**.  
  
12. To save the updated package, click **Save Selected Items** on the **File** menu.  
  
## See Also  
 [Derived Column Transformation](../../../integration-services/data-flow/transformations/derived-column-transformation.md)   
 [Integration Services Data Types](../../../integration-services/data-flow/integration-services-data-types.md)   
 [Integration Services Transformations](../../../integration-services/data-flow/transformations/integration-services-transformations.md)   
 [Integration Services Paths](../../../integration-services/data-flow/integration-services-paths.md)   
 [Data Flow Task](../../../integration-services/control-flow/data-flow-task.md)   
 [Integration Services &#40;SSIS&#41; Expressions](../../../integration-services/expressions/integration-services-ssis-expressions.md)  
  
  
