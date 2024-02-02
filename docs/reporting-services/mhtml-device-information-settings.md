---
title: "MHTML device information settings"
description: Learn about the various device information settings for rendering reports in Web archive (MHTML) format.
author: maggiesMSFT
ms.author: maggies
ms.date: 03/16/2017
ms.service: reporting-services
ms.subservice: reporting-services
ms.topic: conceptual
ms.custom: updatefrequency5
helpviewer_keywords:
  - "device information settings [Reporting Services], MHTML rendering"
  - "MHTML [Reporting Services]"
---
# MHTML device information settings
  The following table lists the device information settings for rendering reports in Web archive (MHTML) format.  
  
|Setting|Value|  
|-------------|-----------|  
|**JavaScript**|Indicates whether JavaScript is supported in the rendered report.|  
|**OutlookCompat**|Indicates whether to render with extra metadata that makes the report look better in Outlook. The default value is **true**.|  
|**MHTML Fragment**|Indicates whether an MHTML fragment is created in place of a full MHTML document. An MHTML fragment includes the report content in a TABLE element and omits the HTML and BODY elements. The default value is **false**.|  
|**DataVisualizationFitSizing**|Indicates data visualization fit behavior when inside a tablix. The types include chart, gauge, and map.<br /><br /> The possible values are **Approximate** and **Exact**.<br /><br /> The default value is **Approximate**. If the setting is removed from the rsreportserver.config file, then the default behavior is **Exact**.<br /><br /> Enabling **Exact** might have performance effects because the processing to determine the exact size might take longer.|  
  
## Related content

- <xref:ReportExecution2005.ReportExecutionService.Render%2A>   
- [Pass device information settings to rendering extensions](../reporting-services/report-server-web-service/net-framework/passing-device-information-settings-to-rendering-extensions.md)   
- [Customize rendering extension parameters in RSReportServer.Config](../reporting-services/customize-rendering-extension-parameters-in-rsreportserver-config.md)   
- [Technical reference &#40;SSRS&#41;](../reporting-services/technical-reference-ssrs.md)  
  
  
