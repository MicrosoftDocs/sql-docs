---
title: "Data Conversion Transformation | Microsoft Docs"
ms.custom: ""
ms.date: "03/16/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "integration-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
f1_keywords: 
  - "sql13.dts.designer.dataconversiontrans.f1"
helpviewer_keywords: 
  - "converting data types [Integration Services]"
  - "Data Conversion transformation"
  - "data types [Integration Services], converting"
ms.assetid: fd515bbc-6f49-4d0c-ae7f-6ea3c3f24a1c
caps.latest.revision: 53
author: "douglaslMS"
ms.author: "douglasl"
manager: "jhubbard"
---
# Data Conversion Transformation
  The Data Conversion transformation converts the data in an input column to a different data type and then copies it to a new output column. For example, a package can extract data from multiple sources, and then use this transformation to convert columns to the data type required by the destination data store. You can apply multiple conversions to a single input column.  
  
 Using this transformation, a package can perform the following types of data conversions:  
  
-   Change the data type. For more information, see [Integration Services Data Types](../../../integration-services/data-flow/integration-services-data-types.md).  
  
    > [!NOTE]  
    >  If you are converting data to a date or a datetime data type, the date in the output column is in the ISO format, although the locale preference may specify a different format.  
  
-   Set the column length of string data and the precision and scale on numeric data. For more information, see [Precision, Scale, and Length &#40;Transact-SQL&#41;](../../../t-sql/data-types/precision-scale-and-length-transact-sql.md).  
  
-   Specify a code page. For more information, see [Comparing String Data](../../../integration-services/data-flow/comparing-string-data.md).  
  
    > [!NOTE]  
    >  When copying between columns with a string data type, the two columns must use the same code page.  
  
 If the length of an output column of string data is shorter than the length of its corresponding input column, the output data is truncated. For more information, see [Error Handling in Data](../../../integration-services/data-flow/error-handling-in-data.md).  
  
 This transformation has one input, one output, and one error output.  
  
## Related Tasks  
 You can set properties through the [!INCLUDE[ssIS](../../../includes/ssis-md.md)] Designer or programmatically. For information about using the Data Conversion Transformation in the SSIS Designer, see [Convert Data to a Different Data Type by Using the Data Conversion Transformation](../../../integration-services/data-flow/transformations/convert-data-type-by-using-data-conversion-transformation.md) and [Data Conversion Transformation Editor](../../../integration-services/data-flow/transformations/data-conversion-transformation-editor.md). For information about setting properties of this transformation programmatically, see [Common Properties](http://msdn.microsoft.com/library/51973502-5cc6-4125-9fce-e60fa1b7b796) and [Transformation Custom Properties](../../../integration-services/data-flow/transformations/transformation-custom-properties.md).  
  
## Related Content  
 Blog entry, [Performance Comparison between Data Type Conversion Techniques in SSIS 2008](http://go.microsoft.com/fwlink/?LinkId=220823), on blogs.msdn.com.  
  
## See Also  
 [Fast Parse](http://msdn.microsoft.com/library/6688707d-3c5b-404e-aa2f-e13092ac8d95)   
 [Data Flow](../../../integration-services/data-flow/data-flow.md)   
 [Integration Services Transformations](../../../integration-services/data-flow/transformations/integration-services-transformations.md)  
  
  