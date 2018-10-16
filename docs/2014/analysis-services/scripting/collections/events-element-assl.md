---
title: "Events Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "Events Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "Events"
helpviewer_keywords: 
  - "Events element"
ms.assetid: de887998-dc4b-44dc-8fec-08d67b92f96d
author: minewiskan
ms.author: owend
manager: craigg
---
# Events Element (ASSL)
  Defines the collection of [Event](../objects/event-element-assl.md) elements to be captured by a [Trace](../objects/trace-element-assl.md).  
  
## Syntax  
  
```xml  
  
<Trace>  
   ...  
   <Events>  
      <Event>...</Event>  
   </Events>  
   ...  
</Trace>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|None|  
|Default value|None|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent element|[Trace](../objects/trace-element-assl.md)|  
|Child elements|[Event](../objects/event-element-assl.md)|  
  
## Remarks  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.TraceEventCollection>.  
  
## See Also  
 [Traces Element &#40;ASSL&#41;](traces-element-assl.md)   
 [Collections &#40;ASSL&#41;](collections-assl.md)  
  
  
