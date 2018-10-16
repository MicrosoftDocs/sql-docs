---
title: "Aggregation Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "Aggregation Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "Aggregation"
helpviewer_keywords: 
  - "Aggregation element"
ms.assetid: f37af388-b2b3-4234-a1d6-936ee9b7f2ae
author: minewiskan
ms.author: owend
manager: craigg
---
# Aggregation Element (ASSL)
  Defines a single aggregation for a [Partition](partition-element-assl.md) element.  
  
## Syntax  
  
```xml  
  
<Aggregations>  
      <Aggregation>  
      <ID>...</ID>  
      <Name>...</Name>  
      <Dimensions>...</Dimensions>  
            <Annotations>...</Annotations>  
      <Description>...</Description>  
   </Aggregation>  
</Aggregations>  
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
|Parent element|[Aggregations](../collections/aggregations-element-assl.md)|  
|Child elements|[Annotations](../collections/annotations-element-assl.md), [Description](../properties/description-element-assl.md), [Dimensions](../collections/dimensions-element-assl.md), [ID](../properties/id-element-assl.md), [Name](../properties/name-element-assl.md)|  
  
## Remarks  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.Aggregation>.  
  
## See Also  
 [Partition Element &#40;ASSL&#41;](partition-element-assl.md)   
 [AggregationDesign Element &#40;ASSL&#41;](aggregationdesign-element-assl.md)   
 [MeasureGroup Element &#40;ASSL&#41;](group-element-assl.md)   
 [Objects &#40;ASSL&#41;](objects-assl.md)  
  
  
