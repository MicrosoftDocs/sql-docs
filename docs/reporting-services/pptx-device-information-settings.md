---
title: "PPTX device information settings"
description: Learn details about the device information settings for rendering Reporting Services reports in the PPTX format.
author: maggiesMSFT
ms.author: maggies
ms.date: 09/25/2024
ms.service: reporting-services
ms.subservice: reporting-services
ms.topic: conceptual
ms.custom:
  - updatefrequency5
helpviewer_keywords:
  - "render"
  - "powerpoint"
  - "pptx"
  - "export"
---
# PPTX device information settings
  The following table lists the device information settings for rendering [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] reports in the PPTX format.  
  
|Setting|Value|  
|-------------|-----------|  
|**Columns**|The number of columns to set for the report. This value overrides the report's original settings.|  
|**ColumnSpacing**|The column spacing to set for the report. This value overrides the report's original settings.|  
|**DpiX**|The horizontal resolution of the output image. The default value is **96**. Applies to **BMP**, **GIF**, **PNG**, and **TIFF** output formats.|  
|**DpiY**|The vertical resolution of the output image. The default value is **96**. Applies to **BMP**, **GIF**, **PNG**, and **TIFF** output formats.|  
|**EndPage**|The last page of the report to render. The default value is the value for **StartPage**.|  
|**MarginBottom**|The bottom margin value, in inches, to set for the report. You must include an integer or decimal value followed by "in" (for example, **1in**). This value overrides the report's original settings.|  
|**MarginLeft**|The left margin value, in inches, to set for the report. You must include an integer or decimal value followed by "in" (for example, **1in**). This value overrides the report's original settings.|  
|**MarginRight**|The right margin value, in inches, to set for the report. You must include an integer or decimal value followed by "in" (for example, **1in**). This value overrides the report's original settings.|  
|**MarginTop**|The top margin value, in inches, to set for the report. You must include an integer or decimal value followed by "in" (for example, **1in**). This value overrides the report's original settings.|  
|**PageHeight**|The page height, in inches, to set for the report. You must include an integer or decimal value followed by "in" (for example, **11in**). This value overrides the report's original settings.|  
|**PageWidth**|The page width, in inches, to set for the report. You must include an integer or decimal value followed by "in" (for example, **8.5in**). This value overrides the report's original settings.|  
|**StartPage**|The first page of the report to render. A value of **0** indicates that all pages are rendered. The default value is **1**.|  
|**UseReportPageSize**|If UseReportPageSize =**false** then the default slide size is PowerPoint's default of 13.333" x 7.5" (16:9 aspect ratio). If UseReportPageSize =true, then the default slide size is the defined page size of the report.<br /><br /> The default value is **false**<br /><br /> Note, the PageWidth and PageHeight settings override the default width and height.|  
  
## Related content

- [Pass device information settings to rendering extensions](../reporting-services/report-server-web-service/net-framework/passing-device-information-settings-to-rendering-extensions.md)
- [Customize rendering extension parameters in RSReportServer.Config](../reporting-services/customize-rendering-extension-parameters-in-rsreportserver-config.md)
- [Technical reference &#40;SSRS&#41;](../reporting-services/technical-reference-ssrs.md)
