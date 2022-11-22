---
title: "Add or remove margins from a paginated report chart | Microsoft Docs"
description: Add or remove margins from a column or scatter chart in Report Builder. Improve readability or appearance of paginated reports. 
ms.date: 03/03/2017
ms.service: reporting-services
ms.subservice: report-design


ms.topic: conceptual
ms.assetid: 91c43f58-5771-4d33-a54d-0e802d2f5cba
author: maggiesMSFT
ms.author: maggies
---
# Add or remove margins from a paginated report chart (Report Builder)

[!INCLUDE[ssrs-appliesto](../../includes/ssrs-appliesto.md)] [!INCLUDE [ssrs-appliesto-ssrs-rb](../../includes/ssrs-appliesto-ssrs-rb.md)] [!INCLUDE [ssrs-appliesto-pbi-rb](../../includes/ssrs-appliesto-pbi-rb.md)] [!INCLUDE [ssrb-applies-to-ssdt-yes](../../includes/ssrb-applies-to-ssdt-yes.md)]

For Column and Scatter chart types in paginated reports, the chart automatically adds side margins on the ends of the x-axis. In Bar chart types, the chart automatically adds side margins on the ends of the y-axis. In all other chart types, the chart does not add side margins. You cannot change the size of the margin.  
  
 This topic does not apply to pie, doughnut, funnel, or pyramid chart types.  
  
> [!NOTE]  
>  [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]  
  
## To enable or disable side margins  
  
1.  Right-click the axis and select **Axis Properties**. The **Vertical** or **HorizontalAxis Properties** dialog box appears.  
  
2.  On the **Axis Options** page, set the **Side margins** property:  
  
    -   **Auto** The chart will determine whether to add a side margin based on the chart type.  
  
    -   **Disabled** Bar, column, and scatter charts will have no side margins.  
  
3.  Select **OK**.
  
## See Also  
 [Formatting Axis Labels on a Chart &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/formatting-axis-labels-on-a-chart-report-builder-and-ssrs.md)   
 [Axis Properties Dialog Box, Axis Options &#40;Report Builder and SSRS&#41;](/previous-versions/sql/)   
 [Specify an Axis Interval &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/specify-an-axis-interval-report-builder-and-ssrs.md)   
 [Format Axis Labels as Dates or Currencies &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/format-axis-labels-as-dates-or-currencies-report-builder-and-ssrs.md)   
 [Charts &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/charts-report-builder-and-ssrs.md)  
  
