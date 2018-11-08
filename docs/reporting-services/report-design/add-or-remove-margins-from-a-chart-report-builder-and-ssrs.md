---
title: "Add or Remove Margins from a Chart (Report Builder and SSRS) | Microsoft Docs"
ms.date: 03/03/2017
ms.prod: reporting-services
ms.prod_service: "reporting-services-sharepoint, reporting-services-native"
ms.technology: report-design


ms.topic: conceptual
ms.assetid: 91c43f58-5771-4d33-a54d-0e802d2f5cba
author: maggiesMSFT
ms.author: maggies
---
# Add or Remove Margins from a Chart (Report Builder and SSRS)
For Column and Scatter chart types in [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] paginated reports, the chart automatically adds side margins on the ends of the x-axis. In Bar chart types, the chart automatically adds side margins on the ends of the y-axis. In all other chart types, the chart does not add side margins. You cannot change the size of the margin.  
  
 This topic does not apply to pie, doughnut, funnel, or pyramid chart types.  
  
> [!NOTE]  
>  [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]  
  
## To enable or disable side margins  
  
1.  Right-click the axis and select **Axis Properties**. The **Vertical** or **HorizontalAxis Properties** dialog box appears.  
  
2.  On the **Axis Options** page, set the **Side margins** property:  
  
    -   **Auto** The chart will determine whether to add a side margin based on the chart type.  
  
    -   **Disabled** Bar, column, and scatter charts will have no side margins.  
  
3.  [!INCLUDE[clickOK](../../includes/clickok-md.md)]  
  
## See Also  
 [Formatting Axis Labels on a Chart &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/formatting-axis-labels-on-a-chart-report-builder-and-ssrs.md)   
 [Axis Properties Dialog Box, Axis Options &#40;Report Builder and SSRS&#41;](https://msdn.microsoft.com/library/b276e210-7a12-48ae-971b-7dabae51df11)   
 [Specify an Axis Interval &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/specify-an-axis-interval-report-builder-and-ssrs.md)   
 [Format Axis Labels as Dates or Currencies &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/format-axis-labels-as-dates-or-currencies-report-builder-and-ssrs.md)   
 [Charts &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/charts-report-builder-and-ssrs.md)  
  
  
