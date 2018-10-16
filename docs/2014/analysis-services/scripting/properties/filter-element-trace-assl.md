---
title: "Filter Element (Trace) (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "Filter Element (Trace)"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "filter"
helpviewer_keywords: 
  - "Filter element"
ms.assetid: 411a598e-3bb1-487b-9f37-cce4b57a67b4
author: minewiskan
ms.author: owend
manager: craigg
---
# Filter Element (Trace) (ASSL)
  Contains an XML document fragment that describes the [Trace](../objects/trace-element-assl.md) filter.  
  
## Syntax  
  
```xml  
  
<Trace>  
   ...  
   <Filter>...</Filter>  
   ...  
</Trace>  
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
|Parent element|[Trace](../objects/trace-element-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The element that corresponds to the parent of `Filter` in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.Trace>.  
  
## See Also  
 [Properties &#40;ASSL&#41;](properties-assl.md)   
 [Traces Element &#40;ASSL&#41;](../collections/traces-element-assl.md)  
  
  
