---
title: "DegenerateMeasureGroupDimension Data Type (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "DegenerateMeasureGroupDimension Data Type"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "DegenerateMeasureGroupDimension"
helpviewer_keywords: 
  - "DegenerateMeasureGroupDimension data type"
ms.assetid: a64fe908-154d-4fea-b435-afb6ee37a6fa
author: minewiskan
ms.author: owend
manager: craigg
---
# DegenerateMeasureGroupDimension Data Type (ASSL)
  Defines a derived data type that represents the relationship between a degenerate dimension (that is, a fact dimension) and a measure group.  
  
## Syntax  
  
```xml  
  
<DegenerateMeasureGroupDimension>  
   <!-- DegenerateMeasureGroupDimension does not have any elements that extend RegularMeasureGroupDimension -->  
</DegenerateMeasureGroupDimension>  
```  
  
## Data Type Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Base data types|[RegularMeasureGroupDimension](dimension-data-type-assl.md)|  
|Derived data types|None|  
  
## Data Type Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|None|  
|Child elements|None|  
|Derived elements|None|  
  
## Remarks  
 For more information about fact dimensions, see [Dimension Relationships](../../multidimensional-models-olap-logical-cube-objects/dimension-relationships.md).  
  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.DegenerateMeasureGroupDimension>.  
  
## See Also  
 [Analysis Services Scripting Language XML Data Types &#40;ASSL&#41;](analysis-services-scripting-language-xml-data-types-assl.md)  
  
  
