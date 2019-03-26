---
title: "Hide Legend Items on the Chart (Report Builder and SSRS) | Microsoft Docs"
ms.date: 03/01/2017
ms.prod: reporting-services
ms.prod_service: "reporting-services-sharepoint, reporting-services-native"
ms.technology: report-design


ms.topic: conceptual
ms.assetid: 92256240-0cd5-4be4-8904-d1e3b93cb6b3
author: markingmyname
ms.author: maghan
---
# Chart Legend - Hide Items (Report Builder)
By default, any series added to a non-Shape chart in a [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] paginated report will be added as an item in the legend. For pie, doughnut, funnel, and pyramid charts, any series added to the chart will add individual data points in the legend.  
  
 You can hide any item on the legend. When you hide a legend item, it will still appear in the chart. This is useful in situations where you do not want to show more information for a series that is added to the chart. For example, if you have added a calculated series like a moving average to the chart, you may want to hide this entry in the legend so that more information is only shown for the original series.  
  
> [!NOTE]  
>  [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]  
  
## To hide an item from display in the legend  
  
1.  Right-click on the series you want to hide and select **Series Properties**.  
  
2.  Click **Legend**. Select the **Do not show this series in a legend** option.  
  
    > [!NOTE]  
    >  You cannot hide a series for one group and not for the others. If you have added a field to the **Series Groups** area, all series belonging to this group will be hidden.  
  
## See Also  
 [Formatting the Legend on a Chart &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/chart-legend-formatting-report-builder.md)   
 [Formatting Data Points on a Chart &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/formatting-data-points-on-a-chart-report-builder-and-ssrs.md)   
 [Change the Text of a Legend Item &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/chart-legend-change-item-text-report-builder.md)   
 [Add a Moving Average to a Chart &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/add-a-moving-average-to-a-chart-report-builder-and-ssrs.md)   
 [Legend Properties Dialog Box, General &#40;Report Builder and SSRS&#41;](https://msdn.microsoft.com/library/db718f8f-f185-422f-871c-96f0749e5893)  
  
  
