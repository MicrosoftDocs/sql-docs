---
title: "PDF device information settings"
description: Learn about the device information settings that are available for rendering reports in PDF format.
author: maggiesMSFT
ms.author: maggies
ms.date: 03/16/2018
ms.service: reporting-services
ms.subservice: reporting-services
ms.topic: conceptual
ms.custom: updatefrequency5
helpviewer_keywords:
  - "device information settings [Reporting Services], PDF rendering"
  - "PDF [Reporting Services]"
---
# PDF device information settings
  The following table lists the device information settings for rendering reports in PDF format.  
  
|Setting|Value|  
|-------------|-----------|  
| **AccessiblePDF** | Indicates whether to render an accessible/tagged PDF. The PDF is larger in size but easier for screen readers and other assistive technologies to read and navigate. The default value is **false**. [Available in Power BI Report Server (March 2018) and later] |
|**Columns**|The number of columns to set for the report. This value overrides the report's original settings.|  
|**ColumnSpacing**|The column spacing to set for the report. This value overrides the report's original settings.|  
|**DpiX**|The resolution of the output device in x-direction.|  
|**DpiY**|The resolution of the output device in y-direction.|  
|**EndPage**|The last page of the report to render. The default value is the value for **StartPage**.|  
|**HumanReadablePDF**|Indicates whether to render an uncompressed PDF file, which is larger in size but more human-readable in a plain-text editor. The default value is **false.**|  
|**MarginBottom**|The bottom margin value, in inches, to set for the report. You must include an integer or decimal value followed by "in" (for example, 1in). This value overrides the report's original settings.|  
|**MarginLeft**|The left margin value, in inches, to set for the report. You must include an integer or decimal value followed by "in" (for example, 1in). This value overrides the report's original settings.|  
|**MarginRight**|The right margin value, in inches, to set for the report. You must include an integer or decimal value followed by "in" (for example, 1in). This value overrides the report's original settings.|  
|**MarginTop**|The top margin value, in inches, to set for the report. You must include an integer or decimal value followed by "in" (for example, 1in). This value overrides the report's original settings.|  
|**PageHeight**|The page height, in inches, to set for the report. You must include an integer or decimal value followed by "in" (for example, 11in). This value overrides the report's original settings.|  
|**PageWidth**|The page width, in inches, to set for the report. You must include an integer or decimal value followed by "in" (for example, 8.5in). This value overrides the report's original settings.|  
|**StartPage**|The first page of the report to render. A value of **0** indicates that all pages are rendered. The default value is **1**.|  
  
## Related content

- [Pass device information settings to rendering extensions](../reporting-services/report-server-web-service/net-framework/passing-device-information-settings-to-rendering-extensions.md)   
- [Customize rendering extension parameters in RSReportServer.Config](../reporting-services/customize-rendering-extension-parameters-in-rsreportserver-config.md)   
- [Technical reference &#40;SSRS&#41;](../reporting-services/technical-reference-ssrs.md)  
  
  
