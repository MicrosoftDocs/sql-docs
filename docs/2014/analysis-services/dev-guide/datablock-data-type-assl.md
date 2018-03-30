---
title: "DataBlock Data Type (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
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
caps.latest.revision: 32
author: "Minewiskan"
ms.author: "owend"
manager: "mblythe"
---
# DataBlock Data Type (ASSL)
  Defines a primitive data type that represents a collection of data blocks used to store the binary contents of a [ClrAssemblyFile](../../../2014/analysis-services/dev-guide/clrassemblyfile-data-type-assl.md) element.  
  
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
|Child elements|[Blocks](../../../2014/analysis-services/dev-guide/blocks-element-assl.md)|  
|Derived elements|[Data](../../../2014/analysis-services/dev-guide/data-element-assl.md) element of [ClrAssemblyFile](../../../2014/analysis-services/dev-guide/clrassemblyfile-data-type-assl.md) type ([Files](../../../2014/analysis-services/dev-guide/files-element-assl.md) collection of [ClrAssembly](../../../2014/analysis-services/dev-guide/clrassembly-data-type-assl.md) type)|  
  
## See Also  
 [Assembly Element &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/assembly-element-assl.md)   
 [File Element &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/file-element-assl.md)   
 [Block Element &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/block-element-assl.md)   
 [Analysis Services Scripting Language XML Data Types &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/analysis-services-scripting-language-xml-data-types-assl.md)  
  
  