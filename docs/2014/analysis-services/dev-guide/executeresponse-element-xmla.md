---
title: "ExecuteResponse Element (XMLA) | Microsoft Docs"
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
  - "ExecuteResponse Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "urn:schemas-microsoft-com:xml-analysis#ExecuteResponse"
  - "http://schemas.microsoft.com/analysisservices/2003/engine#ExecuteResponse"
  - "microsoft.xml.analysis.executeresponse"
helpviewer_keywords: 
  - "ExecuteResponse element"
ms.assetid: 6edb1b82-da4c-4cd9-9385-bea04032f0eb
caps.latest.revision: 13
author: "mgblythe"
ms.author: "mblythe"
manager: "mblythe"
---
# ExecuteResponse Element (XMLA)
  Contains the information returned by an instance of [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] in response to an [Execute](../../../2014/analysis-services/dev-guide/execute-method-xmla.md) method call.  
  
 **Namespace** urn:schemas-microsoft-com:xml-analysis  
  
## Syntax  
  
```xml  
  
<ExecuteResponse>  
   <return>  
</ExecuteResponse>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|None|  
|Default value|None|  
|Cardinality|1-1: Required element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|None|  
|Child elements|[return](../../../2014/analysis-services/dev-guide/return-element-xmla.md)|  
  
## Remarks  
 The `ExecuteResponse` element is the topmost element within the body of a SOAP response for the `Execute` method.  
  
## See Also  
 [DiscoverResponse Element &#40;XMLA&#41;](../../../2014/analysis-services/dev-guide/discoverresponse-element-xmla.md)   
 [Objects &#40;XMLA&#41;](../../../2014/analysis-services/dev-guide/objects-xmla.md)  
  
  