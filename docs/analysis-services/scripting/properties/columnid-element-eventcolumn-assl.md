---
title: "ColumnID Element (EventColumn) (ASSL) | Microsoft Docs"
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
  - "ColumnID Element (EventColumn)"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "ColumnID"
helpviewer_keywords: 
  - "ColumnID element"
ms.assetid: c4f4fbad-9d70-4de2-8cf7-caee80a4a1e4
caps.latest.revision: 37
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# ColumnID Element (EventColumn) (ASSL)
  Contains the identifier (ID) of the column of information to be captured for an event as part of a [Trace](../../../analysis-services/scripting/objects/trace-element-assl.md) element.  
  
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
|Parent element|[EventColumn](../../../analysis-services/scripting/data-type/eventcolumn-data-type-assl.md)|  
|Child elements|None.|  
  
## Remarks  
 The element that corresponds to the parent of **ColumnID** in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.TraceColumn>.  
  
## See Also  
 [Columns Element &#40;ASSL&#41;](../../../analysis-services/scripting/collections/columns-element-assl.md)   
 [Event Element &#40;ASSL&#41;](../../../analysis-services/scripting/objects/event-element-assl.md)   
 [Events Element &#40;ASSL&#41;](../../../analysis-services/scripting/collections/events-element-assl.md)   
 [Properties &#40;ASSL&#41;](../../../analysis-services/scripting/properties/properties-assl.md)  
  
  