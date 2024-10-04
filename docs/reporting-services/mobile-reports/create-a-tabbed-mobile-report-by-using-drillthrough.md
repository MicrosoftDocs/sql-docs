---
title: "Create a tabbed mobile report by using drillthrough | Reporting Services mobile reports"
description: Learn how to create a Reporting Services mobile report that looks and acts like a tabbed report by using drillthrough and parameters.
author: maggiesMSFT
ms.author: maggies
ms.date: 09/25/2024
ms.service: reporting-services
ms.subservice: mobile-reports
ms.topic: conceptual
ms.custom:
  - updatefrequency5
---
# Create a tabbed mobile report by using drillthrough

[!INCLUDE [ssrs-mobile-report-deprecated](../../includes/ssrs-mobile-report-deprecated.md)]

Learn how to create a [!INCLUDE[ssRSnoversion_md](../../includes/ssrsnoversion-md.md)] mobile report that looks and acts like a tabbed report by using drillthrough and parameters.

For example, in this report, the gauges across the top act like tabs. When you select the Transportation gauge, the data in the rest of the chart is filtered to the transportation data.

:::image type="content" source="../../reporting-services/mobile-reports/media/tabbed-mobile-report-web-viewer-transportation-complete.png" alt-text="Screenshot showing a Financials - Transportation report with the Transportation gauge selected.":::

Behind the scenes, this report is really a set of five separate reports, each with a different parameter that filters the report to match the gauge selected at the top of the report. You create all five reports first, then for each of the five reports, you make the other four gauges into drillthroughs to the other four reports.

Here are the steps for this example.

## Create the basic report

1. Create a report called Sales, with five gauges:

    * Sales
    * Transportation
    * Fuel
    * Storage
    * Misc Expenses

   :::image type="content" source="../../reporting-services/mobile-reports/media/01-sales-mobile-report-publisher.png" alt-text="Screenshot of a report called Sales with five gauges.":::

    
1. Set **Accent** to **On** for the Sales gauge, so it contrasts with the rest of the report--in this case, white on black.

    :::image type="content" source="../../reporting-services/mobile-reports/media/01a-sales-accent-mobile-report-publisher.png" alt-text="Screenshot of the Sales gauge with a red arrow pointing to the Accent slider in the On position.":::

1. Save it to a [!INCLUDE[ssRSnoversion_md](../../includes/ssrsnoversion-md.md)] report server.

## Make copies of the report

1. Make four copies of the Sales report and name them: 

    * Transportation
    * Fuel
    * Storage
    * Misc Expenses

1. Save them to the [!INCLUDE[ssRSnoversion_md](../../includes/ssrsnoversion-md.md)] report server.

## Set the gauge as a drillthrough

In this section, you set each gauge, other than the Sales gauge, as a drillthrough to its respective report.

1. In the Sales report, select the Transportation gauge.

    :::image type="content" source="../../reporting-services/mobile-reports/media/02-sales-create-drillthrough-mobile-report-publisher.png" alt-text="Screenshot of the Sales report with a red arrow from the Transportation gauge to the Drillthrough target option.":::

1. With the **Layout** tab selected, in the **Visual properties** pane, select **Drillthrough target**.

1. Select **Mobile report**.

1. Navigate to and select the report that is the destination for the drillthrough--in this case, "Financials - Transportation."

    :::image type="content" source="../../reporting-services/mobile-reports/media/03-sales-select-dashboard-mobile-report-publisher.png" alt-text="Screenshot of the Open from server dialog box with the Financials - Transportation option called out.":::


1. In **Configure target report**, select the parameter to filter the report, and select **Apply**.

   :::image type="content" source="../../reporting-services/mobile-reports/media/04-sales-apply-parameters-mobile-report-publisher.png" alt-text="Screenshot of the Configure target report section showing the Financials - Transportation Report parameters.":::

   
1. Repeat these steps for each of the other gauges in the Sales report. 

## Set the gauges for the other reports

1.  Open the Transportation report, set the Sales gauge as a drillthrough to the Sales report, and the other three gauges as drillthroughs to their respective reports.

1. Still in the Transportation report, set **Accent** for the Transportation gauge to **On**, contrast with the rest of the report.

1. Repeat these steps for the Fuel, Storage, and Misc Expenses reports. 

## View the report in the web portal

1. Go to the [!INCLUDE[ssRSnoversion_md](../../includes/ssrsnoversion-md.md)] report server and open one of the reports. 

1. Notice that each of the gauges has a drillthrough icon in the upper-right corner.

    :::image type="content" source="../../reporting-services/mobile-reports/media/web-viewer-drillthrough-icon-mobile-report-builder.png" alt-text="Screenshot of the Fuel gauge.":::


1. Select one of the gauges to go to the report filtered to that gauge's data.

   :::image type="content" source="../../reporting-services/mobile-reports/media/06-mobile-report-web-viewer-transportation.png" alt-text="Screenshot showing a Financials - Transportation report with a red arrow pointing to the Transportation gauge, which also has a red box around it.":::


### Related content
	
* [Add parameters to a mobile report](../../reporting-services/mobile-reports/add-parameters-to-a-mobile-report-reporting-services.md)
* [Add drillthrough from a mobile report to other mobile reports or URLs](../../reporting-services/mobile-reports/add-drillthrough-from-a-mobile-report-to-other-mobile-reports-or-urls.md)




  

