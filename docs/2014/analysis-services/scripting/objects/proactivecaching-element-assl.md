---
title: "ProactiveCaching Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "ProactiveCaching Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "ProactiveCaching"
helpviewer_keywords: 
  - "ProactiveCaching element"
ms.assetid: 85f9ed44-2ede-406f-b0ca-237ab2f49722
author: minewiskan
ms.author: owend
manager: craigg
---
# ProactiveCaching Element (ASSL)
  Defines proactive caching settings for the parent element.  
  
## Syntax  
  
```xml  
  
<Cube> <!-- or Dimension, MeasureGroup, Partition -->  
   ...  
   <ProactiveCaching>  
      <OnlineMode>...</OnlineMode>  
      <AggregationStorage>...</AggregationStorage>  
      <Source>...</Source>  
      <SilenceInterval>...</SilenceInterval>  
      <Latency>...</Latency>  
      <SilenceOverrideInterval>...</SilenceOverrideInterval>  
      <ForceRebuildInterval>...</ForceRebuildInterval>  
      <Enabled >...</Enabled>  
   </ProactiveCaching>  
   ...  
</Cube>  
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
|Parent elements|[Cube](cube-element-assl.md), [Dimension](dimension-element-assl.md), [MeasureGroup](group-element-assl.md), [Partition](partition-element-assl.md)|  
|Child elements|[AggregationStorage](../properties/aggregationstorage-element-assl.md), [Enabled](../properties/enabled-element-assl.md), [ForceRebuildInterval](../properties/forcerebuildinterval-element-assl.md), [Latency](../properties/latency-element-assl.md), [OnlineMode](../properties/onlinemode-element-assl.md), [SilenceInterval](../properties/silenceinterval-element-assl.md), [SilenceOverrideInterval](../properties/silenceoverrideinterval-element-assl.md), [Source](../properties/source-element-binding-assl.md)|  
  
## Remarks  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.ProactiveCaching>.  
  
## See Also  
 [Objects &#40;ASSL&#41;](objects-assl.md)  
  
  
