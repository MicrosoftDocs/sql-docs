---
title: "ProactiveCaching Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
apiname: 
  - "ProactiveCaching Element"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "ProactiveCaching"
helpviewer_keywords: 
  - "ProactiveCaching element"
ms.assetid: 85f9ed44-2ede-406f-b0ca-237ab2f49722
caps.latest.revision: 34
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
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
|Parent elements|[Cube](../../../analysis-services/scripting/objects/cube-element-assl.md), [Dimension](../../../analysis-services/scripting/objects/dimension-element-assl.md), [MeasureGroup](../../../analysis-services/scripting/objects/measuregroup-element-assl.md), [Partition](../../../analysis-services/scripting/objects/partition-element-assl.md)|  
|Child elements|[AggregationStorage](../../../analysis-services/scripting/properties/aggregationstorage-element-assl.md), [Enabled](../../../analysis-services/scripting/properties/enabled-element-assl.md), [ForceRebuildInterval](../../../analysis-services/scripting/properties/forcerebuildinterval-element-assl.md), [Latency](../../../analysis-services/scripting/properties/latency-element-assl.md), [OnlineMode](../../../analysis-services/scripting/properties/onlinemode-element-assl.md), [SilenceInterval](../../../analysis-services/scripting/properties/silenceinterval-element-assl.md), [SilenceOverrideInterval](../../../analysis-services/scripting/properties/silenceoverrideinterval-element-assl.md), [Source](../../../analysis-services/scripting/properties/source-element-binding-assl.md)|  
  
## Remarks  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.ProactiveCaching>.  
  
## See Also  
 [Objects &#40;ASSL&#41;](../../../analysis-services/scripting/objects/objects-assl.md)  
  
  