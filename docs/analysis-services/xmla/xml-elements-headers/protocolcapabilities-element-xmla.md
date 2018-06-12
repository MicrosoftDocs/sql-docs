---
title: "ProtocolCapabilities Element (XMLA) | Microsoft Docs"
ms.date: 05/08/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: xmla
ms.topic: reference
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# ProtocolCapabilities Element (XMLA)
[!INCLUDE[ssas-appliesto-sqlas-aas](../../../includes/ssas-appliesto-sqlas-aas.md)]
  Uses the SOAP header in a SOAP request message to identify protocol capabilities between an instance of Analysis Services and a client application.  
  
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
  
## Element characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|None|  
|Default value|None|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element relationships  
  
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
  
## See also
 [Managing Connections and Sessions &#40;XMLA&#41;](../../../analysis-services/multidimensional-models-scripting-language-assl-xmla/managing-connections-and-sessions-xmla.md)   
 [Headers &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-headers/xml-elements-headers.md)  
  
  
