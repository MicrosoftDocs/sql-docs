---
title: "Copy Column Transformation | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql12.dts.designer.copycolumntrans.f1"
helpviewer_keywords: 
  - "columns [Integration Services], copying"
  - "copying columns"
  - "Copy Column transformation"
ms.assetid: 1c72a313-9026-46bc-a57f-c6b3f47346f8
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Copy Column Transformation
  The Copy Column transformation creates new columns by copying input columns and adding the new columns to the transformation output. Later in the data flow, different transformations can be applied to the column copies. For example, you can use the Copy Column transformation to create a copy of a column and then convert the copied data to uppercase characters by using the Character Map transformation, or apply aggregations to the new column by using the Aggregate transformation.  
  
## Configuration of the Copy Column Transformation  
 You configure the Copy Column transformation by specifying the input columns to copy. You can create multiple copies of a column, or create copies of multiple columns in one operation.  
  
 This transformation has one input and one output. It does not support an error output.  
  
 You can set properties through the [!INCLUDE[ssIS](../../../includes/ssis-md.md)] Designer or programmatically.  
  
 For more information about the properties that you can set in the **Copy Column Transformation Editor** dialog box, see [Copy Column Transformation Editor](../../copy-column-transformation-editor.md).  
  
 The **Advanced Editor** dialog box reflects the properties that can be set programmatically. For more information about the properties that you can set in the **Advanced Editor** dialog box or programmatically, click one of the following topics:  
  
-   [Common Properties](../../common-properties.md)  
  
-   [Transformation Custom Properties](transformation-custom-properties.md)  
  
 For more information about how to set properties, see [Set the Properties of a Data Flow Component](../set-the-properties-of-a-data-flow-component.md).  
  
## See Also  
 [Data Flow](../data-flow.md)   
 [Integration Services Transformations](integration-services-transformations.md)  
  
  
