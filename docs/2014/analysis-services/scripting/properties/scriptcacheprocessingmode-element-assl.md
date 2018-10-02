---
title: "ScriptCacheProcessingMode Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "ScriptCacheProcessingMode Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "ScriptCacheProcessingMode"
helpviewer_keywords: 
  - "ScriptCacheProcessingMode element"
ms.assetid: 95c0723c-69a4-43e7-b743-f712180a7681
author: minewiskan
ms.author: owend
manager: craigg
---
# ScriptCacheProcessingMode Element (ASSL)
  Indicates whether the server should build the script cache during processing or after processing.  
  
## Syntax  
  
```xml  
  
<Cube>  
   ...  
      <ScriptCacheProcessingMode>...</ScriptCacheProcessingMode>  
   ...  
</Cube>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|String (enumeration)|  
|Default value|*Regular*|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent element|[Cube](../objects/cube-element-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The value of this element is limited to one of the strings in the following table.  
  
|Value|Description|  
|-----------|-----------------|  
|*Regular*|The server builds the script cache during processing.|  
|*Lazy*|The server builds the script cache after processing.|  
  
 The enumeration that corresponds to the allowed values for `ScriptCacheProcessingMode` in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.ScriptCacheProcessingMode>.  
  
 The element that corresponds to the parent of `ScriptCacheProcessingMode` in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.Cube>.  
  
## See Also  
 [Properties &#40;ASSL&#41;](properties-assl.md)  
  
  
