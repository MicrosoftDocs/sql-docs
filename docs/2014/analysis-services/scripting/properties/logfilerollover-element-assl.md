---
title: "LogFileRollover Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "LogFileRollover Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "LogFileRollover"
helpviewer_keywords: 
  - "LogFileRollover element"
ms.assetid: 5484e167-b891-431a-bbae-946ea6eb4a3c
author: minewiskan
ms.author: owend
manager: craigg
---
# LogFileRollover Element (ASSL)
  Specifies whether logging of [Trace](../objects/trace-element-assl.md) output should roll over to a new file or should stop when the maximum log file size that is specified in [LogFileSize](logfilesize-element-assl.md) is reached.  
  
## Syntax  
  
```xml  
  
<Trace>  
   ...  
   <LogFileRollover>...</LogFileRollover>  
   ...  
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
|Parent element|[Trace](../objects/trace-element-assl.md)|  
|Child elements|None|  
  
## Remarks  
 If the value of the `LogFileRollover` element is set to True, a new file is started when the size of the log file exceeds the value that is specified in the `LogFileSize` element of the `Trace` parent element; otherwise, logging stops.  
  
 The element that corresponds to the parent of `LogFileRollover` in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.Trace>.  
  
## See Also  
 [Traces Element &#40;ASSL&#41;](../collections/traces-element-assl.md)   
 [Properties &#40;ASSL&#41;](properties-assl.md)  
  
  
