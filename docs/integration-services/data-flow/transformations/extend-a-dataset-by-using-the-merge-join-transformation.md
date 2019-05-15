---
title: "Extend a Dataset by Using the Merge Join Transformation | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
helpviewer_keywords: 
  - "Merge Join transformation"
  - "datasets [Integration Services], joining"
  - "datasets [Integration Services], extending"
  - "joining datasets [Integration Services]"
ms.assetid: 9e512c3c-f89b-45f3-8281-cdb8f35a2b1f
author: janinezhang
ms.author: janinez
manager: craigg
---
# Extend a Dataset by Using the Merge Join Transformation

[!INCLUDE[ssis-appliesto](../../../includes/ssis-appliesto-ssvrpluslinux-asdb-asdw-xxx.md)]


  To add and configure a Merge Join transformation, the package must already include at least one Data Flow task and two data flow components that provide inputs to the Merge Join transformation.  
  
 The Merge Join transformation requires two sorted inputs. For more information, see [Sort Data for the Merge and Merge Join Transformations](../../../integration-services/data-flow/transformations/sort-data-for-the-merge-and-merge-join-transformations.md).  
  
### To extend a dataset  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../../../includes/ssbidevstudiofull-md.md)], open the [!INCLUDE[ssISnoversion](../../../includes/ssisnoversion-md.md)] project that contains the package you want.  
  
2.  In Solution Explorer, double-click the package to open it.  
  
3.  Click the **Data Flow** tab, and then, from the **Toolbox**, drag the Merge Join transformation to the design surface.  
  
4.  Connect the Merge Join transformation to the data flow by dragging the connector from a data source or a previous transformation to the Merge Join transformation.  
  
5.  Double-click the Merge Join transformation.  
  
6.  In the **Merge Join Transformation Editor** dialog box, select the type of join to use in the **Join type** list.  
  
    > [!NOTE]  
    >  If you select the **Left outer join** type, you can click **Swap Inputs** to switch the inputs and convert the left outer join to a right outer join.  
  
7.  Drag columns in the left input to columns in the right input to specify the join columns. If the columns have the same name, you can select the **Join Key** check box and the Merge Join transformation automatically creates the join.  
  
    > [!NOTE]  
    >  You can create joins only between columns that have the same sort position, and the joins must be created in the order specified by the sort position. If you attempt to create the joins out of order, the **Merge Join Transformation Editor** prompts you to create additional joins for the skipped sort order positions.  
  
    > [!NOTE]  
    >  By default, the output is sorted on the join columns.  
  
8.  In the left and right inputs, select the check boxes of additional columns to include in the output. Join columns are included by default.  
  
9. Optionally, update the names of output columns in the **Output Alias** column.  
  
10. Click **OK**.  
  
11. To save the updated package, click **Save Selected Items** on the **File** menu.  
  
## See Also  
 [Merge Join Transformation](../../../integration-services/data-flow/transformations/merge-join-transformation.md)   
 [Integration Services Transformations](../../../integration-services/data-flow/transformations/integration-services-transformations.md)   
 [Integration Services Paths](../../../integration-services/data-flow/integration-services-paths.md)   
 [Data Flow Task](../../../integration-services/control-flow/data-flow-task.md)  
  
  
