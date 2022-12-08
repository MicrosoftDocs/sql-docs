---
title: "Create a tabbed mobile report by using drillthrough | Reporting Services mobile reports | Microsoft Docs"
description: Learn how to create a Reporting Services mobile report that looks and acts like a tabbed report by using drillthrough and parameters.
ms.date: 07/21/2022
ms.service: reporting-services
ms.subservice: mobile-reports

ms.topic: conceptual
ms.assetid: c4d5d80d-370a-4a6d-8b76-698bd5ba5ba6
author: maggiesMSFT
ms.author: maggies
---
# Create a tabbed mobile report by using drillthrough

[!INCLUDE [ssrs-mobile-report-deprecated](../../includes/ssrs-mobile-report-deprecated.md)]

Learn how to create a [!INCLUDE[ssRSnoversion_md](../../includes/ssrsnoversion-md.md)] mobile report that looks and acts like a tabbed report by using drillthrough and parameters.

For example, in this report, the gauges across the top act like tabs. When you click the Transportation gauge, the data in the rest of the chart is filtered to the transportation data.

![Screenshot showing a Financials - Transportation report with the Transportation gauge selected.](../../reporting-services/mobile-reports/media/tabbed-mobile-report-web-viewer-transportation-complete.png)

Behind the scenes, this is really a set of five separate reports, each with a different parameter that filters the report to match the gauge selected at the top of the report. You create all five reports first, then for each of the five reports, you make the other four gauges into drillthroughs to the other four reports.

Here are the steps for this example.

## Create the basic report

1. Create a report called Sales, with five gauges:

    * Sales
    * Transportation
    * Fuel
    * Storage
    * Misc Expenses

   ![Screenshot of a report called Sales with five gauges.](../../reporting-services/mobile-reports/media/01-sales-mobile-report-publisher.png)
    
2. Set **Accent** to **On** for the Sales gauge, so it will contrast with the rest of the report--in this case, white on black.

    ![Screenshot of the Sales gauge with a red arrow pointing to the Accent slider in the On position.](../../reporting-services/mobile-reports/media/01a-sales-accent-mobile-report-publisher.png)
    
3. Save it to a [!INCLUDE[ssRSnoversion_md](../../includes/ssrsnoversion-md.md)] report server.

## Make copies of the report

1. Make four copies of the Sales report and name them: 

    * Transportation
    * Fuel
    * Storage
    * Misc Expenses

3. Save them to the [!INCLUDE[ssRSnoversion_md](../../includes/ssrsnoversion-md.md)] report server.

## Set the gauge as a drillthrough

In this section, you set each gauge (other than the Sales gauge) as a drillthrough to its respective report.

1. In the Sales report, select the Transportation gauge.

    ![Screenshot of the Sales report with a red arrow from the Transportation gauge to the Drillthrough target option.](../../reporting-services/mobile-reports/media/02-sales-create-drillthrough-mobile-report-publisher.png)

2. With the **Layout** tab selected, in the **Visual properties** pane select **Drillthrough target**.

3. Select **Mobile report**.

4. Navigate to and select the report that will be the destination for the drillthrough--in this case, "Financials - Transportation."

    ![Screenshot of the Open from server dialog box with the Financials - Transportation option called out.](../../reporting-services/mobile-reports/media/03-sales-select-dashboard-mobile-report-publisher.png)

5. In **Configure target report**, select the parameter to filter the report, and select **Apply**.

   ![Screenshot of the Configure target report section showing the Financials - Transportation Report parameters.](../../reporting-services/mobile-reports/media/04-sales-apply-parameters-mobile-report-publisher.png)
   
6. Repeat these steps for each of the other gauges in the Sales report. 

## Set the gauges for the other reports

1.  Open the Transportation report, set the Sales gauge as a drillthrough to the Sales report, and the other three gauges as drillthroughs to their respective reports.

2. Still in the Transportation report, set **Accent** for the Transportation gauge to **On**, contrast with the rest of the report.

3. Repeat these steps for the Fuel, Storage, and Misc Expenses reports. 

## View the report in the web portal

1. Go to the [!INCLUDE[ssRSnoversion_md](../../includes/ssrsnoversion-md.md)] report server and open one of the reports. 

2. Notice that each of the gauges has a drillthrough icon in the upper-right corner.

    ![Screenshot of the Fuel gauge.](../../reporting-services/mobile-reports/media/web-viewer-drillthrough-icon-mobile-report-builder.png)

3. Select one of the gauges to go to the report filtered to that gauge's data.

   ![Screenshot showing a Financials - Transportation report with a red arrow pointing to the Transportation gauge which also has a red box around it.](../../reporting-services/mobile-reports/media/06-mobile-report-web-viewer-transportation.png)

### See also
	
* [Add parameters to a mobile report](../../reporting-services/mobile-reports/add-parameters-to-a-mobile-report-reporting-services.md)
* [Add drillthrough from a mobile report to other mobile reports or URLs](../../reporting-services/mobile-reports/add-drillthrough-from-a-mobile-report-to-other-mobile-reports-or-urls.md)




  

