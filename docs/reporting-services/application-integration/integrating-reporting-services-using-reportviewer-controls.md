---
title: "Integrate using Report Viewer controls"
description: Visual Studio provides two Report Viewer controls for integrating report viewing functionality into your applications.
author: maggiesMSFT
ms.author: maggies
ms.date: 09/18/2018
ms.service: reporting-services
ms.subservice: application-integration
ms.topic: reference
ms.custom: updatefrequency5
helpviewer_keywords:
  - "Report Viewer controls"
  - "integrating reports [Reporting Services]"
---
# Integrate Reporting Services by using Report Viewer controls
  [!INCLUDE[msCoName](../../includes/msconame-md.md)] Visual Studio 2015 provides two Report Viewer controls for integrating report viewing functionality into your applications. There's a version for Windows Forms-based applications and one for Web Forms applications. Each control provides similar functionality but each is designed to target their individual environments. Both controls can process reports that are deployed to a report server (remote processing mode) or are copied to a computer where [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] isn't installed (local processing mode).  
  
 The Report Viewer control doesn't include built-in support for dynamically adapting to different devices with different screen resolutions.  
  
## Remote processing mode  
 Remote processing mode is the preferred method for viewing reports that are deployed to a report server. Remote processing mode provides the following advantages:  
  
-   Remote processing provides an optimized solution for running reports because the report server processes the report.  
  
-   Because the report server handles all processing, a report request can be processed by multiple report servers in a scale-out deployment or a server that has multiple processors in a scale-up scenario.  
  
 In addition, reports run in remote mode can utilize the full functionality of the report server including all rendering and data extensions.  
  
> [!NOTE]  
>  The list of extensions available to the Report Viewer control when it is running in remote processing mode depends on the edition of [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] that is installed on the report server.  
  
## Local processing mode  
 Local processing mode provides an alternative method for viewing and rendering reports when [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] isn't installed. Unlike remote processing, only a subset of the functionality provided by the report server is available in the control. In local processing mode, data processing isn't handled by the control but rather implemented by the hosting application. However report processing is handled by the control itself. In local processing mode, only the PDF, Excel, Word, and Image rendering extensions are available.  
  
## Related content  
 [Integrating Reporting Services into Applications](../../reporting-services/application-integration/integrating-reporting-services-into-applications.md)   
 [Using the WebForms Report Viewer Control](../../reporting-services/application-integration/using-the-webforms-reportviewer-control.md)   
 [Using the WinForms Report Viewer Control](../../reporting-services/application-integration/using-the-winforms-reportviewer-control.md)  

  
  
