---
title: "Excel Device Information Settings | Microsoft Docs"
ms.custom: ""
ms.date: "03/08/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
helpviewer_keywords: 
  - "device information settings [Reporting Services], Excel rendering"
  - "Excel [Reporting Services], rendering"
ms.assetid: bb5f3566-f033-4470-be87-1f52fb7a4ab6
author: maggiesMSFT
ms.author: maggies
manager: kfile
---
# Excel Device Information Settings
  The following table lists the device information settings for rendering in [!INCLUDE[ofprexcel](../includes/ofprexcel-md.md)] format.  
  
|Setting|Value|  
|-------------|-----------|  
|**OmitDocumentMap**|Indicates whether to omit the document map for reports that support it. The default value is `false`.|  
|**OmitFormulas**|Indicates whether to omit formulas from the rendered report. The default value is `false`.|  
|`SimplePageHeade`rs|Indicates whether the page header of the report is rendered to the Excel page header. A value of `false` indicates that the page header is rendered to the first row of the worksheet. The default value is `false`.|  
  
## See Also  
 <xref:ReportExecution2005.ReportExecutionService.Render%2A>   
 [Passing Device Information Settings to Rendering Extensions](report-server-web-service/net-framework/passing-device-information-settings-to-rendering-extensions.md)   
 [Customize Rendering Extension Parameters in RSReportServer.Config](customize-rendering-extension-parameters-in-rsreportserver-config.md)   
 [Technical Reference &#40;SSRS&#41;](../../2014/reporting-services/technical-reference-ssrs.md)  
  
  
