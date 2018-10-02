---
title: "EventID Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "EventID Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "EventID"
helpviewer_keywords: 
  - "EventID element"
ms.assetid: a6b2ee50-1753-496c-af5c-206d63f2542b
author: minewiskan
ms.author: owend
manager: craigg
---
# EventID Element (ASSL)
  Uniquely identifies an [Event](../objects/event-element-assl.md) element that is to be captured as part of a [Trace](../objects/trace-element-assl.md) element.  
  
## Syntax  
  
```xml  
  
<Event>  
  
      <EventID>...</EventID>  
  
</Event>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|String|  
|Default value|None|  
|Cardinality|1-1: Required element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[Event](../objects/event-element-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The element corresponding to the parent of `EventID` in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.TraceEvent>.  
  
## See Also  
 [Events Element &#40;ASSL&#41;](../collections/events-element-assl.md)   
 [Properties &#40;ASSL&#41;](properties-assl.md)  
  
  
