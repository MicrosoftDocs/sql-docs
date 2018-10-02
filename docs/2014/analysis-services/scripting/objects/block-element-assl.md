---
title: "Block Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "Block Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "Block"
helpviewer_keywords: 
  - "Block element"
ms.assetid: f45c32ae-e4e0-465a-9564-9ccfb10a858f
author: minewiskan
ms.author: owend
manager: craigg
---
# Block Element (ASSL)
  Contains all or a portion of the binary contents of a [ClrAssemblyFile](../data-type/clrassemblyfile-data-type-assl.md) element.  
  
## Syntax  
  
```xml  
  
<Blocks>  
   <Block>...</Block>  
</Blocks>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|Base64Binary|  
|Default value|None|  
|Cardinality|1-n: Required element that can occur more than once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[Blocks](../collections/blocks-element-assl.md)|  
|Child elements|None|  
  
## See Also  
 [Assembly Element &#40;ASSL&#41;](assembly-element-assl.md)   
 [ClrAssembly Data Type &#40;ASSL&#41;](../data-type/assembly-data-type-assl.md)   
 [Files Element &#40;ASSL&#41;](../collections/files-element-assl.md)   
 [File Element &#40;ASSL&#41;](file-element-assl.md)   
 [ClrAssemblyFile Data Type &#40;ASSL&#41;](../data-type/clrassemblyfile-data-type-assl.md)   
 [Data Element &#40;ASSL&#41;](data-element-assl.md)   
 [DataBlock Data Type &#40;ASSL&#41;](../data-type/datablock-data-type-assl.md)   
 [Objects &#40;ASSL&#41;](objects-assl.md)  
  
  
