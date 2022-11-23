---
title: "Excel Device Information Settings | Microsoft Docs"
description: Learn details about the various device information settings for rendering in Microsoft Excel format.
ms.date: 01/23/2020
ms.service: reporting-services
ms.subservice: reporting-services


ms.topic: conceptual
helpviewer_keywords: 
  - "device information settings [Reporting Services], Excel rendering"
  - "Excel [Reporting Services], rendering"
ms.assetid: bb5f3566-f033-4470-be87-1f52fb7a4ab6
author: maggiesMSFT
ms.author: maggies
---
# Excel Device Information Settings
  The following table lists the device information settings for rendering in [!INCLUDE[ofprexcel](../includes/ofprexcel-md.md)] format.  
  
|Setting|Value|  
|-------------|-----------|  
|**OmitDocumentMap**|Indicates whether to omit the document map for reports that support it. The default value is **false**.|  
|**OmitFormulas**|Indicates whether to omit formulas from the rendered report. The default value is **false**.|  
|**SimplePageHeaders**|Indicates whether the page header of the report is rendered to the Excel page header. A value of **false** indicates that the page header is rendered to the first row of the worksheet. The default value is **false**.|  
|**DynamicImageDpi**|The resolution of dynamic images like charts, gauges, and maps. The default value is **96**. (Available in Power BI Report Server (January 2020) and later)|  

  
## See Also  
 <xref:ReportExecution2005.ReportExecutionService.Render%2A>   
 [Passing Device Information Settings to Rendering Extensions](../reporting-services/report-server-web-service/net-framework/passing-device-information-settings-to-rendering-extensions.md)   
 [Customize Rendering Extension Parameters in RSReportServer.Config](../reporting-services/customize-rendering-extension-parameters-in-rsreportserver-config.md)   
 [Technical Reference &#40;SSRS&#41;](../reporting-services/technical-reference-ssrs.md)  
  
  
