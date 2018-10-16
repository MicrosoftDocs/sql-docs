---
title: "Aggregate Values in a Dataset by Using the Aggregate Transformation | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
helpviewer_keywords: 
  - "Aggregate transformation [Integration Services]"
  - "aggregate values [Integration Services]"
  - "datasets [Integration Services], aggregate values"
ms.assetid: 01b81c0f-d5e0-483b-81b2-73800a6945ac
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# Aggregate Values in a Dataset by Using the Aggregate Transformation
  To add and configure an Aggregate transformation, the package must already include at least one Data Flow task and one source.  
  
### To aggregate values in a dataset  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../../../includes/ssbidevstudiofull-md.md)], open the [!INCLUDE[ssISnoversion](../../../includes/ssisnoversion-md.md)] project that contains the package you want.  
  
2.  In Solution Explorer, double-click the package to open it.  
  
3.  Click the **Data Flow** tab, and then, from the **Toolbox**, drag the Aggregate transformation to the design surface.  
  
4.  Connect the Aggregate transformation to the data flow by dragging a connector from the source or the previous transformation to the Aggregate transformation.  
  
5.  Double-click the transformation.  
  
6.  In the **Aggregate Transformation Editor** dialog box, click the **Aggregations** tab.  
  
7.  In the **Available Input Columns** list, select the check box by the columns on which you want to aggregate values. The selected columns appear in the table.  
  
    > [!NOTE]  
    >  You can select a column multiple times, applying multiple transformations to the column. To uniquely identify aggregations, a number is appended to the default name of the output alias of the column.  
  
8.  Optionally, modify the value in the **Output Alias** columns.  
  
9. To change the default aggregation operation, **Group by**, select a different operation in the **Operation** list.  
  
10. To change the default comparison, select the individual comparison flags listed in the **Comparison Flags** column. By default, a comparison ignores case, kana type, non-spacing characters, and character width.  
  
11. Optionally, for the **Count distinct** aggregation, specify an exact number of distinct values in the **Count Distinct Keys** column, or select an approximate count in the **Count Distinct Scale** column.  
  
    > [!NOTE]  
    >  Providing the number of distinct values, exact or approximate, optimizes performance, because the transformation can preallocate an appropriate amount of memory to do its work.  
  
12. Optionally, click **Advanced** and update the name of the Aggregate transformation output. If the aggregations include a **Group By** operation, you can select an approximate count of grouping key values in the **Keys Scale** column or specify an exact number of grouping key values in the **Keys** column.  
  
    > [!NOTE]  
    >  Providing the number of distinct values, exact or approximate, optimizes performance, because the transformation can preallocate an appropriate amount of memory to do its work.  
  
    > [!NOTE]  
    >  The **Keys Scale** and **Keys** options are mutually exclusive. If you enter values in both columns, the larger value in either **Keys Scale** or **Keys** is used.  
  
13. Optionally, click the **Advanced** tab and set the attributes that apply to optimizing all the operations that the Aggregate transformation performs.  
  
14. Click **OK**.  
  
15. To save the updated package, click **Save Selected Items** on the **File** menu.  
  
## See Also  
 [Aggregate Transformation](../../../integration-services/data-flow/transformations/aggregate-transformation.md)   
 [Integration Services Transformations](../../../integration-services/data-flow/transformations/integration-services-transformations.md)   
 [Integration Services Paths](../../../integration-services/data-flow/integration-services-paths.md)   
 [Data Flow Task](../../../integration-services/control-flow/data-flow-task.md)  
  
  
