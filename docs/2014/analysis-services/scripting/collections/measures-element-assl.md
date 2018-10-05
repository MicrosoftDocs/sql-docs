---
title: "Measures Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "Measures Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "Measures"
helpviewer_keywords: 
  - "Measures element"
ms.assetid: d2107112-f620-4fd7-a05f-bb2606b4be18
author: minewiskan
ms.author: owend
manager: craigg
---
# Measures Element (ASSL)
  Contains the collection of [Measure](../objects/measure-element-assl.md) elements associated with the parent element.  
  
## Syntax  
  
```xml  
  
<MeasureGroup> <!-- or AggregationInstance, MeasureGroupBinding (out-of-line), PerspectiveMeasureGroup -->  
   ...  
   <Measures>  
      <Measure>...</Measure>  
   </Measures>  
   ...  
</MeasureGroup>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|None|  
|Default value|None|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[AggregationInstance](../objects/aggregationinstance-element-assl.md), [MeasureGroup](../objects/group-element-assl.md), [MeasureGroupBinding (out-of-line)](../data-type/measuregroupbinding-data-type-out-of-line-assl.md), [PerspectiveMeasureGroup](../data-type/perspectivemeasuregroup-data-type-assl.md)|  
|Child elements|[Measure](../objects/measure-element-assl.md)|  
  
## Remarks  
 The corresponding elements in the Analysis Management Objects (AMO) object model are <xref:Microsoft.AnalysisServices.MeasureCollection> and <xref:Microsoft.AnalysisServices.PerspectiveMeasureCollection>.  
  
## See Also  
 [Collections &#40;ASSL&#41;](collections-assl.md)  
  
  
