---
title: "SharePoint site settings for the Report Viewer web part - SSRS"
description: Learn about how to configure SharePoint site settings in the Report Viewer web part in SQL Server Reporting Server.
author: "jt000"
ms.author: "jasontre"
ms.date: 09/25/2024
ms.service: reporting-services
ms.subservice: report-server-sharepoint
ms.topic: conceptual
ms.custom:
  - updatefrequency5
---
# SharePoint site settings for the Report Viewer web part - Reporting Services

[!INCLUDE[ssrs-appliesto](../../includes/ssrs-appliesto.md)] [!INCLUDE[ssrs-appliesto-2016-and-later](../../includes/ssrs-appliesto-2016-and-later.md)]  [!INCLUDE[ssrs-appliesto-pbirsi](../../includes/ssrs-appliesto-pbirs.md)] [!INCLUDE[ssrs-appliesto-sharepoint-2013-and-later](../../includes/ssrs-appliesto-sharepoint-2013-and-later.md)] [!INCLUDE[ssrs-appliesto-not-sharepoint-online](../../includes/ssrs-appliesto-not-sharepoint-online.md)]

The Report Viewer web part has a couple of settings that can be configured. These settings can be enabled and disabled on the SharePoint site settings page by a site administrator. Each site has its own settings. Additionally, these settings aren't reset after reinstalling the Report Viewer web part.

## Accessing the site settings page

The site settings can be accessed by:

1. In your SharePoint site, select the **gear** icon in the upper left and choose **Site Settings**.

    :::image type="content" source="media/sharepoint-site-settings.png" alt-text="Screenshot of the gear icon menu, highlighting Site settings.":::

2. Clicking **Report Viewer Web Part Settings** in the **Reporting Services** site settings group.

    > [!NOTE]
    > The site settings can also be reached by navigating directly to `<site>/_layouts/15/ReportViewerWebPart/ReportViewerWebPartSettings.aspx`

## Report Viewer web part settings

|Setting|Comments|  
|-------------|--------------|  
|Collect usage data|Enables error and usage information to be sent to Microsoft to help improve our products. For the Microsoft error reporting data collection policy, see the [Microsoft SQL Server Privacy Statement](../../sql-server/sql-server-privacy.md).|  
|Enable Accessibility Metadata for Reports|Sets the [`AccessibleTablix` device info](../html-device-information-settings.md) for rendered reports.|
