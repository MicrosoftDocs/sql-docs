---
title: "Choose between URL access and SOAP"
description: "There are two ways to integrate Reporting Services into custom applications: URL access and the Reporting Services SOAP API. Find out how to choose."
ms.date: 10/19/2017
ms.service: reporting-services
ms.subservice: application-integration

ms.custom: seo-lt-2019
ms.topic: reference
author: maggiesMSFT
ms.author: maggies
monikerRange: "= sql-server-2016"
---
# Choose between URL access and SOAP in Reporting Services

[!INCLUDE [ssrs-appliesto](../../includes/ssrs-appliesto.md)] [!INCLUDE[ssrs-appliesto-2016](../../includes/ssrs-appliesto-2016.md)] [!INCLUDE[ssrs-appliesto-not-2017](../../includes/ssrs-appliesto-not-2017.md)] [!INCLUDE [ssrs-appliesto-not-pbirs](../../includes/ssrs-appliesto-not-pbirs.md)]

Integrating [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] into custom applications can be challenging. The challenge, however, is not the complexity of the programming model or APIs, but the many possible ways to integrate it. [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] was designed from the ground up as a developer platform, and as such, it is built with programming flexibility in mind. With flexibility comes the need to make important decisions about integrating [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] report navigation and management functionality into your existing business applications.

> [!NOTE]
> Starting with SQL Server 2017 Reporting Services, REST API access is available for developing solutions. SOAP API access has been deprecated. For more information, see [Develop with the REST APIs for Reporting Services](../developer/rest-api.md).
  
 There are two ways to integrate [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] into custom applications: URL access and the Reporting Services SOAP API. Which to use depends on several factors. In some cases, integrating [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] into your custom business applications requires the use of both URL access and SOAP. You should ask the following questions:  
  
-   What type of enterprise reporting functionality do you or your end users require? Do you need a simple way to launch and navigate reports, or do you need more advanced report server management features from your custom business solution?  
  
-   In which type of environment do your users typically operate? Is your business application a Web application or a Windows application? How easily can your end-users switch from a Win32 environment to a Web environment? What type of control do you need over the environment in which reports are run and managed?  
  
 Once you have answered the previous questions, you can decide how to integrate [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] into your IT infrastructure. Typically, URL access is preferred for viewing and navigating individual reports. URL access enables you to freely and quickly navigate reports without the overhead of the Web service. In addition, URL access is currently the only programming technique that uses the full HTML Viewer for report navigation, which includes the report toolbar. In addition, URL access provides better performance than SOAP because it bypasses the marshaling of SOAP requests to and from the server. In integration scenarios that require quick and easy access to reports with built-in tools for viewing and navigation, URL access is the better choice.  
  
> [!NOTE]  
> Report server URL access supports HTML Viewer and the extended functionality of the report toolbar. The SOAP API does not support this type of rendered report. If you render reports using the SOAP API, design and develop your own report toolbar.
  
 For more information about the report toolbar, see [HTML Viewer and the Report Toolbar](../../reporting-services/html-viewer-and-the-report-toolbar.md).  
  
 For more information about URL access, see [URL Access](../../reporting-services/url-access-ssrs.md).  
  
 URL access is useful for viewing reports, but it does not provide the report and namespace management functionality that can be essential to any enterprise reporting scenario. In this case, the broad and rich functionality of the Reporting Services SOAP API is recommended. With the SOAP API you can manage and deploy reports, create schedules, configure server properties, manage the report server namespace, create subscriptions, and more. The SOAP API exposes the complete set of management functionality in [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)]. The SOAP API can also enable report viewing and navigation through the <xref:ReportExecution2005.ReportExecutionService.Render%2A> method of the API. However, viewing reports through the SOAP API does not enable the built-in viewing functionality of the report toolbar, nor does it automatically handles the report interactivity that URL access provides.  
  
 For more information about the Reporting Services SOAP API, see [Report Server Web Service](../../reporting-services/report-server-web-service/report-server-web-service.md).  
  
 In the majority of cases, URL access and SOAP calls are both required to meet your reporting needs. SOAP is used when initially connecting to the report server database and presenting the available list of reports in a user interface and URL access is used to actually access and navigate individual reports.  
  
 For an example of combining URL access and the Web service to provide integrated reporting, see [SQL Server Reporting Services Product Samples](https://go.microsoft.com/fwlink/?LinkId=177889).

More questions? [Try asking the Reporting Services forum](https://go.microsoft.com/fwlink/?LinkId=620231)
