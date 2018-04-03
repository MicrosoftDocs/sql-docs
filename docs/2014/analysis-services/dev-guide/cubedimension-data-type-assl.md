---
title: "CubeDimension Data Type (ASSL) | Microsoft Docs"
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
  - "CubeDimension Data Type"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "CubeDimension"
helpviewer_keywords: 
  - "CubeDimension data type"
ms.assetid: 128ac790-65a1-4e35-b909-8dba2a61b24c
caps.latest.revision: 40
author: "Minewiskan"
ms.author: "owend"
manager: "mblythe"
---
# CubeDimension Data Type (ASSL)
  Defines a primitive data type that represents the relationship between a dimension and a cube.  
  
## Syntax  
  
```xml  
  
<CubeDimension>  
      <ID>...</ID>  
   <Name>...</Name>  
   <Translations>...</Translations>  
   <DimensionID>...</DimensionID>  
   <Visible>...</Visible>  
   <AllMemberAggregationUsage>...</AllMemberAggregationUsage>  
   <HierarchyUniqueNameStyle>...</HierarchyUniqueNameStyle>  
   <MemberUniqueNameStyle>...</MemberUniqueNameStyle>  
      <Attributes>...</Attributes>  
   <Hierarchies>...</Hierarchies>  
      <Annotations>...</Annotation>  
</CubeDimension>  
```  
  
## Data Type Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Base data types|None|  
|Derived data types|None|  
  
## Data Type Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|None|  
|Child elements|[AllMemberAggregationUsage](../../../2014/analysis-services/dev-guide/allmemberaggregationusage-element-assl.md), [Annotations](../../../2014/analysis-services/dev-guide/annotations-element-assl.md), [Attributes](../../../2014/analysis-services/dev-guide/attributes-element-assl.md), [DimensionID](../../../2014/analysis-services/dev-guide/dimensionid-element-assl.md), [Hierarchies](../../../2014/analysis-services/dev-guide/hierarchies-element-assl.md), [HierarchyUniqueNameStyle](../../../2014/analysis-services/dev-guide/hierarchyuniquenamestyle-element-assl.md), [ID](../../../2014/analysis-services/dev-guide/id-element-assl.md), [MemberUniqueNameStyle](../../../2014/analysis-services/dev-guide/memberuniquenamestyle-element-assl.md), [Name](../../../2014/analysis-services/dev-guide/name-element-assl.md), [Visible](../../../2014/analysis-services/dev-guide/visible-element-assl.md), [Translations](../../../2014/analysis-services/dev-guide/translations-element-assl.md)|  
|Derived elements|[Dimension](../../../2014/analysis-services/dev-guide/dimension-element-assl.md) ([Dimensions](../../../2014/analysis-services/dev-guide/dimensions-element-assl.md) collection of [Cube](../../../2014/analysis-services/dev-guide/cube-element-assl.md))|  
  
## Remarks  
 There is one `CubeDimension` for each dimension relationship on a `Cube`. The `CubeDimension` covers all the `MeasureGroups` of the cube.  
  
 A `CubeDimension` must include a [CubeHierarchy](../../../2014/analysis-services/dev-guide/cubehierarchy-data-type-assl.md) if the dimension has something specific to say about the hierarchy, including disabling the hierarchy (thereby, allowing selection of which hierarchies apply to a particular dimension usage), or making the hierarchy invisible.  
  
 Similarly, a `CubeDimension` must include a [CubeAttribute](../../../2014/analysis-services/dev-guide/cubeattribute-data-type-assl.md) only if the dimension has something specific to say about the attribute. (There is no way to select which attributes apply to a particular dimension usage, though attributes can be made invisible).  
  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.CubeDimension>.  
  
## See Also  
 [Analysis Services Scripting Language XML Data Types &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/analysis-services-scripting-language-xml-data-types-assl.md)  
  
  