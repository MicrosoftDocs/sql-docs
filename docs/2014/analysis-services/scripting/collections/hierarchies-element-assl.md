---
title: "Hierarchies Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "Hierarchies Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "Hierarchies"
helpviewer_keywords: 
  - "Hierarchies element"
ms.assetid: dc844eea-869c-4217-b9be-e543a76f5e92
author: minewiskan
ms.author: owend
manager: craigg
---
# Hierarchies Element (ASSL)
  Contains the collection of [Hierarchy](../objects/hierarchy-element-assl.md) elements associated with the parent element.  
  
## Syntax  
  
```xml  
  
<Dimension> <!-- or CubeDimension, PerspectiveDimension -->  
   ...  
   <Hierarchies>  
            <Hierarchy>...</Hierarchy> <!-- parent: Dimension -->  
      <!-- or -->  
      <Hierarchy xsi:type="CubeHierarchy">...</Hierarchy> <!-- parent: CubeDimension -->  
     <!-- or -->  
      <Hierarchy xsi:type="PerspectiveHierarchy">...</Hierarchy> <!-- parent: PerspectiveDimension -->  
   <Hierarchies>  
   ...  
</Dimension>  
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
|Parent elements|[CubeDimension](../data-type/dimension-data-type-assl.md), [Dimension](../objects/dimension-element-assl.md), [PerspectiveDimension](../data-type/perspectivedimension-data-type-assl.md)|  
  
|Ancestor or Parent|Child Element|  
|------------------------|-------------------|  
|[CubeDimension](../data-type/dimension-data-type-assl.md)|[Hierarchy](../objects/hierarchy-element-assl.md) of type [CubeHierarchy](../data-type/hierarchy-data-type-assl.md)|  
|[Dimension](../objects/dimension-element-assl.md)|[Hierarchy](../objects/hierarchy-element-assl.md)|  
|[PerspectiveDimension](../data-type/perspectivedimension-data-type-assl.md)|[Hierarchy](../objects/hierarchy-element-assl.md) of type [PerspectiveHierarchy](../data-type/perspectivehierarchy-data-type-assl.md)|  
  
## Remarks  
 The corresponding elements in the Analysis Management Objects (AMO) object model are <xref:Microsoft.AnalysisServices.HierarchyCollection>, <xref:Microsoft.AnalysisServices.CubeHierarchyCollection>, and <xref:Microsoft.AnalysisServices.PerspectiveHierarchyCollection>.  
  
## See Also  
 [Collections &#40;ASSL&#41;](collections-assl.md)  
  
  
