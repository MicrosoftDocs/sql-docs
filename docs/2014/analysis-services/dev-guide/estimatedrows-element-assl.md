---
title: "EstimatedRows Element (ASSL) | Microsoft Docs"
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
  - "EstimatedRows Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "EstimatedRows"
helpviewer_keywords: 
  - "EstimatedRows element"
ms.assetid: 08a66481-6479-4a93-aa7e-15e8b1f854f2
caps.latest.revision: 33
author: "Minewiskan"
ms.author: "owend"
manager: "mblythe"
---
# EstimatedRows Element (ASSL)
  Contains the estimated number of rows represented by the parent element.  
  
## Syntax  
  
```xml  
  
<AggregationDesign> <!-- or Cube, MeasureGroup, MeasureGroupBinding, Partition -->  
   ...  
   <EstimatedRows>...</EstimatedRows>  
   ...  
</AggregationDimension>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|Long|  
|Default value|None|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[AggregationDesign](../../../2014/analysis-services/dev-guide/aggregationdesign-element-assl.md), [Cube](../../../2014/analysis-services/dev-guide/cube-element-assl.md), [MeasureGroup](../../../2014/analysis-services/dev-guide/measuregroup-element-assl.md), [MeasureGroupBinding](../../../2014/analysis-services/dev-guide/measuregroupbinding-data-type-assl.md), [Partition](../../../2014/analysis-services/dev-guide/partition-element-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The elements that correspond to the parents of `EstimatedRows` in the Analysis Management Objects (AMO) object model are <xref:Microsoft.AnalysisServices.AggregationDesign>, <xref:Microsoft.AnalysisServices.Cube>, <xref:Microsoft.AnalysisServices.MeasureGroup>, <xref:Microsoft.AnalysisServices.MeasureGroupBinding>, and <xref:Microsoft.AnalysisServices.Partition>.  
  
## See Also  
 [Properties &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/properties-assl.md)  
  
  