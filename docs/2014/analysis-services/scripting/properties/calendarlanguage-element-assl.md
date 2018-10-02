---
title: "CalendarLanguage Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "CalendarLanguage Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "CalendarLanguage"
helpviewer_keywords: 
  - "CalendarLanguage element"
ms.assetid: e43a0f48-a583-418b-a0a4-d73a40035573
author: minewiskan
ms.author: owend
manager: craigg
---
# CalendarLanguage Element (ASSL)
  Defines the calendar language used for the [TimeBinding](../data-type/binding-data-type-assl.md) element.  
  
## Syntax  
  
```xml  
  
<TimeBinding>  
   ...  
   <CalendarLanguage>...</CalendarLanguage>  
   ...  
</TimeBinding>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|Integer|  
|Default value|`1033`|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent element|[TimeBinding](../data-type/binding-data-type-assl.md)|  
|Child elements|None|  
  
## Remarks  
 This is the language in which dimension member names are created. The language of captions should be defined using integer-based LCID codes. For example, the default value represents the English-US LCID.  
  
 The element that corresponds to the parent of `CalendarLanguage` in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.TimeBinding>.  
  
## See Also  
 [Properties &#40;ASSL&#41;](properties-assl.md)  
  
  
