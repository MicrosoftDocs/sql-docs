---
title: "ReportingWeekToMonthPattern Element (ASSL) | Microsoft Docs"
ms.date: 5/8/2018
ms.prod: sql
ms.custom: assl
ms.reviewer: owend
ms.technology: analysis-services
ms.topic: reference
author: minewiskan
ms.author: owend
manager: kfile
---
# ReportingWeekToMonthPattern Element (ASSL)
[!INCLUDE[ssas-appliesto-sqlas](../../../includes/ssas-appliesto-sqlas.md)]
  Defines the reporting week-to-month pattern for the [TimeBinding](../../../analysis-services/scripting/data-type/timebinding-data-type-assl.md) element.  
  
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
|Parent element|[TimeBinding](../../../analysis-services/scripting/data-type/timebinding-data-type-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The value of this element is limited to one of the strings listed in the following table.  
  
|Value|Description|  
|-----------|-----------------|  
|*445*|4 weeks in the first month of the quarter, 4 weeks in the second month, and 5 weeks in the third month.|  
|*454*|4 weeks in the first month of the quarter, 5 weeks in the second month, and 4 weeks in the third month.|  
|*544*|5 weeks in the first month of the quarter, 4 weeks in the second month, and 4 weeks in the third month.|  
  
 The enumeration that corresponds to the allowed values for **ReportingWeekToMonthPattern** in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.ReportingWeekToMonthPattern>.  
  
## See Also  
 [Properties &#40;ASSL&#41;](../../../analysis-services/scripting/properties/properties-assl.md)  
  
  
