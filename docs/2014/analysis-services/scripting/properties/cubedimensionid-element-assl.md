---
title: "CubeDimensionID Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/08/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "CubeDimensionID Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "CubeDimensionID"
helpviewer_keywords: 
  - "CubeDimensionID element"
ms.assetid: d1341fb2-9afe-40f1-a704-ce548bce48fc
author: minewiskan
ms.author: owend
manager: craigg
---
# CubeDimensionID Element (ASSL)
  Identifies the [CubeDimension](../data-type/dimension-data-type-assl.md) element associated with the parent element.  
  
## Syntax  
  
```xml  
  
<AggregationDesignDimension> <!-- or one of the elements listed below in the Element Relationships table -->  
   ...  
   <CubeDimensionID>...</CubeDimensionID>  
   ...  
</AggregationDesignDimension>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|String|  
|Default value|None|  
|Cardinality|1-1: Required element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[AggregationDesignDimension](../data-type/aggregationdesigndimension-data-type-assl.md), [AggregationDimension](../data-type/aggregationdimension-data-type-assl.md), [AggregationInstanceCubeDimension](../data-type/aggregationinstancecubedimension-data-type-assl.md), [CubeAttributeBinding](../data-type/binding-data-type-assl.md), [CubeDimensionBinding](../data-type/dimensionbinding-data-type-assl.md), [CubeDimensionPermission](../data-type/permission-data-type-assl.md), [MeasureGroupDimension](../data-type/measuregroupdimension-data-type-assl.md), [MeasureGroupDimensionBinding](../data-type/measuregroupdimensionbinding-data-type-assl.md), [PerspectiveDimension](../data-type/perspectivedimension-data-type-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The elements that correspond to the parents of `CubeDimensionID` in the Analysis Management Objects (AMO) object model are <xref:Microsoft.AnalysisServices.AggregationDesignDimension>, <xref:Microsoft.AnalysisServices.AggregationDimension>, <xref:Microsoft.AnalysisServices.CubeAttributeBinding>, <xref:Microsoft.AnalysisServices.CubeDimensionBinding>, <xref:Microsoft.AnalysisServices.CubeDimensionPermission>, <xref:Microsoft.AnalysisServices.MeasureGroupDimension>, <xref:Microsoft.AnalysisServices.MeasureGroupDimensionBinding>, and <xref:Microsoft.AnalysisServices.PerspectiveDimension>.  
  
## See Also  
 [Properties &#40;ASSL&#41;](properties-assl.md)  
  
  
