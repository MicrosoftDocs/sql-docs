---
title: "BeginSession Element (XMLA) | Microsoft Docs"
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
# BeginSession Element (XMLA)
[!INCLUDE[ssas-appliesto-sqlas-aas](../../../includes/ssas-appliesto-sqlas-aas.md)]
  Uses a SOAP header in a SOAP request message to start a new session on an instance of Analysis Services.  
  
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
|Child elements|None|  
  
## Remarks  
 The **BeginSession** header element is part of a SOAP request sent to an [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] instance, and explicitly starts a new session on the instance. The SOAP header returned by the SOAP response contains a [Session](../../../analysis-services/xmla/xml-elements-headers/session-element-xmla.md) element that identifies the new session. This new session identifier be stored and sent in subsequent SOAP requests using the **Session** header element.  
  
 If the **BeginSession** header element is not sent, a session is not explicitly started. If a session is not explicitly started, transactions on that session cannot be managed. In other words, you cannot use the following XML for Analysis (XMLA) commands: [BeginTransaction](../../../analysis-services/xmla/xml-elements-commands/begintransaction-element-xmla.md), [CommitTransaction](../../../analysis-services/xmla/xml-elements-commands/committransaction-element-xmla.md), and [RollbackTransaction](../../../analysis-services/xmla/xml-elements-commands/rollbacktransaction-element-xmla.md). All XMLA methods and commands executed on an implicitly started session are considered to be atomic transactions.  
  
## See also
 [EndSession Element &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-headers/endsession-element-xmla.md)   
 [Session Element &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-headers/session-element-xmla.md)   
 [Managing Connections and Sessions &#40;XMLA&#41;](../../../analysis-services/multidimensional-models-scripting-language-assl-xmla/managing-connections-and-sessions-xmla.md)   
 [Headers &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-headers/xml-elements-headers.md)  
  
  
