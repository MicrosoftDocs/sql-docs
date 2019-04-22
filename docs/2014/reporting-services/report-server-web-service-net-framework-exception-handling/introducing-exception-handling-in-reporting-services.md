---
title: "Introducing Exception Handling in Reporting Services | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: reporting-services
ms.topic: "reference"
helpviewer_keywords: 
  - "Web service [Reporting Services], exception handling"
  - "errors [Reporting Services]"
  - "exceptions [Reporting Services]"
  - "Report Server Web service, exception handling"
  - "XML Web service [Reporting Services], exception handling"
ms.assetid: 54381870-ce67-482b-aa83-6a838cdbf9b9
author: maggiesMSFT
ms.author: maggies
manager: kfile
---
# Introducing Exception Handling in Reporting Services
  If your [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] application sends a request to the Report Server Web service that the service is unable to process, the service returns a SOAP exception to the client. Handling exceptions thrown by the Report Server Web service is an important part of the applications that you develop because you can return useful information to users when errors occur.  
  
 This section contains specific information about handling exceptions, preventing user input that is not valid, and returning meaningful error information to users. For general information about exception handling, see "Handling and Throwing Exceptions" in the [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] SDK documentation.  
  
## In This Section  
  
|Topic|Description|  
|-----------|-----------------|  
|[Handling Exceptions in Reporting Services](handling-exceptions-in-reporting-services.md)|Provides an overview of exceptions in [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] and the role of SOAP in returning errors from a Web service.|  
|[Best Practices for Reporting Services Exception Handling](best-practices/best-practices-for-reporting-services-exception-handling.md)|Provides recommendations on how to handle exceptions in [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)].|  
|[Reporting Services SoapException Class](soapexception-class/reporting-services-soapexception-class.md)|Describes the **SoapException** class in [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)].|  
  
## See Also  
 [Building Applications Using the Web Service and the .NET Framework](../report-server-web-service/net-framework/building-applications-using-the-web-service-and-the-net-framework.md)  
  
  
