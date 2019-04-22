---
title: "PDF Device Information Settings | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
helpviewer_keywords: 
  - "device information settings [Reporting Services], PDF rendering"
  - "PDF [Reporting Services]"
ms.assetid: 9a4aabe5-dbdc-4884-b999-1200983fee47
author: maggiesMSFT
ms.author: maggies
manager: kfile
---
# PDF Device Information Settings
  The following table lists the device information settings for rendering reports in PDF format.  
  
|Setting|Value|  
|-------------|-----------|  
|**Columns**|The number of columns to set for the report. This value overrides the report's original settings.|  
|**ColumnSpacing**|The column spacing to set for the report. This value overrides the report's original settings.|  
|**DpiX**|The resolution of the output device in x-direction.|  
|**DpiY**|The resolution of the output device in y-direction.|  
|**EndPage**|The last page of the report to render. The default value is the value for `StartPage`.|  
|`HumanReadablePDF`|Indicates whether the PDF should be compressed, which allows the source to be more readable. The default value is `false.`|  
|**MarginBottom**|The bottom margin value, in inches, to set for the report. You must include an integer or decimal value followed by "in" (for example, 1in). This value overrides the report's original settings.|  
|**MarginLeft**|The left margin value, in inches, to set for the report. You must include an integer or decimal value followed by "in" (for example, 1in). This value overrides the report's original settings.|  
|**MarginRight**|The right margin value, in inches, to set for the report. You must include an integer or decimal value followed by "in" (for example, 1in). This value overrides the report's original settings.|  
|**MarginTop**|The top margin value, in inches, to set for the report. You must include an integer or decimal value followed by "in" (for example, 1in). This value overrides the report's original settings.|  
|**PageHeight**|The page height, in inches, to set for the report. You must include an integer or decimal value followed by "in" (for example, 11in). This value overrides the report's original settings.|  
|**PageWidth**|The page width, in inches, to set for the report. You must include an integer or decimal value followed by "in" (for example, 8.5in). This value overrides the report's original settings.|  
|`StartPage`|The first page of the report to render. A value of `0` indicates that all pages are rendered. The default value is `1`.|  
  
## See Also  
 [Passing Device Information Settings to Rendering Extensions](report-server-web-service/net-framework/passing-device-information-settings-to-rendering-extensions.md)   
 [Customize Rendering Extension Parameters in RSReportServer.Config](customize-rendering-extension-parameters-in-rsreportserver-config.md)   
 [Technical Reference &#40;SSRS&#41;](../../2014/reporting-services/technical-reference-ssrs.md)  
  
  
