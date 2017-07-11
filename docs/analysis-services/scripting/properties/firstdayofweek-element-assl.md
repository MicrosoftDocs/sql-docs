---
title: "FirstDayOfWeek Element (ASSL) | Microsoft Docs"
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
  - "FirstDayOfWeek Element"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "FirstDayOfWeek"
helpviewer_keywords: 
  - "FirstDayOfWeek element"
ms.assetid: d3c92fa3-b293-43b5-806e-cd1c146a3a7c
caps.latest.revision: 28
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# FirstDayOfWeek Element (ASSL)
  Defines the first day of the week for a [TimeBinding](../../../analysis-services/scripting/data-type/timebinding-data-type-assl.md) element.  
  
## Syntax  
  
```xml  
  
<TimeBinding>  
   ...  
   <FirstDayOfWeek>...</FirstDayOfWeek>  
   ...  
</TimeBinding>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|Integer (between 1 and 7, where 1 = Sunday and 7 = Saturday)|  
|Default value|**1**|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent element|[TimeBinding](../../../analysis-services/scripting/data-type/timebinding-data-type-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The element that corresponds to the parent of **FirstDayOfWeek** in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.TimeBinding>.  
  
## See Also  
 [Properties &#40;ASSL&#41;](../../../analysis-services/scripting/properties/properties-assl.md)  
  
  