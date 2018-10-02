---
title: "Attribute Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
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
author: minewiskan
ms.author: owend
manager: craigg
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
|[AggregationDesignDimension](../data-type/aggregationdesignattribute-data-type-assl.md)|  
|[AggregationDimension](../data-type/aggregationattribute-data-type-assl.md)|  
|[AggregationInstanceCubeDimension](../data-type/aggregationinstanceattribute-data-type-assl.md)|  
|[CubeDimension](../data-type/cubeattribute-data-type-assl.md)|  
|[Dimension](../data-type/dimensionattribute-data-type-assl.md)|  
|[RegularMeasureGroupDimension](../data-type/measuregroupattribute-data-type-assl.md)|  
|[PerspectiveDimension](../data-type/perspectiveattribute-data-type-assl.md)|  
|[RelationshipEnd](../data-type/relationshipend-data-type-assl.md)|\<Attribute><br />      \<[AttributeID](../properties/id-element-assl.md)>...\</AttributeID>\</Attribute>|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[Attributes](../collections/attributes-element-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The corresponding elements in the Analysis Management Objects (AMO) object model are <xref:Microsoft.AnalysisServices.AggregationDesignAttribute>, <xref:Microsoft.AnalysisServices.AggregationAttribute>, <xref:Microsoft.AnalysisServices.CubeAttribute>, <xref:Microsoft.AnalysisServices.DimensionAttribute>, <xref:Microsoft.AnalysisServices.MeasureGroupAttribute>, and <xref:Microsoft.AnalysisServices.PerspectiveAttribute>.  
  
## See Also  
 [Objects &#40;ASSL&#41;](objects-assl.md)  
  
  
