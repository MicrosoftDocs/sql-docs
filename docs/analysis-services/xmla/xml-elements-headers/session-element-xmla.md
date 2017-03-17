---
title: "Session Element (XMLA) | Microsoft Docs"
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
  - "Session Element"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "microsoft.xml.analysis.session"
  - "http://schemas.microsoft.com/analysisservices/2003/engine#Session"
  - "urn:schemas-microsoft-com:xml-analysis#Session"
helpviewer_keywords: 
  - "Session element"
ms.assetid: 884ed090-968e-41d3-97e5-6d12787467da
caps.latest.revision: 15
author: "jeannt"
ms.author: "jeannt"
manager: "erikre"
---
# Session Element (XMLA)
  Uses the SOAP header in a SOAP request message to identify an existing, explicit session on an instance of [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)].  
  
 **Namespace** urn:schemas-microsoft-com:xml-analysis  
  
## Syntax  
  
```xml  
  
<soap:Envelope xmlns:soap="http://schemas.xmlsoap.org/soap/envelope/">  
   <soap:Header>  
      ...  
      <Session  
         xmlns="urn:schemas-microsoft-com:xml-analysis"  
         SessionId="string" />  
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
  
## Attributes  
  
|Attribute|Description|  
|---------------|-----------------|  
|SessionId|Required **String** attribute that identifies the session to be used. [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] uses a globally unique identifier (GUID) to identify a session.|  
  
## Remarks  
 The **Session** header element identifies an existing, explicitly started session on the [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] instance. The **Session** element is part of the SOAP header in the following types of messages:  
  
-   A SOAP response that contains a [BeginSession](../../../analysis-services/xmla/xml-elements-headers/beginsession-element-xmla.md) SOAP header element.  
  
-   A SOAP request to identify the session on which to run the [Discover](../../../analysis-services/xmla/xml-elements-methods-discover.md) or [Execute](../../../analysis-services/xmla/xml-elements-methods-execute.md) method.  
  
 A session identifier does not guarantee that a session remains valid. The session specified in the **Session** element can expire. For example, a session can expire if the session times out or the connection associated with the session is disconnected. If the session expires and is no longer valid, [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] ends the session and rolls back any transaction currently in process. Any SOAP message sent with a session identifier that is no longer valid fails with a SOAP fault indicating that the specified session cannot be found.  
  
 If a **Session** element is not sent as part of a SOAP request, the [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] instance implicitly begins a session for the duration of the **Discover** or **Execute** method call, and then ends that session once the method call has completed.  
  
## See Also  
 [EndSession Element &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-headers/endsession-element-xmla.md)   
 [Managing Connections and Sessions &#40;XMLA&#41;](../../../analysis-services/multidimensional-models-scripting-language-assl-xmla/managing-connections-and-sessions-xmla.md)   
 [Headers &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-headers/xml-elements-headers.md)  
  
  