---
title: "Attributes Element (ASSL) | Microsoft Docs"
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
caps.latest.revision: 35
author: "Minewiskan"
ms.author: "owend"
manager: "mblythe"
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
|Parent elements|[AggregationDesignDimension](../../../2014/analysis-services/dev-guide/aggregationdesigndimension-data-type-assl.md), [AggregationDimension](../../../2014/analysis-services/dev-guide/aggregationdimension-data-type-assl.md), [AggregationInstanceCubeDimension](../../../2014/analysis-services/dev-guide/aggregationinstancecubedimension-data-type-assl.md), [CubeDimension](../../../2014/analysis-services/dev-guide/cubedimension-data-type-assl.md), [Dimension](../../../2014/analysis-services/dev-guide/dimension-element-assl.md), [PerspectiveDimension](../../../2014/analysis-services/dev-guide/perspectivedimension-data-type-assl.md), [RegularMeasureGroupDimension](../../../2014/analysis-services/dev-guide/regularmeasuregroupdimension-data-type-assl.md), [RelationshipEnd](../../../2014/analysis-services/dev-guide/relationshipend-data-type-assl.md)|  
|Child elements|[Attribute](../../../2014/analysis-services/dev-guide/attribute-element-assl.md)|  
  
## Remarks  
 The corresponding elements in the Analysis Management Objects (AMO) object model are <xref:Microsoft.AnalysisServices.AggregationAttributeCollection>, <xref:Microsoft.AnalysisServices.AggregationDesignAttributeCollection>, <xref:Microsoft.AnalysisServices.AggregationInstanceAttributeCollection>, <xref:Microsoft.AnalysisServices.CubeAttributeCollection>, <xref:Microsoft.AnalysisServices.DimensionAttributeCollection>, <xref:Microsoft.AnalysisServices.MeasureGroupAttributeCollection>, and <xref:Microsoft.AnalysisServices.PerspectiveAttributeCollection>.  
  
## See Also  
 [Collections &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/collections-assl.md)  
  
  