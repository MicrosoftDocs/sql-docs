---
title: "KeyErrorLimitAction Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "KeyErrorLimitAction Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "KeyErrorLimitAction"
helpviewer_keywords: 
  - "KeyErrorLimitAction element"
ms.assetid: a2a01aae-0571-499f-9025-b61c741f3ddb
author: minewiskan
ms.author: owend
manager: craigg
---
# KeyErrorLimitAction Element (ASSL)
  Specifies the action [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] takes when the key error count that is specified in the [KeyErrorLimit](keyerrorlimit-element-assl.md) element is reached.  
  
## Syntax  
  
```xml  
  
<ErrorConfiguration>  
   ...  
      <KeyErrorLimitAction>...</KeyErrorLimitAction>  
   ...  
</ErrorConfiguration>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|String (enumeration)|  
|Default value|*StopProcessing*|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent element|[ErrorConfiguration](../objects/errorconfiguration-element-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The value of this element is limited to one of the strings in the following table.  
  
|Value|Description|  
|-----------|-----------------|  
|*StopProcessing*|Stops processing the object.|  
|*StopLogging*|Continues processing the object, but stops logging errors that are encountered during processing.|  
  
 The enumeration that corresponds to the allowed values for `KeyErrorLimitAction` in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.KeyErrorLimitAction>.  
  
## See Also  
 [Properties &#40;ASSL&#41;](properties-assl.md)  
  
  
