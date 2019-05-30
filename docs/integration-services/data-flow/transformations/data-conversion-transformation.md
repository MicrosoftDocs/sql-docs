---
title: "Data Conversion Transformation | Microsoft Docs"
ms.custom: ""
ms.date: "03/16/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql13.dts.designer.dataconversiontrans.f1"
  - "sql13.dts.designer.dataconversiontransformation.f1"
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

[!INCLUDE[ssis-appliesto](../../../includes/ssis-appliesto-ssvrpluslinux-asdb-asdw-xxx.md)]


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
 You can set properties through the [!INCLUDE[ssIS](../../../includes/ssis-md.md)] Designer or programmatically. For information about using the Data Conversion Transformation in the SSIS Designer, see [Convert Data to a Different Data Type by Using the Data Conversion Transformation](../../../integration-services/data-flow/transformations/convert-data-type-by-using-data-conversion-transformation.md). For information about setting properties of this transformation programmatically, see [Common Properties](https://msdn.microsoft.com/library/51973502-5cc6-4125-9fce-e60fa1b7b796) and [Transformation Custom Properties](../../../integration-services/data-flow/transformations/transformation-custom-properties.md).  
  
## Related Content  
 Blog entry, [Performance Comparison between Data Type Conversion Techniques in SSIS 2008](https://go.microsoft.com/fwlink/?LinkId=220823), on blogs.msdn.com.  
  
## Data Conversion Transformation Editor
  Use the **Data Conversion Transformation Editor** dialog box to select the columns to convert, select the data type to which the column is converted, and set conversion attributes.  
  
> [!NOTE]  
>  The **FastParse** property of the output columns of the Data Conversion transformation is not available in the **Data Conversion Transformation Editor**, but can be set by using the **Advanced Editor**. For more information on this property, see the Data Conversion Transformation section of [Transformation Custom Properties](../../../integration-services/data-flow/transformations/transformation-custom-properties.md).  
  
### Options  
 **Available Input Columns**  
 Select columns to convert by using the check boxes. Your selections add input columns to the table below.  
  
 **Input Column**  
 Select columns to convert from the list of available input columns. Your selections are reflected in the check box selections above.  
  
 **Output Alias**  
 Type an alias for each new column. The default is **Copy of** followed by the input column name; however, you can choose any unique, descriptive name.  
  
 **Data Type**  
 Select an available data type from the list. For more information, see [Integration Services Data Types](../../../integration-services/data-flow/integration-services-data-types.md).  
  
 **Length**  
 Set the column length for string data.  
  
 **Precision**  
 Set the precision for numeric data.  
  
 **Scale**  
 Set the scale for numeric data.  
  
 **Code page**  
 Select the appropriate code page for columns of type DT_STR.  
  
 **Configure error output**  
 Specify how to handle row-level errors by using the [Configure Error Output](https://msdn.microsoft.com/library/5f8da390-fab5-44f8-b268-d8fa313ce4b9) dialog box.  
  
## See Also  
 [Fast Parse](https://msdn.microsoft.com/library/6688707d-3c5b-404e-aa2f-e13092ac8d95)   
 [Data Flow](../../../integration-services/data-flow/data-flow.md)   
 [Integration Services Transformations](../../../integration-services/data-flow/transformations/integration-services-transformations.md)  
  
  
