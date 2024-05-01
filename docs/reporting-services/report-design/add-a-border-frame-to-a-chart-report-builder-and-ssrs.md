---
title: "Add a border frame to a chart in a paginated report"
description: Learn how to give a chart more visual impact using a border frame around the outside of the chart in paginated reports in Report Builder.
author: maggiesMSFT
ms.author: maggies
ms.date: 03/03/2017
ms.service: reporting-services
ms.subservice: report-design
ms.topic: conceptual
ms.custom: updatefrequency5
---
# Add a border frame to a chart in a paginated report (Report Builder)

[!INCLUDE[ssrs-appliesto](../../includes/ssrs-appliesto.md)] [!INCLUDE [ssrs-appliesto-ssrs-rb](../../includes/ssrs-appliesto-ssrs-rb.md)] [!INCLUDE [ssrs-appliesto-pbi-rb](../../includes/ssrs-appliesto-pbi-rb.md)] [!INCLUDE [ssrb-applies-to-ssdt-yes](../../includes/ssrb-applies-to-ssdt-yes.md)]

  To give a chart in a paginated report more visual impact, consider using a border frame around the outside of the chart. You can select a border frame by using the **Chart Properties** dialog or by using the **Properties** pane. The chart border frames can't be applied to any other data region.  
  
> [!NOTE]  
>  [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]  
  
### Apply a border to a chart  
  
1.  Right-click anywhere on the chart and select **Chart Properties**.  
  
    > [!NOTE]  
    >  If you don't see the **Chart Properties**, point to **Chart** on the shortcut menu and select **Chart Properties**.  
  
1.  Select **Border**, and choose the type of border to apply to the chart.  
  
1.  (Optional) Select a value or type an expression that represents the style of the border.  
  
1.  (Optional) Specify the color of the line that is drawn around the chart as the border.  
  
    > [!NOTE]  
    >  The **Line color** list contains common colors. If you want to select from a list of more colors, choose **More colors** in the list or select the expression (**fx**) button next to the list to bring up the **Expression** editor.  
  
1.  (Optional) Specify the width of the border. Valid values are between 0.25 pt and 20 pt. Consider keeping the size of your border to between 1 pt and 3 pt for the best visual effect.  
  
1.  (Optional) If your report contains a background color other than white, consider defining a page color that is the same color. The page color is the background color outside of the border line.  
  
1.  (Optional) If you choose a Frame type, specify a style and color for the frame. The **Frame fill color** list contains common colors.  
  
## Related content 
 [Charts &#40;Report Builder&#41;](../../reporting-services/report-design/charts-report-builder-and-ssrs.md)   
 [Format a chart &#40;Report Builder&#41;](../../reporting-services/report-design/formatting-a-chart-report-builder-and-ssrs.md)   
 [Add bevel, emboss, and texture styles to a chart &#40;Report Builder&#41;](../../reporting-services/report-design/chart-effects-add-bevel-emboss-or-texture-report-builder.md)  
  
  
