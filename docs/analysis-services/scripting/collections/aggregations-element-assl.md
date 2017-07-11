---
title: "Aggregations Element (ASSL) | Microsoft Docs"
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
  - "Aggregations Element"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "Aggregations"
helpviewer_keywords: 
  - "Aggregations element"
ms.assetid: 79b7de7a-53b2-4202-bc0f-de1daaf1b179
caps.latest.revision: 35
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# Aggregations Element (ASSL)
  Contains the collection of aggregations defined for an [AggregationDesign](../../../analysis-services/scripting/objects/aggregationdesign-element-assl.md) element.  
  
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
|Parent element|[AggregationDesign](../../../analysis-services/scripting/objects/aggregationdesign-element-assl.md)|  
|Child elements|[Aggregation](../../../analysis-services/scripting/objects/aggregation-element-assl.md)|  
  
## Remarks  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.AggregationCollection>.  
  
## See Also  
 [MeasureGroup Element &#40;ASSL&#41;](../../../analysis-services/scripting/objects/measuregroup-element-assl.md)   
 [Partition Element &#40;ASSL&#41;](../../../analysis-services/scripting/objects/partition-element-assl.md)   
 [Collections &#40;ASSL&#41;](../../../analysis-services/scripting/collections/collections-assl.md)  
  
  