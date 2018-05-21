---
title: "ClrAssembly Data Type (ASSL) | Microsoft Docs"
ms.date: 05/03/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: assl
ms.topic: reference
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# ClrAssembly Data Type (ASSL)
[!INCLUDE[ssas-appliesto-sqlas](../../../includes/ssas-appliesto-sqlas.md)]
  Defines a derived data type that represents a [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[dnprdnshort](../../../includes/dnprdnshort-md.md)] assembly associated with a [Database](../../../analysis-services/scripting/objects/database-element-assl.md) or [Server](../../../analysis-services/scripting/objects/server-element-assl.md) element  
  
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
|Base data types|[Assembly](../../../analysis-services/scripting/objects/assembly-element-assl.md)|  
|Derived data types|None|  
  
## Data Type Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|None (abstract type)|  
|Child elements|[Files](../../../analysis-services/scripting/collections/files-element-assl.md), [PermissionSet](../../../analysis-services/scripting/properties/permissionset-element-assl.md)|  
|Derived elements|See [Assembly](../../../analysis-services/scripting/objects/assembly-element-assl.md) ([Assemblies](../../../analysis-services/scripting/collections/assemblies-element-assl.md) collection of [Database](../../../analysis-services/scripting/objects/database-element-assl.md) or [Server](../../../analysis-services/scripting/objects/server-element-assl.md))|  
  
## Remarks  
 The **ClrAssembly** element contains the files needed to recreate a [!INCLUDE[dnprdnshort](../../../includes/dnprdnshort-md.md)] assembly, associated either with an instance of [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] or with a specific database on an instance of [!INCLUDE[ssAS](../../../includes/ssas-md.md)], as well as the permissions needed to execute the assembly.  
  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.ClrAssembly>.  
  
## See Also  
 [File Element &#40;ASSL&#41;](../../../analysis-services/scripting/objects/file-element-assl.md)   
 [ClrAssemblyFile Data Type &#40;ASSL&#41;](../../../analysis-services/scripting/data-type/clrassemblyfile-data-type-assl.md)   
 [Data Element &#40;ASSL&#41;](../../../analysis-services/scripting/objects/data-element-assl.md)   
 [DataBlock Data Type &#40;ASSL&#41;](../../../analysis-services/scripting/data-type/datablock-data-type-assl.md)   
 [Blocks Element &#40;ASSL&#41;](../../../analysis-services/scripting/collections/blocks-element-assl.md)   
 [Block Element &#40;ASSL&#41;](../../../analysis-services/scripting/objects/block-element-assl.md)   
 [ComAssembly Data Type &#40;ASSL&#41;](../../../analysis-services/scripting/data-type/comassembly-data-type-assl.md)   
 [Analysis Services Scripting Language XML Data Types &#40;ASSL&#41;](../../../analysis-services/scripting/data-type/analysis-services-scripting-language-xml-data-types-assl.md)  
  
  
