---
title: "AggregationDesign Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "AggregationDesign Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "AggregationDesign"
helpviewer_keywords: 
  - "AggregationDesign element"
ms.assetid: 80ad98d8-73a8-4353-b5ad-d2a9ac3bc531
author: minewiskan
ms.author: owend
manager: craigg
---
# AggregationDesign Element (ASSL)
  Defines a set of aggregation definitions that can be shared across multiple partitions in a database.  
  
## Syntax  
  
```xml  
  
<AggregationDesigns>  
   <AggregationDesign>  
      <ID>...</ID>  
      <Name>...</Name>  
            <Description>...</Description>  
      <EstimatedRows>...</EstimatedRows>  
      <Dimensions>...</Dimensions>  
            <Aggregations>...</Aggregation>  
      <EstimatedPerformanceGain>...</EstimatedPerformanceGain>  
      <Annotations>...</Annotations>  
   </AggregationDesign>  
</AggregationDesigns>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|None|  
|Default value|None|  
|Cardinality|0-n: Optional element that can occur more than once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[AggregationDesigns](../collections/aggregationdesigns-element-assl.md)|  
|Child elements|[Aggregations](../collections/aggregations-element-assl.md), [Annotations](../collections/annotations-element-assl.md), [Description](../properties/description-element-assl.md), [Dimensions](../collections/dimensions-element-assl.md), [EstimatedPerformanceGain](../properties/estimatedperformancegain-element-assl.md), [EstimatedRows](../properties/estimatedrows-element-assl.md), [ID](../properties/id-element-assl.md), [Name](../properties/name-element-assl.md)|  
  
## Remarks  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.AggregationDesign>.  
  
## See Also  
 [Partition Element &#40;ASSL&#41;](partition-element-assl.md)   
 [Aggregation Element &#40;ASSL&#41;](aggregation-element-assl.md)   
 [Aggregations Element &#40;ASSL&#41;](../collections/aggregations-element-assl.md)   
 [Objects &#40;ASSL&#41;](objects-assl.md)  
  
  
