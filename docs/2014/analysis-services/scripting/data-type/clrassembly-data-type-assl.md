---
title: "ClrAssembly Data Type (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
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
author: minewiskan
ms.author: owend
manager: craigg
---
# ClrAssembly Data Type (ASSL)
  Defines a derived data type that represents a [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[dnprdnshort](../../../includes/dnprdnshort-md.md)] assembly associated with a [Database](../objects/database-element-assl.md) or [Server](../objects/server-element-assl.md) element  
  
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
|Base data types|[Assembly](../objects/assembly-element-assl.md)|  
|Derived data types|None|  
  
## Data Type Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|None (abstract type)|  
|Child elements|[Files](../collections/files-element-assl.md), [PermissionSet](../properties/permissionset-element-assl.md)|  
|Derived elements|See [Assembly](../objects/assembly-element-assl.md) ([Assemblies](../collections/assemblies-element-assl.md) collection of [Database](../objects/database-element-assl.md) or [Server](../objects/server-element-assl.md))|  
  
## Remarks  
 The `ClrAssembly` element contains the files needed to recreate a [!INCLUDE[dnprdnshort](../../../includes/dnprdnshort-md.md)] assembly, associated either with an instance of [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] or with a specific database on an instance of [!INCLUDE[ssAS](../../../includes/ssas-md.md)], as well as the permissions needed to execute the assembly.  
  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.ClrAssembly>.  
  
## See Also  
 [File Element &#40;ASSL&#41;](../objects/file-element-assl.md)   
 [ClrAssemblyFile Data Type &#40;ASSL&#41;](clrassemblyfile-data-type-assl.md)   
 [Data Element &#40;ASSL&#41;](../objects/data-element-assl.md)   
 [DataBlock Data Type &#40;ASSL&#41;](datablock-data-type-assl.md)   
 [Blocks Element &#40;ASSL&#41;](../collections/blocks-element-assl.md)   
 [Block Element &#40;ASSL&#41;](../objects/block-element-assl.md)   
 [ComAssembly Data Type &#40;ASSL&#41;](assembly-data-type-assl.md)   
 [Analysis Services Scripting Language XML Data Types &#40;ASSL&#41;](analysis-services-scripting-language-xml-data-types-assl.md)  
  
  
