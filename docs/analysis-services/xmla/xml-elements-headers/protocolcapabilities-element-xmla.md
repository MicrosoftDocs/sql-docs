---
title: "ProtocolCapabilities Element (XMLA) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: "analysis-services"
ms.prod_service: "analysis-services, azure-analysis-services"
ms.service: ""
ms.component: ""
ms.reviewer: ""
ms.suite: "pro-bi"
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
apiname: 
  - "ProtocolCapabilities Element"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "microsoft.xml.analysis.protocolcapabilities"
  - "http://schemas.microsoft.com/analysisservices/2003/engine#ProtocolCapabilities"
  - "urn:schemas-microsoft-com:xml-analysis#ProtocolCapabilities"
helpviewer_keywords: 
  - "ProtocolCapabilities element"
ms.assetid: f923896a-3f32-46a3-9543-388c30b3465d
caps.latest.revision: 13
author: "Minewiskan"
ms.author: "owend"
manager: "kfile"
ms.workload: "Inactive"
---
# ProtocolCapabilities Element (XMLA)
[!INCLUDE[ssas-appliesto-sqlas-aas](../../../includes/ssas-appliesto-sqlas-aas.md)]
  Uses the SOAP header in a SOAP request message to identify protocol capabilities between an instance of [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] and a client application.  
  
 **Namespace** `http://schemas.microsoft.com/analysisservices/2003/engine`  
  
## Syntax  
  
```xml  
  
<soap:Envelope xmlns:soap="http://schemas.xmlsoap.org/soap/envelope/">  
   <soap:Header>  
      ...  
      <ProtocolCapabilities xmlns="http://schemas.microsoft.com/analysisservices/2003/engine">  
         <Capability>...</Capability>  
      </ProtocolCapabilities>  
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
|Child elements|[Capability](../../../analysis-services/xmla/xml-elements-properties/capability-element-xmla.md)|  
  
## Remarks  
 The **ProtocolCapabilities** element enables client applications to negotiate protocol capabilities, such as binary XML or compression support, with an [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] instance at any time. Protocol negotiation involves the following steps:  
  
1.  The client application identifies its protocol capability by sending a SOAP request that includes the **ProtocolCapabilities** element as part of the SOAP header.  
  
2.  The [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] instance receives and processes the SOAP request.  
  
3.  If the [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] instance has the same protocol capability as that requested, the instance sends a SOAP response that includes the same **ProtocolCapabilities** element sent in the SOAP request, and the protocol has been successfully negotiated. Otherwise, the protocol capabilities are not successfully negotiated, and the instance returns a SOAP fault.  
  
 After successfully negotiating protocol capabilities, how long the client application and the [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] instance use a particular protocol depends upon whether the session is explicit or implicit:  
  
-   An explicit session is one that is created using the [BeginSession](../../../analysis-services/xmla/xml-elements-headers/beginsession-element-xmla.md) header element. For an explicit session, the negotiated protocol is used until either the client application sends a new **ProtocolCapabilities** element or the session ends.  
  
-   An implicit session is one that is created by an [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] instance and not explicitly specified by the client application when submitting a SOAP request. For an implicit session, the negotiated protocol is used only until the SOAP request is completed.  
  
 Protocol capabilities do not have to be explicitly negotiated. That is, a client application does not have to include a **ProtocolCapabilities** element as part of the SOAP request. If a SOAP request does not include a **ProtocolCapabilities** element, the [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] instance responds using the same format as the SOAP request.  
  
## See Also  
 [Managing Connections and Sessions &#40;XMLA&#41;](../../../analysis-services/multidimensional-models-scripting-language-assl-xmla/managing-connections-and-sessions-xmla.md)   
 [Headers &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-headers/xml-elements-headers.md)  
  
  
