---
title: "ATOM Device Information Settings | Microsoft Docs"
ms.date: 03/16/2017
ms.prod: reporting-services
ms.prod_service: "reporting-services-sharepoint, reporting-services-native"
ms.technology: reporting-services


ms.topic: conceptual
ms.assetid: fe4a56a4-5552-423c-85c1-895e2e212fee
author: maggiesMSFT
ms.author: maggies
---
# ATOM Device Information Settings
  The device information settings for the Atom rendering extension support submittal of the name of an Atom feed and character encoding to use.  
  
 The following table lists the device information settings for rendering to a data service document.  
  
|Setting|Value|  
|-------------|-----------|  
|**DataFeed**|If specified, renders the Atom feed corresponding to the feed name provided in this setting. If not, renders the Atom service document for the report. The unique auto-generated identifier of the data feed. This  value is used internally and you should not change it.|  
|**Encoding**|The Internet Assigned Numbers Authority (IANA) name of a character encoding that is supported by the .NET Framework. The default value is **UTF-8**. Examples of other values include ASCII, UTF-7, and UTF-16.|  
  
## See Also  
 <xref:ReportExecution2005.ReportExecutionService.Render%2A>   
 [Passing Device Information Settings to Rendering Extensions](../reporting-services/report-server-web-service/net-framework/passing-device-information-settings-to-rendering-extensions.md)   
 [Customize Rendering Extension Parameters in RSReportServer.Config](../reporting-services/customize-rendering-extension-parameters-in-rsreportserver-config.md)   
 [Technical Reference &#40;SSRS&#41;](../reporting-services/technical-reference-ssrs.md)  
  
  
