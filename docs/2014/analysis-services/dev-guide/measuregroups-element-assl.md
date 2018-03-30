---
title: "MeasureGroups Element (ASSL) | Microsoft Docs"
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
  - "MeasureGroups Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "MeasureGroups"
helpviewer_keywords: 
  - "MeasureGroups element"
ms.assetid: 80e970e9-6ea6-47a9-9e5c-d0f9b01c09c0
caps.latest.revision: 32
author: "Minewiskan"
ms.author: "owend"
manager: "mblythe"
---
# MeasureGroups Element (ASSL)
  Contains the collection of [MeasureGroup](../../../2014/analysis-services/dev-guide/measuregroup-element-assl.md) elements associated with the parent element.  
  
## Syntax  
  
```xml  
  
<Cube> <!-- or CubeBinding, Perspective -->  
   ...  
   <MeasureGroups>  
      <MeasureGroup>...</MeasureGroup> <!-- parent: Cube -->  
      <MeasureGroup xsi:type="MeasureGroupBinding">...</MeasureGroup> <!-- parent: CubeBinding -->  
      <MeasureGroup xsi:type="PerspectiveMeasureGroup">...</MeasureGroup> <!-- parent: Perspective -->  
   </MeasureGroups>  
   ...  
</Cube>  
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
|Parent elements|[Cube](../../../2014/analysis-services/dev-guide/cube-element-assl.md), [CubeBinding](../../../2014/analysis-services/dev-guide/cubebinding-data-type-out-of-line-assl.md), [Perspective](../../../2014/analysis-services/dev-guide/perspective-element-assl.md)|  
  
|Ancestor or Parent|Child Element|  
|------------------------|-------------------|  
|[Cube](../../../2014/analysis-services/dev-guide/cube-element-assl.md)|[MeasureGroup](../../../2014/analysis-services/dev-guide/measuregroup-element-assl.md)|  
|[CubeBinding](../../../2014/analysis-services/dev-guide/cubebinding-data-type-out-of-line-assl.md)|[MeasureGroup](../../../2014/analysis-services/dev-guide/measuregroup-element-assl.md) of type [MeasureGroupBinding](../../../2014/analysis-services/dev-guide/measuregroupbinding-data-type-assl.md)|  
|[Perspective](../../../2014/analysis-services/dev-guide/perspective-element-assl.md)|[MeasureGroup](../../../2014/analysis-services/dev-guide/measuregroup-element-assl.md) of type [PerspectiveMeasureGroup](../../../2014/analysis-services/dev-guide/perspectivemeasuregroup-data-type-assl.md)|  
  
## Remarks  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.MeasureGroupCollection> or <xref:Microsoft.AnalysisServices.PerspectiveMeasureGroupCollection>.  
  
## See Also  
 [Collections &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/collections-assl.md)  
  
  