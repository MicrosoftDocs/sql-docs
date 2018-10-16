---
title: "DataAggregation Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "DataAggregation Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
helpviewer_keywords: 
  - "DataAggregation element"
ms.assetid: baf6d2c9-54f6-4a6d-95f7-e1e758be458d
author: minewiskan
ms.author: owend
manager: craigg
---
# DataAggregation Element (ASSL)
  Determines whether the instance can aggregate persisted data or cached data for the [MeasureGroup](../objects/group-element-assl.md).  
  
## Syntax  
  
```xml  
  
<MeasureGroup>  
   ...  
   <DataAggregation>...</DataAggregation>  
   ...  
</MeasureGroup>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|String (enumeration)|  
|Default value|*DataAndCacheAggregatable*|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[MeasureGroup](../objects/group-element-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The value of this element is limited to one of the strings in the following table.  
  
|Value|Description|  
|-----------|-----------------|  
|*None*|Aggregation is not performed for this measure group.|  
|*DataAggregatable*|Persisted data is aggregatable for this measure group.|  
|*CacheAggregatable*|Cached data is aggregatable for this measure group.|  
|*DataAndCacheAggregatable*|Both persisted data and cached data are aggregatable for this measure group.|  
  
 The element that corresponds to the parent of `DataAggregation` in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.MeasureGroup>.  
  
## See Also  
 [Cube Element &#40;ASSL&#41;](../objects/cube-element-assl.md)   
 [Dimension Element &#40;ASSL&#41;](../objects/dimension-element-assl.md)   
 [Properties &#40;ASSL&#41;](properties-assl.md)  
  
  
