---
title: "CurrentTimeMember Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "CurrentTimeMember Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "CurrentTimeMember"
helpviewer_keywords: 
  - "CurrentTimeMember element"
ms.assetid: 2e73009c-9f2b-441c-bdf0-ca19b160da4f
author: minewiskan
ms.author: owend
manager: craigg
---
# CurrentTimeMember Element (ASSL)
  Defines the current member of a time dimension associated with a [Kpi](../objects/kpi-element-assl.md) element.  
  
## Syntax  
  
```xml  
  
<Kpi>  
   ...  
   <CurrentTimeMember>...</CurrentTimeMember>  
   ...  
</AggregationDimension>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|String|  
|Default value|None|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent element|[Kpi](../objects/kpi-element-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The value of this element is a Multidimensional Expressions (MDX) statement that evaluates to a single member in a time dimension, used to retrieve the current timeframe when evaluating the key performance indicator (KPI).  
  
 The element that corresponds to the parent of `CurrentTimeMember` in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.Kpi>.  
  
## See Also  
 [Properties &#40;ASSL&#41;](properties-assl.md)  
  
  
