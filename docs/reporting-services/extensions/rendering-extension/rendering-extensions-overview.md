---
title: "Rendering extensions overview"
description: See which data rendering extensions are included with Reporting Services. Learn how to add custom rendering extensions to generate reports in other formats.
author: maggiesMSFT
ms.author: maggies
ms.date: 09/25/2024
ms.service: reporting-services
ms.subservice: extensions
ms.topic: reference
ms.custom:
  - updatefrequency5
helpviewer_keywords:
  - "formats [Reporting Services], rendering extensions"
  - "rendering extensions [Reporting Services], about extensions"
---

# Rendering extensions overview
  A rendering extension is a component or module of a report server that transforms report data and layout information into a device-specific format. [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] includes several rendering extensions: HTML, Excel, Word, PowerPoint, CSV or Text, XML, Image, Data Feed and PDF. You can create other rendering extensions to generate reports in other formats.
  
> [!NOTE]  
>  To determine which rendering extensions are available, you can view the list of installed extensions in the RSReportServer.config file.  
  
 The following table describes the rendering extensions that are included with [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)].  

|Extension Name|Description|
|--------------------|-----------------|
|**WORDOPENXML**|Render a report in [!INCLUDE[ofprword](../../../includes/ofprword-md.md)] as a DOCX file. The format is shown as **Word** in the Export drop-down of the report toolbar.|  
|**WORD**|Render a report in [!INCLUDE[ofprword](../../../includes/ofprword-md.md)] as a DOC file. The format isn't shown in the Export drop-down of the report toolbar as it is included for backward compatibility.|  
|**EXCELOPENXML**|Renders a report in [!INCLUDE[ofprexcel](../../../includes/ofprexcel-md.md)] as an XLSX file. The format is shown as **Excel** in the Export drop-down of the report toolbar.|
|**EXCEL**|Renders a report in [!INCLUDE[ofprexcel](../../../includes/ofprexcel-md.md)] as an XLS file. The format isn't shown in the Export drop-down of the report toolbar as it is included for backward compatibility.| 
|**PPTX**|Renders a report in PowerPoint as a PPTX file. The format is shown as **PowerPoint** in the Export drop-down of the report toolbar.|
|**PDF**|Renders a report in the PDF.|  
|**IMAGE**|Renders a report in a page-oriented format. The format is shown as **TIFF file** in the Export drop-down of the report toolbar.| 
|**MHTML** (part of the HTML rendering extension)|Renders a report in MHTML. The report opens in the browser. The format is shown as **MHTML (web archive)** in the Export drop-down of the report toolbar.|   
|**CSV**|Renders a report in comma-delimited format. The report opens in a viewing tool associated with CSV file formats. The format is shown as **CSV (comma delimited)** in the Export drop-down of the report toolbar.|  
|**XML**|Renders a report in XML. The report opens in a browser. Extra transformations applied to this XML output might be a cost effective way of avoiding developing your own rendering extension. The format is shown as **XML file with report data** in the Export drop-down of the report toolbar.|
|**ATOM**|Renders a report in data feed format as an ATOMSVC file. The format is shown as **Data feed** in the Export drop-down of the report toolbar.|
|**HTML4.0** (part of the HTML rendering extension)|HTML4.0 is used if the browser doesn't support HTML5.| 
|**HTML5** (part of the HTML rendering extension)|HTML5 is the format used to initially render the report in the browser. If your browser supports HTML5, that is the format that is used. Otherwise, HTML 4.0 is used.| 
|**RPL**|RPL is the intermediate format used by the report server and is the default rendering extension.|
|**NULL**|Doesn't render a report to a specific format. This rendering extension is useful for placing reports in cache. Null rendering should be used with a scheduled execution or delivery.|  
  
 For more information on the recommended formats and their uses, see [Export reports &#40;Report Builder and SSRS&#41;](../../../reporting-services/report-builder/export-reports-report-builder-and-ssrs.md).  
  
 Each of the rendering extensions implemented by [!INCLUDE[msCoName](../../../includes/msconame-md.md)] and shipped with [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] uses a common set of interfaces. This common set of interfaces ensures that each extension implements comparable functionality and reduces the complexity of the rendering code in the core of the report server.  
  
## Rendering Object Model  
 When a report is processed, the result is a publicly exposed object model known as the Rendering Object Model (ROM). The Rendering Object Model is a collection of classes that define the contents, layout, and data of a report that has been processed. The ROM is available to developers who wish to design, develop, and deploy custom rendering extensions for [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)]. ROM is produced when the report server processes a report's XML definition along with the user-defined report data. When processing is complete, the public object model is used by a rendering extension to define the output of the report. The ROM's available public classes are defined in the **Microsoft.ReportingServices.OnDemandReportRendering** namespace.  
  
## Write custom rendering extensions  
 Before you decide to create a custom rendering extension, you should evaluate simpler alternatives. You can:  
  
-   Customize rendered output by specifying device information settings for existing extensions.  
  
-   Add custom formatting and presentation features by combining XSL Transformations (XSLT) with the output of the XML rendering format.  
  
 Writing a custom rendering extension is difficult. A rendering extension must typically support all possible combinations of report elements and requires that you implement hundreds of classes, interfaces, methods, and properties. If you must render a report in a format that isn't included with [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] and decide to write your own managed code implementation of a rendering extension, the rendering extension code must implement the **Microsoft.ReportingServices.OnDemandReportRendering.IRenderingExtension** interface, which is required by the report server.  
  
## Related content

- [Implement a rendering extension](../../../reporting-services/extensions/rendering-extension/implementing-a-rendering-extension.md)
- [Reporting Services extension library](../../../reporting-services/extensions/reporting-services-extension-library.md)
