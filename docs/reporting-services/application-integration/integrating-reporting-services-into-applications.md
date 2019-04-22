---
title: "Integrating Reporting Services into Applications | Microsoft Docs"
ms.date: 09/18/2018
ms.prod: reporting-services
ms.prod_service: "reporting-services-native"
ms.technology: application-integration
ms.topic: reference
author: maggiesMSFT
ms.author: maggies
monikerRange: "= sql-server-2016 || = sqlallproducts-allversions"

---
# Integrating Reporting Services into Applications

[!INCLUDE [ssrs-appliesto](../../includes/ssrs-appliesto.md)] [!INCLUDE[ssrs-appliesto-2016](../../includes/ssrs-appliesto-2016.md)] [!INCLUDE[ssrs-appliesto-not-2017](../../includes/ssrs-appliesto-not-2017.md)] [!INCLUDE [ssrs-appliesto-not-pbirs](../../includes/ssrs-appliesto-not-pbirs.md)]

  [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] is an open and extensible reporting platform designed to provide developers with a comprehensive set of APIs for developing solutions.

> [!NOTE]
> Starting with SQL Server 2017 Reporting Services, REST API access is available for developing solutions. SOAP API access has been deprecated. For more information, see [Develop with the REST APIs for Reporting Services](../developer/rest-api.md).
  
 There are three options for integrating [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] into custom applications: the Report Server Web service, also known as the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] SOAP API, the Report Viewer controls for [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[vsprvs](../../includes/vsprvs-md.md)], and URL access. Each option provides a different approach for integrating [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] into your applications.
  
## Report server web service

 The Report Server Web service is the primary interface for developing against [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)]. Whether you are developing code to manage your report catalog or developing code to render reports to a supported format, the Web service exposes all the necessary methods to integrate [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] into your applications. An example of one such application is Report Manager, which is included with [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)]; it uses the Web service to manage the report server database.  
  
## Report Viewer controls for Visual Studio

 The Report Viewer controls available for [!INCLUDE[vsprvs](../../includes/vsprvs-md.md)] are used for integrating report viewing into your applications. There are two controls: one for Windows Forms-based applications and one for Web Forms applications. Each control provides the capability for viewing reports that have been deployed to a report server as well as the ability to render reports that exist in an environment where a report server has not been installed.  
  
## URL access  
 URL access is another option for integrating report viewing into your applications if the Report Viewer controls are not an option. In addition, URL access is useful for sending links to reports to users via e-mail.  
  
## In this section

 [Integrating Reporting Services Using SOAP](../../reporting-services/application-integration/integrating-reporting-services-using-soap.md)  
 Describes how to integrate [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] report navigation and management into your existing business applications using the Report Server Web service.  
  
 [Integrating Reporting Services Using the Report Viewer Controls](../../reporting-services/application-integration/integrating-reporting-services-using-reportviewer-controls.md)  
 Describes how to integrate report viewing into your existing applications using the Report Viewer controls.  
  
 [Integrating Reporting Services Using URL Access](../../reporting-services/application-integration/integrating-reporting-services-using-url-access.md)  
 Describes how to integrate [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] report navigation into your existing business applications using URL access.  
  
## Next steps

For deciding on using URL access or the SOAP APIs, see [Choosing between URL access and SOAP in Reporting Services](choosing-between-url-access-and-soap.md).

For information on the SQL Server 2017 Reporting Services REST API, see [Develop with the REST APIs for Reporting Services](../developer/rest-api.md).

More questions? [Try asking the Reporting Services forum](https://go.microsoft.com/fwlink/?LinkId=620231)
