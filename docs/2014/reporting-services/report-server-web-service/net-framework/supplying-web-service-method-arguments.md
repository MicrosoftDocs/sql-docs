---
title: "Supplying Web Service Method Arguments | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "docset-sql-devref"
  - "reporting-services-native"
ms.topic: "reference"
helpviewer_keywords: 
  - "Report Server Web service, methods"
  - "Web service [Reporting Services], methods"
  - "methods [Reporting Services], arguments"
  - "XML Web service [Reporting Services], methods"
ms.assetid: f7b9ca05-fc4c-4b30-8e5d-172dd0f4a832
author: maggiesMSFT
ms.author: maggies
manager: kfile
---
# Supplying Web Service Method Arguments
  A Report Server Web service method sends a request to the service at a given URL using SOAP over HTTP. The service receives the request, processes it, and then returns a response. These requests and responses are in the form of XML documents.  
  
## Optional Parameters  
 In some cases, a Web service method can have optional input parameters. Even if an input parameter for a Web service method is optional, you must still include it and set the parameter value to `null` (`Nothing` in [!INCLUDE[vbprvb](../../../includes/vbprvb-md.md)]). Setting a parameter value to `null` sets the element value for that parameter in the SOAP request to `null`.  
  
 The following example uses the <xref:ReportService2010.ReportingService2010.CreateFolder%2A> method to create a new folder named Product Sales in the Sales folder. By supplying a `null` value for the folder properties, no user-specific properties are supplied for the folder:  
  
```  
// C#  
rs.CreateFolder("Product Sales", "/Sales", null);  
```  
  
## Complex Data Types  
 The core class of the Report Server Web service is <xref:ReportService2010.ReportingService2010>, which you use to invoke the SOAP operations or Web methods of the proxy class. To support this class and its methods, [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] includes user-defined, complex data types that are specific to the input and output parameters of the Web service methods. These complex data types are part of the generated proxy class, which you can use when developing in the [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[dnprdnshort](../../../includes/dnprdnshort-md.md)] environment.  
  
 When you generate a proxy class, the complex data types that are defined in the WSDL file are represented by the classes of the proxy, which include properties that correspond to the various SOAP elements of the complex data types. Sequences of these data types become arrays of objects that you can enumerate through in your code. This eliminates the need to work directly with the XML structures sent in SOAP messages. The [!INCLUDE[dnprdnshort](../../../includes/dnprdnshort-md.md)] handles that translation for you.  
  
## See Also  
 [Building Applications Using the Web Service and the .NET Framework](building-applications-using-the-web-service-and-the-net-framework.md)   
 [Report Server Web Service](../report-server-web-service.md)   
 [Technical Reference &#40;SSRS&#41;](../../technical-reference-ssrs.md)  
  
  
