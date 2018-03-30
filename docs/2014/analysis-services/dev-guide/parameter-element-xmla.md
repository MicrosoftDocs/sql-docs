---
title: "Parameter Element (XMLA) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
api_name: 
  - "Parameter Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "urn:schemas-microsoft-com:xml-analysis#Parameter"
  - "microsoft.xml.analysis.parameter"
  - "http://schemas.microsoft.com/analysisservices/2003/engine#Parameter"
helpviewer_keywords: 
  - "Parameter element"
ms.assetid: fe31ac3d-a3e8-4f60-a81a-c43271ddbed4
caps.latest.revision: 11
author: "mgblythe"
ms.author: "mblythe"
manager: "mblythe"
---
# Parameter Element (XMLA)
  Contains the name and value of a parameter used by the [Execute](../../../2014/analysis-services/dev-guide/execute-method-xmla.md) method.  
  
## Syntax  
  
```  
  
<Parameters>  
...  
   <Parameter>  
      <Name>...</Name>  
      <Value>...</Value>  
   </Parameter>  
...  
</Parameters>  
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
|Parent elements|[Parameters](../../../2014/analysis-services/dev-guide/parameters-element-xmla.md)|  
|Child elements|[Name](../../../2014/analysis-services/dev-guide/name-element-parameter-xmla.md), [Value](../../../2014/analysis-services/dev-guide/value-element-parameter-xmla.md)|  
  
## Remarks  
 Some XML for Analysis (XMLA) commands, such as the [Process](../../../2014/analysis-services/dev-guide/process-element-xmla.md) command, can require additional information. The `Parameter` element provides a mechanism for providing additional information, including chunked information, for an XMLA command.  
  
## See Also  
 [Properties &#40;XMLA&#41;](../../../2014/analysis-services/dev-guide/properties-xmla.md)  
  
  