---
title: "LogFileAppend Element (ASSL) | Microsoft Docs"
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
  - "LogFileAppend Element"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "LogFileAppend"
helpviewer_keywords: 
  - "LogFileAppend element"
ms.assetid: f85e94a9-e5c5-478a-a5a0-fc99ed19b582
caps.latest.revision: 32
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# LogFileAppend Element (ASSL)
  Determines whether the [Trace](../../../analysis-services/scripting/objects/trace-element-assl.md) element appends its logging output to the existing log file, or overwrites it.  
  
## Syntax  
  
```xml  
  
<Trace>  
  
   <LogFileAppend>...</LogFileAppend>  
  
</Trace>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|Boolean|  
|Default value|False|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent element|[Trace](../../../analysis-services/scripting/objects/trace-element-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The element that corresponds to the parent of **LogFileAppend** in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.Trace>.  
  
## See Also  
 [Traces Element &#40;ASSL&#41;](../../../analysis-services/scripting/collections/traces-element-assl.md)   
 [Properties &#40;ASSL&#41;](../../../analysis-services/scripting/properties/properties-assl.md)  
  
  