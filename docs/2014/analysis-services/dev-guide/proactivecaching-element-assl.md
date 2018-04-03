---
title: "ProactiveCaching Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
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
caps.latest.revision: 33
author: "Minewiskan"
ms.author: "owend"
manager: "mblythe"
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
|Parent elements|[Cube](../../../2014/analysis-services/dev-guide/cube-element-assl.md), [Dimension](../../../2014/analysis-services/dev-guide/dimension-element-assl.md), [MeasureGroup](../../../2014/analysis-services/dev-guide/measuregroup-element-assl.md), [Partition](../../../2014/analysis-services/dev-guide/partition-element-assl.md)|  
|Child elements|[AggregationStorage](../../../2014/analysis-services/dev-guide/aggregationstorage-element-assl.md), [Enabled](../../../2014/analysis-services/dev-guide/enabled-element-assl.md), [ForceRebuildInterval](../../../2014/analysis-services/dev-guide/forcerebuildinterval-element-assl.md), [Latency](../../../2014/analysis-services/dev-guide/latency-element-assl.md), [OnlineMode](../../../2014/analysis-services/dev-guide/onlinemode-element-assl.md), [SilenceInterval](../../../2014/analysis-services/dev-guide/silenceinterval-element-assl.md), [SilenceOverrideInterval](../../../2014/analysis-services/dev-guide/silenceoverrideinterval-element-assl.md), [Source](../../../2014/analysis-services/dev-guide/source-element-binding-assl.md)|  
  
## Remarks  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.ProactiveCaching>.  
  
## See Also  
 [Objects &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/objects-assl.md)  
  
  