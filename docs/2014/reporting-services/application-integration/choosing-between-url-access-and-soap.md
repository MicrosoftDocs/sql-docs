---
title: "Choosing Between URL Access and SOAP | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: reporting-services
ms.topic: "reference"
helpviewer_keywords: 
  - "SOAP [Reporting Services], vs. URL access"
  - "Report Server Web service, application integration"
  - "URL access [Reporting Services], vs. SOAP"
  - "Web service [Reporting Services], application integration"
ms.assetid: bccdc243-4366-4ce5-8e63-3dd6c463fa52
author: maggiesMSFT
ms.author: maggies
manager: kfile
---
# Choosing Between URL Access and SOAP
  Integrating [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] into custom applications can be challenging. The challenge, however, is not the complexity of the programming model or APIs, but the many possible ways to integrate it. [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] was designed from the ground up as a developer platform, and as such, it is built with programming flexibility in mind. With flexibility comes the need to make important decisions about integrating [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] report navigation and management functionality into your existing business applications.  
  
 ![Reporting Services programming scenarios](../../../2014/reporting-services/media/bk-ext-04.gif "Reporting Services programming scenarios")  
Reporting Services programming supports a wide range of scenarios.  
  
 There are two ways to integrate [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] into custom applications: URL access and the Reporting Services SOAP API. Which to use depends on several factors. In some cases, integrating [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] into your custom business applications requires a you to use both URL access and SOAP. You should ask the following questions:  
  
-   What type of enterprise reporting functionality do you or your end users require? Do you need a simple way to launch and navigate reports, or do you need more advanced report server management features from your custom business solution?  
  
-   In which type of environment do your users typically operate? Is your business application a Web application or a Windows application? How easily can your end users switch from a Win32 environment to a Web environment? What type of control do you need over the environment in which reports are run and managed?  
  
 Once you have answered the previous questions, you can decide how to integrate [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] into your IT infrastructure. Typically, URL access is preferred for viewing and navigating individual reports. URL access enables you to freely and quickly navigate reports without the overhead of the Web service. In addition, URL access is currently the only programming technique that uses the full HTML Viewer for report navigation, which includes the report toolbar. In addition, URL access provides better performance than SOAP because it bypasses the marshalling of SOAP requests to and from the server. In integration scenarios that require quick and easy access to reports with built-in tools for viewing and navigation, URL access is the better choice.  
  
> [!NOTE]  
>  Report server URL access supports HTML Viewer and the extended functionality of the report toolbar. The SOAP API does not support this type of rendered report. You need to design and develop your own report toolbar, if you render reports using SOAP.  
  
 For more information about the report toolbar, see [HTML Viewer and the Report Toolbar](../html-viewer-and-the-report-toolbar.md).  
  
 For more information about URL access, see [URL Access &#40;SSRS&#41;](../url-access-ssrs.md).  
  
 URL access is useful for viewing reports, but it does not provide the report and namespace management functionality that can be essential to any enterprise reporting scenario. In this case, the broad and rich functionality of the Reporting Services SOAP API is recommended. With the SOAP API you can manage and deploy reports, create schedules, configure server properties, manage the report server namespace, create subscriptions, and more. The SOAP API exposes the complete set of management functionality in [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)]. The SOAP API can also enable report viewing and navigation through the <xref:ReportExecution2005.ReportExecutionService.Render%2A> method of the API. However, viewing reports through the SOAP API does not enable the built-in viewing functionality of the report toolbar, nor does it automatically handle the report interactivity that URL access provides.  
  
 For more information about the Reporting Services SOAP API, see [Report Server Web Service](../report-server-web-service/report-server-web-service.md).  
  
 In the majority of cases, URL access and SOAP calls are both required to meet your reporting needs. SOAP is used when initially connecting to the report server database and presenting the available list of reports in a user interface and URL access is used to actually access and navigate individual reports.  
  
 For an example of combining URL access and the Web service to provide integrated reporting, see [SQL Server Reporting Services Product Samples](https://go.microsoft.com/fwlink/?LinkId=177889).  
  
## See Also  
 [Integrating Reporting Services into Applications](../../../2014/reporting-services/application-integration/integrating-reporting-services-into-applications.md)   
 [Integrating Reporting Services Using SOAP](../application-integration/integrating-reporting-services-using-soap.md)   
 [Integrating Reporting Services Using URL Access](../application-integration/integrating-reporting-services-using-url-access.md)   
 [Technical Reference &#40;SSRS&#41;](../../../2014/reporting-services/technical-reference-ssrs.md)  
  
  
