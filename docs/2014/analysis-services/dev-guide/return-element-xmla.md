---
title: "return Element (XMLA) | Microsoft Docs"
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
  - "return Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "microsoft.xml.analysis.return"
  - "http://schemas.microsoft.com/analysisservices/2003/engine#return"
  - "urn:schemas-microsoft-com:xml-analysis#return"
helpviewer_keywords: 
  - "return element"
ms.assetid: 3cfe8b74-fec3-4987-a74a-5f731444e024
caps.latest.revision: 14
author: "mgblythe"
ms.author: "mblythe"
manager: "mblythe"
---
# return Element (XMLA)
  Contains information returned by a [DiscoverResponse](../../../2014/analysis-services/dev-guide/discoverresponse-element-xmla.md) element in response to a [Discover](../../../2014/analysis-services/dev-guide/discover-method-xmla.md) method call or an [ExecuteResponse](../../../2014/analysis-services/dev-guide/executeresponse-element-xmla.md) element in response to an [Execute](../../../2014/analysis-services/dev-guide/execute-method-xmla.md) method call.  
  
## Syntax  
  
```xml  
  
<DiscoverResponse> <!-- or ExecuteResponse -->  
   <return>  
      <root>...</root>  
      <!-- or -->  
      <results>...</results> <!-- ExecuteResponse only -->  
   </return>  
</DiscoverResponse>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|None|  
|Default value|None|  
|Cardinality|1-1: Required element that occurs once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[DiscoverResponse](../../../2014/analysis-services/dev-guide/discoverresponse-element-xmla.md), [ExecuteResponse](../../../2014/analysis-services/dev-guide/executeresponse-element-xmla.md)|  
|Ancestor:[DiscoverResponse](../../../2014/analysis-services/dev-guide/discoverresponse-element-xmla.md)|Child: <br />                        [root](../../../2014/analysis-services/dev-guide/root-element-xmla.md)|  
|Ancestor: <br />                        [ExecuteResponse](../../../2014/analysis-services/dev-guide/executeresponse-element-xmla.md)|Child: [root](../../../2014/analysis-services/dev-guide/root-element-xmla.md) or [results](../../../2014/analysis-services/dev-guide/results-element-xmla.md)|  
  
## Remarks  
 The `return` element contains the data returned by the `Discover` and `Execute` methods. Typically, the `return` element contains a single `root` element that contains either the data returned by a successful `Discover` or `Execute` method call or an XML for Analysis (XMLA) exception returned by an unsuccessful method call. If the `Execute` method contains a `Batch` command that performs multiple operations, the `return` element contains a `results` element which, in turn, contains one `root` element for each command executed successfully or unsuccessfully by the `Batch` command.  
  
## See Also  
 [Properties &#40;XMLA&#41;](../../../2014/analysis-services/dev-guide/properties-xmla.md)  
  
  