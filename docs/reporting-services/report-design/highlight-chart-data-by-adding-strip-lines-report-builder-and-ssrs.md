---
title: "Highlight Chart Data by Adding Strip Lines (Report Builder and SSRS) | Microsoft Docs"
ms.date: 03/01/2017
ms.prod: reporting-services
ms.prod_service: "reporting-services-sharepoint, reporting-services-native"
ms.technology: report-design


ms.topic: conceptual
ms.assetid: addd6137-4b6e-4e88-a7e8-9600fcd1ccce
author: maggiesMSFT
ms.author: maggies
---
# Highlight Chart Data by Adding Strip Lines (Report Builder and SSRS)
  Strip lines, or strips, are horizontal or vertical ranges that shade the background of the chart in regular or custom intervals. You can use strip lines to:  
  
-   Improve readability for looking up individual values on the chart. Specify strip lines at regular intervals to help separate data points when reading the chart.  
  
-   Highlight dates that occur at regular intervals. For example, in a sales report you might use strip lines to identify weekend data points.  
  
-   Highlight a specific key range. Using the previous example, you can use one strip line to highlight the highest range of sales that is between $80-100.  
  
 Strip lines are not applicable to Shape or Polar chart types.  
  
> [!NOTE]  
>  [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]  
  
### To display interlaced strip lines at regular intervals on a chart  
  
1.  To show horizontal strip lines, right-click the vertical chart axis and click **VerticalAxis Properties**.  
  
     To show vertical strip lines, right-click the horizontal chart axis and click **Horizontal Axis Properties**.  
  
2.  Select the **Use interlacing** option. Grey strip lines will appear on your chart.  
  
3.  (Optional) Specify a color for the strip lines using the adjacent **Color** drop-down list.  
  
### To display interlaced strip lines at custom intervals on a chart  
  
1.  To show horizontal strip lines, right-click the vertical chart axis and click **VerticalAxis Properties**.  
  
     To show vertical strip lines, right-click the horizontal chart axis and click **Horizontal Axis Properties**.  
  
     The axis properties are displayed in the Properties window.  
  
2.  In the **Appearance** section of the Properties pane, for the StripLines property, click the Edit Collection (...) button to open the **ChartStripLine Collection Editor**.  
  
3.  Click **Add** to add a new strip line to the collection.  
  
4.  Click StripWidth to specify the width of the strip line, measured in inches on the report. If you are highlighting dates or times, click StripWidthType and select a time interval.  
  
5.  Type a value or expression for the Interval to specify how often the strip line will repeat.  For example, if you specify an interval of 10, and your strip line width is 5, strip lines will display at values of 0 to 5, 15 to 20, 30 to 35, and so on.  
  
> [!NOTE]  
>  By default, Interval is set to Auto, which means the chart will not calculate an interval for custom strip lines. The chart only calculates intervals for strip lines if an interval value is set.  
  
## See Also  
 [Formatting Axis Labels on a Chart &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/formatting-axis-labels-on-a-chart-report-builder-and-ssrs.md)   
 [Formatting a Chart &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/formatting-a-chart-report-builder-and-ssrs.md)   
 [Add a Moving Average to a Chart &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/add-a-moving-average-to-a-chart-report-builder-and-ssrs.md)  
  
  
