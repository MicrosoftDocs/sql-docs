---
title: "ReportingWeekToMonthPattern Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "04/27/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "ReportingWeekToMonthPattern Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "ReportingWeekToMonthPattern"
helpviewer_keywords: 
  - "ReportingWeekToMonthPattern element"
ms.assetid: 8d7c694d-d5c5-4faa-af78-155779e84fe9
author: minewiskan
ms.author: owend
manager: craigg
---
# ReportingWeekToMonthPattern Element (ASSL)
  Defines the reporting week-to-month pattern for the [TimeBinding](../data-type/binding-data-type-assl.md) element.  
  
## Syntax  
  
```xml  
  
<TimeBinding>  
   ...  
   <ReportingWeekToMonthPattern>...</ReportingWeekToMonthPattern>  
   ...  
</TimeBinding>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|String (enumeration)|  
|Default value|*445*|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent element|[TimeBinding](../data-type/binding-data-type-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The value of this element is limited to one of the strings listed in the following table.  
  
|Value|Description|  
|-----------|-----------------|  
|*445*|4 weeks in the first month of the quarter, 4 weeks in the second month, and 5 weeks in the third month.|  
|*454*|4 weeks in the first month of the quarter, 5 weeks in the second month, and 4 weeks in the third month.|  
|*544*|5 weeks in the first month of the quarter, 4 weeks in the second month, and 4 weeks in the third month.|  
  
 The enumeration that corresponds to the allowed values for `ReportingWeekToMonthPattern` in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.ReportingWeekToMonthPattern>.  
  
## See Also  
 [Properties &#40;ASSL&#41;](properties-assl.md)  
  
  
