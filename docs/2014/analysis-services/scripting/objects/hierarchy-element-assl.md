---
title: "Hierarchy Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "Hierarchy Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "Hierarchy"
helpviewer_keywords: 
  - "Hierarchy element"
ms.assetid: ac54d74a-5e6c-4c24-83bf-766440478f6c
author: minewiskan
ms.author: owend
manager: craigg
---
# Hierarchy Element (ASSL)
  Defines a hierarchy in a dimension.  
  
## Syntax  
  
```xml  
  
<Hierarchies>  
   <Hierarchy xsi:type="Hierarchy">...</Hierarchy> <!-- ancestor: Dimension -->  
   <!-- or -->  
   <Hierarchy xsi:type="CubeHierarchy">...</Hierarchy> <!-- ancestor: CubeDimension -->  
   <!-- or -->  
   <Hierarchy xsi:type="PerspectiveHierarchy">...</Hierarchy> <!-- ancestor: PerspectiveDimension -->  
   <!-- or -->  
   <Hierarchy xsi:type="MeasureGroupHierarchy">...</Hierarchy> <!-- ancestor: RegularMeasureGroupDimension -->  
</Hierarchies>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length||  
|Default value|None|  
|Cardinality|0-n: Optional element that can occur more than once.|  
  
## Data Type and Length  
  
|Ancestor or Parent|Data Type|  
|------------------------|---------------|  
|[Dimension](../data-type/hierarchy-data-type-assl.md)|  
|[CubeDimension](../data-type/cubehierarchy-data-type-assl.md)|  
|[PerspectiveDimension](../data-type/perspectivehierarchy-data-type-assl.md)|  
|[RegularMeasureGroupDimension](../data-type/measuregrouphierarchy-data-type-assl.md)|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[Hierarchies](../collections/hierarchies-element-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The corresponding elements in the Analysis Management Objects (AMO) object model are <xref:Microsoft.AnalysisServices.Hierarchy>, <xref:Microsoft.AnalysisServices.CubeHierarchy>, and <xref:Microsoft.AnalysisServices.PerspectiveHierarchy>.  
  
## See Also  
 [Objects &#40;ASSL&#41;](objects-assl.md)  
  
  
