---
title: "CalendarStartDate Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "CalendarStartDate Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "CalendarStartDate"
helpviewer_keywords: 
  - "CalendarStartDate element"
ms.assetid: f6204107-9123-41f0-acbd-52134fe36e37
author: minewiskan
ms.author: owend
manager: craigg
---
# CalendarStartDate Element (ASSL)
  Defines the start date of the calendar period for the [TimeBinding](../data-type/binding-data-type-assl.md) element.  
  
## Syntax  
  
```xml  
  
<TimeBinding>  
   ...  
   <CalendarStartDate>...</CalendarStartDate>  
   ...  
</TimeBinding>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|DateTime|  
|Default value|None|  
|Cardinality|1-1: Required element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent element|[TimeBinding](../data-type/binding-data-type-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The `CalendarEndDate` must fall after the `CalendarStartDate`.  
  
 The element that corresponds to the parent of `CalendarStartDate` in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.TimeBinding>.  
  
## See Also  
 [Properties &#40;ASSL&#41;](properties-assl.md)  
  
  
