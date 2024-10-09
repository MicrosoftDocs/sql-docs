---
title: "Bar charts in a paginated report"
description: Display data horizontally with a bar chart to represent data in a paginated report with a finite start and end date in Report Builder.
author: maggiesMSFT
ms.author: maggies
ms.date: 09/25/2024
ms.service: reporting-services
ms.subservice: report-design
ms.topic: conceptual
ms.custom:
  - updatefrequency5
---
# Bar charts in a paginated report (Report Builder)

[!INCLUDE[ssrs-appliesto](../../includes/ssrs-appliesto.md)] [!INCLUDE [ssrs-appliesto-ssrs-rb](../../includes/ssrs-appliesto-ssrs-rb.md)] [!INCLUDE [ssrs-appliesto-pbi-rb](../../includes/ssrs-appliesto-pbi-rb.md)] [!INCLUDE [ssrb-applies-to-ssdt-yes](../../includes/ssrb-applies-to-ssdt-yes.md)]

  A bar chart displays series as sets of horizontal bars in a paginated report. The plain bar chart is closely related to the column chart, which displays series as sets of vertical bars. The bar chart is also related to the range bar chart, which displays series as sets of horizontal bars with varying beginning and end points.  
  
 The bar chart is the only chart type that displays data horizontally. For this reason, it's popular for representing data that occurs over time, with a finite start and end date. It's also popular for showing categorical information since the categories can be displayed horizontally. For more information about how to add data to a bar chart, see [Charts &#40;Report Builder&#41;](../../reporting-services/report-design/charts-report-builder-and-ssrs.md).  
  
 The following illustration shows a bar chart. The bar chart is well suited for this data because all three series share a common time period, allowing for valid comparisons to be made.  
  
 :::image type="content" source="../../reporting-services/report-design/media/barchart.gif" alt-text="Screenshot of a bar chart with data for Japan, the United States, and Europe.":::
 
  
> [!NOTE]  
>  [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]  
  
## Variations of the bar chart  
  
-   **Stacked**: A bar chart where multiple series are stacked vertically. If there's only one series in your chart, the stacked bar chart displays the same as a bar chart.  
  
-   **Percent stacked**: A bar chart where multiple series are stacked vertically to fit 100% of the chart area. If there's only one series in your chart, all the bars fit to 100% of the chart area.  
  
-   **3D clustered**: A bar chart that shows individual series in separate rows on a 3D chart.  
  
-   **3D cylinder**: A bar chart that shapes the bars as cylinders on a 3D chart.  
  
## Data considerations for bar charts  
  
-   Bar charts have their axes reversed. The category axis is the vertical axis, or y-axis, and the value axis is the horizontal axis, or x-axis. In a bar chart, you have more space for category labels to be displayed along the y-axis as a list that reads from top to bottom.  
  
-   Bar and column charts are most commonly used to show comparisons between groups. If more than three series are present on the chart, consider using a stacked bar or column chart. You can also collect stacked bar or column charts into multiple groups if you have several series on your chart.  
  
-   A bar chart displays values from left to right, which might be more intuitive when displaying data related to durations.  
  
-   If you're looking to add bars to a table or matrix within the report, consider using a linear gauge instead of a bar chart. The linear gauge is designed to show one value instead of multiple groups, so it's more flexible for use within a list or table data region. For more information, see [Gauges &#40;Report Builder&#41;](../../reporting-services/report-design/gauges-report-builder-and-ssrs.md).  
  
-   You can add special drawing styles to the individual bars on a bar chart to increase its visual impact. Drawing styles include wedge, emboss, cylinder, and light-to-dark. These effects are designed to improve the appearance of your 2D chart. If you're using a 3D chart, the drawing styles are still applied, but might not have the same effect. For more information about how to add a drawing style to a bar chart, see [Add bevel, emboss, and texture styles to a chart &#40;Report Builder&#41;](../../reporting-services/report-design/chart-effects-add-bevel-emboss-or-texture-report-builder.md).  
  
-   Stacked bar charts place series on top of each other to create a single bar stack. You have the option of separating the stacked bar chart into multiple sets of stacks for each category. The grouped stacked chart is displayed side-by-side. You can have any number of grouped stacked series in a chart.  
  
-   When data point labels are shown on a bar chart, the labels are placed on the outside of each bar. This placement can cause labels to overlap when the bars take up all of the allotted space within the chart area. You can change the position of the data point labels displayed for each bar by setting the **BarLabelStyle** property in the **Properties** pane.  
  
-   If there are many data points in your dataset relative to the size of your chart, the size of the columns or bars and the spacing between them are reduced. To manually set the width of the columns in a chart, modify their width, in pixels, by modifying the **PointWidth** property. By default, this property has a value of 0.8. When you increase the width of the columns or bars in a chart, the space between each column or bar decreases.  
  
## Related content

- [Charts &#40;Report Builder&#41;](../../reporting-services/report-design/charts-report-builder-and-ssrs.md)
- [Chart types &#40;Report Builder&#41;](../../reporting-services/report-design/chart-types-report-builder-and-ssrs.md)
- [Empty and null data points in charts &#40;Report Builder&#41;](../../reporting-services/report-design/empty-and-null-data-points-in-charts-report-builder-and-ssrs.md)
- [Column charts &#40;Report Builder&#41;](../../reporting-services/report-design/column-charts-report-builder-and-ssrs.md)
- [Range charts &#40;Report Builder&#41;](../../reporting-services/report-design/range-charts-report-builder-and-ssrs.md)
- [Format series colors on a chart &#40;Report Builder&#41;](../../reporting-services/report-design/formatting-series-colors-on-a-chart-report-builder-and-ssrs.md)
- [Format axis labels on a chart &#40;Report Builder&#41;](../../reporting-services/report-design/formatting-axis-labels-on-a-chart-report-builder-and-ssrs.md)
- [Format the legend on a chart &#40;Report Builder&#41;](../../reporting-services/report-design/chart-legend-formatting-report-builder.md)
- [Add bevel, emboss, and texture styles to a chart &#40;Report Builder&#41;](../../reporting-services/report-design/chart-effects-add-bevel-emboss-or-texture-report-builder.md)
- [Tutorial: Add a bar chart to a report (Report Builder)](../tutorial-add-a-bar-chart-to-your-report-report-builder.md)
- [Tutorial: Add a bar chart to a report](/previous-versions/sql/sql-server-2008-r2/cc281302(v=sql.105))
