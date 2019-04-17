---
title: "Character Map Transformation | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql13.dts.designer.charactertrans.f1"
  - "sql13.dts.designer.charactermaptransformation.f1"
helpviewer_keywords: 
  - "mutually exclusive mapping [Integration Services]"
  - "mapping data [Integration Services]"
  - "string functions"
  - "Character Map transformation [Integration Services]"
ms.assetid: e0f50eb6-b893-400f-bb8c-fb3072cc2620
author: janinezhang
ms.author: janinez
manager: craigg
---
# Character Map Transformation
  The Character Map transformation applies string functions, such as conversion from lowercase to uppercase, to character data. This transformation operates only on column data with a string data type.  
  
 The Character Map transformation can convert column data in place or add a column to the transformation output and put the converted data in the new column. You can apply different sets of mapping operations to the same input column and put the results in different columns. For example, you can convert the same column to uppercase and lowercase and put the results in two different columns.  
  
 Mapping can, under some circumstances, cause data to be truncated. For example, truncation can occur when single-byte characters are mapped to characters with a multibyte representation. The Character Map transformation includes an error output, which can be used to direct truncated data to separate output. For more information, see [Error Handling in Data](../../../integration-services/data-flow/error-handling-in-data.md).  
  
 This transformation has one input, one output, and one error output.  
  
## Mapping Operations  
 The following table describes the mapping operations that the Character Map transformation supports.  
  
|Operation|Description|  
|---------------|-----------------|  
|Byte reversal|Reverses byte order.|  
|Full width|Maps half-width characters to full-width characters.|  
|Half width|Maps full-width characters to half-width characters.|  
|Hiragana|Maps katakana characters to hiragana characters.|  
|Katakana|Maps hiragana characters to katakana characters.|  
|Linguistic casing|Applies linguistic casing instead of the system rules. Linguistic casing refers to functionality provided by the Win32 API for Unicode simple case mapping of Turkic and other locales.|  
|Lowercase|Converts characters to lowercase.|  
|Simplified Chinese|Maps traditional Chinese characters to simplified Chinese characters.|  
|Traditional Chinese|Maps simplified Chinese characters to traditional Chinese characters.|  
|Uppercase|Converts characters to uppercase.|  
  
## Mutually Exclusive Mapping Operations  
 More than one operation can be performed in a transformation. However, some mapping operations are mutually exclusive. The following table lists restrictions that apply when you use multiple operations on the same column. Operations in the columns Operation A and Operation B are mutually exclusive.  
  
|Operation A|Operation B|  
|-----------------|-----------------|  
|Lowercase|Uppercase|  
|Hiragana|Katakana|  
|Half width|Full width|  
|Traditional Chinese|Simplified Chinese|  
|Lowercase|Hiragana, katakana, half width, full width|  
|Uppercase|Hiragana, katakana, half width, full width|  
  
## Configuration of the Character Map Transformation  
 You configure the Character Map transformation in the following ways:  
  
-   Specify the columns to convert.  
  
-   Specify the operations to apply to each column.  
  
 You can set properties through [!INCLUDE[ssIS](../../../includes/ssis-md.md)] Designer or programmatically.  
  
 The **Advanced Editor** dialog box reflects the properties that can be set programmatically. For more information about the properties that you can set in the **Advanced Editor** dialog box or programmatically, click one of the following topics:  
  
-   [Common Properties](https://msdn.microsoft.com/library/51973502-5cc6-4125-9fce-e60fa1b7b796)  
  
-   [Transformation Custom Properties](../../../integration-services/data-flow/transformations/transformation-custom-properties.md)  
  
 For more information about how to set properties, click one of the following topics:  
  
-   [Set the Properties of a Data Flow Component](../../../integration-services/data-flow/set-the-properties-of-a-data-flow-component.md)  
  
-   [Sort Data for the Merge and Merge Join Transformations](../../../integration-services/data-flow/transformations/sort-data-for-the-merge-and-merge-join-transformations.md)  
  
## Character Map Transformation Editor
  Use the **Character Map Transformation Editor** dialog box to select the string functions to apply to column data and to specify whether mapping is an in-place change or added as a new column.  
  
### Options  
 **Available Input Columns**  
 Use the check boxes to select the columns to transform using string functions. Your selections appear in the table below.  
  
 **Input Column**  
 View input columns selected from the table above. You can also change or remove a selection by using the list of available input columns.  
  
 **Destination**  
 Specify whether to save the results of the string operations in place, using the existing column, or to save the modified data as a new column.  
  
|Value|Description|  
|-----------|-----------------|  
|New column|Save the data in a new column. Assign the column name under **Output Alias**.|  
|In-place change|Save the modified data in the existing column.|  
  
 **Operation**  
 Select from the list the string functions to apply to column data.  
  
|Value|Description|  
|-----------|-----------------|  
|Lowercase|Convert to lower case.|  
|Uppercase|Convert to upper case.|  
|Byte reversal|Convert by reversing byte order.|  
|Hiragana|Convert Japanese katakana characters to hiragana.|  
|Katakana|Convert Japanese hiragana characters to katakana.|  
|Half width|Convert full-width characters to half width.|  
|Full width|Convert half-width characters to full width.|  
|Linguistic casing|Apply linguistic rules of casing (Unicode simple case mapping for Turkic and other locales) instead of the system rules.|  
|Simplified Chinese|Convert traditional Chinese characters to simplified Chinese.|  
|Traditional Chinese|Convert simplified Chinese characters to traditional Chinese.|  
  
 **Output Alias**  
 Type an alias for each output column. The default is **Copy of** followed by the input column name; however, you can choose any unique, descriptive name.  
  
 **Configure Error Output**  
 Use the [Configure Error Output](https://msdn.microsoft.com/library/5f8da390-fab5-44f8-b268-d8fa313ce4b9) dialog box to specify error handling options for this transformation.  
  
  
