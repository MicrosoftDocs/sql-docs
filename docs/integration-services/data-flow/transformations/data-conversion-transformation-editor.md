---
title: "Data Conversion Transformation Editor | Microsoft Docs"
ms.custom: ""
ms.date: "03/07/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "integration-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
f1_keywords: 
  - "sql13.dts.designer.dataconversiontransformation.f1"
helpviewer_keywords: 
  - "Data Conversion Transformation Editor"
ms.assetid: 7b4e4873-e8fe-4549-a965-65bebdb270bc
caps.latest.revision: 28
author: "douglaslMS"
ms.author: "douglasl"
manager: "jhubbard"
---
# Data Conversion Transformation Editor
  Use the **Data Conversion Transformation Editor** dialog box to select the columns to convert, select the data type to which the column is converted, and set conversion attributes.  
  
> [!NOTE]  
>  The **FastParse** property of the output columns of the Data Conversion transformation is not available in the **Data Conversion Transformation Editor**, but can be set by using the **Advanced Editor**. For more information on this property, see the Data Conversion Transformation section of [Transformation Custom Properties](../../../integration-services/data-flow/transformations/transformation-custom-properties.md).  
  
 To learn more about the Data Conversion transformation, see [Data Conversion Transformation](../../../integration-services/data-flow/transformations/data-conversion-transformation.md).  
  
## Options  
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
 Specify how to handle row-level errors by using the [Configure Error Output](http://msdn.microsoft.com/library/5f8da390-fab5-44f8-b268-d8fa313ce4b9) dialog box.  
  
## See Also  
 [Integration Services Error and Message Reference](../../../integration-services/integration-services-error-and-message-reference.md)   
 [Convert Data to a Different Data Type by Using the Data Conversion Transformation](../../../integration-services/data-flow/transformations/convert-data-type-by-using-data-conversion-transformation.md)  
  
  