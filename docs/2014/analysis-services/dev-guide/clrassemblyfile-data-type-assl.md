---
title: "ClrAssemblyFile Data Type (ASSL) | Microsoft Docs"
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
  - "ClrAssemblyFile Data Type"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "ClrAssemblyFile"
helpviewer_keywords: 
  - "ClrAssemblyFile data type"
ms.assetid: 91074677-c149-483b-a56d-0e35d959d9eb
caps.latest.revision: 41
author: "Minewiskan"
ms.author: "owend"
manager: "mblythe"
---
# ClrAssemblyFile Data Type (ASSL)
  Defines a primitive data type that represents one of the files that compose a [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[dnprdnshort](../../../includes/dnprdnshort-md.md)] `Assembly` ([ClrAssembly](../../../2014/analysis-services/dev-guide/clrassembly-data-type-assl.md) element).  
  
## Syntax  
  
```xml  
  
<ClrAssemblyFile>  
   <Name>...</Name>  
      <Type>...</Type>  
      <Data>...</Data>  
</ClrAssemblyFile>  
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
|Child elements|[Data](../../../2014/analysis-services/dev-guide/data-element-assl.md), [Name](../../../2014/analysis-services/dev-guide/name-element-assl.md), [Type](../../../2014/analysis-services/dev-guide/type-element-clrassemblyfile-assl.md)|  
|Derived elements|[File](../../../2014/analysis-services/dev-guide/file-element-assl.md) ([Files](../../../2014/analysis-services/dev-guide/files-element-assl.md) collection of [ClrAssembly](../../../2014/analysis-services/dev-guide/clrassembly-data-type-assl.md))|  
  
## Remarks  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.ClrAssemblyFile>.  
  
## See Also  
 [Server Element &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/server-element-assl.md)   
 [Database Element &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/database-element-assl.md)   
 [Assemblies Element &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/assemblies-element-assl.md)   
 [Assembly Element &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/assembly-element-assl.md)   
 [DataBlock Data Type &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/datablock-data-type-assl.md)   
 [Blocks Element &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/blocks-element-assl.md)   
 [Block Element &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/block-element-assl.md)   
 [ComAssembly Data Type &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/comassembly-data-type-assl.md)   
 [Analysis Services Scripting Language XML Data Types &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/analysis-services-scripting-language-xml-data-types-assl.md)  
  
  