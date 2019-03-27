---
title: "Merge Transformation | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql13.dts.designer.mergetrans.f1"
  - "sql13.dts.designer.mergetransformation.f1"
helpviewer_keywords: 
  - "merging datasets [Integration Services]"
  - "merging data [Integration Services]"
  - "Merge transformation"
  - "combining datasets"
  - "datasets [Integration Services], merging"
ms.assetid: cff8690c-07ac-46a0-aab5-20bd4848c677
author: janinezhang
ms.author: janinez
manager: craigg
---
# Merge Transformation
  The Merge transformation combines two sorted datasets into a single dataset. The rows from each dataset are inserted into the output based on values in their key columns.  
  
 By including the Merge transformation in a data flow, you can perform the following tasks:  
  
-   Merge data from two data sources, such as tables and files.  
  
-   Create complex datasets by nesting Merge transformations.  
  
-   Remerge rows after correcting errors in the data.  
  
 The Merge transformation is similar to the Union All transformations. Use the Union All transformation instead of the Merge transformation in the following situations:  
  
-   The transformation inputs are not sorted.  
  
-   The combined output does not need to be sorted.  
  
-   The transformation has more than two inputs.  
  
## Input Requirements  
 The Merge Transformation requires sorted data for its inputs. For more information about this important requirement, see [Sort Data for the Merge and Merge Join Transformations](../../../integration-services/data-flow/transformations/sort-data-for-the-merge-and-merge-join-transformations.md).  
  
 The Merge transformation also requires that the merged columns in its inputs have matching metadata. For example, you cannot merge a column that has a numeric data type with a column that has a character data type. If the data has a string data type, the length of the column in the second input must be less than or equal to the length of the column in the first input with which it is merged.  
  
 In the [!INCLUDE[ssIS](../../../includes/ssis-md.md)] Designer, the user interface for the Merge transformation automatically maps columns that have the same metadata. You can then manually map other columns that have compatible data types.  
  
 This transformation has two inputs and one output. It does not support an error output.  
  
## Configuration of the Merge Transformation  
 You can set properties through the [!INCLUDE[ssIS](../../../includes/ssis-md.md)] Designer or programmatically.  
  
 For more information about the properties that you can programmatically, click one of the following topics:  
  
-   [Common Properties](https://msdn.microsoft.com/library/51973502-5cc6-4125-9fce-e60fa1b7b796)  
  
-   [Transformation Custom Properties](../../../integration-services/data-flow/transformations/transformation-custom-properties.md)  
  
## Related Tasks  
 For details about how to set properties, see the following topics:  
  
-   [Set the Properties of a Data Flow Component](../../../integration-services/data-flow/set-the-properties-of-a-data-flow-component.md)  
  
-   [Sort Data for the Merge and Merge Join Transformations](../../../integration-services/data-flow/transformations/sort-data-for-the-merge-and-merge-join-transformations.md)  
  
## Merge Transformation Editor
  Use the **Merge Transformation Editor** to specify columns from two sorted sets of data to be merged.  
  
> [!IMPORTANT]  
>  The Merge Transformation requires sorted data for its inputs. For more information about this important requirement, see [Sort Data for the Merge and Merge Join Transformations](../../../integration-services/data-flow/transformations/sort-data-for-the-merge-and-merge-join-transformations.md).  
  
### Options  
 **Output Column Name**  
 Specify the name of the output column.  
  
 **Merge Input 1**  
 Select the column to merge as Merge Input 1.  
  
 **Merge Input 2**  
 Select the column to merge as Merge Input 2.  
  
## See Also  
 [Merge Join Transformation](../../../integration-services/data-flow/transformations/merge-join-transformation.md)   
 [Union All Transformation](../../../integration-services/data-flow/transformations/union-all-transformation.md)   
 [Data Flow](../../../integration-services/data-flow/data-flow.md)   
 [Integration Services Transformations](../../../integration-services/data-flow/transformations/integration-services-transformations.md)  
  
  
