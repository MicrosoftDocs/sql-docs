---
title: "Trace Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
api_name: 
  - "Trace Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "Trace"
helpviewer_keywords: 
  - "Trace element"
ms.assetid: dda9136a-a9c1-44a1-b8d3-b0ec4dc65c87
caps.latest.revision: 35
author: "Minewiskan"
ms.author: "owend"
manager: "mblythe"
---
# Trace Element (ASSL)
  Defines a trace that can be queried.  
  
## Syntax  
  
```  
  
      Profiler syntax  
<Traces>  
   <Trace>  
      <Name>...</Name>  
      <ID>...</ID>  
      <Description>...</Description>  
      <CreatedTimestamp>...</CreatedTimestamp>  
      <LastSchemaUpdate>...</LastSchemaUpdate>  
      <LogFileName>...</LogFileName>  
      <LogFileAppend>...</LogFileAppend>  
      <LogFileSize>...</LogFileSize>  
      <Audit>...</Audit>  
      <LogFileRollover>...</LogFileRollover>  
      <AutoRestart>...</AutoRestart>  
      <StopTime>...</StopTime>  
      <Filter>...</Filter>  
      <Events>...</Events>  
      <Annotations>...</Annotations>  
   </Trace>  
</Traces>  
```  
  
```  
  
      Extended Events syntax  
<Traces>  
   <Trace>  
      <Name>...</Name>  
      <ID>...</ID>  
      <Description>...</Description>  
      <CreatedTimestamp>...</CreatedTimestamp>  
      <LastSchemaUpdate>...</LastSchemaUpdate>  
      <XEvent>...</XEvent>  
      <Annotations>...</Annotations>  
   </Trace>  
</Traces>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|None|  
|Default value|None|  
|Cardinality|0-n: Optional element that can occur more than once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[Traces](../../../2014/analysis-services/dev-guide/traces-element-assl.md)|  
|Child elements|[Annotations](../../../2014/analysis-services/dev-guide/annotations-element-assl.md), [Audit](../../../2014/analysis-services/dev-guide/audit-element-assl.md), [AutoRestart](../../../2014/analysis-services/dev-guide/autorestart-element-assl.md), [CreatedTimestamp](../../../2014/analysis-services/dev-guide/createdtimestamp-element-assl.md), [Description](../../../2014/analysis-services/dev-guide/description-element-assl.md), [Events](../../../2014/analysis-services/dev-guide/events-element-assl.md), [Filter](../../../2014/analysis-services/dev-guide/filter-element-trace-assl.md), [ID](../../../2014/analysis-services/dev-guide/id-element-assl.md), [LastSchemaUpdate](../../../2014/analysis-services/dev-guide/lastschemaupdate-element-assl.md), [LogFileAppend](../../../2014/analysis-services/dev-guide/logfileappend-element-assl.md), [LogFileName](../../../2014/analysis-services/dev-guide/logfilename-element-assl.md), [LogFileRollover](../../../2014/analysis-services/dev-guide/logfilerollover-element-assl.md), [LogFileSize](../../../2014/analysis-services/dev-guide/logfilesize-element-assl.md), [Name](../../../2014/analysis-services/dev-guide/name-element-assl.md), [StopTime](../../../2014/analysis-services/dev-guide/stoptime-element-assl.md)|  
  
## Remarks  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.Trace>.  
  
## See Also  
 [Server Element &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/server-element-assl.md)   
 [Objects &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/objects-assl.md)  
  
  