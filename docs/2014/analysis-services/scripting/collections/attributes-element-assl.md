---
title: "Attributes Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "Attributes Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "Attributes"
helpviewer_keywords: 
  - "Attributes element"
ms.assetid: d6b545e6-1521-496f-a731-f2c2c44118e4
author: minewiskan
ms.author: owend
manager: craigg
---
# Attributes Element (ASSL)
  Contains the collection of attributes for the associated dimension.  
  
## Syntax  
  
```xml  
  
<AggregationDesignDimension> <!-- or one of the elements listed below in the Element Relationships table -->  
   ...  
   <Attributes>  
      <Attribute>...</Attribute>  
  </Attributes>  
   ...  
</AggregationDesignDimension>  
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
|Parent elements|[AggregationDesignDimension](../data-type/dimension-data-type-assl.md), [AggregationDimension](../data-type/aggregationdimension-data-type-assl.md), [AggregationInstanceCubeDimension](../data-type/cubedimension-data-type-assl.md), [CubeDimension](../data-type/cubedimension-data-type-assl.md), [Dimension](../objects/dimension-element-assl.md), [PerspectiveDimension](../data-type/perspectivedimension-data-type-assl.md), [RegularMeasureGroupDimension](../data-type/measuregroupdimension-data-type-assl.md), [RelationshipEnd](../data-type/relationshipend-data-type-assl.md)|  
|Child elements|[Attribute](../objects/attribute-element-assl.md)|  
  
## Remarks  
 The corresponding elements in the Analysis Management Objects (AMO) object model are <xref:Microsoft.AnalysisServices.AggregationAttributeCollection>, <xref:Microsoft.AnalysisServices.AggregationDesignAttributeCollection>, <xref:Microsoft.AnalysisServices.AggregationInstanceAttributeCollection>, <xref:Microsoft.AnalysisServices.CubeAttributeCollection>, <xref:Microsoft.AnalysisServices.DimensionAttributeCollection>, <xref:Microsoft.AnalysisServices.MeasureGroupAttributeCollection>, and <xref:Microsoft.AnalysisServices.PerspectiveAttributeCollection>.  
  
## See Also  
 [Collections &#40;ASSL&#41;](collections-assl.md)  
  
  
