---
title: "Event Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "Event Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "EVENT"
helpviewer_keywords: 
  - "Event element"
ms.assetid: c7911bcd-e601-4a96-a6d8-20b7c7375ff2
author: minewiskan
ms.author: owend
manager: craigg
---
# Event Element (ASSL)
  Defines an `Event` to be captured as part of a [Trace](trace-element-assl.md) element.  
  
## Syntax  
  
```xml  
  
<Events>  
   <Event>  
      <EventID>...</EventID>  
      <Columns>...</Columns>  
   </Event>  
</Events>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|None|  
|Default value|None|  
|Cardinality|1-n: Required element that can occur more than once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[Events](../collections/events-element-assl.md)|  
|Child elements|[Columns](../collections/columns-element-assl.md), [EventID](../properties/id-element-assl.md)|  
  
## Remarks  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.TraceEvent>.  
  
## See Also  
 [Trace Element &#40;ASSL&#41;](trace-element-assl.md)   
 [Objects &#40;ASSL&#41;](objects-assl.md)  
  
  
