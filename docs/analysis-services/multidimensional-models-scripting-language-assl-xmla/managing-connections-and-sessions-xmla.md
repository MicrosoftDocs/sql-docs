---
title: "Managing Connections and Sessions (XMLA) | Microsoft Docs"
ms.date: 05/02/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: xmla
ms.topic: conceptual
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Managing Connections and Sessions (XMLA)
  *Statefulness* is a condition during which the server preserves the identity and context of a client between method calls. *Statelessness* is a condition during which the server does not remember the identity and context of a client after a method call finishes.  
  
 To provide statefulness, XML for Analysis (XMLA) supports *sessions* that allow a series of statements to be performed together. An example of such a series of statements would be the creation of a calculated member that is to be used in subsequent queries.  
  
 In general, sessions in XMLA follow the following behavior outlined by the OLE DB 2.6 specification:  
  
-   Sessions define transaction and command context scope.  
  
-   Multiple commands can be run in the context of a single session.  
  
-   Support for transactions in the XMLA context is through provider-specific commands sent with the [Execute](https://docs.microsoft.com/bi-reference/xmla/xml-elements-methods-execute) method.  
  
 XMLA defines a way to support sessions in a Web environment in a mode similar to the approach used by the Distributed Authoring and Versioning (DAV) protocol to implement locking in a loosely coupled environment. This implementation parallels DAV in that the provider is allowed to expire sessions for various reasons (for example, a timeout or connection error). When sessions are supported, Web services must be aware and ready to handle interrupted sets of commands that must be restarted.  
  
 The World Wide Web Consortium (W3C) Simple Object Access Protocol (SOAP) specification recommends using SOAP headers for building up new protocols on top of SOAP messages. The following table lists the SOAP header elements and attributes that XMLA defines for initiating, maintaining, and closing a session.  
  
|SOAP header|Description|  
|-----------------|-----------------|  
|BeginSession|This header requests the provider to create a new session. The provider should respond by constructing a new session and returning the session ID as part of the Session header in the SOAP response.|  
|SessionId|The value area contains the session ID that must be used in each method call for the rest of the session. The provider in the SOAP response sends this tag and the client must also send this attribute with each Session header element.|  
|Session|For every method call that occurs in the session, this header must be used, and the session ID must be included in the value area of the header.|  
|EndSession|To terminate the session, use this header. The session ID must be included with the value area.|  
  
> [!NOTE]  
>  A session ID does not guarantee that a session stays valid. If the session expires (for example, if it times out or the connection is lost), the provider can choose to end and roll back that session's actions. As a result, all subsequent method calls from the client on a session ID fail with an error signaling a session that is not valid. A client should handle this condition and be prepared to resend the session method calls from the beginning.  
  
## Legacy Code Example  
 The following example shows how sessions are supported.  
  
1.  To begin the session, add a BeginSession header in SOAP to the outbound XMLA method call from the client. The value area is initially blank because the session ID is not yet known.  
  
    ```  
    <SOAP-ENV:Envelope  
       xmlns:SOAP-ENV="http://schemas.xmlsoap.org/soap/envelope/"  
       SOAP-ENV:encodingStyle="http://schemas.xmlsoap.org/soap/encoding/">  
       <SOAP-ENV:Header>  
          <XA:BeginSession  
             xmlns:XA="urn:schemas-microsoft-com:xml-analysis"  
             xsi:type="xsd:int"  
             mustUnderstand="1"/>  
       </SOAP-ENV:Header>  
       <SOAP-ENV:Body>  
          ...<!-- Discover or Execute call goes here.-->  
       </SOAP-ENV:Body>  
    </SOAP-ENV:Envelope>  
    ```  
  
2.  The SOAP response message from the provider includes the session ID in the return header area, using the XMLA header tag \<SessionId>.  
  
    ```  
    <SOAP-ENV:Header>  
       <XA:Session  
          xmlns:XA="urn:schemas-microsoft-com:xml-analysis"  
          SessionId="581"/>  
    </SOAP-ENV:Header>  
    ```  
  
3.  For each method call in the session, the Session header must be added, containing the session ID returned from the provider.  
  
    ```  
    <SOAP-ENV:Header>  
       <XA:Session  
          xmlns:XA="urn:schemas-microsoft-com:xml-analysis"  
          mustUnderstand="1"  
          SessionId="581"/>  
    </SOAP-ENV:Header>  
    ```  
  
4.  When the session is complete, the \<EndSession> tag is used, containing the related session ID value.  
  
    ```  
    <SOAP-ENV:Header>  
       <XA:EndSession  
          xmlns:XA="urn:schemas-microsoft-com:xml-analysis"  
          xsi:type="xsd:int"  
          mustUnderstand="1"  
          SessionId="581"/>  
    </SOAP-ENV:Header>  
    ```  
  
## See Also  
 [Developing with XMLA in Analysis Services](../../analysis-services/multidimensional-models-scripting-language-assl-xmla/developing-with-xmla-in-analysis-services.md)  
  
  
