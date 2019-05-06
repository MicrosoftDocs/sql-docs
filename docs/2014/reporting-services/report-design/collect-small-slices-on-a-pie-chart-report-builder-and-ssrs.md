---
title: "Collect Small Slices on a Pie Chart (Report Builder and SSRS) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
ms.assetid: 21c2b8cb-b9ca-4bc0-bf49-50ba432562f6
author: maggiesMSFT
ms.author: maggies
manager: kfile
---
# Collect Small Slices on a Pie Chart (Report Builder and SSRS)
  When pie charts display too many points of data, they begin to look cluttered. To resolve this issue, you can show all data that falls beneath a certain value as one slice on the pie chart.  
  
 To collect small slices into one slice, first decide whether your threshold for collecting small slices is measured as a percentage of the pie chart or as a fixed value. Then set the CollectedThreshold and CollectedThresholdUsePercent properties.Set the CollectedThreshold property to either the percentage of the chart that a value must fall below to be collected, or the actual threshold data value for collection. Set the CollectedThresholdUsePercent property to `True` to use a percentage or `False` to use an actual value.  
  
 You can also collect small slices into a second pie chart that is called out from a collected slice of the first pie chart. The second pie chart is drawn to the right of the original pie chart.  
  
 You cannot combine slices of funnel or pyramid charts into one slice.  
  
 An example of this chart is available as a sample report. For more information about downloading this sample report and others, see [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)][Report Builder and Report Designer sample reports](https://go.microsoft.com/fwlink/?LinkId=198283).  
  
### To collect small slices into a single slice on a pie chart  
  
1.  Open the Properties pane.  
  
2.  On the design surface, click on any slice of the pie chart. The properties for the series are displayed in the Properties pane.  
  
3.  In the **General** section, expand the **CustomAttributes** node.  
  
4.  Set the CollectedStyle property to **SingleSlice**.  
  
5.  Set the collected threshold value and type of threshold. The following examples are common ways of setting collected thresholds.  
  
    -   **By percentage.** For example, to collect any slice on your pie chart that is less than 10% into a single slice:  
  
         Set the CollectedThresholdUsePercent property to `True`.  
  
         Set the CollectedThreshold property to **10**.  
  
        > [!NOTE]  
        >  If you set CollectedStyle to **SingleSlice**, CollectedThreshold to a value greater than **100**, and CollectedThresholdUsePercent is `True`, the chart will throw an exception because it cannot calculate a percentage. To resolve this issue, set the CollectedThreshold to a value less than **100**.  
  
    -   **By data value.** For example, to collect any slice on your pie chart that is less than 5000 into a single slice:  
  
         Set the CollectedThresholdUsePercent property to `False`.  
  
         Set the CollectedThreshold property to **5000**.  
  
6.  Set the CollectedLabel property to a string that represents the text label that will be shown on the collected slice.  
  
7.  (Optional) Set the CollectedSliceExploded, CollectedColor, CollectedLegendText and CollectedToolTip properties. These properties change the appearance, color, label text, legend text and tooltip aspects of the single slice.  
  
### To collect small slices into a secondary, callout pie chart  
  
1.  Follow Steps 1-3 from above.  
  
2.  Set the CollectedStyle property to **CollectedPie**.  
  
3.  Set the CollectedThresholdproperty to a value that represents the threshold at which small slices will be collected into one slice. When the CollectedStyle property is set to **CollectedPie**, the CollectedThresholdUsePercentproperty is always set to `True`, and the collected threshold is always measured in percent.  
  
4.  (Optional) Set the CollectedColor, CollectedLabel, CollectedLegendText and CollectedToolTip properties. All other properties named "Collected" do not apply to the collected pie.  
  
> [!NOTE]  
>  The secondary pie chart is calculated based on the small slices in your data so it will only appear in Preview. It does not appear on the design surface.  
  
> [!NOTE]  
>  You cannot format the secondary pie chart. For this reason, it is strongly recommended to use the first approach when collecting pie slices.  
  
## See Also  
 [Pie Charts &#40;Report Builder and SSRS&#41;](charts-report-builder-and-ssrs.md)   
 [Formatting Data Points on a Chart &#40;Report Builder and SSRS&#41;](formatting-data-points-on-a-chart-report-builder-and-ssrs.md)   
 [Display Data Point Labels Outside a Pie Chart &#40;Report Builder and SSRS&#41;](display-data-point-labels-outside-a-pie-chart-report-builder-and-ssrs.md)   
 [Display Percentage Values on a Pie Chart &#40;Report Builder and SSRS&#41;](display-percentage-values-on-a-pie-chart-report-builder-and-ssrs.md)   
 [Add 3D Effects to a Chart &#40;Report Builder and SSRS&#41;](chart-effects-add-3d-effects-report-builder.md)  
  
  
