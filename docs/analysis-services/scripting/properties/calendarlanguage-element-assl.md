---
title: "CalendarLanguage Element (ASSL) | Microsoft Docs"
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
  - "CalendarLanguage Element"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "CalendarLanguage"
helpviewer_keywords: 
  - "CalendarLanguage element"
ms.assetid: e43a0f48-a583-418b-a0a4-d73a40035573
caps.latest.revision: 31
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# CalendarLanguage Element (ASSL)
  Defines the calendar language used for the [TimeBinding](../../../analysis-services/scripting/data-type/timebinding-data-type-assl.md) element.  
  
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
|Default value|**1033**|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent element|[TimeBinding](../../../analysis-services/scripting/data-type/timebinding-data-type-assl.md)|  
|Child elements|None|  
  
## Remarks  
 This is the language in which dimension member names are created. The language of captions should be defined using integer-based LCID codes. For example, the default value represents the English-US LCID.  
  
 The element that corresponds to the parent of **CalendarLanguage** in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.TimeBinding>.  
  
## See Also  
 [Properties &#40;ASSL&#41;](../../../analysis-services/scripting/properties/properties-assl.md)  
  
  