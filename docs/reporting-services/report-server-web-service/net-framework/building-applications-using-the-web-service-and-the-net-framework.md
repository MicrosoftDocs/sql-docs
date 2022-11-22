---
title: "Building Applications Using the Web Service and the .NET Framework | Microsoft Docs"
description: A Report Server Web service client communicates with a report server by SOAP. Use the .NET Framework to create Web service clients to work with any Web service.
ms.date: 03/16/2017
ms.prod: reporting-services
ms.technology: report-server-web-service


ms.topic: reference
helpviewer_keywords: 
  - "Report Server Web service, application building"
  - ".NET Framework [Reporting Services]"
  - "XML Web service [Reporting Services], client creation"
  - "reports [Reporting Services], building"
  - "Web service [Reporting Services], application building"
  - "Report Server Web service, client creation"
  - "client configuration [Reporting Services]"
  - "XML Web service [Reporting Services], application building"
  - "Web service [Reporting Services], client creation"
ms.assetid: 92a9678c-bc4f-4d7a-ba44-85989bfe27ca
author: maggiesMSFT
ms.author: maggies
---
# Building Applications Using the Web Service and the .NET Framework
  With the [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[dnprdnshort](../../../includes/dnprdnshort-md.md)], you can use familiar programming constructs, such as methods, primitive types, and user-defined complex types to work with Web services. The [!INCLUDE[dnprdnshort](../../../includes/dnprdnshort-md.md)] contains an infrastructure and tools you can use to create Web service clients that can call any World Wide Web Consortium (W3C) standards-compliant Web service.  
  
 A Report Server Web service client is any component or application that communicates with a report server using Simple Object Access Protocol (SOAP) messages.  
  
 **To create a Report Server Web service client using the .NET Framework, follow these basic steps:**  
  
1.  Create a proxy class for the Web service.  
  
     To do this, add a proxy class or Web reference to your development project, reference the proxy class in your client code, and create an instance of that proxy. For more information, see [Creating the Web Service Proxy](../../../reporting-services/report-server-web-service/net-framework/creating-the-web-service-proxy.md).  
  
2.  Authenticate the Web service client with the report server.  
  
     To do this, set the service object's <xref:System.Web.Services.Protocols.WebClientProtocol.Credentials%2A> property equal to the credentials of an authenticated user on the report server. For more information, see [Web Service Authentication](../../../reporting-services/report-server-web-service/net-framework/web-service-authentication.md).  
  
3.  Call the method of the proxy class corresponding to the Web service operation that you want to invoke.  
  
     To do this, call a Web service method and supply the necessary arguments. For more information about the Web service methods, see [Report Server Web Service Methods](../../../reporting-services/report-server-web-service/methods/report-server-web-service-methods.md). For more information about calling, see [Calling Web Service Methods](../../../reporting-services/report-server-web-service/net-framework/calling-web-service-methods.md).  
  
## In This Section  
  
|Topic|Description|  
|-----------|-----------------|  
|[Creating the Web Service Proxy](../../../reporting-services/report-server-web-service/net-framework/creating-the-web-service-proxy.md)|Describes the ways to add a proxy class to your project using [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[dnprdnshort](../../../includes/dnprdnshort-md.md)].|  
|[Web Service Authentication](../../../reporting-services/report-server-web-service/net-framework/web-service-authentication.md)|Describes how calls to the Report Server Web service are authenticated.|  
|[Calling Web Service Methods](../../../reporting-services/report-server-web-service/net-framework/calling-web-service-methods.md)|Describes how to use the SOAP API to call Web service methods in [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[vsprvs](../../../includes/vsprvs-md.md)].|  
|[Setting the Url Property of the Web Service](../../../reporting-services/report-server-web-service/net-framework/setting-the-url-property-of-the-web-service.md)|Explains how to programmatically direct your Web service proxy to a new server URL after you have created your Web reference.|  
|[Supplying Web Service Method Arguments](../../../reporting-services/report-server-web-service/net-framework/supplying-web-service-method-arguments.md)|Describes how to invoke a Web service method and supply method arguments.|  
|[Omitting Values for Optional Web Service Objects](../../../reporting-services/report-server-web-service/net-framework/omitting-values-for-optional-web-service-objects.md)|Describes how to omit values for optional Web service objects.|  
|[Using Secure Web Service Methods](../../../reporting-services/report-server-web-service/net-framework/using-secure-web-service-methods.md)|Describes the **SecureConnectionLevel** setting and the way in which it affects the use of the Reporting Services SOAP API.|  
|[Passing Device Information Settings to Rendering Extensions](../../../reporting-services/report-server-web-service/net-framework/passing-device-information-settings-to-rendering-extensions.md)|Describes the device information settings that are used to render reports to different formats.|  
|[Reporting Services Delivery Extension Settings](../../../reporting-services/report-server-web-service/net-framework/reporting-services-delivery-extension-settings.md)|Describes the settings that are used to deliver reports using report server e-mail.|  
|[Using Reporting Services SOAP Headers](../../../reporting-services/report-server-web-service-net-framework-soap-headers/using-reporting-services-soap-headers.md)|Explains the use of SOAP headers in [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)].|  
|[Introducing Exception Handling in Reporting Services](../../../reporting-services/report-server-web-service-net-framework-exception-handling/introducing-exception-handling-in-reporting-services.md)|Provides information about the way in which [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] handles errors.|  
  
## See Also  
 [Report Server Web Service](../../../reporting-services/report-server-web-service/report-server-web-service.md)   
 [Technical Reference &#40;SSRS&#41;](../../../reporting-services/technical-reference-ssrs.md)  
  
  
