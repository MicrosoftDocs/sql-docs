---
title: "EventID Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "analysis-services"
ms.prod_service: "analysis-services"
ms.service: ""
ms.component: ""
ms.reviewer: ""
ms.suite: "pro-bi"
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
apiname: 
  - "EventID Element"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "EventID"
helpviewer_keywords: 
  - "EventID element"
ms.assetid: a6b2ee50-1753-496c-af5c-206d63f2542b
caps.latest.revision: 37
author: "Minewiskan"
ms.author: "owend"
manager: "kfile"
ms.workload: "Inactive"
---
# EventID Element (ASSL)
[!INCLUDE[ssas-appliesto-sqlas](../../../includes/ssas-appliesto-sqlas.md)]
  Uniquely identifies an [Event](../../../analysis-services/scripting/objects/event-element-assl.md) element that is to be captured as part of a [Trace](../../../analysis-services/scripting/objects/trace-element-assl.md) element.  
  
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
|Parent elements|[Event](../../../analysis-services/scripting/objects/event-element-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The element corresponding to the parent of **EventID** in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.TraceEvent>.  
  
## See Also  
 [Events Element &#40;ASSL&#41;](../../../analysis-services/scripting/collections/events-element-assl.md)   
 [Properties &#40;ASSL&#41;](../../../analysis-services/scripting/properties/properties-assl.md)  
  
  
