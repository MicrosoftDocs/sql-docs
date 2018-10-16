---
title: "Assembly Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "Assembly Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "ASSEMBLY"
helpviewer_keywords: 
  - "Assembly element [ASSL]"
ms.assetid: 1910ccb0-7da0-4ee1-9548-ad6e0068d23d
author: minewiskan
ms.author: owend
manager: craigg
---
# Assembly Element (ASSL)
  Represents a [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[dnprdnshort](../../../includes/dnprdnshort-md.md)] assembly or a COM dynamic link library (DLL) associated with a [Server](server-element-assl.md) element or a [Database](database-element-assl.md) element.  
  
## Syntax  
  
```xml  
  
<Assemblies>  
   <Assembly xsi:type="ClrAssembly">...</Assembly>  
   <!-- or -->  
   <Assembly xsi:type="ComAssembly">...</Assembly>  
</Assemblies>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|[ClrAssembly](../data-type/assembly-data-type-assl.md), [ComAssembly](../data-type/comassembly-data-type-assl.md)|  
|Default value|None|  
|Cardinality|0-n: Optional element that can occur more than once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[Assemblies](../collections/assemblies-element-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.Assembly>.  
  
## See Also  
 [Server Element &#40;ASSL&#41;](server-element-assl.md)   
 [Database Element &#40;ASSL&#41;](database-element-assl.md)   
 [Objects &#40;ASSL&#41;](objects-assl.md)  
  
  
