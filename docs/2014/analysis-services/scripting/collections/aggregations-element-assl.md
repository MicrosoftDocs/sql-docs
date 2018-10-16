---
title: "Aggregations Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "Aggregations Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "Aggregations"
helpviewer_keywords: 
  - "Aggregations element"
ms.assetid: 79b7de7a-53b2-4202-bc0f-de1daaf1b179
author: minewiskan
ms.author: owend
manager: craigg
---
# Aggregations Element (ASSL)
  Contains the collection of aggregations defined for an [AggregationDesign](../objects/aggregationdesign-element-assl.md) element.  
  
## Syntax  
  
```xml  
  
<AggregationDesign>  
   ...  
   <Aggregations>  
      <Aggregation>...</Aggregation>  
   </Aggregations>  
   ...  
</AggregationDimension>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|None (collection)|  
|Default value|None (collection)|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent element|[AggregationDesign](../objects/aggregationdesign-element-assl.md)|  
|Child elements|[Aggregation](../objects/aggregation-element-assl.md)|  
  
## Remarks  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.AggregationCollection>.  
  
## See Also  
 [MeasureGroup Element &#40;ASSL&#41;](../objects/group-element-assl.md)   
 [Partition Element &#40;ASSL&#41;](../objects/partition-element-assl.md)   
 [Collections &#40;ASSL&#41;](collections-assl.md)  
  
  
