---
title: "RefreshInterval Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "RefreshInterval Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "RefreshInterval"
helpviewer_keywords: 
  - "RefreshInterval element"
ms.assetid: 2761d26a-5fb0-452c-9a89-12f8dc658c33
author: minewiskan
ms.author: owend
manager: craigg
---
# RefreshInterval Element (ASSL)
  Specifies the interval at which the bound data associated with the parent element is refreshed.  
  
## Syntax  
  
```xml  
  
<DimensionBinding> <!-- or MeasureGroupBinding, ProactiveCachingIncrementalProcessingBinding, ProactiveCachingQueryBinding -->  
   ...  
   <RefreshInterval>...</RefreshInterval>  
   ...  
</DimensionBinding>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|XML duration|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
|Default value|[ProactiveCachingIncrementalProcessingBinding](../data-type/binding-data-type-assl.md) or [ProactiveCachingQueryBinding](../data-type/querybinding-data-type-assl.md) = PT-1s|  
|Default value|All others = PT1m|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[DimensionBinding](../data-type/dimensionbinding-data-type-assl.md), [MeasureGroupBinding](../data-type/measuregroupbinding-data-type-assl.md), [ProactiveCachingIncrementalProcessingBinding](../data-type/binding-data-type-assl.md), [ProactiveCachingQueryBinding](../data-type/querybinding-data-type-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The elements that correspond to the parents of `RefreshInterval` in the Analysis Management Objects (AMO) object model are <xref:Microsoft.AnalysisServices.DimensionBinding>, <xref:Microsoft.AnalysisServices.MeasureGroupBinding>, <xref:Microsoft.AnalysisServices.ProactiveCachingIncrementalProcessingBinding>, and <xref:Microsoft.AnalysisServices.ProactiveCachingQueryBinding>.  
  
## See Also  
 [Properties &#40;ASSL&#41;](properties-assl.md)  
  
  
