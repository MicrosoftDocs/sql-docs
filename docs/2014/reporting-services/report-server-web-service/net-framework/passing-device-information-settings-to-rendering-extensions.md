---
title: "Passing Device Information Settings to Rendering Extensions | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "docset-sql-devref"
  - "reporting-services-native"
ms.topic: "reference"
helpviewer_keywords: 
  - "device information settings [Reporting Services]"
  - "Render method"
  - "Report Server Web service, device information settings"
  - "Web service [Reporting Services], device information settings"
  - "XML Web service [Reporting Services], device information settings"
  - "passing device information [Reporting Services]"
  - "rendering extensions [Reporting Services], device information settings"
  - "rendering [Reporting Services], settings"
  - "device information settings [Reporting Services], about device information settings"
  - "extensions [Reporting Services], device information settings"
ms.assetid: fe718939-7efe-4c7f-87cb-5f5b09caeff4
author: maggiesMSFT
ms.author: maggies
manager: kfile
---
# Passing Device Information Settings to Rendering Extensions
  In [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)], device information settings are used to pass rendering parameters to a rendering extension. Settings in the Report Server Web service are passed as a **DeviceInfo** XML element and processed by the report server. Because device information settings have default values, they are considered optional arguments in the rendering process. However, you can use device information settings to customize rendering and to override the default values that are supplied by the server.  
  
 You can specify device information settings in a variety of ways. Programmatically, you can use the Render method. If you are accessing a report through its URL, you can specify device information as URL parameters. You can also edit the device information settings in the [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] configuration files to specify rendering parameters globally. For more information about specifying rendering parameters globally, see [Customize Rendering Extension Parameters in RSReportServer.Config](../../customize-rendering-extension-parameters-in-rsreportserver-config.md).  
  
## Passing Device Information Using the Render Method  
 To pass device information settings to a rendering extension, use the <xref:ReportExecution2005.ReportExecutionService.Render%2A> method. For example, the following XML string can be passed to the <xref:ReportExecution2005.ReportExecutionService.Render%2A> method to create an HTML fragment when rendering to HTML.  
  
```  
<DeviceInfo>  
   <HTMLFragment>True</HTMLFragment>  
</DeviceInfo>  
```  
  
 When a report is rendered as an HTML fragment, the content of the report is contained within a TABLE element without the use of an HTML or BODY element. You can use the HTML fragment to incorporate the report into an existing HTML document. For more information about device information settings for HTML output, see [HTML Device Information Settings](../../html-device-information-settings.md).  
  
## Passing Device Information Using URL Access  
 You can also pass device information settings through URL access. Device information settings are passed as URL parameters. The following URL access string can be passed to the report server to generate a rendered report without the HTML viewer toolbar.  
  
```  
http://<Server Name>/reportserver?/SampleReports/Sales Order Detail&rs:Command=Render&rs:Format=HTML4.0&rc:Toolbar=False  
```  
  
 For more information, see [Specify Device Information Settings in a URL](../../specify-device-information-settings-in-a-url.md).  
  
## See Also  
 [Device Information Settings for Rendering Extensions &#40;Reporting Services&#41;](../../device-information-settings-for-rendering-extensions-reporting-services.md)   
 [Customize Rendering Extension Parameters in RSReportServer.Config](../../customize-rendering-extension-parameters-in-rsreportserver-config.md)   
 [Building Applications Using the Web Service and the .NET Framework](building-applications-using-the-web-service-and-the-net-framework.md)  
  
  
