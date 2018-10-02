---
title: "Objects (XMLA) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
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
  - "objects [XML for Analysis]"
  - "XML for Analysis, objects"
  - "XMLA, objects"
ms.assetid: 768188ef-85d4-432a-9390-be05c835137f
author: minewiskan
ms.author: owend
manager: craigg
---
# Objects (XMLA)
  The XML for Analysis (XMLA) protocol uses two methods, `Discover` and `Execute`, to offer a standard way for applications to access information on an instance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)]. Because these methods are invoked by using the Simple Object Access Protocol (SOAP) protocol, they accept input and deliver output in XML.  
  
## In This Section  
 The following topics describe the XMLA objects implemented by [!INCLUDE[ssAS](../../includes/ssas-md.md)].  
  
|Method|Description|  
|------------|-----------------|  
|[DiscoverResponse Element &#40;XMLA&#41;](xml-elements-objects-discoverresponse.md)|Contains the information returned by an instance of [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] in response to a [Discover](xml-elements-methods-discover.md) method call.|  
|[ExecuteResponse Element &#40;XMLA&#41;](xml-elements-objects-executeresponse.md)|Contains the information returned by an instance of [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] in response to an [Execute](xml-elements-methods-execute.md) method call.|  
  
## See Also  
 [XML Elements &#40;XMLA&#41;](../dev-guide/xml-elements-xmla.md)   
 [XML Data Types &#40;XMLA&#41;](xml-data-types/xml-data-types-xmla.md)   
 [XML Elements &#40;XMLA&#41;](../dev-guide/xml-elements-xmla.md)  
  
  
