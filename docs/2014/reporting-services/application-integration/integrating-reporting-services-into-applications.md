---
title: "Integrating Reporting Services into Applications | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: reporting-services
ms.topic: "reference"
helpviewer_keywords: 
  - "integrating reports [Reporting Services]"
  - "custom applications [SSRS]"
  - "Reporting Services, application integration"
  - "application integration [Reporting Services]"
  - "reports [Reporting Services], integrating"
ms.assetid: 49eb539c-d89b-4291-839a-0ec1a65cd270
author: maggiesMSFT
ms.author: maggies
manager: kfile
---
# Integrating Reporting Services into Applications
  [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] is an open and extensible reporting platform designed to provide developers with a comprehensive set of APIs for developing solutions.  
  
 There are three options for integrating [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] into custom applications: the Report Server Web service, also known as the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] SOAP API, the ReportViewer controls for [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[vsOrcas](../../includes/vsorcas-md.md)], and URL access. Each option provides a different approach for integrating [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] into your applications.  
  
## Report Server Web Service  
 The Report Server Web service is the primary interface for developing against [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)]. Whether you are developing code to manage your report catalog or developing code to render reports to a supported format, the Web service exposes all the necessary methods to integrate [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] into your applications. An example of one such application is Report Manager, which is included with [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)]; it uses the Web service to manage the report server database.  
  
## ReportViewer Controls for Visual Studio  
 The ReportViewer controls included with [!INCLUDE[vsOrcas](../../includes/vsorcas-md.md)] are used for integrating report viewing into your applications. There are two controls: one for Windows Forms-based applications and one for Web Forms applications. Each control provides the capability for viewing reports that have been deployed to a report server as well as the ability to render reports that exist in an environment where a report server has not been installed.  
  
## URL Access  
 URL access is another option for integrating report viewing into your applications if the ReportViewer controls are not an option. In addition, URL access is useful for sending links to reports to users via e-mail.  
  
## In This Section  
 [Integrating Reporting Services Using SOAP](../application-integration/integrating-reporting-services-using-soap.md)  
 Describes how to integrate [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] report navigation and management into your existing business applications using the Report Server Web service.  
  
 [Integrating Reporting Services Using the ReportViewer Controls](../application-integration/integrating-reporting-services-using-reportviewer-controls.md)  
 Describes how to integrate report viewing into your existing applications using the ReportViewer controls.  
  
 [Integrating Reporting Services Using URL Access](../application-integration/integrating-reporting-services-using-url-access.md)  
 Describes how to integrate [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] report navigation into your existing business applications using URL access.  
  
## See Also  
 [Report Manager  &#40;SSRS Native Mode&#41;](../../../2014/reporting-services/report-manager-ssrs-native-mode.md)   
 [Choosing Between URL Access and SOAP](../../../2014/reporting-services/application-integration/choosing-between-url-access-and-soap.md)   
 [Technical Reference &#40;SSRS&#41;](../../../2014/reporting-services/technical-reference-ssrs.md)   
 [Report Server Web Service](../report-server-web-service/report-server-web-service.md)  
  
  
