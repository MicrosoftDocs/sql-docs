---
title: "Empty and Null Data Points in Charts (Report Builder and SSRS) | Microsoft Docs"
ms.date: 05/30/2017
ms.prod: reporting-services
ms.prod_service: "reporting-services-sharepoint, reporting-services-native"
ms.technology: report-design


ms.topic: conceptual
ms.assetid: faddd29d-4cc1-4c2c-8e29-d3d9918fe22a
author: markingmyname
ms.author: maghan
---

# Empty and Null Data Points in Charts (Report Builder and SSRS)

  If you are displaying fields with empty or null values in a chart in your [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] paginated report, the chart may not look as you expect. Charts process empty values differently depending on the specified chart type:  
  
-   If the chart type is a linear chart type (bar, column, scatter, line, area, range), empty values are displayed as empty spaces or "gaps" in the chart. If you want to indicate empty points, you must add empty point placeholders. For more information, see [Add Empty Points to a Chart &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/add-empty-points-to-a-chart-report-builder-and-ssrs.md).  
  
-   If the chart type is a contiguous, linear chart type (area, bar, column, line, scatter), empty data points are added to the chart to maintain continuity in the series.  
  
-   If the chart type is a nonlinear chart type (polar, pie, doughnut, funnel or pyramid), empty values are omitted from display on the chart.  
  
-   In shape chart types, null values are omitted.  
  
 An example of a chart with empty data points is available as a sample report. For more information about downloading this sample report and others, see [Report Builder and Report Designer sample reports](https://go.microsoft.com/fwlink/?LinkId=198283).  
  
> [!NOTE]  
>  [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]  
  
## Removing Empty or Null Values  
 To avoid obscuring important data, consider removing empty values from your dataset. To filter nulls, you can use the NOT IS NULL clause in your query. Alternatively, you can add a filtering expression that specifies that you only want to display values not equal to zero. For more information, see [Add Dataset Filters, Data Region Filters, and Group Filters &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/add-dataset-filters-data-region-filters-and-group-filters.md).  
  
## Fields with No Values in a Chart  
 If a field does not contain any values in the returned dataset, the chart displays an empty chart with no data points, but the series name (typically the field name) is added as a legend item.  
  
 This behavior differs from the case where there are zero rows of data in the returned dataset, which can occur when the report is parameterized and the selected value returns an empty result set. If your dataset query returns zero rows of data, a message is displayed at run time to indicate that no data can be shown. You can customize this message by modifying the NoDataMessage caption for the report in the **Properties** pane. For more information, see [Report Embedded Datasets and Shared Datasets &#40;Report Builder and SSRS&#41;](../../reporting-services/report-data/report-embedded-datasets-and-shared-datasets-report-builder-and-ssrs.md).  

## Next steps

[Charts](../../reporting-services/report-design/charts-report-builder-and-ssrs.md)   
[Formatting a Chart](../../reporting-services/report-design/formatting-a-chart-report-builder-and-ssrs.md)   
[Add a Chart to a Report](../../reporting-services/report-design/add-a-chart-to-a-report-report-builder-and-ssrs.md)   
[Troubleshoot Charts](../../reporting-services/report-design/troubleshoot-charts-report-builder-and-ssrs.md)  

More questions? [Try asking the Reporting Services forum](https://go.microsoft.com/fwlink/?LinkId=620231)
