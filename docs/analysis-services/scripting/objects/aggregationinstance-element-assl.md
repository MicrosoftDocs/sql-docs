---
title: "AggregationInstance Element (ASSL) | Microsoft Docs"
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
# AggregationInstance Element (ASSL)
[!INCLUDE[ssas-appliesto-sqlas](../../../includes/ssas-appliesto-sqlas.md)]
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
|Parent elements|[AggregationInstances](../../../analysis-services/scripting/collections/aggregationinstances-element-assl.md)|  
|Child elements|[AggregationID](../../../analysis-services/scripting/properties/aggregationid-element-assl.md), [AggregationType](../../../analysis-services/scripting/properties/aggregationtype-element-assl.md), [Annotations](../../../analysis-services/scripting/collections/annotations-element-assl.md), [Dimensions](../../../analysis-services/scripting/collections/dimensions-element-assl.md), [Measures](../../../analysis-services/scripting/collections/measures-element-assl.md), [Source](../../../analysis-services/scripting/properties/source-element-binding-assl.md)|  
  
## Remarks  
 When a [Partition](../../../analysis-services/scripting/objects/partition-element-assl.md) element uses an [AggregationDesign](../../../analysis-services/scripting/objects/aggregationdesign-element-assl.md) element to generate aggregations for that partition, each [Aggregation](../../../analysis-services/scripting/objects/aggregation-element-assl.md) in the **AggregationDesign** is instantiated for that partition. Multiple partitions can use the same aggregation design to generate multiple instances of a defined aggregation. The **AggregationInstance** element represents an instance of a defined aggregation.  
  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.AggregationInstance>.  
  
## See Also  
 [Objects &#40;ASSL&#41;](../../../analysis-services/scripting/objects/objects-assl.md)  
  
  
