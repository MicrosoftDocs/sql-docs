---
title: "AggregationStorage Element (ASSL) | Microsoft Docs"
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
  - "AggregationStorage Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "AggregationStorage"
helpviewer_keywords: 
  - "AggregationStorage element"
ms.assetid: dd082388-534d-4847-9232-8f80fc9fe96e
caps.latest.revision: 35
author: "Minewiskan"
ms.author: "owend"
manager: "mblythe"
---
# AggregationStorage Element (ASSL)
  Identifies the storage method for aggregations.  
  
## Syntax  
  
```xml  
  
<ProactiveCaching>  
   ...  
   <AggregationStorage>...</AggregationStorage>  
   ...  
</ProactiveCaching>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|String (enumeration)|  
|Default value|*Regular*|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent element|[ProactiveCaching](../../../2014/analysis-services/dev-guide/proactivecaching-element-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The value of this element is limited to one of the following strings:  
  
|Value|Description|  
|-----------|-----------------|  
|*Regular*|Aggregation data is stored in the default manner.|  
|*MolapOnly*|Aggregation data is stored using multidimensional OLAP (MOLAP) storage only.|  
  
 The *MolapOnly* option is available only for the [Partition](../../../2014/analysis-services/dev-guide/partition-element-assl.md) element.  
  
 The enumeration that corresponds to the allowed values for `AggregationStorage` in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.ProactiveCachingAggregationStorage>.  
  
## See Also  
 [Properties &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/properties-assl.md)  
  
  