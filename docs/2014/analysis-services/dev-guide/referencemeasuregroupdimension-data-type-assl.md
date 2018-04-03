---
title: "ReferenceMeasureGroupDimension Data Type (ASSL) | Microsoft Docs"
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
  - "ReferenceMeasureGroupDimension Data Type"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "ReferenceMeasureGroupDimension"
helpviewer_keywords: 
  - "ReferenceMeasureGroupDimension data type"
ms.assetid: 81f7b83e-71a3-4eab-b291-0500d05903dc
caps.latest.revision: 35
author: "Minewiskan"
ms.author: "owend"
manager: "mblythe"
---
# ReferenceMeasureGroupDimension Data Type (ASSL)
  Defines a derived data type that represents a dimension that is indirectly related to the fact table through an intermediate dimension. (For example, a Sales measure group can reference a Geography dimension, which is related through the Customer dimension.)  
  
## Syntax  
  
```xml  
  
<ReferenceMeasureGroupDimension>  
   <!-- The following elements extend MeasureGroupDimension -->  
      <IntermediateCubeDimensionID>...</IntermediateCubeDimensionID>  
   <IntermediateGranularityAttributeID>...</IntermediateGranularityAttributeID>  
   <Materialization>...</Materialization>  
</ReferenceMeasureGroupDimension>  
```  
  
## Data Type Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Base data types|[MeasureGroupDimension](../../../2014/analysis-services/dev-guide/measuregroupdimension-data-type-assl.md)|  
|Derived data types|None|  
  
## Data Type Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|None|  
|Child elements|[IntermediateCubeDimensionID](../../../2014/analysis-services/dev-guide/intermediatecubedimensionid-element-assl.md), [IntermediateGranularityAttributeID](../../../2014/analysis-services/dev-guide/intermediategranularityattributeid-element-assl.md), [Materialization](../../../2014/analysis-services/dev-guide/materialization-element-assl.md)|  
|Derived elements|None|  
  
## Remarks  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.ReferenceMeasureGroupDimension>.  
  
## See Also  
 [Analysis Services Scripting Language XML Data Types &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/analysis-services-scripting-language-xml-data-types-assl.md)  
  
  