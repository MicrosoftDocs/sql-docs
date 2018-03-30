---
title: "ColumnID Element (EventColumn) (ASSL) | Microsoft Docs"
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
  - "ColumnID Element (EventColumn)"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "ColumnID"
helpviewer_keywords: 
  - "ColumnID element"
ms.assetid: c4f4fbad-9d70-4de2-8cf7-caee80a4a1e4
caps.latest.revision: 37
author: "Minewiskan"
ms.author: "owend"
manager: "mblythe"
---
# ColumnID Element (EventColumn) (ASSL)
  Contains the identifier (ID) of the column of information to be captured for an event as part of a [Trace](../../../2014/analysis-services/dev-guide/trace-element-assl.md) element.  
  
## Syntax  
  
```xml  
  
<EventColumn>  
   <ColumnID>...</ColumnID>  
</EventColumn>  
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
|Parent element|[EventColumn](../../../2014/analysis-services/dev-guide/eventcolumn-data-type-assl.md)|  
|Child elements|None.|  
  
## Remarks  
 The element that corresponds to the parent of `ColumnID` in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.TraceColumn>.  
  
## See Also  
 [Columns Element &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/columns-element-assl.md)   
 [Event Element &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/event-element-assl.md)   
 [Events Element &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/events-element-assl.md)   
 [Properties &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/properties-assl.md)  
  
  