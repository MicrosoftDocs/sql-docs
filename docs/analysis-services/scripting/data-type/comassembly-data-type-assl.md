---
title: "ComAssembly Data Type (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
apiname: 
  - "ComAssembly Data Type"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "ComAssembly"
helpviewer_keywords: 
  - "ComAssembly data type"
ms.assetid: 23c0f4b3-b6ac-4ec8-9254-74d2f84f5244
caps.latest.revision: 48
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# ComAssembly Data Type (ASSL)
  Defines a derived data type that represents a COM library associated with a [Server](../../../analysis-services/scripting/objects/server-element-assl.md) or [Database](../../../analysis-services/scripting/objects/database-element-assl.md) element.  
  
> [!IMPORTANT]  
>  COM assemblies might pose a security risk. Due to this risk and other considerations, COM assemblies were deprecated in [!INCLUDE[ssASversion10](../../../includes/ssasversion10-md.md)]. COM assemblies might not be supported in future releases.  
  
## Syntax  
  
```xml  
  
<ComAssembly>  
      <!-- The following elements extend Assembly -->  
   <Source>...</Source>  
</ComAssembly>  
```  
  
## Data Type Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Base data types|[Assembly](../../../analysis-services/scripting/objects/assembly-element-assl.md)|  
|Derived data types|None|  
  
## Data Type Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|None|  
|Child elements|[Source](../../../analysis-services/scripting/properties/source-element-comassembly-assl.md)|  
|Derived elements|See [Assembly](../../../analysis-services/scripting/objects/assembly-element-assl.md) ([Assemblies](../../../analysis-services/scripting/collections/assemblies-element-assl.md) collection of [Database](../../../analysis-services/scripting/objects/database-element-assl.md) or [Server](../../../analysis-services/scripting/objects/server-element-assl.md))|  
  
## Remarks  
 The **ComAssembly** element contains a reference (either the fully qualified file name or the programmatic identifier) to a COM library associated with an instance of [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] or with a specific database on an instance of [!INCLUDE[ssAS](../../../includes/ssas-md.md)].  
  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.ComAssembly>.  
  
## See Also  
 [ClrAssembly Data Type &#40;ASSL&#41;](../../../analysis-services/scripting/data-type/clrassembly-data-type-assl.md)   
 [Analysis Services Scripting Language XML Data Types &#40;ASSL&#41;](../../../analysis-services/scripting/data-type/analysis-services-scripting-language-xml-data-types-assl.md)  
  
  