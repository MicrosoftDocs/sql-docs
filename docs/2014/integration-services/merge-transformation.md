---
title: "Merge Transformation | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "integration-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
f1_keywords: 
  - "sql12.dts.designer.mergetrans.f1"
helpviewer_keywords: 
  - "merging datasets [Integration Services]"
  - "merging data [Integration Services]"
  - "Merge transformation"
  - "combining datasets"
  - "datasets [Integration Services], merging"
ms.assetid: cff8690c-07ac-46a0-aab5-20bd4848c677
caps.latest.revision: 42
author: "douglaslMS"
ms.author: "douglasl"
manager: "jhubbard"
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
 The Merge Transformation requires sorted data for its inputs. For more information about this important requirement, see [Sort Data for the Merge and Merge Join Transformations](../../2014/integration-services/sort-data-for-the-merge-and-merge-join-transformations.md).  
  
 The Merge transformation also requires that the merged columns in its inputs have matching metadata. For example, you cannot merge a column that has a numeric data type with a column that has a character data type. If the data has a string data type, the length of the column in the second input must be less than or equal to the length of the column in the first input with which it is merged.  
  
 In the [!INCLUDE[ssIS](../includes/ssis-md.md)] Designer, the user interface for the Merge transformation automatically maps columns that have the same metadata. You can then manually map other columns that have compatible data types.  
  
 This transformation has two inputs and one output. It does not support an error output.  
  
## Configuration of the Merge Transformation  
 You can set properties through the [!INCLUDE[ssIS](../includes/ssis-md.md)] Designer or programmatically.  
  
 For more information about the properties that you can set in the **Merge Transformation Editor** dialog box, see [Merge Transformation Editor](../../2014/integration-services/merge-transformation-editor.md).  
  
 For more information about the properties that you can programmatically, click one of the following topics:  
  
-   [Common Properties](../../2014/integration-services/common-properties.md)  
  
-   [Transformation Custom Properties](../../2014/integration-services/transformation-custom-properties.md)  
  
## Related Tasks  
 For details about how to set properties, see the following topics:  
  
-   [Set the Properties of a Data Flow Component](../../2014/integration-services/set-the-properties-of-a-data-flow-component.md)  
  
-   [Sort Data for the Merge and Merge Join Transformations](../../2014/integration-services/sort-data-for-the-merge-and-merge-join-transformations.md)  
  
## See Also  
 [Merge Join Transformation](../../2014/integration-services/merge-join-transformation.md)   
 [Union All Transformation](../../2014/integration-services/union-all-transformation.md)   
 [Data Flow](../../2014/integration-services/data-flow.md)   
 [Integration Services Transformations](../../2014/integration-services/integration-services-transformations.md)  
  
  