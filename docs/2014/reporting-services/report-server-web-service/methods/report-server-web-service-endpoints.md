---
title: "Report Server Web Service Endpoints | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "docset-sql-devref"
  - "reporting-services-native"
ms.topic: "reference"
helpviewer_keywords: 
  - "management endpoints [Reporting Services]"
  - "Web service [Reporting Services], endpoints"
  - "endpoints [Reporting Services]"
  - "execution endpoints [Reporting Services]"
  - "Report Server Web service, endpoints"
ms.assetid: f3f5d85f-9359-4508-bc5a-7f78a3cf7421
author: markingmyname
ms.author: maghan
manager: craigg
---
# Report Server Web Service Endpoints
  The Report Server Web service provides several endpoints for managing a report server as well as executing and navigating reports.  
  
## The Management Endpoints  
 There are three endpoints available for managing objects on a report server, <xref:ReportService2005>, <xref:ReportService2006>, and <xref:ReportService2010>. The <xref:ReportService2005> endpoint is used for managing objects on a report server that is configured for native mode. The <xref:ReportService2006> endpoint is used for managing objects on a report server that is configured for SharePoint integrated mode. The <xref:ReportService2010> endpoint merges the functionalities of <xref:ReportService2005> and <xref:ReportService2006> and can manage objects on a report server that are configured for either native or SharePoint integrated mode.  
  
> [!IMPORTANT]  
>  When a report server is configured for SharePoint integrated mode, the <xref:ReportService2005> APIs will return an `rsOperationNotSupportedSharePointMode` error. If the report server is configured for native mode, the <xref:ReportService2006> APIs will return an `rsOperationNotSupportedNativeMode` error. Similarly, when mode-specific APIs in <xref:ReportService2010> are used on unintended modes, the APIs will return the respective errors.  
  
> [!NOTE]  
>  The <xref:ReportService2005> and <xref:ReportService2006> endpoints are deprecated in [!INCLUDE[ssKilimanjaro](../../../includes/sskilimanjaro-md.md)]. The <xref:ReportService2010> endpoint includes the functionalities of both endpoints and contains additional management features.  
  
 If the report server is configured for native mode or SharePoint integrate mode, the WSDL for the management endpoint can be accessed using one of the following URL:  
  
```  
http://<Server Name>/ReportServer/ReportService2010.asmx?wsdl  
```  
  
 For more information, see [Accessing the SOAP API](../accessing-the-soap-api.md).  
  
## The Execution Endpoint  
 The <xref:ReportExecution2005> endpoint makes it easy for developers to customize report processing and rendering from a report server in both native and SharePoint integrated modes. The endpoint includes classes and methods that existed in earlier versions of the Report Server Web service. In addition, many new classes and methods have been added to the Report Server Web service that are exposed through the execution endpoint.  
  
 The WSDL for the management endpoint can be accessed using the following URL:  
  
```  
http://<Server Name>/ReportServer/ReportExecution2005.asmx?wsdl  
```  
  
 If the report server is configured for SharePoint integrate mode, the WSDL can be accessed using the following URL:  
  
```  
http://<Server Name>/<Site Name>/_vti_bin/ReportServer/ReportExecution2005.asmx?wsdl  
```  
  
 For more information, please see [Accessing the SOAP API](../accessing-the-soap-api.md).  
  
## SharePoint Proxy Endpoints  
 When a report server is configured for SharePoint integrated mode and the [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] Add-in has been installed, a set of proxy endpoints are installed on the SharePoint server. The proxy endpoints are the primary API for developing report solutions when a report server is configured for SharePoint integrated mode. When developing against the proxy endpoints, the [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] Add-in manages the exchange of credentials between the SharePoint server and the report server in Trusted account authentication mode. When developing against the report server endpoints, the calling application will have to manage the credential exchange in Trusted account authentication mode. The following table lists the endpoints that are installed with the [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] Add-in.  
  
|Proxy Endpoint|Description|  
|--------------------|-----------------|  
|<xref:ReportService2006>|Provides the APIs for managing a report server that is configured for SharePoint integrate mode. **Note:**  This endpoint is deprecated in [!INCLUDE[ssKilimanjaro](../../../includes/sskilimanjaro-md.md)].|  
|<xref:ReportService2010>|Provides the APIs for managing a report server that is configured for either native or SharePoint integrated mode.|  
|<xref:ReportExecution2005>|Provides the APIs for running and navigating reports.|  
|<xref:ReportServiceAuthentication>|Provides the APIs for authenticating users against a report server when the SharePoint Web application is configured for Forms Authentication.|  
  
 The following are example URLs for referencing the proxy endpoints on a SharePoint site.  
  
```  
http://<Server Name>/<Site Name>/_vti_bin/ReportServer/ReportService2010.asmx  
```  
  
```  
http://<Server Name>/<Site Name>/_vti_bin/ReportServer/ReportExecution2005.asmx  
```  
  
```  
http://<Server Name>/<Site Name>/_vti_bin/ReportServer/ReportServiceAuthentication.asmx  
```  
  
## See Also  
 [Building Applications Using the Web Service and the .NET Framework](../net-framework/building-applications-using-the-web-service-and-the-net-framework.md)  
  
  
