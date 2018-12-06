---
title: "Flat File Custom Properties | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
ms.assetid: 7f2caeab-784c-4b0c-9b3e-6a88d1ccdbf9
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Flat File Custom Properties
  **Source Custom Properties**  
  
 The Flat File source has both custom properties and the properties common to all data flow components.  
  
 The following table describes the custom properties of the Flat File source. All properties are read/write.  
  
|Property name|Data Type|Description|  
|-------------------|---------------|-----------------|  
|FileNameColumnName|String|The name of an output column that contains the file name. If no name is specified, no output column containing the file name will be generated.<br /><br /> Note: This property is not available in the **Flat File Source Editor**, but can be set by using the **Advanced Editor**.|  
|RetainNulls|Boolean|A value that specifies whether to retain Null values from the source file as Null values when the data is processed by the Data Transformation Pipeline engine. The default value of this property is `False`.|  
  
 The output of the Flat File source has no custom properties.  
  
 The following table describes the custom properties of the output columns of the Flat File source. All properties are read/write.  
  
|Property name|Data Type|Description|  
|-------------------|---------------|-----------------|  
|FastParse|Boolean|A value that indicates whether the column uses the quicker, but locale-insensitive, fast parsing routines that DTS provides or the locale-sensitive standard parsing routines. For more information, see [Fast Parse](../fast-parse.md) and [Standard Parse](../standard-parse.md). The default value of this property is `False`.<br /><br /> Note: This property is not available in the **Flat File Source Editor**, but can be set by using the **Advanced Editor**.|  
  
 For more information, see [Flat File Source](flat-file-source.md).  
  
 **Destination Custom Properties**  
  
 The Flat File destination has both custom properties and the properties common to all data flow components.  
  
 The following table describes the custom properties of the Flat File destination. All properties are read/write.  
  
|Property name|Data Type|Description|  
|-------------------|---------------|-----------------|  
|Header|String|A block of text that is inserted in the file before any data is written.<br /><br /> The value of this property can be specified by using a property expression.|  
|Overwrite|Boolean|A value that specifies whether to overwrite or append to an existing destination file that has the same name. The default value of this property is `True`.|  
  
 The input and the input columns of the Flat File destination have no custom properties.  
  
 For more information, see [Flat File Destination](flat-file-destination.md).  
  
## See Also  
 [Common Properties](../common-properties.md)  
  
  
