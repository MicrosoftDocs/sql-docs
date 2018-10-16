---
title: "SilenceOverrideInterval Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "SilenceOverrideInterval Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "SilenceOverrideInterval"
helpviewer_keywords: 
  - "SilenceOverrideInterval element"
ms.assetid: 0dcd2db4-9bc0-4460-b1dd-def0b38c4617
author: minewiskan
ms.author: owend
manager: craigg
---
# SilenceOverrideInterval Element (ASSL)
  Defines the amount of time that must elapse after receiving initial notification before multidimensional OLAP (MOLAP) imaging begins unconditionally.  
  
## Syntax  
  
```xml  
  
<ProactiveCaching>  
   ...  
   <SilenceOverrideInterval>...</SilenceOverrideInterval>  
   ...  
</ProactiveCaching>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|duration|  
|Default value|P0s|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent element|[ProactiveCaching](../objects/proactivecaching-element-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The value of `SilenceOverrideInterval` overrides the value of `SilenceInterval` if a notification is received during the silence period.  
  
 The element that corresponds to the parent of `SilenceOverrideInterval` in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.ProactiveCaching>.  
  
## See Also  
 [Properties &#40;ASSL&#41;](properties-assl.md)  
  
  
