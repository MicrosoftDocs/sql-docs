---
title: "AggregationInstanceMeasure Data Type (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
apiname: 
  - "AggregationInstanceMeasure Data Type"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
helpviewer_keywords: 
  - "AggregationInstanceMeasure data type"
ms.assetid: 3250970a-a67d-486c-b205-038f1bd1770f
caps.latest.revision: 11
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
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
|Child elements|[MeasureID](../../../analysis-services/scripting/properties/measureid-element-assl.md), [Source](../../../analysis-services/scripting/properties/source-element-binding-assl.md)|  
|Derived elements|[Measure](../../../analysis-services/scripting/objects/measure-element-assl.md)|  
  
## Remarks  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.AggregationInstanceMeasure>.  
  
## See Also  
 [Analysis Services Scripting Language XML Data Types &#40;ASSL&#41;](../../../analysis-services/scripting/data-type/analysis-services-scripting-language-xml-data-types-assl.md)  
  
  