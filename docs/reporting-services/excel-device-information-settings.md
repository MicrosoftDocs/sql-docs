---
title: "Excel device information settings"
description: Learn details about the various device information settings for rendering in Microsoft Excel format.
ms.date: 09/25/2024
ms.service: reporting-services
ms.subservice: reporting-services
ms.topic: concept-article
ms.custom:
  - updatefrequency5
helpviewer_keywords:
  - "device information settings [Reporting Services], Excel rendering"
  - "Excel [Reporting Services], rendering"
---
# Excel device information settings
  The following table lists the device information settings for rendering in [!INCLUDE[ofprexcel](../includes/ofprexcel-md.md)] format.  
  
|Setting|Value|  
|-------------|-----------|  
|**OmitDocumentMap**|Indicates whether to omit the document map for reports that support it. The default value is **false**.|  
|**OmitFormulas**|Indicates whether to omit formulas from the rendered report. The default value is **false**.|  
|**SimplePageHeaders**|Indicates whether the page header of the report is rendered to the Excel page header. A value of **false** indicates that the page header is rendered to the first row of the worksheet. The default value is **false**.|  
|**DynamicImageDpi**|The resolution of dynamic images like charts, gauges, and maps. The default value is **96**. (Available in Power BI Report Server (January 2020) and later)|  

## Related content

- [Passing Device Information Settings to Rendering Extensions](report-server-web-service/net-framework/passing-device-information-settings-to-rendering-extensions.md)
- [Customize rendering extension parameters in RSReportServer.Config](customize-rendering-extension-parameters-in-rsreportserver-config.md)
- [Technical reference (SSRS)](technical-reference-ssrs.md)
