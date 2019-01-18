---
title: "Term Lookup Transformation Editor (Term Lookup Tab) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "integration-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.dts.designer.termlookup.termlookup.f1"
helpviewer_keywords: 
  - "Term Lookup Transformation Editor"
ms.assetid: 245d3466-d51f-4073-978a-694a8d9dfaec
author: douglaslms
ms.author: douglasl
manager: craigg
---
# Term Lookup Transformation Editor (Term Lookup Tab)
  Use the **Term Lookup** tab of the **Term Lookup Transformation Editor** dialog box to map an input column to a lookup column in a reference table and to provide an alias for each output column.  
  
 To learn more about the Term Lookup transformation, see [Term Lookup Transformation](data-flow/transformations/lookup-transformation.md).  
  
## Options  
 **Available Input Columns**  
 Using the check boxes, select input columns to pass through to the output unchanged. Drag an input column to the **Available Reference Columns** list to map it to a lookup column in the reference table. The input and lookup columns must have matching, supported data types, either DT_NTEXT or DT_WSTR. Select a mapping line and right-click to edit the mappings in the [Create Relationships](data-flow/transformations/create-relationships.md) dialog box.  
  
 **Available Reference Columns**  
 View the available columns in the reference table. Choose the column that contains the list of terms to match.  
  
 **Pass-Through Column**  
 Select from the list of available input columns. Your selections are reflected in the check box selections in the **Available Input Columns** table.  
  
 **Output Column Alias**  
 Type an alias for each output column. The default is the name of the column; however, you can choose any unique, descriptive name.  
  
 **Configure Error Output**  
 Use the [Configure Error Output](../../2014/integration-services/configure-error-output.md) dialog box to specify error handling options for rows that cause errors.  
  
## See Also  
 [Integration Services Error and Message Reference](../../2014/integration-services/integration-services-error-and-message-reference.md)   
 [Term Lookup Transformation Editor &#40;Reference Table Tab&#41;](../../2014/integration-services/term-lookup-transformation-editor-reference-table-tab.md)   
 [Term Lookup Transformation Editor &#40;Advanced Tab&#41;](../../2014/integration-services/term-lookup-transformation-editor-advanced-tab.md)   
 [Term Extraction Transformation](data-flow/transformations/term-extraction-transformation.md)  
  
  
