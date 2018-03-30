---
title: "Data Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
api_name: 
  - "Data Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "Data"
helpviewer_keywords: 
  - "Data element"
ms.assetid: e52b1961-7e11-4029-8ab1-84d275845067
caps.latest.revision: 37
author: "Minewiskan"
ms.author: "owend"
manager: "mblythe"
---
# Data Element (ASSL)
  Contains (in the collection of child [Block Element &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/block-element-assl.md) elements) the binary contents of a [ClrAssemblyFile](../../../2014/analysis-services/dev-guide/clrassemblyfile-data-type-assl.md) element.  
  
## Syntax  
  
```xml  
  
<File xsi:type="ClrAssemblyFile">  
   ...  
   <Data xsi:type="DataBlock">...</Data>  
   ...  
</ClrAssemblyFile>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|[DataBlock](../../../2014/analysis-services/dev-guide/datablock-data-type-assl.md)|  
|Default value|None|  
|Cardinality|1-1: Required element that occurs once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[File](../../../2014/analysis-services/dev-guide/file-element-assl.md) of type [ClrAssemblyFile](../../../2014/analysis-services/dev-guide/clrassemblyfile-data-type-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The element that corresponds to the parent of `Data` in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.ClrAssemblyFile>.  
  
## See Also  
 [Assembly Element &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/assembly-element-assl.md)   
 [ClrAssembly Data Type &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/clrassembly-data-type-assl.md)   
 [Files Element &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/files-element-assl.md)   
 [Blocks Element &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/blocks-element-assl.md)   
 [Objects &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/objects-assl.md)  
  
  