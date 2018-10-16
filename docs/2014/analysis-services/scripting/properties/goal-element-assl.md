---
title: "Goal Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "Goal Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "Goal"
helpviewer_keywords: 
  - "Goal element"
ms.assetid: 75fa5b57-418e-43ad-8704-764e4f0a40cf
author: minewiskan
ms.author: owend
manager: craigg
---
# Goal Element (ASSL)
  Identifies the desired goal in a [Kpi](../objects/kpi-element-assl.md) element.  
  
## Syntax  
  
```xml  
  
<Kpi>  
   ...  
   <Goal>...</Goal>  
   ...  
</Kpi>  
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
 The `Goal` element contains a Multidimensional Expressions (MDX) expression.  
  
 The element that corresponds to the parent of `Goal` in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.Kpi>.  
  
## See Also  
 [Status Element &#40;ASSL&#41;](status-element-assl.md)   
 [Trend Element &#40;ASSL&#41;](trend-element-assl.md)   
 [Value Element &#40;ASSL&#41;](value-element-assl.md)   
 [Properties &#40;ASSL&#41;](properties-assl.md)  
  
  
