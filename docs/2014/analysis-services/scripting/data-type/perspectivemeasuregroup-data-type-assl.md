---
title: "PerspectiveMeasureGroup Data Type (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "PerspectiveMeasureGroup Data Type"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "PerspectiveMeasureGroup"
helpviewer_keywords: 
  - "PerspectiveMeasureGroup data type"
ms.assetid: 5927120d-f30e-4f87-8523-6d17012817d7
author: minewiskan
ms.author: owend
manager: craigg
---
# PerspectiveMeasureGroup Data Type (ASSL)
  Defines a primitive data type that represents information about a measure group in a [Perspective](../objects/perspective-element-assl.md) element.  
  
## Syntax  
  
```xml  
  
<PerspectiveMeasureGroup>  
   <MeasureGroupID>...</MeasureGroupID>  
   <Measures>...</Measures>  
   <Annotations>...</Annotations>  
</PerspectiveMeasureGroup>  
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
|Child elements|[Annotations](../collections/annotations-element-assl.md), [MeasureGroupID](../properties/id-element-assl.md), [Measures](../collections/measures-element-assl.md)|  
|Derived elements|[MeasureGroup](../objects/group-element-assl.md) ([MeasureGroups](../collections/groups-element-assl.md) collection of [Perspective](../objects/perspective-element-assl.md))|  
  
## Remarks  
 A measure group in a perspective has the same structure as a measure group in the underlying cube.  
  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.PerspectiveMeasureGroup>.  
  
## See Also  
 [Analysis Services Scripting Language XML Data Types &#40;ASSL&#41;](analysis-services-scripting-language-xml-data-types-assl.md)  
  
  
