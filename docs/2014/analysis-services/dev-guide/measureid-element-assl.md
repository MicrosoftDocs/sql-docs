---
title: "MeasureID Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
api_name: 
  - "MeasureID Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "MeasureID"
helpviewer_keywords: 
  - "MeasureID element"
ms.assetid: 8457aebc-8fdd-4683-8640-baaf9d89b2a2
caps.latest.revision: 32
author: "Minewiskan"
ms.author: "owend"
manager: "mblythe"
---
# MeasureID Element (ASSL)
  Associates a [Measure](../../../2014/analysis-services/dev-guide/measure-element-assl.md) element with the parent element.  
  
## Syntax  
  
```xml  
  
<MeasureBinding> <!-- or AggregationInstanceMeasure, PerspectiveMeasure -->  
   ...  
   <MeasureID>...</MeasureID>  
   ...  
</MeasureBinding>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|String|  
|Default value|None|  
|Cardinality|1-1: Required element that occurs once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[AggregationInstanceMeasure](../../../2014/analysis-services/dev-guide/aggregationinstancemeasure-data-type-assl.md), [MeasureBinding](../../../2014/analysis-services/dev-guide/measurebinding-data-type-assl.md), [PerspectiveMeasure](../../../2014/analysis-services/dev-guide/perspectivemeasure-data-type-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The elements that correspond to the parents of `MeasureID` in the Analysis Management Objects (AMO) object model are <xref:Microsoft.AnalysisServices.AggregationInstanceMeasure>, <xref:Microsoft.AnalysisServices.MeasureBinding>, and <xref:Microsoft.AnalysisServices.PerspectiveMeasure>.  
  
## See Also  
 [Properties &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/properties-assl.md)  
  
  