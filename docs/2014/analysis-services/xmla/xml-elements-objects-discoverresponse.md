---
title: "DiscoverResponse Element (XMLA) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "DiscoverResponse Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine#DiscoverResponse"
  - "microsoft.xml.analysis.discoverresponse"
  - "urn:schemas-microsoft-com:xml-analysis#DiscoverResponse"
helpviewer_keywords: 
  - "DiscoverResponse element"
ms.assetid: 20e10a82-dbd1-4ead-b92d-f84b4b2f10c6
author: minewiskan
ms.author: owend
manager: craigg
---
# DiscoverResponse Element (XMLA)
  Contains the information returned by an instance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] in response to a [Discover](xml-elements-methods-discover.md) method call.  
  
 **Namespace** urn:schemas-microsoft-com:xml-analysis  
  
## Syntax  
  
```xml  
  
<DiscoverResponse>  
   <return>  
</DiscoverResponse>  
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
|Child elements|[return](xml-elements-properties/return-element-xmla.md)|  
  
## Remarks  
 The `DiscoverResponse` element is the topmost element within the body of a SOAP response for the `Discover` method.  
  
## See Also  
 [ExecuteResponse Element &#40;XMLA&#41;](xml-elements-objects-executeresponse.md)   
 [Objects &#40;XMLA&#41;](xml-elements-objects.md)  
  
  
