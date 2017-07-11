---
title: "FiscalYearName Element (ASSL) | Microsoft Docs"
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
  - "FiscalYearName Element"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "FiscalYearName"
helpviewer_keywords: 
  - "FiscalYearName element"
ms.assetid: ce613a21-6890-4796-aac5-b029eca46255
caps.latest.revision: 30
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# FiscalYearName Element (ASSL)
  Defines the naming convention for the name of the fiscal year for a [TimeBinding](../../../analysis-services/scripting/data-type/timebinding-data-type-assl.md) element.  
  
## Syntax  
  
```xml  
  
<TimeBinding>  
   ...  
   <FiscalYearName>...</FiscalYearName>  
   ...  
</TimeBinding>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|String (enumeration)|  
|Default value|*NextCalendarYearName*|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent element|[TimeBinding](../../../analysis-services/scripting/data-type/timebinding-data-type-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The value of this element is limited to one of the strings in the following table.  
  
|Value|Description|  
|-----------|-----------------|  
|*CalendarYearName*|Assigns the fiscal year the name of the current calendar year.|  
|*NextCalendarYearName*|Assigns the fiscal year the name of the next calendar year.|  
  
 The enumeration that corresponds to the allowed values for **FiscalYearName** in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.FiscalYearName>.  
  
## See Also  
 [Properties &#40;ASSL&#41;](../../../analysis-services/scripting/properties/properties-assl.md)  
  
  