---
title: "My Settings for Power BI integration (web portal)"
description: Learn about the My Settings page in the Reporting Services web portal and how individual users can manage their sign-in with Power BI.
author: maggiesMSFT
ms.author: maggies
ms.date: 08/17/2017
ms.service: reporting-services
ms.subservice: reporting-services
ms.topic: conceptual
ms.custom: updatefrequency5
f1_keywords:
  - "pbi"
  - "power bi"
  - "power bi integration"
---

# My Settings for Power BI integration (web portal)

[!INCLUDE[ssrs-appliesto](../includes/ssrs-appliesto.md)] [!INCLUDE[ssrs-appliesto-2016-and-later](../includes/ssrs-appliesto-2016-and-later.md)] [!INCLUDE[ssrs-appliesto-pbirsi](../includes/ssrs-appliesto-pbirs.md)]

The **My Settings** page in the [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] [!INCLUDE[ssRSWebPortal](../includes/ssrswebportal.md)] is used by individual users to manage their sign-in with [!INCLUDE[sspowerbi](../includes/sspowerbi-md.md)]. When you go through the steps to pin a report item, you automatically see a prompt to sign.  However,  you can use the **My Settings** page if you need to manually sign in or if you need to sign out.  If the **My Settings** menu option isn't visible, the report server isn't integrated with [!INCLUDE[sspowerbi](../includes/sspowerbi-md.md)].  For more information, see [Power BI report server integration &#40;Configuration Manager&#41;](../reporting-services/install-windows/power-bi-report-server-integration-configuration-manager.md).  

:::image type="content" source="../reporting-services/media/ssrs-webportal-mysettings.png" alt-text="Screenshot of the web portal highlighting My Settings in the Settings dropdown.":::
  
## Why sign in

 When you sign in, you establish a relationship between your [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] user account and  your [!INCLUDE[sspowerbi](../includes/sspowerbi-md.md)] account. The sign-in creates a security token that is good for 90 days. If the token expires, and you have items pinned to Power BI, you see a notification.  

:::image type="content" source="../reporting-services/media/ssrs-webportal-powerbi-notification.png" alt-text="Screenshot of the notification dropdown showing the message, 'We couldn't refresh some tiles in Power BI'.":::
   
Tiles within [!INCLUDE[sspowerbi](../includes/sspowerbi-md.md)] dashboards don't refresh until you sign in again through **MySettings**.  

:::image type="content" source="../reporting-services/media/ssrs-webportal-powerbi-signin-again.png" alt-text="Screenshot of the Sign In section of My Settings.":::
  
Once you sign in, you receive a new security token. Your dashboard tiles begin updating on their previously configured schedules.  

## Related content

- [Power BI report server integration (Configuration Manager)](../reporting-services/install-windows/power-bi-report-server-integration-configuration-manager.md)
- [Pin Reporting Services paginated report items to dashboards in Power BI](../reporting-services/pin-reporting-services-items-to-power-bi-dashboards.md)
- [Introduction to dashboards for Power BI designers](https://powerbi.microsoft.com/documentation/powerbi-service-dashboards/)
- [The web portal of a report server (SSRS Native Mode)](../reporting-services/web-portal-ssrs-native-mode.md)  

More questions? [Try asking the Reporting Services forum](/answers/search.html?c=&f=&includeChildren=&q=ssrs+OR+reporting+services&redirect=search%2fsearch&sort=relevance&type=question+OR+idea+OR+kbentry+OR+answer+OR+topic+OR+user)
