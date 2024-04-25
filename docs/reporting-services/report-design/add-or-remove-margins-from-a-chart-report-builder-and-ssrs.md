---
title: "Add or remove margins from a paginated report chart"
description: Add or remove margins from a column or scatter chart in Report Builder. Improve readability or appearance of paginated reports.
author: maggiesMSFT
ms.author: maggies
ms.date: 03/03/2017
ms.service: reporting-services
ms.subservice: report-design
ms.topic: conceptual
ms.custom: updatefrequency5
---
# Add or remove margins from a paginated report chart (Report Builder)

[!INCLUDE[ssrs-appliesto](../../includes/ssrs-appliesto.md)] [!INCLUDE [ssrs-appliesto-ssrs-rb](../../includes/ssrs-appliesto-ssrs-rb.md)] [!INCLUDE [ssrs-appliesto-pbi-rb](../../includes/ssrs-appliesto-pbi-rb.md)] [!INCLUDE [ssrb-applies-to-ssdt-yes](../../includes/ssrb-applies-to-ssdt-yes.md)]

For Column and Scatter chart types in paginated reports, the chart automatically adds side margins on the ends of the x-axis. In Bar chart types, the chart automatically adds side margins on the ends of the y-axis. In all other chart types, the chart doesn't add side margins. You can't change the size of the margin.  
  
 This article doesn't apply to pie, doughnut, funnel, or pyramid chart types.  
  
> [!NOTE]  
>  [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]  
  
## Enable or disable side margins  
  
1.  Right-click the axis and select **Axis Properties**. The **Vertical** or **HorizontalAxis Properties** dialog appears.  
  
1.  On the **Axis Options** page, set the **Side margins** property:  
  
    -   **Auto**: The chart determines whether to add a side margin based on the chart type.  
  
    -   **Disabled**: Bar, column, and scatter charts have no side margins.  
  
1.  Select **OK**.
  
## Related content
 [Format axis labels on a chart &#40;Report Builder&#41;](../../reporting-services/report-design/formatting-axis-labels-on-a-chart-report-builder-and-ssrs.md)   
 [Axis Properties dialog, axis options &#40;Report Builder&#41;](/previous-versions/sql/)   
 [Specify an axis interval &#40;Report Builder&#41;](../../reporting-services/report-design/specify-an-axis-interval-report-builder-and-ssrs.md)   
 [Format axis labels as dates or currencies &#40;Report Builder&#41;](../../reporting-services/report-design/format-axis-labels-as-dates-or-currencies-report-builder-and-ssrs.md)   
 [Charts &#40;Report Builder&#41;](../../reporting-services/report-design/charts-report-builder-and-ssrs.md)  
  
