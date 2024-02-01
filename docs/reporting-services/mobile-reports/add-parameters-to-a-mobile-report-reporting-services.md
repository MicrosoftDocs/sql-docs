---
title: "Add parameters to a mobile report"
description: Reporting Services mobile report can have parameters, so report readers can filter your reports. Such a report can also be the target of a drillthrough.
author: maggiesMSFT
ms.author: maggies
ms.date: 07/21/2022
ms.service: reporting-services
ms.subservice: mobile-reports
ms.topic: conceptual
ms.custom: updatefrequency5
---
# Add parameters to a mobile report

[!INCLUDE [ssrs-mobile-report-deprecated](../../includes/ssrs-mobile-report-deprecated.md)]

You can create a [!INCLUDE[ssRSnoversion_md](../../includes/ssrsnoversion-md.md)] mobile report with parameters, so you and your report readers can filter your reports. A report with parameters can also be the target of a [drillthrough from a source report](../../reporting-services/mobile-reports/add-drillthrough-from-a-mobile-report-to-other-mobile-reports-or-urls.md). 

To create a mobile report with parameters, you start with a shared dataset with at least one parameter. Read about [creating parameters in a shared dataset](../../reporting-services/report-data/create-a-shared-dataset-or-embedded-dataset-report-builder-and-ssrs.md). Mobile reports don't support null value(s) for default parameters, so make sure your parameters have default values other than null.

After you add parameters to a mobile report, you create a URL to [open the report with query string parameters](../../reporting-services/mobile-reports/open-a-mobile-report-with-specific-query-string-parameters-reporting-services.md). 

1. In the top bar of the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] web portal, select **New** > **Mobile Report**.  
  
   :::image type="content" source="../../reporting-services/mobile-reports/media/pbi-ssmrp-newmenu.png" alt-text="Screenshot of the New menu and the Mobile Report option.":::
  
     
1. Select the **Data** tab.   
  
1. Select **Add Data**.  
  
1. Select **Report Server**, then select a server.  
  
1. Navigate to the shared datasets on the server and select one that has parameters.  
  
   In the grid, you see the data in the dataset. The green circle with brackets **{ }** marks a dataset with a parameter.  
     
   :::image type="content" source="../../reporting-services/mobile-reports/media/ssmrp-pforparam.png" alt-text="Screenshot of the TimeChartLoD with the brackets highlighted.":::
  
1. Select the cog on the tab, then select **Param {}**.  
  
   :::image type="content" source="../../reporting-services/mobile-reports/media/ssmrp-paramwheel.png" alt-text="Screenshot of the cog with the Param {} option highlighted.":::
  
  
1. Select the report element that passes values to the parameter.  
  
   :::image type="content" source="../../reporting-services/mobile-reports/media/ssmrp-setparam.png" alt-text="Screenshot of the Set dataset parameters screen.":::
  
     
1. Select **Preview** to see how the report looks. In this report, the selection list is using the Category parameter.

   :::image type="content" source="../../reporting-services/mobile-reports/media/sql-server-mobile-report-publisher-selection-list-view-no-selection.png" alt-text="Screenshot of the preview of the report with the Selection list 1 called out.":::
 
   
1. When you select a value in the selection list, the report is filtered to that value, in this case, Accessories.

   :::image type="content" source="../../reporting-services/mobile-reports/media/sql-server-mobile-report-publisher-selection-list-category-selected.png" alt-text="Screenshot of the preview of the report with the Selection list 1 called out and the Accessories option selected.":::
   
  
### Related content 
-  [Open a mobile report with specific query string parameters](../../reporting-services/mobile-reports/open-a-mobile-report-with-specific-query-string-parameters-reporting-services.md)
-  [Add drillthrough from a mobile report to other mobile reports or URLs](../../reporting-services/mobile-reports/add-drillthrough-from-a-mobile-report-to-other-mobile-reports-or-urls.md)
-  [Create a shared or embedded dataset](../../reporting-services/report-data/create-a-shared-dataset-or-embedded-dataset-report-builder-and-ssrs.md)
- [Create and publish mobile reports with SQL Server Mobile Report Publisher](../../reporting-services/mobile-reports/create-mobile-reports-with-sql-server-mobile-report-publisher.md)  
  
  

