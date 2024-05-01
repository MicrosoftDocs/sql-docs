---
title: Pin Reporting Services paginated report items to dashboards in Power BI
description: You can pin on-premises Reporting Services paginated report items to a dashboard in the Power BI service, as a new tile.
author: maggiesMSFT
ms.author: maggies
ms.date: 10/14/2022
ms.service: reporting-services
ms.subservice: reporting-services
ms.topic: conceptual
ms.custom: updatefrequency5
helpviewer_keywords:
  - "pbi"
  - "dashboard"
  - "pin"
  - "powerbi"
  - "power bi integration"
---
# Pin Reporting Services paginated report items to dashboards in Power BI

[!INCLUDE[ssrs-appliesto](../includes/ssrs-appliesto.md)] [!INCLUDE[ssrs-appliesto-2016-and-later](../includes/ssrs-appliesto-2016-and-later.md)] [!INCLUDE[ssrs-appliesto-pbirsi](../includes/ssrs-appliesto-pbirs.md)]

You can pin an on-premises [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] paginated report item to a dashboard in the [!INCLUDE[sspowerbi](../includes/sspowerbi-md.md)] service, as a new tile.   To pin, your administrator needs to first integrate your report server with Microsoft Entra ID ([formerly Azure Active Directory](/entra/fundamentals/new-name)) and [!INCLUDE[sspowerbi](../includes/sspowerbi-md.md)].
 
[!INCLUDE [ssrs-no-pin-2-power-bi](../includes/ssrs-no-pin-2-power-bi.md)]
 
##  <a name="bkmk_requirements_to_pin"></a> Requirements to pin  
  
-   The report server is configured for [!INCLUDE[sspowerbi](../includes/sspowerbi-md.md)] integration. For more information, see [Power BI report server integration &#40;Configuration Manager&#41;](../reporting-services/install-windows/power-bi-report-server-integration-configuration-manager.md). If you don't configure the report server, the **Pin to Power BI Dashboard** button doesn't appear on the report viewer toolbar.  
  
    :::image type="content" source="../reporting-services/media/ssrs-report-powerbi.png" alt-text="Screenshot of the report viewer toolbar highlighting the Pin to Power BI Dashboard button.":::
  
-   You pin from the [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] report viewer in the [!INCLUDE[ssRSWebPortal](../includes/ssrswebportal.md)], for example, `https://myserver/Reports`.  You can't pin from [!INCLUDE[ssRBnoversion](../includes/ssrbnoversion.md)], from report designer in [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)], or from a report server URL. For example,  `https://myserver/ReportServer`.  
  
-   You need to configure your browser to allow pop-ups from your report server site.  
  
-   You need to configure reports for stored credentials, if you want the pinned item to refresh.  When you pin an item, a [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] subscription is automatically created to manage the data refresh of the item to the dashboard. If the report doesn't use stored credentials, when the subscription runs you see a message similar to this one on the **My subscriptions** page.  
  
    "Power BI Delivery error: dashboard: IT Spend Analysis Sample, visual: Chart2, error: The current action cannot be completed. The user data source credentials do not meet the requirements to run this report or shared dataset. Either the user data source credential."
 
    See the section "Configure stored credentials for a report-specific data source (Native mode)" in [Store credentials in a Reporting Services data source](../reporting-services/report-data/store-credentials-in-a-reporting-services-data-source.md). 
  
##  <a name="bkmk_supported_items"></a> Items you can pin  
 The following report items can be pinned to a [!INCLUDE[sspowerbi](../includes/sspowerbi-md.md)] dashboard. You can't pin items that are nested inside a data region. For example, you can't pin an item that is nested inside a [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] table or list.  
  
-   Charts  
-   Gauge panels  
-   Maps  
-   Images  
-   Items need to be in the report body. You can't pin items that are in the page header or page footer.  
-   You can pin individual items that are inside a top-level rectangle but you can't pin them all as a single group.  
  
##  <a name="bkmk_to_pin"></a> Pin a report item  
  
1. Verify you're signed into [!INCLUDE[sspowerbi](../includes/sspowerbi-md.md)]. In the [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] [!INCLUDE[ssRSWebPortal](../includes/ssrswebportal.md)], select the menu item **My Settings** and sign in. For more information, see [My Settings for Power BI integration &#40;web portal&#41;](my-settings-for-power-bi-integration-web-portal.md).

    :::image type="content" source="../reporting-services/media/ssrs-webportal-mysettings.png" alt-text="Screenshot of the web portal highlighting My Settings in the Settings dropdown.":::
  
2. Navigate to the [!INCLUDE[ssRSWebPortal](../includes/ssrswebportal.md)] folder that contains your report, and then view the report.  
  
3. While viewing the report, select the **Pin to Power BI** button the toolbar. You're prompted to sign in, if you aren't already signed in. If the [!INCLUDE[sspowerbi](../includes/sspowerbi-md.md)] button isn't visible, the report server isn't integrated with [!INCLUDE[sspowerbi](../includes/sspowerbi-md.md)]. For more information, see [Power BI Report Server integration &#40;Configuration Manager&#41;](../reporting-services/install-windows/power-bi-report-server-integration-configuration-manager.md).  
  
    :::image type="content" source="../reporting-services/media/ssrs-report-powerbi.png" alt-text="Screenshot of the report viewer toolbar highlighting the Pin to Power BI button.":::
  
4. Select the report item you want to pin to [!INCLUDE[sspowerbi](../includes/sspowerbi-md.md)]. You can only pin one item at a time. The report viewer presents a shaded view of your report, and the report items you can pin are highlighted. The items that you can't pin are shaded dark.  
  
    **(1)** select the group that contains the dashboard you want to pin to, **(2)** choose the dashboard you want to pin the item too and **(3)** select how frequently you want the tile updated in the dashboard. [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] subscriptions manage the refresh. After the item is pinned, you can edit the subscription and configure a different refresh schedule.  
  
    :::image type="content" source="../reporting-services/media/ssrs-pin-to-powerbi.png" alt-text="Screenshot of the Pin to Power BI Dashboard dialog box.":::
  
5. Select **Pin**  
  
    In the **Pin Successful** dialog, you can select the link **See it in Power BI** to navigate to the dashboard and see the item you pinned.  
  
6. Select **Close** to return the report to the normal view.  
  
##  <a name="bkmk_in_the_dashboard"></a> In the dashboard

After your report item is pinned in the dashboard, the tile looks like other dashboard tiles and there's no visible indication the tile came from [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)]. The following list summarizes how tile properties are populated from the report item.  
  
From the [!INCLUDE[sspowerbi](../includes/sspowerbi-md.md)] dashboard the pinned report item behaves like other tiles:

**(1)** You can pin the tile to other dashboards.

**(2)** In the **Tile Details** the [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] report title is used for the default title of the tile.

**(3)** The tile subtitle is based on the date and time the tile was pinned or the data was last refreshed from [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)].  The [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] subscription that was automatically created when you pinned the report item manages the refresh schedule.

**(5)** If you select the tile itself, [!INCLUDE[sspowerbi](../includes/sspowerbi-md.md)] uses the **(4) custom link** to navigate to the [!INCLUDE[ssRSWebPortal](../includes/ssrswebportal.md)] page of the registered report server. The link was set when the item was pinned from [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)]. If you don't have internet connectivity to the report server, you see an error in the browser.  

:::image type="content" source="../reporting-services/media/ssrs-pinned-tile-details.png" alt-text="Screenshot of the tiles in the dashboard.":::
  
##  <a name="bkmk-troubleshoot"></a> Troubleshoot issues  
  
-   **No [!INCLUDE[sspowerbi](../includes/sspowerbi-md.md)] button on the report viewer toolbar:** This message indicates the report server isn't integrated with [!INCLUDE[sspowerbi](../includes/sspowerbi-md.md)]. For more information, see [Power BI Report Server Integration &#40;Configuration Manager&#41;](../reporting-services/install-windows/power-bi-report-server-integration-configuration-manager.md).  
  
- **Cannot Pin**: When you attempt to pin an item, you see the following error message: See the section [Items you can pin](#bkmk_supported_items).  
  
    "Cannot Pin: There are no report items on this page that you can pin to [!INCLUDE[sspowerbi](../includes/sspowerbi-md.md)]."  
  
-   **Pinned items show stale data** in a [!INCLUDE[sspowerbi](../includes/sspowerbi-md.md)] dashboard and it did update for some time. The user credentials token expired, and you need to sign in again. The user credential registration with Azure and [!INCLUDE[sspowerbi](../includes/sspowerbi-md.md)] is good for 90 days. In the [!INCLUDE[ssRSWebPortal](../includes/ssrswebportal.md)], select **My Settings**. For more information, see [My Settings for Power BI Integration &#40;web portal&#41;](my-settings-for-power-bi-integration-web-portal.md).  
  
-   **Pinned items show stale data** in a [!INCLUDE[sspowerbi](../includes/sspowerbi-md.md)] dashboard and it hasn't refreshed even once.  The issue is the report isn't configured to use stored credentials. A report must use stored credentials because the action of pinning a report item creates a [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] subscription to manage the refresh schedule of the tiles. [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] subscriptions require stored credentials. If you review the **My Subscriptions** page, you see an error message similar to this one:  
  
    "Power BI Delivery error: dashboard: SSRS items, visual: Image3, error: The current action can't be completed. The user data source credentials do not meet the requirements to run this report or shared dataset. Either the user data source credentials are not stored in the report server database, or the user data source is configured not to require credentials but the unattended execution account is not specified. (rsInvalidDataSourceCredentialSetting)"
  
-   **Expired Power BI credentials:** You attempt to pin an item and see the following error message. In the [!INCLUDE[ssRSWebPortal](../includes/ssrswebportal.md)], select **My Settings** and on the My Settings page, choose **Sign in**. For more information, see [My Settings for Power BI integration &#40;web portal&#41;](my-settings-for-power-bi-integration-web-portal.md).  
  
    "Cannot Pin: Unexpected Server Error: Missing, invalid or expired Power BI credentials."  
  
-   **Cannot Pin**: If you attempt to pin an item to a dashboard that is in a read-only state, you see an error message similar to this one:  
  
    "Server Error: The item 'Dashboard deleted 015cf022-8e2f-462e-88e5-75ab0a04c4d0' can't be found. (rsItemNotFound)"  

-   **Tiles in Power BI apps show stale data:** If you pin a Reporting Services report item to a dashboard, and then distribute that dashboard in an app, the pinned report item in that dashboard doesn't update.

##  <a name="bkmk_subscription_management"></a> Subscription management  
 In addition to the subscription-related issues described in the troubleshooting section, the following information helps you maintain [!INCLUDE[sspowerbi](../includes/sspowerbi-md.md)] related subscriptions.
  
-   **Item name changed:** If a pinned report item is renamed or deleted, the [!INCLUDE[sspowerbi](../includes/sspowerbi-md.md)] tile no longer updates and you see an error message similar to the following.  If you rename the item back the original name, the subscription starts working again and the tile refreshes on the subscriptions schedule.  
  
    "Power BI Delivery error: dashboard: SSRS items, visual: Image1, error: Error: Report item 'Image1' cannot be found."  
  
    You could also edit the subscription properties and change the **Report Visual Name** to the appropriate report item name. :::image type="icon" source="../reporting-services/media/ssrs-powerbi-subscription-visual.png "::: 
  
-   **Delete a tile**. If you delete a tile in [!INCLUDE[sspowerbi](../includes/sspowerbi-md.md)], the associated subscription isn't deleted in [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] and on the **My subscriptions** page, you see an error similar to the following. You can delete the subscription.  
  
    "Power BI Delivery error: dashboard: SSRS items, visual: Image3, error: The item 'Tile deleted af7131d9-5eaf-480f-ba45-943a07d19c9f' cannot be found."  

## Video

> [!VIDEO https://www.youtube-nocookie.com/embed/QhPQObqmMPc]

## Related content

- [Power BI report server integration &#40;Configuration Manager&#41;](../reporting-services/install-windows/power-bi-report-server-integration-configuration-manager.md)   
- [My Settings for Power BI Integration &#40;web portal&#41;](my-settings-for-power-bi-integration-web-portal.md)  
- [Introduction to dashboards for Power BI designers](https://powerbi.microsoft.com/documentation/powerbi-service-dashboards/)  
  
  
[!INCLUDE[feedback-qa-stackoverflow-md](../includes/feedback-qa-stackoverflow-md.md)]
