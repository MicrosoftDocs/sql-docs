---
title: "AggregationInstance Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "AggregationInstance Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
helpviewer_keywords: 
  - "AggregationInstance element"
ms.assetid: 2e77e9e1-9f2c-4df4-9aa6-5b7b911016a3
author: minewiskan
ms.author: owend
manager: craigg
---
# AggregationInstance Element (ASSL)
  Defines an aggregation instance for a partition.  
  
## Syntax  
  
```xml  
  
<AggregationInstances>  
   <AggregationInstance>  
      <AggregationType>...</AggregationType>  
      <AggregationID>...</AggregationID>  
      <Source>...</Source>  
      <Dimensions>...</Dimensions>  
      <Measures>...</Measures>  
      <Annotations>...</Annotations>  
   </AggregationInstance>  
</AggregationInstances>  
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
|Parent elements|[AggregationInstances](../collections/aggregationinstances-element-assl.md)|  
|Child elements|[AggregationID](../properties/id-element-assl.md), [AggregationType](../properties/aggregationtype-element-assl.md), [Annotations](../collections/annotations-element-assl.md), [Dimensions](../collections/dimensions-element-assl.md), [Measures](../collections/measures-element-assl.md), [Source](../properties/source-element-binding-assl.md)|  
  
## Remarks  
 When a [Partition](partition-element-assl.md) element uses an [AggregationDesign](aggregationdesign-element-assl.md) element to generate aggregations for that partition, each [Aggregation](aggregation-element-assl.md) in the `AggregationDesign` is instantiated for that partition. Multiple partitions can use the same aggregation design to generate multiple instances of a defined aggregation. The `AggregationInstance` element represents an instance of a defined aggregation.  
  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.AggregationInstance>.  
  
## See Also  
 [Objects &#40;ASSL&#41;](objects-assl.md)  
  
  
