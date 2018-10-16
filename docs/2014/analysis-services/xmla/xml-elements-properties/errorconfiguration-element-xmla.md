---
title: "ErrorConfiguration Element (XMLA) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
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
author: minewiskan
ms.author: owend
manager: craigg
---
# ErrorConfiguration Element (XMLA)
  Specifies settings for handling errors that can occur during a [Batch](../xml-elements-commands/batch-element-xmla.md) or [Process](../xml-elements-commands/process-element-xmla.md) operation.  
  
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
|Parent elements|[Batch](../xml-elements-commands/batch-element-xmla.md), [Process](../xml-elements-commands/process-element-xmla.md)|  
|Child elements|[KeyDuplicate](../../scripting/properties/keyduplicate-element-assl.md), [KeyErrorAction](../../scripting/objects/action-element-assl.md), [KeyErrorLimit](../../scripting/properties/keyerrorlimit-element-assl.md), [KeyErrorLimitAction](../../scripting/properties/keyerrorlimitaction-element-assl.md), [KeyErrorLogFile](../../scripting/objects/file-element-assl.md), [KeyNotFound](../../scripting/properties/keynotfound-element-assl.md), [NullKeyConvertedToUnknown](../../scripting/properties/nullkeyconvertedtounknown-element-assl.md), [NullKeyNotAllowed](../../scripting/properties/nullkeynotallowed-element-assl.md)|  
  
## Remarks  
 The structure of this element is identical to the structure of the `ErrorConfiguration` element in Analysis Services Scripting Language (ASSL). For more information about the `ErrorConfiguration` element, see [ErrorConfiguration Element &#40;ASSL&#41;](../../scripting/objects/errorconfiguration-element-assl.md).  
  
## See Also  
 [Properties &#40;XMLA&#41;](xml-elements-properties.md)  
  
  
