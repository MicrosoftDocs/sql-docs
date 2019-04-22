---
title: "Raw File Custom Properties | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
ms.assetid: 7e81f7e1-fac0-4b57-b145-8f1b9e4720bf
author: janinezhang
ms.author: janinez
manager: craigg
---
# Raw File Custom Properties
  **Source Custom Properties**  
  
 The Raw File source has both custom properties and the properties common to all data flow components.  
  
 The following table describes the custom properties of the Raw File source. All properties are read/write.  
  
|Property name|Data Type|Description|  
|-------------------|---------------|-----------------|  
|AccessMode|Integer (enumeration)|The mode used to access the raw data. The possible values are **File name** (0) and **File name from variable** (1). The default value is **File name** (0).|  
|FileName|String|The path and file name of the source file.|  
  
 The output and the output columns of the Raw File source have no custom properties.  
  
 For more information, see [Raw File Source](../../integration-services/data-flow/raw-file-source.md).  
  
 **Destination Custom Properties**  
  
 The Raw File destination has both custom properties and the properties common to all data flow components.  
  
 The following table describes the custom properties of the Raw File destination. All properties are read/write.  
  
|Property name|Data Type|Description|  
|-------------------|---------------|-----------------|  
|AccessMode|Integer (enumeration)|A value that specifies whether the FileName property contains a file name, or the name of a variable that contains a file name. The options are **File name** (0) and **File name from variable** (1).|  
|FileName|String|The name of the file to which the Raw File destination writes.|  
|WriteOption|Integer (enumeration)|A value that specifies whether the Raw File destination deletes an existing file that has the same name. The options are **Create Always** (0), **Create Once** (1), **Truncate and Append** (3), and **Append** (2). The default value of this property is **Create Always** (0).|  
  
> [!NOTE]  
>  An append operation requires the metadata of the appended data to match the metadata of the data already present in the file.  
  
 The input and the input columns of the Raw File destination have no custom properties.  
  
 For more information, see [Raw File Destination](../../integration-services/data-flow/raw-file-destination.md).  
  
## See Also  
 [Common Properties](https://msdn.microsoft.com/library/51973502-5cc6-4125-9fce-e60fa1b7b796)  
  
  
