---
title: "Column Charts (Report Builder and SSRS) | Microsoft Docs"
ms.date: 03/03/2017
ms.prod: reporting-services
ms.prod_service: "reporting-services-sharepoint, reporting-services-native"
ms.technology: report-design


ms.topic: conceptual
ms.assetid: ae8c138b-e356-4ad8-862c-a4a8d0c04149
author: markingmyname
ms.author: maghan
---
# Column Charts (Report Builder and SSRS)
  A column chart displays a series as a set of vertical bars that are grouped by category. Column charts are useful for showing data changes over a period of time or for illustrating comparisons among items. The plain column chart is closely related to the bar chart, which displays series as sets of horizontal bars, and the range column chart, which displays series as sets of vertical bars with varying beginning and end points. For more information, see [Bar Charts &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/bar-charts-report-builder-and-ssrs.md) and [Range Charts &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/range-charts-report-builder-and-ssrs.md).  
  
 The column chart is well suited for this data because all three series share a common time period, allowing for valid comparisons to be made.  
  
> [!NOTE]  
>  [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]  
  
## Variations of a Column Chart  
  
-   **Stacked**. A column chart where multiple series are stacked vertically. If there is only one series in your chart, the stacked column chart will display the same as a column chart.  
  
-   **Percent stacked**. A column chart where multiple series are stacked vertically to fit 100% of the chart area. If there is only one series in your chart, all the column bars will fit to 100% of the chart area.  
  
-   **3D clustered**. A column chart that shows individual series in separate rows on a 3D chart.  
  
-   **3D cylinder**. A column chart whose bars are shaped like cylinders on a 3D chart.  
  
-   **Histogram**. A column chart which the chart calculates so that its bars are arranged in a normal distribution.  
  
-   **Pareto**. A column chart whose bars are arranged from highest to lowest.  
  
## Data Considerations for a Column Chart  
  
-   Bar and column charts are most commonly used to show comparisons between groups. If more than three series are present on the chart, consider using a stacked bar or column chart. You can also collect stacked bar or column charts into multiple groups if you have several series on your chart. For more information, see [Bar Charts &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/bar-charts-report-builder-and-ssrs.md).  
  
-   In a column chart, you have less space for category axis labels to display horizontally. If you have longer category labels, consider using a bar chart or changing the rotation angle of the label.  
  
-   You can add special drawing styles to the individual bars on a column chart to increase its visual impact. Drawing styles include wedge, emboss, cylinder and light-to-dark. These effects are designed to improve the appearance of your 2D chart. If you are using a 3D chart, the drawing styles will still be applied, but may not have the same effect. For more information about how to add a drawing style to a bar chart, see [Add Bevel, Emboss, and Texture Styles to a Chart &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/chart-effects-add-bevel-emboss-or-texture-report-builder.md).  
  
-   Unique to column charts is the ability to show your chart as a histogram or Pareto chart. To do so, set the ShowColumnAs property to **Histogram** or **Pareto** in the Properties window to **true**.  
  
## See Also  
 [Charts &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/charts-report-builder-and-ssrs.md)   
 [Chart Types &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/chart-types-report-builder-and-ssrs.md)   
 [Bar Charts &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/bar-charts-report-builder-and-ssrs.md)   
 [Range Charts &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/range-charts-report-builder-and-ssrs.md)   
 [Tutorial: Add a Bar Chart to Your Report &#40;Report Builder&#41;](../../reporting-services/tutorial-add-a-bar-chart-to-your-report-report-builder.md)   
 [Empty and Null Data Points in Charts &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/empty-and-null-data-points-in-charts-report-builder-and-ssrs.md)  
  
  
