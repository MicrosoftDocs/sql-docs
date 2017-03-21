---
title: "Sort Transformation Editor | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "integration-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
f1_keywords: 
  - "sql13.dts.designer.sorttransformation.f1"
helpviewer_keywords: 
  - "Sort Transformation Editor"
ms.assetid: 8ae23970-49a9-4d6d-9f15-c7074783347c
caps.latest.revision: 25
author: "douglaslMS"
ms.author: "douglasl"
manager: "jhubbard"
---
# Sort Transformation Editor
  Use the **Sort Transformation Editor** dialog box to select the columns to sort, set the sort order, and specify whether duplicates are removed.  
  
 To learn more about the Sort transformation, see [Sort Transformation](../../../integration-services/data-flow/transformations/sort-transformation.md).  
  
## Options  
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
 [Integration Services Error and Message Reference](../../../integration-services/integration-services-error-and-message-reference.md)  
  
  