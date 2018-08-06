---
title: "DataAggregation Element (ASSL) | Microsoft Docs"
ms.date: 05/08/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: assl
ms.topic: reference
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# DataAggregation Element (ASSL)
[!INCLUDE[ssas-appliesto-sqlas](../../../includes/ssas-appliesto-sqlas.md)]
  Determines whether the instance can aggregate persisted data or cached data for the [MeasureGroup](../../../analysis-services/scripting/objects/measuregroup-element-assl.md).  
  
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
|Parent elements|[MeasureGroup](../../../analysis-services/scripting/objects/measuregroup-element-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The value of this element is limited to one of the strings in the following table.  
  
|Value|Description|  
|-----------|-----------------|  
|*None*|Aggregation is not performed for this measure group.|  
|*DataAggregatable*|Persisted data is aggregatable for this measure group.|  
|*CacheAggregatable*|Cached data is aggregatable for this measure group.|  
|*DataAndCacheAggregatable*|Both persisted data and cached data are aggregatable for this measure group.|  
  
 The element that corresponds to the parent of **DataAggregation** in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.MeasureGroup>.  
  
## See Also  
 [Cube Element &#40;ASSL&#41;](../../../analysis-services/scripting/objects/cube-element-assl.md)   
 [Dimension Element &#40;ASSL&#41;](../../../analysis-services/scripting/objects/dimension-element-assl.md)   
 [Properties &#40;ASSL&#41;](../../../analysis-services/scripting/properties/properties-assl.md)  
  
  
