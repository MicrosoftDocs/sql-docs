---
title: "Handle exceptions in Reporting Services"
description: Learn how to handle exceptions that occur in Reporting Services so you can determine the next appropriate action to take in your applications.
author: maggiesMSFT
ms.author: maggies
ms.date: 03/14/2017
ms.service: reporting-services
ms.subservice: report-server-web-service
ms.topic: reference
ms.custom: updatefrequency5
helpviewer_keywords:
  - "SOAP [Reporting Services], exceptions"
  - ".NET Framework [Reporting Services]"
  - "exceptions [Reporting Services], about exception handling"
  - "SoapException object"
---
# Handle exceptions in Reporting Services
  When a Reporting Services SOAP API client request can't be completed, the report server returns an error rather than the expected results of the call. When a call can't complete, an error for the Report Server Web service is returned as a SOAP **Fault** XML element. The key descriptive element of the fault is the **detail** element, which includes all of the error information provided by the report server and any other Web service error information. The key information in the **detail** element is the report server error code. Based on the message and error code, you can determine the next appropriate action to take in your applications. For more information about SOAP faults, see the World Wide Web Consortium (W3C) Web site at http://www.w3.org/TR/SOAP.  
  
## SOAP faults and the .NET framework  
 In the [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)], if an error occurs in a client request to the Web service, the report server communicates the error to the client code that calls the Web service by throwing a **SoapException** object. The **SoapException** wraps the information contained in a SOAP fault. The **Detail** property of the **SoapException** maps to the **detail** element in the SOAP fault. Applications should catch the **SoapException** object with a try/catch block and use the **Detail** property of the **SoapException** to take appropriate action. For more information about the **SoapException** class and the **Detail** property in [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)], see [Reporting Services SoapException class](../../reporting-services/report-server-web-service-net-framework-exception-handling/soapexception-class/reporting-services-soapexception-class.md). For more information about the **SoapException** class, see the [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] SDK documentation.  
  
## Related content 
 [Detail property](../../reporting-services/report-server-web-service-net-framework-exception-handling/soapexception-class/detail-property.md)   
 [Introduction to exception management in Reporting Services](../../reporting-services/report-server-web-service-net-framework-exception-handling/introducing-exception-handling-in-reporting-services.md)   
 [Reporting Services SoapException class](../../reporting-services/report-server-web-service-net-framework-exception-handling/soapexception-class/reporting-services-soapexception-class.md)  
  
  
