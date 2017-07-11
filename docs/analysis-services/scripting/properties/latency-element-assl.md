---
title: "Latency Element (ASSL) | Microsoft Docs"
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
  - "Latency Element"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "Latency"
helpviewer_keywords: 
  - "Latency element"
ms.assetid: 93940637-b83e-4773-b80d-3394ca3a1ce5
caps.latest.revision: 35
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# Latency Element (ASSL)
  Defines the "grace period" between the earliest notification and the moment when the multidimensional OLAP (MOLAP) images are destroyed.  
  
## Syntax  
  
```xml  
  
<ProactiveCaching>  
   ...  
   <Latency>...</Latency>  
   ...  
</ProactiveCaching element>  
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
|Parent element|[ProactiveCaching](../../../analysis-services/scripting/objects/proactivecaching-element-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The element that corresponds to the parent of **Latency** in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.ProactiveCaching>.  
  
## See Also  
 [Properties &#40;ASSL&#41;](../../../analysis-services/scripting/properties/properties-assl.md)  
  
  