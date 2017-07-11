---
title: "PerspectiveMeasureGroup Data Type (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/07/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
apiname: 
  - "PerspectiveMeasureGroup Data Type"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "PerspectiveMeasureGroup"
helpviewer_keywords: 
  - "PerspectiveMeasureGroup data type"
ms.assetid: 5927120d-f30e-4f87-8523-6d17012817d7
caps.latest.revision: 35
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# PerspectiveMeasureGroup Data Type (ASSL)
  Defines a primitive data type that represents information about a measure group in a [Perspective](../../../analysis-services/scripting/objects/perspective-element-assl.md) element.  
  
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
|Child elements|[Annotations](../../../analysis-services/scripting/collections/annotations-element-assl.md), [MeasureGroupID](../../../analysis-services/scripting/properties/measuregroupid-element-assl.md), [Measures](../../../analysis-services/scripting/collections/measures-element-assl.md)|  
|Derived elements|[MeasureGroup](../../../analysis-services/scripting/objects/measuregroup-element-assl.md) ([MeasureGroups](../../../analysis-services/scripting/collections/measuregroups-element-assl.md) collection of [Perspective](../../../analysis-services/scripting/objects/perspective-element-assl.md))|  
  
## Remarks  
 A measure group in a perspective has the same structure as a measure group in the underlying cube.  
  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.PerspectiveMeasureGroup>.  
  
## See Also  
 [Analysis Services Scripting Language XML Data Types &#40;ASSL&#41;](../../../analysis-services/scripting/data-type/analysis-services-scripting-language-xml-data-types-assl.md)  
  
  