---
title: "Split a Dataset by Using the Conditional Split Transformation | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
helpviewer_keywords: 
  - "Conditional Split transformation"
  - "splitting dataset"
  - "datasets [Integration Services], splitting"
ms.assetid: 23b3e84f-9296-4dc9-81c0-c7f06ae3f1ff
author: janinezhang
ms.author: janinez
manager: craigg
---
# Split a Dataset by Using the Conditional Split Transformation
  To add and configure a Conditional Split transformation, the package must already include at least one Data Flow task and a source.  
  
### To conditionally split a dataset  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../../../includes/ssbidevstudiofull-md.md)], open the [!INCLUDE[ssISnoversion](../../../includes/ssisnoversion-md.md)] project that contains the package you want.  
  
2.  In Solution Explorer, double-click the package to open it.  
  
3.  Click the **Data Flow** tab, and, from the **Toolbox**, drag the Conditional Split transformation to the design surface.  
  
4.  Connect the Conditional Split transformation to the data flow by dragging the connector from the data source or the previous transformation to the Conditional Split transformation.  
  
5.  Double-click the Conditional Split transformation.  
  
6.  In the **Conditional Split Transformation Editor**, build the expressions to use as conditions by dragging variables, columns, functions, and operators to the **Condition** column in the grid. Or, you can type the expression in the **Condition** column.  
  
    > [!NOTE]  
    >  A variable or column can be used in multiple expressions.  
  
    > [!NOTE]  
    >  If the expression is not valid, the expression text is highlighted and a ToolTip on the column describes the errors.  
  
7.  Optionally, modify the values in the **Output Name** column. The default names are Case 1, Case 2, and so forth.  
  
8.  To modify the sequence in which the conditions are evaluated, click the up arrow or down arrow.  
  
    > [!NOTE]  
    >  Place the conditions that are most likely to be encountered at the top of the list.  
  
9. Optionally, modify the name of the default output for data rows that do not match any condition.  
  
10. To configure an error output, click **Configure Error Output**. For more information, see [Debugging Data Flow](../../../integration-services/troubleshooting/debugging-data-flow.md).  
  
11. Click **OK**.  
  
12. To save the updated package, click **Save Selected Items** on the **File** menu.  
  
## See Also  
 [Conditional Split Transformation](../../../integration-services/data-flow/transformations/conditional-split-transformation.md)   
 [Integration Services Transformations](../../../integration-services/data-flow/transformations/integration-services-transformations.md)   
 [Integration Services Paths](../../../integration-services/data-flow/integration-services-paths.md)   
 [Integration Services Data Types](../../../integration-services/data-flow/integration-services-data-types.md)   
 [Data Flow Task](../../../integration-services/control-flow/data-flow-task.md)   
 [Integration Services &#40;SSIS&#41; Expressions](../../../integration-services/expressions/integration-services-ssis-expressions.md)  
  
  
