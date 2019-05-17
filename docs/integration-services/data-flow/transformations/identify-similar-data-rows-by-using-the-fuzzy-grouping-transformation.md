---
title: "Identify Similar Data Rows by Using the Fuzzy Grouping Transformation | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
helpviewer_keywords: 
  - "Fuzzy Grouping transformation"
  - "match similar data [Integration Services]"
  - "similar data rows [Integration Services]"
  - "fuzzy matches"
ms.assetid: ffcb41a6-e23d-49ea-8c32-ac980e3dc495
author: janinezhang
ms.author: janinez
manager: craigg
---
# Identify Similar Data Rows by Using the Fuzzy Grouping Transformation

[!INCLUDE[ssis-appliesto](../../../includes/ssis-appliesto-ssvrpluslinux-asdb-asdw-xxx.md)]


  To add and configure a Fuzzy Grouping transformation, the package must already include at least one Data Flow task and a source.  
  
### To implement Fuzzy Grouping transformation in a data flow  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../../../includes/ssbidevstudiofull-md.md)], open the [!INCLUDE[ssISnoversion](../../../includes/ssisnoversion-md.md)] project that contains the package you want.  
  
2.  In Solution Explorer, double-click the package to open it.  
  
3.  Click the **Data Flow** tab, and then, from the **Toolbox**, drag the Fuzzy Grouping transformation to the design surface.  
  
4.  Connect the Fuzzy Grouping transformation to the data flow by dragging the connector from the data source or a previous transformation to the Fuzzy Grouping transformation.  
  
5.  Double-click the Fuzzy Grouping transformation.  
  
6.  In the **Fuzzy Grouping Transformation Editor** dialog box, on the **Connection Manager** tab, select an OLE DB connection manager that connects to a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] database.  
  
    > [!NOTE]  
    >  The transformation requires a connection to a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] database to create temporary tables and indexes.  
  
7.  Click the **Columns** tab and, in the **Available Input Columns** list, select the check box of the input columns to use to identify similar rows in the dataset.  
  
8.  Select the check box in the **Pass Through** column to identify the input columns to pass through to the transformation output. Pass-through columns are not included in the process of identification of duplicate rows.  
  
    > [!NOTE]  
    >  Input columns that are used for grouping are automatically selected as pass-through columns, and they cannot be unselected while used for grouping.  
  
9. Optionally, update the names of output columns in the **Output Alias** column.  
  
10. Optionally, update the names of cleaned columns in the **Group OutputAlias** column.  
  
    > [!NOTE]  
    >  The default names of columns are the names of the input columns with a "_clean" suffix.  
  
11. Optionally, update the type of match to use in the **Match Type** column.  
  
    > [!NOTE]  
    >  At least one column must use fuzzy matching.  
  
12. Specify the minimum similarity level columns in the **Minimum Similarity** column. The value must be between 0 and 1. The closer the value is to 1, the more similar the values in the input columns must be to form a group. A minimum similarity of 1 indicates an exact match.  
  
13. Optionally, update the names of similarity columns in the **Similarity Output Alias** column.  
  
14. To specify the handling of numbers in data values, update the values in the **Numerals** column.  
  
15. To specify how the transformation compares the string data in a column, modify the default selection of comparison options in the **Comparison Flags** column.  
  
16. Click the **Advanced** tab to modify the names of the columns that the transformation adds to the output for the unique row identifier (_key_in), the duplicate row identifier (_key_out), and the similarity value (_score).  
  
17. Optionally, adjust the similarity threshold by moving the slider bar.  
  
18. Optionally, clear the token delimiter check boxes to ignore delimiters in the data.  
  
19. Click **OK**.  
  
20. To save the updated package, click **Save Selected Items** on the **File** menu.  
  
## See Also  
 [Fuzzy Grouping Transformation](../../../integration-services/data-flow/transformations/fuzzy-grouping-transformation.md)   
 [Integration Services Transformations](../../../integration-services/data-flow/transformations/integration-services-transformations.md)   
 [Integration Services Paths](../../../integration-services/data-flow/integration-services-paths.md)   
 [Data Flow Task](../../../integration-services/control-flow/data-flow-task.md)  
  
  
