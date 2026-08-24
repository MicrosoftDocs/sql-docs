---
title: "Word device information settings"
description: Learn about the device information settings that are available for rendering in Microsoft Word format.
ms.date: 09/25/2024
ms.service: reporting-services
ms.subservice: reporting-services
ms.topic: concept-article
ms.custom:
  - updatefrequency5
helpviewer_keywords:
  - "Word [Reporting Services]"
  - "device information settings [Reporting Services], Word"
---
# Word device information settings
  The following table lists the device information settings for rendering in [!INCLUDE[ofprword](../includes/ofprword-md.md)] format.  
  
|Setting|Value|  
|-------------|-----------|  
|**AutoFit**|**False**. AutoFit is set to **false** set on any Word table.<br /><br /> **True**. AutoFit is set to **true** on every Word table.<br /><br /> **Never**. AutoFit values aren't set on any Word table and behavior reverts to the Word default.<br /><br /> **Default**. AutoFit is set on tables that are narrower than the physical drawing area (physical page width excluding margins) per logical page.|  
|**ExpandToggles**|Indicates whether all items that can be toggled should render in their fully expanded state. The default value is **false**.|  
|**FixedPageWidth**|Indicates whether the Page Width written to the DOC file grows to accommodate the width of the largest page in the Report Body. The default value is **false**.|  
|**OmitHyperlinks**|Indicates whether to omit the Hyperlink action on all set items. The default value is **false**|  
|**OmitDrillthroughs**|Indicates whether to omit the Drillthrough action on all set items. The default value is **false**|  
  
## Related content

- [Passing Device Information Settings to Rendering Extensions](report-server-web-service/net-framework/passing-device-information-settings-to-rendering-extensions.md)
- [Customize rendering extension parameters in RSReportServer.Config](customize-rendering-extension-parameters-in-rsreportserver-config.md)
- [Technical reference (SSRS)](technical-reference-ssrs.md)
