---
title: "BeginSession Element (XMLA) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
apiname: 
  - "BeginSession Element"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine#BeginSession"
  - "urn:schemas-microsoft-com:xml-analysis#BeginSession"
  - "microsoft.xml.analysis.beginsession"
helpviewer_keywords: 
  - "BeginSession element"
ms.assetid: 49873a97-58d7-42a9-ab7f-e045e2856737
caps.latest.revision: 16
author: "jeannt"
ms.author: "jeannt"
manager: "erikre"
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
 The **BeginSession** header element is part of a SOAP request sent to an [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] instance, and explicitly starts a new session on the instance. The SOAP header returned by the SOAP response contains a [Session](../../../analysis-services/xmla/xml-elements-headers/session-element-xmla.md) element that identifies the new session. This new session identifier be stored and sent in subsequent SOAP requests using the **Session** header element.  
  
 If the **BeginSession** header element is not sent, a session is not explicitly started. If a session is not explicitly started, transactions on that session cannot be managed. In other words, you cannot use the following XML for Analysis (XMLA) commands: [BeginTransaction](../../../analysis-services/xmla/xml-elements-commands/begintransaction-element-xmla.md), [CommitTransaction](../../../analysis-services/xmla/xml-elements-commands/committransaction-element-xmla.md), and [RollbackTransaction](../../../analysis-services/xmla/xml-elements-commands/rollbacktransaction-element-xmla.md). All XMLA methods and commands executed on an implicitly started session are considered to be atomic transactions.  
  
## See Also  
 [EndSession Element &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-headers/endsession-element-xmla.md)   
 [Session Element &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-headers/session-element-xmla.md)   
 [Managing Connections and Sessions &#40;XMLA&#41;](../../../analysis-services/multidimensional-models-scripting-language-assl-xmla/managing-connections-and-sessions-xmla.md)   
 [Headers &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-headers/xml-elements-headers.md)  
  
  