---
title: "Rendering Extensions Overview | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: reporting-services
ms.topic: "reference"
helpviewer_keywords: 
  - "formats [Reporting Services], rendering extensions"
  - "rendering extensions [Reporting Services], about extensions"
ms.assetid: 909356a0-4709-43e5-b597-33bd9bb22882
author: maggiesMSFT
ms.author: maggies
manager: kfile
---
# Rendering Extensions Overview
  A rendering extension is a component or module of a report server that transforms report data and layout information into a device-specific format. [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] includes seven rendering extensions: HTML, Excel, Word, CSV or Text, XML, Image, and PDF. You can create additional rendering extensions to generate reports in other formats.  
  
> [!NOTE]  
>  To determine which rendering extensions are available, you can view the list of installed extensions in the RSReportServer.config file.  
  
 The following table describes the rendering extensions that are included with [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)].  
  
|Extension Name|Description|  
|--------------------|-----------------|  
|`XML`|Renders a report in XML. The report opens in a browser. Additional transformations applied to this XML output may be a cost effective way of avoiding developing your own rendering extension.|  
|`CSV`|Renders a report in comma-delimited format. The report opens in a viewing tool associated with CSV file formats.|  
|`IMAGE`|Renders a report in a page-oriented format. The format is shown as **TIFF** in the Export drop-down of the report toolbar.|  
|`PDF`|Renders a report in the Adobe Acrobat Reader. The format is shown as **Acrobat (PDF) File** in the Export drop-down of the report toolbar.|  
|`EXCEL`|Renders a report in [!INCLUDE[ofprexcel](../../../includes/ofprexcel-md.md)].|  
|`WORD`|Render a report in [!INCLUDE[ofprword](../../../includes/ofprword-md.md)].|  
|`HTML 4.0` (part of the HTML rendering extension)|HTML is the format used to initially render the report. If your browser support HTML 4.0, that is the format that is used. Otherwise, HTML 3.2 is used.|  
|`MHTML` (part of the HTML rendering extension)|Renders a report in MHTML. The report opens in Internet Explorer. The format is shown as **Web Archive** in the Export drop-down of the report toolbar.|  
|`NULL`|Does not render a report to a specific format. This rendering extension is useful for placing reports in cache. Null rendering should be used in conjunction with a scheduled execution or delivery.|  
  
 For more information on the recommended formats and their uses, see [Exporting Reports &#40;Report Builder and SSRS&#41;](../../report-builder/export-reports-report-builder-and-ssrs.md).  
  
 Each of the rendering extensions implemented by [!INCLUDE[msCoName](../../../includes/msconame-md.md)] and shipped with [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] uses a common set of interfaces. This ensures that each extension implements comparable functionality and reduces the complexity of the rendering code in the core of the report server.  
  
## Rendering Object Model  
 When a report is processed, the result is a publicly exposed object model known as the Rendering Object Model (ROM). The Rendering Object Model is a collection of classes that define the contents, layout, and data of a report that has been processed. The ROM is available to developers who wish to design, develop, and deploy custom rendering extensions for [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)]. ROM is produced when the report server processes a report's XML definition along with the user-defined report data. When processing is complete, the public object model is used by a rendering extension to define the output of the report. The ROM's available public classes are defined in the `Microsoft.ReportingServices.OnDemandReportRendering` namespace.  
  
## Writing Custom Rendering Extensions  
 Before you decide to create a custom rendering extension, you should evaluate simpler alternatives. You can:  
  
-   Customize rendered output by specifying device information settings for existing extensions.  
  
-   Add custom formatting and presentation features by combining XSL Transformations (XSLT) with the output of the XML rendering format.  
  
 Writing a custom rendering extension is difficult. A rendering extension must typically support all possible combinations of report elements and requires that you implement hundreds of classes, interfaces, methods, and properties. If you must render a report in a format that is not included with [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] and decide to write your own managed code implementation of a rendering extension, the rendering extension code must implement the `Microsoft.ReportingServices.OnDemandReportRendering.IRenderingExtension` interface, which is required by the report server.  
  
 For supplemental documentation and whitepapers on [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)], see the latest technical resources at the [Reporting Services Web site](https://go.microsoft.com/fwlink/?LinkId=19951).  
  
## See Also  
 [Implementing a Rendering Extension](implementing-a-rendering-extension.md)   
 [Reporting Services Extension Library](../reporting-services-extension-library.md)  
  
  
