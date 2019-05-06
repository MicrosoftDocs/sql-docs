---
title: "Display Percentage Values on a Pie Chart (Report Builder and SSRS) | Microsoft Docs"
ms.date: 03/01/2017
ms.prod: reporting-services
ms.prod_service: "reporting-services-sharepoint, reporting-services-native"
ms.technology: report-design


ms.topic: conceptual
ms.assetid: eb905fc1-5235-4773-a27e-b07be9318be5
author: maggiesMSFT
ms.author: maggies
---
# Display Percentage Values on a Pie Chart (Report Builder and SSRS)
In [!INCLUDE[ssRSnoversion_md](../../includes/ssrsnoversion-md.md)] paginated reports, by default the legend shows categories. You may also want percentages in the legend or the pie slices themselves.   

![report-builder-pie-chart-preview-percents](../../reporting-services/media/report-builder-pie-chart-preview-percents.png)

 The [Tutorial: Add a Pie Chart to Your Report (Report Builder)](Tutorial:%20Add%20a%20Pie%20Chart%20to%20Your%20Report%20\(Report%20Builder\).md) walks you through adding percentages to pie slices, if you'd like to try this with sample data first.
 
  
## To display percentage values as labels on a pie chart  
  
1.  Add a pie chart to your report. For more information, see [Add a Chart to a Report &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/add-a-chart-to-a-report-report-builder-and-ssrs.md).  
  
2.  On the design surface, right-click on the pie and select **Show Data Labels**. The data labels should appear within each slice on the pie chart.  
  
3.  On the design surface, right-click on the labels and select **Series Label Properties**. The **Series Label Properties** dialog box appears.  
  
4.  Type **#PERCENT** for the **Label data** option.  
  
5.  (Optional) To specify how many decimal places the label shows, type "#PERCENT{P*n*}" where *n* is the number of decimal places to display. For example, to display no decimal places, type "#PERCENT{P0}".  
  
## To display percentage values in the legend of a pie chart  
  
1.  On the design surface, right-click on the pie chart and select **Series Properties**. The **Series Properties** dialog box displays.  
  
2.  In **Legend**, type **#PERCENT** for the **Custom legend text** property.  
  
## See Also  
* [Tutorial: Add a Pie Chart to Your Report (Report Builder)](Tutorial:%20Add%20a%20Pie%20Chart%20to%20Your%20Report%20\(Report%20Builder\).md)
*  [Pie Charts &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/pie-charts-report-builder-and-ssrs.md)   
*  [Formatting the Legend on a Chart &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/chart-legend-formatting-report-builder.md)   
*  [Display Data Point Labels Outside a Pie Chart &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/display-data-point-labels-outside-a-pie-chart-report-builder-and-ssrs.md)   
 
  
