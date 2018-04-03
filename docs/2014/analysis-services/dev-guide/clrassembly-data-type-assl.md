---
title: "ClrAssembly Data Type (ASSL) | Microsoft Docs"
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
  - "ClrAssembly Data Type"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "ClrAssembly"
helpviewer_keywords: 
  - "ClrAssembly data type"
ms.assetid: 3f5dc5a1-ebd6-41b8-ac04-91d4de137eb4
caps.latest.revision: 44
author: "Minewiskan"
ms.author: "owend"
manager: "mblythe"
---
# ClrAssembly Data Type (ASSL)
  Defines a derived data type that represents a [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] assembly associated with a [Database](../../../2014/analysis-services/dev-guide/database-element-assl.md) or [Server](../../../2014/analysis-services/dev-guide/server-element-assl.md) element  
  
## Syntax  
  
```xml  
  
<ClrAssembly>  
      <!-- The following elements extend Assembly -->  
   <Files>...</Files>  
      <PermissionSet>...</PermissionSet>  
</ClrAssembly>  
```  
  
## Data Type Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Base data types|[Assembly](../../../2014/analysis-services/dev-guide/assembly-element-assl.md)|  
|Derived data types|None|  
  
## Data Type Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|None (abstract type)|  
|Child elements|[Files](../../../2014/analysis-services/dev-guide/files-element-assl.md), [PermissionSet](../../../2014/analysis-services/dev-guide/permissionset-element-assl.md)|  
|Derived elements|See [Assembly](../../../2014/analysis-services/dev-guide/assembly-element-assl.md) ([Assemblies](../../../2014/analysis-services/dev-guide/assemblies-element-assl.md) collection of [Database](../../../2014/analysis-services/dev-guide/database-element-assl.md) or [Server](../../../2014/analysis-services/dev-guide/server-element-assl.md))|  
  
## Remarks  
 The `ClrAssembly` element contains the files needed to recreate a [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] assembly, associated either with an instance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] or with a specific database on an instance of [!INCLUDE[ssAS](../../includes/ssas-md.md)], as well as the permissions needed to execute the assembly.  
  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.ClrAssembly>.  
  
## See Also  
 [File Element &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/file-element-assl.md)   
 [ClrAssemblyFile Data Type &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/clrassemblyfile-data-type-assl.md)   
 [Data Element &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/data-element-assl.md)   
 [DataBlock Data Type &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/datablock-data-type-assl.md)   
 [Blocks Element &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/blocks-element-assl.md)   
 [Block Element &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/block-element-assl.md)   
 [ComAssembly Data Type &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/comassembly-data-type-assl.md)   
 [Analysis Services Scripting Language XML Data Types &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/analysis-services-scripting-language-xml-data-types-assl.md)  
  
  