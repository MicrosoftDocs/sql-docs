---
title: "Add a border frame to a chart in a paginated report | Microsoft Docs"
description: Learn how to give a chart more visual impact using a border frame around the outside of the chart in paginated reports in Report Builder. 
ms.date: 03/03/2017
ms.service: reporting-services
ms.subservice: report-design


ms.topic: conceptual
ms.assetid: ca0c5040-40bb-4cb7-bc2b-5bcbe73858bb
author: maggiesMSFT
ms.author: maggies
---
# Add a border frame to a chart in a paginated report (Report Builder)

[!INCLUDE[ssrs-appliesto](../../includes/ssrs-appliesto.md)] [!INCLUDE [ssrs-appliesto-ssrs-rb](../../includes/ssrs-appliesto-ssrs-rb.md)] [!INCLUDE [ssrs-appliesto-pbi-rb](../../includes/ssrs-appliesto-pbi-rb.md)] [!INCLUDE [ssrb-applies-to-ssdt-yes](../../includes/ssrb-applies-to-ssdt-yes.md)]

  To give a chart in a paginated report more visual impact, consider using a border frame around the outside of the chart. You can select a border frame by using the **Chart Properties** dialog box or by using the Properties pane. The chart border frames cannot be applied to any other data region.  
  
> [!NOTE]  
>  [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]  
  
### To apply a border to a chart  
  
1.  Right-click anywhere on the chart and select **Chart Properties**.  
  
    > [!NOTE]  
    >  If you do not see the **Chart Properties**, point to **Chart** on the shortcut menu and select **Chart Properties**.  
  
2.  Select **Border**, and click the type of border to apply to the chart.  
  
3.  (Optional) Select a value or type an expression that represents the style of the border.  
  
4.  (Optional) Specify the color of the line that will be drawn around the chart as the border.  
  
    > [!NOTE]  
    >  The **Line color** list contains common colors. If you want to select from a list of more colors, click **More colors** in the list or click the expression (**fx**) button next to the list to bring up the **Expression** editor.  
  
5.  (Optional) Specify the width of the border. Valid values are between 0.25pt and 20pt. Consider keeping the size of your border to between 1 and 3pt for the best visual effect.  
  
6.  (Optional) If your report contains a background color other than white, consider defining a page color that is the same color. The page color is the background color outside of the border line.  
  
7.  (Optional) If you choose a Frame type, specify a style and color for the frame. The **Frame fill color** list contains common colors.  
  
## See Also  
 [Charts &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/charts-report-builder-and-ssrs.md)   
 [Formatting a Chart &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/formatting-a-chart-report-builder-and-ssrs.md)   
 [Add Bevel, Emboss, and Texture Styles to a Chart &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/chart-effects-add-bevel-emboss-or-texture-report-builder.md)  
  
  
