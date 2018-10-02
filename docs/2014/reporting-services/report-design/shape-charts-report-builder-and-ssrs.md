---
title: "Shape Charts (Report Builder and SSRS) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
ms.assetid: 4b8404c1-aa89-4350-8bd6-203bc0446ee4
author: maggiesMSFT
ms.author: maggies
manager: craigg
---
# Shape Charts (Report Builder and SSRS)
  A shape chart displays value data as percentages of a whole. Shape charts are typically used to show proportional comparisons between different values in a set. Categories are represented by individual segments of the shape. The size of the segment is determined by the value. Shape charts are similar in use to pie charts, except that they order categories from largest to smallest.  
  
 A funnel chart displays values as progressively decreasing proportions. The size of the area is determined by the series value as a percentage of the total of all values. For example, you might use a funnel chart to display Web site visitor trends. It is likely that the funnel chart will display a wide area at the top, indicating visitor page hits to the homepage, and the other areas will be proportionally smaller. For more information about how to add data to a funnel chart, see [Charts &#40;Report Builder and SSRS&#41;](charts-report-builder-and-ssrs.md).  
  
 The following illustration shows an example of a funnel chart.  
  
 ![Funnel chart](../media/rs-funnelchart.gif "Funnel chart")  
  
> [!NOTE]  
>  [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]  
  
## Variations  
  
-   **Pyramid**. A pyramid chart displays proportional data so that the chart looks like a pyramid.  
  
## Data Considerations for Shape Charts  
  
-   Shape charts are popular in reports because of their visual impact. However, shape charts are a very simplified chart type that may not best represent your data. Consider using a shape chart only once the data has been aggregated to seven data points or less. In general, use the shape chart to display only one category per data region.  
  
-   Shape charts display each data group as a separate segment of the chart. You must add at least one data field and one category field. If more than one data field is added to a shape chart, the shape chart will display both data fields in the same chart.  
  
-   Shape charts are most effective for showing proportional percentages in sorted order. However, in order to maintain consistency, the chart does not sort the values in your dataset by default. Consider ordering your values from highest to lowest to most accurately represent your data as a funnel or a pyramid. For more information, see [Filter, Group, and Sort Data &#40;Report Builder and SSRS&#41;](filter-group-and-sort-data-report-builder-and-ssrs.md).  
  
-   Null, empty, negative and zero values have no effect when calculating ratios. For this reason, these values are not shown on a shape chart. If you want to visually indicate these types of values on your chart, change the chart type to be something other than a shape chart. For more information about how to add empty points to a non-shape chart, see [Add Empty Points to the Chart &#40;Report Builder and SSRS&#41;](add-empty-points-to-a-chart-report-builder-and-ssrs.md).  
  
-   If you are defining your own colors on a shape chart using a custom palette, be sure that you have enough colors in your palette to highlight each data point with its own unique color. For more information, see [Formatting Series Colors on a Chart &#40;Report Builder and SSRS&#41;](formatting-series-colors-on-a-chart-report-builder-and-ssrs.md).  
  
-   Unlike all other chart types, a shape chart will display individual data points, and not individual series, in its legend.  
  
-   Settings for the value and category axis are ignored for funnel charts. If you have multiple category or series groups, the group labels are displayed in the chart legend.  
  
-   Shape chart types cannot be combined with any other chart type in the same chart area. If you have to show comparisons between data displayed on a shape chart, and data displayed on another chart type, you will need to add a second chart area.  
  
-   Adding 3-D effects significantly improves the overall appearance of a shape chart type.  
  
-   You can apply additional drawing styles to pie and doughnut charts for increased visual impact. See [Formatting Series Colors on a Chart &#40;Report Builder and SSRS&#41;](formatting-series-colors-on-a-chart-report-builder-and-ssrs.md) for more information.  
  
## See Also  
 [Charts &#40;Report Builder and SSRS&#41;](charts-report-builder-and-ssrs.md)   
 [Formatting a Chart &#40;Report Builder and SSRS&#41;](formatting-a-chart-report-builder-and-ssrs.md)   
 [Empty and Null Data Points in Charts &#40;Report Builder and SSRS&#41;](empty-and-null-data-points-in-charts-report-builder-and-ssrs.md)   
 [Pie Charts &#40;Report Builder and SSRS&#41;](pie-charts-report-builder-and-ssrs.md)  
  
  
