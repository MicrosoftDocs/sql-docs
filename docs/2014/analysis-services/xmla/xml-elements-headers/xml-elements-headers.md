---
title: "Headers (XMLA) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
topic_type: 
  - "apiref"
  - "apinav"
helpviewer_keywords: 
  - "XMLA, headers"
  - "SOAP headers [XML for Analysis]"
  - "XML for Analysis, headers"
  - "headers [XML for Analysis]"
ms.assetid: 36407b5c-d98d-47e4-8d36-d8896e15a748
author: minewiskan
ms.author: owend
manager: craigg
---
# Headers (XMLA)
  The XML for Analysis (XMLA) protocol uses XML elements within the SOAP header to manage protocol-level features, such as session support and the negotiation of supported features.  
  
## In This Section  
 The following topics describe the XMLA header elements implemented by [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)].  
  
|Method|Description|  
|------------|-----------------|  
|[BeginSession Element &#40;XMLA&#41;](session-element-xmla.md)|Uses a SOAP header in a SOAP request message to start a new session on an instance of [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)].|  
|[EndSession Element &#40;XMLA&#41;](endsession-element-xmla.md)|Uses the SOAP header in a SOAP request message to end an existing session on an instance of [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)].|  
|[Session Element &#40;XMLA&#41;](session-element-xmla.md)|Uses the SOAP header in a SOAP request message to identify an existing, explicit session on an instance of [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)].|  
|[ProtocolCapabilities Element &#40;XMLA&#41;](protocolcapabilities-element-xmla.md)|Uses the SOAP header in a SOAP request message to identify protocol capabilities between an instance of [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] and a client application.|  
  
## See Also  
 [XML Elements &#40;XMLA&#41;](../../dev-guide/xml-elements-xmla.md)   
 [XML Data Types &#40;XMLA&#41;](../xml-data-types/xml-data-types-xmla.md)   
 [XML Elements &#40;XMLA&#41;](../../dev-guide/xml-elements-xmla.md)  
  
  
