---
title: "DataBlock Data Type (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "DataBlock Data Type"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "DataBlock"
helpviewer_keywords: 
  - "DataBlock data type"
ms.assetid: 4192b388-613a-472b-881c-f9c02215aa81
author: minewiskan
ms.author: owend
manager: craigg
---
# DataBlock Data Type (ASSL)
  Defines a primitive data type that represents a collection of data blocks used to store the binary contents of a [ClrAssemblyFile](clrassemblyfile-data-type-assl.md) element.  
  
## Syntax  
  
```xml  
  
<DataBlock>  
   <Blocks>...</Blocks>  
</DataBlock>  
```  
  
## Data Type Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Base data types|None|  
|Derived data types|None|  
  
## Data Type Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|None|  
|Child elements|[Blocks](../collections/blocks-element-assl.md)|  
|Derived elements|[Data](../objects/data-element-assl.md) element of [ClrAssemblyFile](clrassemblyfile-data-type-assl.md) type ([Files](../collections/files-element-assl.md) collection of [ClrAssembly](assembly-data-type-assl.md) type)|  
  
## See Also  
 [Assembly Element &#40;ASSL&#41;](../objects/assembly-element-assl.md)   
 [File Element &#40;ASSL&#41;](../objects/file-element-assl.md)   
 [Block Element &#40;ASSL&#41;](../objects/block-element-assl.md)   
 [Analysis Services Scripting Language XML Data Types &#40;ASSL&#41;](analysis-services-scripting-language-xml-data-types-assl.md)  
  
  
