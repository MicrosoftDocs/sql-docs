---
title: "RegularMeasureGroupDimension Data Type (ASSL) | Microsoft Docs"
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
  - "RegularMeasureGroupDimension Data Type"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "RegularMeasureGroupDimension"
helpviewer_keywords: 
  - "RegularMeasureGroupDimension data type"
ms.assetid: 5c4ce400-6d7c-40fc-9bcb-392718b77182
caps.latest.revision: 39
author: "Minewiskan"
ms.author: "owend"
manager: "mblythe"
---
# RegularMeasureGroupDimension Data Type (ASSL)
  Defines a derived data type that represents a regular relationship between a dimension and a measure group.  
  
## Syntax  
  
```xml  
  
<RegularMeasureGroupDimension>  
   <!-- The following elements extend MeasureGroupDimension -->  
   <Cardinality>...</Cardinality>  
   <Attributes>...</Attributes>  
</RegularMeasureGroupDimension>  
```  
  
## Data Type Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Base data types|[MeasureGroupDimension](../../../2014/analysis-services/dev-guide/measuregroupdimension-data-type-assl.md)|  
|Derived data types|[ReferenceMeasureGroupDimension](../../../2014/analysis-services/dev-guide/referencemeasuregroupdimension-data-type-assl.md), [DegenerateMeasureGroupDimension](../../../2014/analysis-services/dev-guide/degeneratemeasuregroupdimension-data-type-assl.md)|  
  
## Data Type Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|None|  
|Child elements|[Attributes](../../../2014/analysis-services/dev-guide/attributes-element-assl.md), [Cardinality](../../../2014/analysis-services/dev-guide/cardinality-element-assl.md)|  
|Derived elements|[Dimension](../../../2014/analysis-services/dev-guide/dimension-element-assl.md) ([Dimensions](../../../2014/analysis-services/dev-guide/dimensions-element-assl.md) collection)|  
  
## Remarks  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.RegularMeasureGroupDimension>.  
  
## See Also  
 [Analysis Services Scripting Language XML Data Types &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/analysis-services-scripting-language-xml-data-types-assl.md)  
  
  