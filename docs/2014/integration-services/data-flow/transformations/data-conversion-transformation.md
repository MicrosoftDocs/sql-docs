---
title: "Data Conversion Transformation | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql12.dts.designer.dataconversiontrans.f1"
helpviewer_keywords: 
  - "converting data types [Integration Services]"
  - "Data Conversion transformation"
  - "data types [Integration Services], converting"
ms.assetid: fd515bbc-6f49-4d0c-ae7f-6ea3c3f24a1c
author: janinezhang
ms.author: janinez
manager: craigg
---
# Data Conversion Transformation
  The Data Conversion transformation converts the data in an input column to a different data type and then copies it to a new output column. For example, a package can extract data from multiple sources, and then use this transformation to convert columns to the data type required by the destination data store. You can apply multiple conversions to a single input column.  
  
 Using this transformation, a package can perform the following types of data conversions:  
  
-   Change the data type. For more information, see [Integration Services Data Types](../integration-services-data-types.md).  
  
    > [!NOTE]  
    >  If you are converting data to a date or a datetime data type, the date in the output column is in the ISO format, although the locale preference may specify a different format.  
  
-   Set the column length of string data and the precision and scale on numeric data. For more information, see [Precision, Scale, and Length &#40;Transact-SQL&#41;](/sql/t-sql/data-types/precision-scale-and-length-transact-sql).  
  
-   Specify a code page. For more information, see [Comparing String Data](../comparing-string-data.md).  
  
    > [!NOTE]  
    >  When copying between columns with a string data type, the two columns must use the same code page.  
  
 If the length of an output column of string data is shorter than the length of its corresponding input column, the output data is truncated. For more information, see [Error Handling in Data](../error-handling-in-data.md).  
  
 This transformation has one input, one output, and one error output.  
  
## Related Tasks  
 You can set properties through the [!INCLUDE[ssIS](../../../includes/ssis-md.md)] Designer or programmatically. For information about using the Data Conversion Transformation in the SSIS Designer, see [Convert Data to a Different Data Type by Using the Data Conversion Transformation](data-conversion-transformation.md) and [Data Conversion Transformation Editor](../../data-conversion-transformation-editor.md). For information about setting properties of this transformation programmatically, see [Common Properties](../../common-properties.md) and [Transformation Custom Properties](transformation-custom-properties.md).  
  
## Related Content  
 Blog entry, [Performance Comparison between Data Type Conversion Techniques in SSIS 2008](https://go.microsoft.com/fwlink/?LinkId=220823), on blogs.msdn.com.  
  
## See Also  
 [Fast Parse](../../fast-parse.md)   
 [Data Flow](../data-flow.md)   
 [Integration Services Transformations](integration-services-transformations.md)  
  
  
