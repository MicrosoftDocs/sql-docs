---
title: "EstimatedPerformanceGain Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "EstimatedPerformanceGain Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "EstimatedPerformanceGain"
helpviewer_keywords: 
  - "EstimatedPerformanceGain element"
ms.assetid: d7487977-73c3-4244-ad5d-3c357b219db4
author: minewiskan
ms.author: owend
manager: craigg
---
# EstimatedPerformanceGain Element (ASSL)
  Contains the read-only percentage of estimated performance gain for the partition.  
  
## Syntax  
  
```xml  
  
<AggregationDesign>  
      ...  
   <EstimatedPerformanceGain>...</EstimatedPerformanceGain>  
   ...  
</AggregationDesign>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|Integer|  
|Default value|None|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent element|[AggregationDesign](../objects/aggregationdesign-element-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The element corresponding to the parent of `EstimatedPerformanceGain` in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.AggregationDesign>.  
  
## See Also  
 [Properties &#40;ASSL&#41;](properties-assl.md)  
  
  
