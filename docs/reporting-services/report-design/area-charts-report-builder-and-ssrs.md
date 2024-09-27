---
title: "Area charts in a paginated report"
description: Learn how to use area charts in a paginated report. Area charts display a series as a set of points connected by a line, with all the area filled in below the line in Report Builder.
author: maggiesMSFT
ms.author: maggies
ms.date: 09/25/2024
ms.service: reporting-services
ms.subservice: report-design
ms.topic: conceptual
ms.custom:
  - updatefrequency5
---
# Area charts in a paginated report (Report Builder)

[!INCLUDE[ssrs-appliesto](../../includes/ssrs-appliesto.md)] [!INCLUDE [ssrs-appliesto-ssrs-rb](../../includes/ssrs-appliesto-ssrs-rb.md)] [!INCLUDE [ssrs-appliesto-pbi-rb](../../includes/ssrs-appliesto-pbi-rb.md)] [!INCLUDE [ssrb-applies-to-ssdt-yes](../../includes/ssrb-applies-to-ssdt-yes.md)]

  An area chart in a paginated report displays a series as a set of points connected by a line, with all the area filled in below the line. For more information on how to add data to an area chart, see [Charts &#40;Report Builder&#41;](../../reporting-services/report-design/charts-report-builder-and-ssrs.md).  
  
 The following illustration shows an example of a stacked area chart. The data is well suited for display on a stacked area chart because the chart can display totals for all series and the proportion that each series contributes to the total.  
  
 :::image type="content" source="../../reporting-services/report-design/media/areachart.gif" alt-text="Screenshot of an area chart with data for Japan, the United States, and Europe.":::
  
> [!NOTE]  
>  [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]  
  
## Variations  
  
-   **Stacked area**: An area chart where multiple series are stacked vertically. If there's only one series in your chart, the stacked area chart displays the same as an area chart.  
  
-   **Percent stacked area**: An area chart where multiple series are stacked vertically to fit the entire chart area. If there's only one series in your chart, the stacked area chart displays the same as an area chart.  
  
-   **Smooth area**: An area chart where the data points are connected by a smooth line instead of a regular line. Use a smooth area chart instead of an area chart when you're more concerned with displaying trends than with displaying the values of individual data points.  
  
## Data considerations for area charts  
  
-   Along with the line chart, the area chart is the only chart type that displays data contiguously. For this reason, an area chart is commonly used to represent data that occurs over a continuous period of time.  
  
-   A percent stacked area chart is useful for showing proportional data that occurs over time.  
  
-   If there's only one series, a stacked area chart is drawn as an area chart.  
  
-   In a plain area chart, if the values in multiple series are similar, the areas might overlap, obscuring important data point values. You can resolve this issue by changing the chart type to a stacked area chart, which is designed to show multiple series on an area chart.  
  
-   If your stacked area chart contains gaps, it's possible that your dataset includes empty values, which is shown as a vacant section on a stacked area chart. If your dataset includes empty values, consider inserting empty points on the chart. Adding empty points fills in the empty areas on the chart with a different color to indicate null or zero values. For more information, see [Add empty points to a chart &#40;Report Builder&#41;](../../reporting-services/report-design/add-empty-points-to-a-chart-report-builder-and-ssrs.md).  
  
-   Area chart types are similar to column and line charts in behavior. If you're making a comparison between multiple series, consider using a column chart instead. If you're analyzing trends over a period of time, consider using a line chart.  
  
## Related content

- [Charts &#40;Report Builder&#41;](../../reporting-services/report-design/charts-report-builder-and-ssrs.md)
- [Chart types &#40;Report Builder&#41;](../../reporting-services/report-design/chart-types-report-builder-and-ssrs.md)
- [Line charts &#40;Report Builder&#41;](../../reporting-services/report-design/line-charts-report-builder-and-ssrs.md)
- [Change a chart type &#40;Report Builder&#41;](../../reporting-services/report-design/change-a-chart-type-report-builder-and-ssrs.md)
- [Empty and null data points in charts &#40;Report Builder&#41;](../../reporting-services/report-design/empty-and-null-data-points-in-charts-report-builder-and-ssrs.md)
