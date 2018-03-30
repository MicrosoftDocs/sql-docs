---
title: "ErrorConfiguration Element (XMLA) | Microsoft Docs"
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
  - "http://schemas.microsoft.com/analysisservices/2003/engine#ErrorConfiguration"
  - "urn:schemas-microsoft-com:xml-analysis#ErrorConfiguration"
  - "microsoft.xml.analysis.errorconfiguration"
helpviewer_keywords: 
  - "ErrorConfiguration element"
ms.assetid: 5e350f5f-3a14-49b4-80c0-208c61f336d5
caps.latest.revision: 13
author: "mgblythe"
ms.author: "mblythe"
manager: "mblythe"
---
# ErrorConfiguration Element (XMLA)
  Specifies settings for handling errors that can occur during a [Batch](../../../2014/analysis-services/dev-guide/batch-element-xmla.md) or [Process](../../../2014/analysis-services/dev-guide/process-element-xmla.md) operation.  
  
## Syntax  
  
```xml  
  
<Batch> <!-- or Process -->  
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
</Batch>  
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
|Parent elements|[Batch](../../../2014/analysis-services/dev-guide/batch-element-xmla.md), [Process](../../../2014/analysis-services/dev-guide/process-element-xmla.md)|  
|Child elements|[KeyDuplicate](../../../2014/analysis-services/dev-guide/keyduplicate-element-assl.md), [KeyErrorAction](../../../2014/analysis-services/dev-guide/keyerroraction-element-assl.md), [KeyErrorLimit](../../../2014/analysis-services/dev-guide/keyerrorlimit-element-assl.md), [KeyErrorLimitAction](../../../2014/analysis-services/dev-guide/keyerrorlimitaction-element-assl.md), [KeyErrorLogFile](../../../2014/analysis-services/dev-guide/keyerrorlogfile-element-assl.md), [KeyNotFound](../../../2014/analysis-services/dev-guide/keynotfound-element-assl.md), [NullKeyConvertedToUnknown](../../../2014/analysis-services/dev-guide/nullkeyconvertedtounknown-element-assl.md), [NullKeyNotAllowed](../../../2014/analysis-services/dev-guide/nullkeynotallowed-element-assl.md)|  
  
## Remarks  
 The structure of this element is identical to the structure of the `ErrorConfiguration` element in Analysis Services Scripting Language (ASSL). For more information about the `ErrorConfiguration` element, see [ErrorConfiguration Element &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/errorconfiguration-element-assl.md).  
  
## See Also  
 [Properties &#40;XMLA&#41;](../../../2014/analysis-services/dev-guide/properties-xmla.md)  
  
  