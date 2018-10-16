---
title: "File Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/09/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "File Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "File"
helpviewer_keywords: 
  - "File element"
ms.assetid: 21c70707-d2f8-4040-9acb-cbce23076bcc
author: minewiskan
ms.author: owend
manager: craigg
---
# File Element (ASSL)
  Defines one of the files that compose a [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[dnprdnshort](../../../includes/dnprdnshort-md.md)] [ClrAssembly](../data-type/assembly-data-type-assl.md) element.  
  
## Syntax  
  
```xml  
  
<Files>  
   <File xsi:type="ClrAssemblyFile">...</File>  
</Files>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|[ClrAssemblyFile](../data-type/clrassemblyfile-data-type-assl.md)|  
|Default value|None|  
|Cardinality|1-n: Required element that can occur more than once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[Files](../collections/files-element-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The element that corresponds to the parent of `Files` in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.ClrAssemblyFile>.  
  
## See Also  
 [Server Element &#40;ASSL&#41;](server-element-assl.md)   
 [Database Element &#40;ASSL&#41;](database-element-assl.md)   
 [Assemblies Element &#40;ASSL&#41;](../collections/assemblies-element-assl.md)   
 [Assembly Element &#40;ASSL&#41;](assembly-element-assl.md)   
 [ClrAssembly Data Type &#40;ASSL&#41;](../data-type/assembly-data-type-assl.md)   
 [Data Element &#40;ASSL&#41;](data-element-assl.md)   
 [DataBlock Data Type &#40;ASSL&#41;](../data-type/datablock-data-type-assl.md)   
 [Blocks Element &#40;ASSL&#41;](../collections/blocks-element-assl.md)   
 [Block Element &#40;ASSL&#41;](block-element-assl.md)   
 [ComAssembly Data Type &#40;ASSL&#41;](../data-type/comassembly-data-type-assl.md)   
 [Objects &#40;ASSL&#41;](objects-assl.md)  
  
  
