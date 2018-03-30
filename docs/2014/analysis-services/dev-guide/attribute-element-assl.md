---
title: "Attribute Element (ASSL) | Microsoft Docs"
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
  - "Attribute Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "Attribute"
helpviewer_keywords: 
  - "Attribute element"
ms.assetid: 079ec9f8-a314-4e3c-821a-b42c65cc7363
caps.latest.revision: 34
author: "Minewiskan"
ms.author: "owend"
manager: "mblythe"
---
# Attribute Element (ASSL)
  Contains the description of an attribute.  
  
## Syntax  
  
```xml  
  
<Attributes>  
   <Attribute xsi:type="AggregationAttribute">...</Attribute> <!-- ancestor: AggregationDimension -->  
   <!-- or -->  
   <Attribute xsi:type="AggregationDesignAttribute">...</Attribute> <!-- ancestor: AggregationDesignDimension -->  
   <!-- or -->  
   <Attribute xsi:type="AggregationInstanceAttribute">...</Attribute> <!-- ancestor: AggregationInstanceCubeDimension -->  
   <!-- or -->  
   <Attribute xsi:type="CubeAttribute">...</Attribute> <!--ancestor:  CubeDimension -->  
   <!-- or -->  
   <Attribute xsi:type="DimensionAttribute">...</Attribute> <!-- ancestor: Dimension -->  
   <!-- or -->  
   <Attribute xsi:type="MeasureGroupAttribute">...</Attribute> <!-- ancestor: RegularMeasureGroupDimension -->  
   <!-- or -->  
   <Attribute xsi:type="PerspectiveAttribute">...</Attribute> <!-- ancestor: PerspectiveDimension -->  
   <!-- or -->  
   <Attribute>  
      <AttributeID>...</AttributeID>  
   </Attribute> <!-- ancestor: RelationshipEnd -->  
</Attributes>  
```  
  
## Element Characteristics  
 Default value: None  
  
 Cardinality: 0-1: Optional element that can occur once and only once.  
  
|Ancestor or Parent|Data Type|  
|------------------------|---------------|  
|[AggregationDesignDimension](../../../2014/analysis-services/dev-guide/aggregationdesigndimension-data-type-assl.md)|[AggregationDesignAttribute](../../../2014/analysis-services/dev-guide/aggregationdesignattribute-data-type-assl.md)|  
|[AggregationDimension](../../../2014/analysis-services/dev-guide/aggregationdimension-data-type-assl.md)|[AggregationAttribute](../../../2014/analysis-services/dev-guide/aggregationattribute-data-type-assl.md)|  
|[AggregationInstanceCubeDimension](../../../2014/analysis-services/dev-guide/aggregationinstancecubedimension-data-type-assl.md)|[AggregationInstanceAttribute](../../../2014/analysis-services/dev-guide/aggregationinstanceattribute-data-type-assl.md)|  
|[CubeDimension](../../../2014/analysis-services/dev-guide/cubedimension-data-type-assl.md)|[CubeAttribute](../../../2014/analysis-services/dev-guide/cubeattribute-data-type-assl.md)|  
|[Dimension](../../../2014/analysis-services/dev-guide/dimension-element-assl.md)|[DimensionAttribute](../../../2014/analysis-services/dev-guide/dimensionattribute-data-type-assl.md)|  
|[RegularMeasureGroupDimension](../../../2014/analysis-services/dev-guide/regularmeasuregroupdimension-data-type-assl.md)|[MeasureGroupAttribute](../../../2014/analysis-services/dev-guide/measuregroupattribute-data-type-assl.md)|  
|[PerspectiveDimension](../../../2014/analysis-services/dev-guide/perspectivedimension-data-type-assl.md)|[PerspectiveAttribute](../../../2014/analysis-services/dev-guide/perspectiveattribute-data-type-assl.md)|  
|[RelationshipEnd](../../../2014/analysis-services/dev-guide/relationshipend-data-type-assl.md)|\<Attribute><br />      \<[AttributeID](../../../2014/analysis-services/dev-guide/attributeid-element-assl.md)>...\</AttributeID>\</Attribute>|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[Attributes](../../../2014/analysis-services/dev-guide/attributes-element-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The corresponding elements in the Analysis Management Objects (AMO) object model are <xref:Microsoft.AnalysisServices.AggregationDesignAttribute>, <xref:Microsoft.AnalysisServices.AggregationAttribute>, <xref:Microsoft.AnalysisServices.CubeAttribute>, <xref:Microsoft.AnalysisServices.DimensionAttribute>, <xref:Microsoft.AnalysisServices.MeasureGroupAttribute>, and <xref:Microsoft.AnalysisServices.PerspectiveAttribute>.  
  
## See Also  
 [Objects &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/objects-assl.md)  
  
  