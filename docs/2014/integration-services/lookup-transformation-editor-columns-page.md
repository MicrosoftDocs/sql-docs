---
title: "Lookup Transformation Editor (Columns Page) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "integration-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.dts.designer.lookuptransformation.columns.f1"
helpviewer_keywords: 
  - "Lookup Transformation Editor"
ms.assetid: 690ffef5-fd59-4e95-a27d-4fcf0d6b1c0b
author: douglaslms
ms.author: douglasl
manager: craigg
---
# Lookup Transformation Editor (Columns Page)
  Use the **Columns** page of the **Lookup Transformation Editor** dialog box to specify the join between the source table and the reference table, and to select lookup columns from the reference table.  
  
 To learn more about the Lookup transformation, see [Lookup Transformation](data-flow/transformations/lookup-transformation.md).  
  
## Options  
 **Available Input Columns**  
 View the list of available input columns. The input columns are the columns in the data flow from a connected source. The input columns and lookup column must have matching data types.  
  
 Use a drag-and-drop operation to map available input columns to lookup columns.  
  
 You can also map input columns to lookup columns using the keyboard, by highlighting a column in the **Available Input Columns** table, pressing the Application key, and then clicking **Edit Mappings**.  
  
 **Available Lookup Columns**  
 View the list of lookup columns. The lookup columns are columns in the reference table in which you want to look up values that match the input columns.  
  
 Use a drag-and-drop operation to map available lookup columns to input columns.  
  
 Use the check boxes to select lookup columns in the reference table on which to perform lookup operations.  
  
 You can also map lookup columns to input columns using the keyboard, by highlighting a column in the **Available Lookup Columns** table, pressing the Application key, and then clicking **Edit Mappings**.  
  
 **Lookup Column**  
 View the selected lookup columns. The selections are reflected in the check box selections in the **Available Lookup Columns** table.  
  
 **Lookup Operation**  
 Select a lookup operation from the list to perform on the lookup column.  
  
 **Output Alias**  
 Type an alias for the output for each lookup column. The default is the name of the lookup column; however, you can select any unique, descriptive name.  
  
## See Also  
 [Lookup Transformation Editor &#40;General Page&#41;](general-page-of-integration-services-designers-options.md)   
 [Lookup Transformation Editor &#40;Connection Page&#41;](../../2014/integration-services/lookup-transformation-editor-connection-page.md)   
 [Lookup Transformation Editor &#40;Advanced Page&#41;](../../2014/integration-services/lookup-transformation-editor-advanced-page.md)   
 [Lookup Transformation Editor &#40;Error Output Page&#41;](../../2014/integration-services/lookup-transformation-editor-error-output-page.md)   
 [Fuzzy Lookup Transformation](data-flow/transformations/fuzzy-lookup-transformation.md)  
  
  
