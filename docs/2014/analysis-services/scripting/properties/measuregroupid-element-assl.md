---
title: "MeasureGroupID Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "MeasureGroupID Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "MeasureGroupID"
helpviewer_keywords: 
  - "MeasureGroupID element"
ms.assetid: 3b075f86-dbbc-4285-8d2d-61fa722181c7
author: minewiskan
ms.author: owend
manager: craigg
---
# MeasureGroupID Element (ASSL)
  Associates a [MeasureGroup](../objects/group-element-assl.md) with the parent element, binding, or out-of-line binding.  
  
## Syntax  
  
```xml  
  
<ManyToManyMeasureGroupDimension> <!-- or MeasureGroupAttributeBinding, MeasureGroupBinding, PerspectiveMeasureGroup -->  
   ...  
   <MeasureGroupID>...</MeasureGroupID>  
   ...  
</ManyToManyMeasureGroupDimension>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|String|  
|Default value|None|  
|Cardinality||  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[ManyToManyMeasureGroupDimension](../data-type/dimension-data-type-assl.md), [MeasureGroupAttributeBinding](../data-type/measuregroupattributebinding-data-type-out-of-line-assl.md), [MeasureGroupBinding](../data-type/binding-data-type-assl.md), [PerspectiveMeasureGroup](../data-type/perspectivemeasuregroup-data-type-assl.md)|  
  
|Child Elements - Ancestor or Parent|Cardinality|  
|------------------------------------------|-----------------|  
|[ManyToManyMeasureGroupDimension](../data-type/dimension-data-type-assl.md)|0-1: Optional element that can occur once and only once.|  
|[MeasureGroupAttributeBinding](../data-type/measuregroupattributebinding-data-type-out-of-line-assl.md), [MeasureGroupBinding](../data-type/binding-data-type-assl.md) and [PerspectiveMeasureGroup](../data-type/perspectivemeasuregroup-data-type-assl.md)|1-1: Required element that occurs once and only once.|  
  
## Remarks  
 The elements that correspond to the parents of `MeasureGroupID` in the Analysis Management Objects (AMO) object model are <xref:Microsoft.AnalysisServices.ManyToManyMeasureGroupDimension>, <xref:Microsoft.AnalysisServices.MeasureGroupBinding>, and <xref:Microsoft.AnalysisServices.PerspectiveMeasureGroup>.  
  
## See Also  
 [Properties &#40;ASSL&#41;](properties-assl.md)  
  
  
