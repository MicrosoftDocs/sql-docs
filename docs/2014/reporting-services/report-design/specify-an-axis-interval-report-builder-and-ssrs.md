---
title: "Specify an Axis Interval (Report Builder and SSRS) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
ms.assetid: ae46712d-a5bf-44c0-9929-e30ccc1e7e33
author: markingmyname
ms.author: maghan
manager: kfile
---
# Specify an Axis Interval (Report Builder and SSRS)
  The axis interval defines the number of labels and accompanying tick marks on an axis. On the value axis, axis intervals provide a consistent measure of the data points on the chart. However, on the category axis, this functionality can cause categories to appear without axis labels. You can specify the number of intervals you want in the axis Interval property. [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] calculates the number of intervals at run time, based on the data in the result set. For more information about how axis intervals are calculated, see [Formatting Axis Labels on a Chart &#40;Report Builder and SSRS&#41;](formatting-axis-labels-on-a-chart-report-builder-and-ssrs.md).  
  
 This topic is not applicable for date or time values on the category axis. Be default, `DateTime` values appear as days. To specify a different date or time interval, such as a month or time interval, you must format the axis labels and set the axis to display instances of `DateTime` types instead of `String` types. In addition, you must set the Interval property. For more information, see [Format Axis Labels as Dates or Currencies &#40;Report Builder and SSRS&#41;](format-axis-labels-as-dates-or-currencies-report-builder-and-ssrs.md).  
  
 This topic does not apply to pie, doughnut, funnel or pyramid charts, which do not have axes.  
  
> [!NOTE]  
>  The category axis is usually the horizontal or x-axis. However, for bar charts, the category axis is the vertical or y-axis.  
  
 An example of a chart specifying different axis intervals is available as a sample report. For more information about downloading this sample report and others, see [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)][Report Builder and Report Designer sample reports](https://go.microsoft.com/fwlink/?LinkId=198283).  
  
> [!NOTE]  
>  [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]  
  
### To show all category labels on the x-axis  
  
1.  Right-click the category axis and click **Axis Properties**. The **Axis Properties** dialog box opens.  
  
2.  In **Axis Options**, set `Interval` to **1**. Every category group label is displayed. If you want to show every other category group label on the x-axis, type **2**.  
  
3.  [!INCLUDE[clickOK](../../includes/clickok-md.md)]  
  
    > [!NOTE]  
    >  When an axis interval is set, all automatic labeling is disabled. If you specify a value for the axis interval, you may experience unpredictable labeling behavior depending on how many categories are on the category axis.  
  
### To enable a variable interval calculation on an axis  
  
1.  Right-click the chart axis that you want to change, and then click **Axis Properties**. The **Axis Properties** dialog box opens.  
  
2.  In **Axis Options**, set `Interval` to **Auto**. The chart will display the optimal number of category labels that can fit along the axis.  
  
3.  [!INCLUDE[clickOK](../../includes/clickok-md.md)]  
  
## See Also  
 [Formatting a Chart &#40;Report Builder and SSRS&#41;](formatting-a-chart-report-builder-and-ssrs.md)   
 [Formatting Data Points on a Chart &#40;Report Builder and SSRS&#41;](formatting-data-points-on-a-chart-report-builder-and-ssrs.md)   
 [Sort Data in a Data Region &#40;Report Builder and SSRS&#41;](sort-data-in-a-data-region-report-builder-and-ssrs.md)   
 [Axis Properties Dialog Box, Axis Options &#40;Report Builder and SSRS&#41;](../axis-properties-dialog-box-axis-options-report-builder-and-ssrs.md)   
 [Specify a Logarithmic Scale &#40;Report Builder and SSRS&#41;](specify-a-logarithmic-scale-report-builder-and-ssrs.md)   
 [Plot Data on a Secondary Axis &#40;Report Builder and SSRS&#41;](plot-data-on-a-secondary-axis-report-builder-and-ssrs.md)  
  
  
