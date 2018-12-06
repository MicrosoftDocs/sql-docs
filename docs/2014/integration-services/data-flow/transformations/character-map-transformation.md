---
title: "Character Map Transformation | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql12.dts.designer.charactertrans.f1"
helpviewer_keywords: 
  - "mutually exclusive mapping [Integration Services]"
  - "mapping data [Integration Services]"
  - "string functions"
  - "Character Map transformation [Integration Services]"
ms.assetid: e0f50eb6-b893-400f-bb8c-fb3072cc2620
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Character Map Transformation
  The Character Map transformation applies string functions, such as conversion from lowercase to uppercase, to character data. This transformation operates only on column data with a string data type.  
  
 The Character Map transformation can convert column data in place or add a column to the transformation output and put the converted data in the new column. You can apply different sets of mapping operations to the same input column and put the results in different columns. For example, you can convert the same column to uppercase and lowercase and put the results in two different columns.  
  
 Mapping can, under some circumstances, cause data to be truncated. For example, truncation can occur when single-byte characters are mapped to characters with a multibyte representation. The Character Map transformation includes an error output, which can be used to direct truncated data to separate output. For more information, see [Error Handling in Data](../error-handling-in-data.md).  
  
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
  
 For more information about the properties that you can set in the **Character Map Transformation Editor** dialog box, see [Character Map Transformation Editor](../../character-map-transformation-editor.md).  
  
 The **Advanced Editor** dialog box reflects the properties that can be set programmatically. For more information about the properties that you can set in the **Advanced Editor** dialog box or programmatically, click one of the following topics:  
  
-   [Common Properties](../../common-properties.md)  
  
-   [Transformation Custom Properties](transformation-custom-properties.md)  
  
 For more information about how to set properties, click one of the following topics:  
  
-   [Set the Properties of a Data Flow Component](../set-the-properties-of-a-data-flow-component.md)  
  
-   [Sort Data for the Merge and Merge Join Transformations](sort-data-for-the-merge-and-merge-join-transformations.md)  
  
  
