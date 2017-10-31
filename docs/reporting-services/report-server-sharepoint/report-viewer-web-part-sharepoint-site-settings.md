---
title: "SharePoint site settings for the Report Viewer web part | Microsoft Docs"
ms.custom: ""
ms.date: "10/31/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "reporting-services-sharepoint"
ms.tgt_pltfrm: ""
ms.topic: "article"
author: "jt000"
ms.author: "jasontre"
ms.workload: "Inactive"
---
# SharePoint site settings for the Report Viewer web part

[!INCLUDE[ssrs-appliesto](../../includes/ssrs-appliesto.md)] [!INCLUDE[ssrs-appliesto-2016-and-later](../../includes/ssrs-appliesto-2016-and-later.md)] [!INCLUDE[ssrs-appliesto-sharepoint-2013-2016i](../../includes/ssrs-appliesto-sharepoint-2013-2016.md)] [!INCLUDE[ssrs-appliesto-pbirsi](../../includes/ssrs-appliesto-pbirs.md)]

The Report Viewer web part has a couple of settings that can be configured. These settings can be enabled and disabled on the SharePoint site settings page by a site administrator. Please note that each site will have its own settings. Additionally, these settings will not be reset after re-installing the Report Viewer web part.

## Accessing the site settings page

The site settings can be accessed by:

1. Clicking the gear dropdown in the upper right of the page and selecting **Site Settings**. 

![SharePoint site settings](media/sharepoint-site-settings.png)

2. Clicking **Report Viewer Web Part Settings** in the **Reporting Services** site settings group.

> [!NOTE]
> The site settings can also be reached by navigating directly to `<site>/_layouts/15/ReportViewerWebPart/ReportViewerWebPartSettings.aspx`

## Report Viewer web part settings

|Setting|Comments|  
|-------------|--------------|  
|Collect usage data|Enables error and usage information to be sent to Microsoft to help improve our products. For the Microsoft error reporting data collection policy, see the [Microsoft SQL Server Privacy Statement](https://go.microsoft.com/fwlink/?linkid=860782&clcid=0x409).|  
|Enable Accessibility Metadata for Reports|Sets the [`AccessibleTablix` device info](../html-device-information-settings.md) for rendered reports.| 
