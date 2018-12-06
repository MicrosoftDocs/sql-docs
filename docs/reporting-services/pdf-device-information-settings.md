---
title: "PDF Device Information Settings | Microsoft Docs"
ms.date: 03/16/2018
ms.prod: reporting-services
ms.prod_service: "reporting-services-sharepoint, reporting-services-native"
ms.technology: reporting-services


ms.topic: conceptual
helpviewer_keywords: 
  - "device information settings [Reporting Services], PDF rendering"
  - "PDF [Reporting Services]"
ms.assetid: 9a4aabe5-dbdc-4884-b999-1200983fee47
author: markingmyname
ms.author: maghan
---
# PDF Device Information Settings
  The following table lists the device information settings for rendering reports in PDF format.  
  
|Setting|Value|  
|-------------|-----------|  
| **AccessiblePDF** | Indicates whether to render an accessible/tagged PDF, which is larger in size but easier for screen readers and other assistive technologies to read and navigate. The default value is **false**. [Available in Power BI Report Server (March 2018) and later] |
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
  
## See Also  
 [Passing Device Information Settings to Rendering Extensions](../reporting-services/report-server-web-service/net-framework/passing-device-information-settings-to-rendering-extensions.md)   
 [Customize Rendering Extension Parameters in RSReportServer.Config](../reporting-services/customize-rendering-extension-parameters-in-rsreportserver-config.md)   
 [Technical Reference &#40;SSRS&#41;](../reporting-services/technical-reference-ssrs.md)  
  
  
