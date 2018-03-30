---
title: "AggregationInstanceMeasure Data Type (ASSL) | Microsoft Docs"
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
  - "AggregationInstanceMeasure Data Type"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
helpviewer_keywords: 
  - "AggregationInstanceMeasure data type"
ms.assetid: 3250970a-a67d-486c-b205-038f1bd1770f
caps.latest.revision: 10
author: "Minewiskan"
ms.author: "owend"
manager: "mblythe"
---
# AggregationInstanceMeasure Data Type (ASSL)
  Defines a primitive data type that represents information about a measure used by an aggregation instance.  
  
## Syntax  
  
```xml  
  
<AggregationInstanceMeasure>  
   <MeasureID>...</MeasureID>  
   <Source>...</Source>  
</AggregationInstanceMeasure>  
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
|Child elements|[MeasureID](../../../2014/analysis-services/dev-guide/measureid-element-assl.md), [Source](../../../2014/analysis-services/dev-guide/source-element-binding-assl.md)|  
|Derived elements|[Measure](../../../2014/analysis-services/dev-guide/measure-element-assl.md)|  
  
## Remarks  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.AggregationInstanceMeasure>.  
  
## See Also  
 [Analysis Services Scripting Language XML Data Types &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/analysis-services-scripting-language-xml-data-types-assl.md)  
  
  