---
title: "Fuzzy Lookup Transformation Editor (Columns Tab) | Microsoft Docs"
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
  - "sql13.dts.designer.fuzzylookuptransformation.columns.f1"
helpviewer_keywords: 
  - "Fuzzy Lookup Transformation Editor"
ms.assetid: aaf45327-79e9-4760-9b4d-546ace91b5b4
caps.latest.revision: 26
author: "douglaslMS"
ms.author: "douglasl"
manager: "jhubbard"
---
# Fuzzy Lookup Transformation Editor (Columns Tab)
  Use the **Columns** tab of the **Fuzzy Lookup Transformation Editor** dialog box to set properties for input and output columns.  
  
 To learn more about the Fuzzy Lookup transformation, see [Fuzzy Lookup Transformation](../../../integration-services/data-flow/transformations/fuzzy-lookup-transformation.md).  
  
## Options  
 **Available Input Columns**  
 Drag input columns to connect them to available lookup columns. These columns must have matching, supported data types. Select a mapping line and right-click to edit the mappings in the [Create Relationships](../../../integration-services/data-flow/transformations/create-relationships.md) dialog box.  
  
 **Name**  
 View the names of the available input columns.  
  
 **Pass Through**  
 Specify whether to include the input columns in the output of the transformation.  
  
 **Available Lookup Columns**  
 Use the check boxes to select columns on which to perform fuzzy lookup operations.  
  
 **Lookup Column**  
 Select lookup columns from the list of available reference table columns. Your selections are reflected in the check box selections in the **Available Lookup Columns** table. Selecting a column in the **Available Lookup Columns** table creates an output column that contains the reference table column value for each matching row returned.  
  
 **Output Alias**  
 Type an alias for the output for each lookup column. The default is the name of the lookup column with a numeric index value appended; however, you can choose any unique, descriptive name.  
  
## See Also  
 [Integration Services Error and Message Reference](../../../integration-services/integration-services-error-and-message-reference.md)   
 [Fuzzy Lookup Transformation Editor &#40;Reference Table Tab&#41;](../../../integration-services/data-flow/transformations/fuzzy-lookup-transformation-editor-reference-table-tab.md)   
 [Fuzzy Lookup Transformation Editor &#40;Advanced Tab&#41;](../../../integration-services/data-flow/transformations/fuzzy-lookup-transformation-editor-advanced-tab.md)  
  
  