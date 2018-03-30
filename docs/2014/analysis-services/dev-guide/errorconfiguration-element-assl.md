---
title: "ErrorConfiguration Element (ASSL) | Microsoft Docs"
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
  - "ErrorConfiguration Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "ErrorConfiguration"
helpviewer_keywords: 
  - "ErrorConfiguration element"
ms.assetid: e8363ec2-fbbf-48f6-a55d-01793afa759c
caps.latest.revision: 34
author: "Minewiskan"
ms.author: "owend"
manager: "mblythe"
---
# ErrorConfiguration Element (ASSL)
  Specifies settings for handling errors that can occur when the parent element is processed.  
  
## Syntax  
  
```xml  
  
<Cube> <!-- or Dimension, MeasureGroup, MiningStructure, Partition -->  
   ...  
   <ErrorConfiguration>  
      <KeyErrorLimit>...</KeyErrorLimit>  
      <KeyErrorLogFile>...</KeyErrorLogFile>  
      <KeyErrorAction>...</KeyErrorAction>  
      <KeyErrorLimitAction>...</KeyErrorLimitAction>  
      <KeyNotFound>...</KeyNotFound>  
      <KeyDuplicate>...</KeyDuplicate>  
      <NullKeyConvertedToUnknown>...</NullKeyConvertedToUnknown>  
      <NullKeyNotAllowed>...<NullKeyNotAllowed>  
   </ErrorConfiguration>  
   ...  
</Cube >  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|None|  
|Default value|None|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[Cube](../../../2014/analysis-services/dev-guide/cube-element-assl.md), [Dimension](../../../2014/analysis-services/dev-guide/dimension-element-assl.md), [MeasureGroup](../../../2014/analysis-services/dev-guide/measuregroup-element-assl.md), [MiningStructure](../../../2014/analysis-services/dev-guide/miningstructure-element-assl.md), [Partition](../../../2014/analysis-services/dev-guide/partition-element-assl.md)|  
|Child elements|[KeyDuplicate](../../../2014/analysis-services/dev-guide/keyduplicate-element-assl.md), [KeyErrorAction](../../../2014/analysis-services/dev-guide/keyerroraction-element-assl.md), [KeyErrorLimit](../../../2014/analysis-services/dev-guide/keyerrorlimit-element-assl.md), [KeyErrorLimitAction](../../../2014/analysis-services/dev-guide/keyerrorlimitaction-element-assl.md), [KeyErrorLogFile](../../../2014/analysis-services/dev-guide/keyerrorlogfile-element-assl.md), [KeyNotFound](../../../2014/analysis-services/dev-guide/keynotfound-element-assl.md), [NullKeyConvertedToUnknown](../../../2014/analysis-services/dev-guide/nullkeyconvertedtounknown-element-assl.md), [NullKeyNotAllowed](../../../2014/analysis-services/dev-guide/nullkeynotallowed-element-assl.md)|  
  
## Remarks  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.ErrorConfiguration>.  
  
## See Also  
 [Objects &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/objects-assl.md)  
  
  