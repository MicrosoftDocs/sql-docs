---
title: "Add drillthrough from a mobile report to other mobile reports or URLs"
description: You can add drillthrough from any gauge, chart, or data grid in a Reporting Services mobile report to another mobile report or custom URL.
author: maggiesMSFT
ms.author: maggies
ms.date: 09/25/2024
ms.service: reporting-services
ms.subservice: mobile-reports
ms.topic: conceptual
ms.custom:
  - updatefrequency5
---
# Add drillthrough from a mobile report to other mobile reports or URLs

[!INCLUDE [ssrs-mobile-report-deprecated](../../includes/ssrs-mobile-report-deprecated.md)]

You can add drillthrough from any gauge, chart, or data grid in a [!INCLUDE[ssRSnoversion_md](../../includes/ssrsnoversion-md.md)] mobile report to another mobile report or custom URL. 

A *drillthrough*  is a link from a source report that opens another target report or URL. The target drillthrough report often contains details about some item in the summary report. Depending on the source mobile report, one or more parameters can be passed to the target mobile report or integrated into a custom URL.  
  
When you view the source mobile report in the [!INCLUDE[ssRSnoversion_md](../../includes/ssrsnoversion-md.md)] web portal and select an element with a drillthrough target, you go to that target, either another mobile report or a URL.  

Report items with drillthrough, either to a URL or another mobile report, have the drillthrough icon :::image type="icon" source="../../reporting-services/mobile-reports/media/mobile-report-drill-through-icon.png"::: in the upper-right corner.

:::image type="content" source="../../reporting-services/mobile-reports/media/mobile-report-gauge-drill-through.png" alt-text="Screenshot of a mobile report gauge with a drillthrough.":::

> [!TIP]  
> Create the target report and save it to a [!INCLUDE[ssRSnoversion_md](../../includes/ssrsnoversion-md.md)] web portal first. If you plan to pass parameters from the source report, add the parameters to the target report, too. Then, you can set up drillthrough from the source report to the target report. For more information, see [Add parameters to a mobile report](../../reporting-services/mobile-reports/add-parameters-to-a-mobile-report-reporting-services.md).
 
## Set up drillthrough to a mobile report  

1. In Layout view in [!INCLUDE[SS_MobileReptPub_Long](../../includes/ss-mobilereptpub-long.md)], select a visualization that supports drillthrough.   

   Maps and gauges support drillthrough, as do most charts and simple data grids.
   
2. In the **Visual Properties** pane, select **Drillthrough target**, and choose **Mobile report**.  
3. Select the server and the target mobile report.  

    > [!Note]
    > If the target mobile report is not on the same server as the source mobile report, connect to it with a custom URL instead, as explained in the next section.  
 
4. After you select a target mobile report, you see its available input parameters. The list includes properties that can be bound to navigator controls and parameters configured on datasets of the target mobile report.  

   :::image type="content" source="../../reporting-services/mobile-reports/media/mobile-report-drillthrough-target.PNG" alt-text="Screenshot of the Configure target report dialog showing available Report parameters.":::

   
   *Drillthrough properties for the target mobile report*  
  
5. Select the arrow to the right of each property to connect properties with matching data types to available output properties on the source mobile report. You can also set defaults for each output here, in case the report user doesn't interact with the source mobile report before drilling through to the destination mobile report.  
  
## Set up a drillthrough to a custom URL  
  
1. In Layout view in [!INCLUDE[SS_MobileReptPub_Long](../../includes/ss-mobilereptpub-long.md)], select a visualization that supports drillthrough targets.    
1. In the **Visual Properties** pane, select **Drillthrough target** > **Custom URL**.  This action opens the drillthrough configuration dialog.  
  
1. In **Set drillthrough URL**, enter the destination URL to go to when the visualization is selected, and choose from **Available parameters** listed on the screen. A preview of the custom URL combined with sample resolved parameters (if included) is displayed in the panel in the following screenshot.  
  
   :::image type="content" source="../../reporting-services/mobile-reports/media/mobile-report-drillthrough-url.PNG" alt-text="Screenshot of the Set drillthrough URL dialog.":::
  
   *Drill through to custom URL properties*  
  
1. Select **Apply**.  
  
When previewing a mobile report in [!INCLUDE[SS_MobileReptPub_Short](../../includes/ss-mobilereptpub-short.md)], select a visualization with drillthrough to disable the drillthrough feature. A message indicating that the drillthrough is disabled displays. You can only actually drill through to the target after you save or publish a mobile report and then view it, not from within [!INCLUDE[SS_MobileReptPub_Short](../../includes/ss-mobilereptpub-short.md)] layout or preview mode.  

## Hide a target mobile report on the web portal
If you're not going to set a default value for the target report, consider hiding it on the web portal. Otherwise, when someone tries to view it on the web portal directly, without going through the source report, it's empty.

1. In the web portal, select the ellipsis (...) on the target report you want to hide, then select Manage.

1. In **Properties**, select **Hide in tile view**.

You can choose to view hidden items in the web portal: 

* In the web portal, select **View**, and then choose **Show hidden.** 

Hidden items are displayed in a lighter color.
    
### Related content
 
* [Add parameters to a Reporting Services mobile report](../../reporting-services/mobile-reports/add-parameters-to-a-mobile-report-reporting-services.md)
* [Create mobile reports with SQL Server Mobile Report Publisher](../../reporting-services/mobile-reports/create-mobile-reports-with-sql-server-mobile-report-publisher.md) 
* [Web portal (SSRS native mode)](../../reporting-services/web-portal-ssrs-native-mode.md)

