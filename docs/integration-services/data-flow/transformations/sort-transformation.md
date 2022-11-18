---
description: "Sort Transformation"
title: "Sort Transformation | Microsoft Docs"
ms.custom: ""
ms.date: "03/04/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql13.dts.designer.sorttrans.f1"
  - "sql13.dts.designer.sorttransformation.f1"
helpviewer_keywords: 
  - "Sort transformation"
  - "descending sorts"
  - "ascending sorts"
  - "sorting data [Integration Services]"
  - "multiple sorts"
  - "duplicate data [Integration Services]"
ms.assetid: 728c9351-84a8-4a89-be4d-d50d4adc04e0
author: chugugrace
ms.author: chugu
---
# Sort Transformation

[!INCLUDE[sqlserver-ssis](../../../includes/applies-to-version/sqlserver-ssis.md)]


  The Sort transformation sorts input data in ascending or descending order and copies the sorted data to the transformation output. You can apply multiple sorts to an input; each sort is identified by a numeral that determines the sort order. The column with the lowest number is sorted first, the sort column with the second lowest number is sorted next, and so on. For example, if a column named **CountryRegion** has a sort order of 1 and a column named **City** has a sort order of 2, the output is sorted by country/region and then by city. A positive number denotes that the sort is ascending, and a negative number denotes that the sort is descending. Columns that are not sorted have a sort order of 0. Columns that are not selected for sorting are automatically copied to the transformation output together with the sorted columns.  
  
 The Sort transformation includes a set of comparison options to define how the transformation handles the string data in a column. For more information, see [Comparing String Data](../../../integration-services/data-flow/comparing-string-data.md).  
  
> [!NOTE]  
>  The Sort transformation does not sort GUIDs in the same order as the ORDER BY clause does in Transact-SQL. While the Sort transformation sorts GUIDs that start with 0-9 before GUIDs that start with A-F, the ORDER BY clause, as implemented in the [!INCLUDE[ssDEnoversion](../../../includes/ssdenoversion-md.md)], sorts them differently. For more information, see [ORDER BY Clause &#40;Transact-SQL&#41;](../../../t-sql/queries/select-order-by-clause-transact-sql.md).  
  
 The Sort transformation can also remove duplicate rows as part of its sort. Duplicate rows are rows with the same sort key values. The sort key value is generated based on the string comparison options being used, which means that different literal strings may have the same sort key values. The transformation identifies rows in the input columns that have different values but the same sort key as duplicates.  
  
 The Sort transformation includes the **MaximumThreads** custom property that can be updated by a property expression when the package is loaded. For more information, see [Integration Services &#40;SSIS&#41; Expressions](../../../integration-services/expressions/integration-services-ssis-expressions.md), [Use Property Expressions in Packages](../../../integration-services/expressions/use-property-expressions-in-packages.md), and [Transformation Custom Properties](../../../integration-services/data-flow/transformations/transformation-custom-properties.md).  
  
 This transformation has one input and one output. It does not support error outputs.  
  
## Configuration of the Sort Transformation  
 You can set properties through the [!INCLUDE[ssIS](../../../includes/ssis-md.md)] Designer or programmatically.  
  
 The **Advanced Editor** dialog box reflects the properties that can be set programmatically. For more information about the properties that you can set in the **Advanced Editor** dialog box or programmatically, click one of the following topics:  
  
-   [Common Properties](../set-the-properties-of-a-data-flow-component.md)  
  
-   [Transformation Custom Properties](../../../integration-services/data-flow/transformations/transformation-custom-properties.md)  
  
## Related Tasks  
 For more information about how to set properties of the component, see [Set the Properties of a Data Flow Component](../../../integration-services/data-flow/set-the-properties-of-a-data-flow-component.md).  
  
  
## Sort Transformation Editor
  Use the **Sort Transformation Editor** dialog box to select the columns to sort, set the sort order, and specify whether duplicates are removed.  
  
### Options  
 **Available Input Columns**  
 Using the check boxes, specify the columns to sort.  
  
 **Name**  
 View the name of each available input column.  
  
 **Passthrough**  
 Indicate whether to include the column in the sorted output.  
  
 **Input Column**  
 Select from the list of available input columns for each row. Your selections are reflected in the check box selections in the **Available Input Columns** table.  
  
 **Output Alias**  
 Type an alias for each output column. The default is the name of the input column; however, you can choose any unique, descriptive name.  
  
 **Sort Type**  
 Indicate whether to sort in ascending or descending order.  
  
 **Sort Order**  
 Indicate the order in which to sort columns. This must be set manually for each column.  
  
 **Comparison Flags**  
 For information about the string comparison options, see [Comparing String Data](../../../integration-services/data-flow/comparing-string-data.md).  
  
 **Remove rows with duplicate sort values**  
 Indicate whether the transformation copies duplicate rows to the transformation output, or creates a single entry for all duplicates, based on the specified string comparison options.  
  
## See Also  
 [Data Flow](../../../integration-services/data-flow/data-flow.md)   
 [Integration Services Transformations](../../../integration-services/data-flow/transformations/integration-services-transformations.md)  
