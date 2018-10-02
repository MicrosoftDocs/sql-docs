---
title: "BeginSession Element (XMLA) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "BeginSession Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine#BeginSession"
  - "urn:schemas-microsoft-com:xml-analysis#BeginSession"
  - "microsoft.xml.analysis.beginsession"
helpviewer_keywords: 
  - "BeginSession element"
ms.assetid: 49873a97-58d7-42a9-ab7f-e045e2856737
author: minewiskan
ms.author: owend
manager: craigg
---
# BeginSession Element (XMLA)
  Uses a SOAP header in a SOAP request message to start a new session on an instance of [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)].  
  
 **Namespace** urn:schemas-microsoft-com:xml-analysis  
  
## Syntax  
  
```xml  
  
<soap:Envelope xmlns:soap="http://schemas.xmlsoap.org/soap/envelope/">  
   <soap:Header>  
      ...  
      <BeginSession  
         xmlns="urn:schemas-microsoft-com:xml-analysis" />  
      ...  
   </soap:Header>  
   <soap:Body>  
      ...  
   </soap:Body>  
</soap:Envelope>  
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
|Parent elements|None|  
|Child elements|None|  
  
## Remarks  
 The `BeginSession` header element is part of a SOAP request sent to an [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] instance, and explicitly starts a new session on the instance. The SOAP header returned by the SOAP response contains a [Session](session-element-xmla.md) element that identifies the new session. This new session identifier be stored and sent in subsequent SOAP requests using the `Session` header element.  
  
 If the `BeginSession` header element is not sent, a session is not explicitly started. If a session is not explicitly started, transactions on that session cannot be managed. In other words, you cannot use the following XML for Analysis (XMLA) commands: [BeginTransaction](../xml-elements-commands/begintransaction-element-xmla.md), [CommitTransaction](../xml-elements-commands/committransaction-element-xmla.md), and [RollbackTransaction](../xml-elements-commands/rollbacktransaction-element-xmla.md). All XMLA methods and commands executed on an implicitly started session are considered to be atomic transactions.  
  
## See Also  
 [EndSession Element &#40;XMLA&#41;](endsession-element-xmla.md)   
 [Session Element &#40;XMLA&#41;](session-element-xmla.md)   
 [Managing Connections and Sessions &#40;XMLA&#41;](../../multidimensional-models-scripting-language-assl-xmla/managing-connections-and-sessions-xmla.md)   
 [Headers &#40;XMLA&#41;](xml-elements-headers.md)  
  
  
