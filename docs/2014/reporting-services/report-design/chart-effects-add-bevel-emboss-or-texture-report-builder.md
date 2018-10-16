---
title: "Add Bevel, Emboss, and Texture Styles to a Chart (Report Builder and SSRS) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
ms.assetid: 737cfc80-b39e-497c-817b-b46693deb58f
author: maggiesMSFT
ms.author: maggies
manager: craigg
---
# Add Bevel, Emboss, and Texture Styles to a Chart (Report Builder and SSRS)
  When using certain chart types, you can specify a drawing effect to increase the visual impact of your chart. These drawing effects are only applied to the series of your chart. They have no effect on any other chart element.  
  
 When you are using any variant of a pie or doughnut chart, you can specify a soft edge or concave drawing style, similar to bevel or emboss effects that can be applied to an image.  
  
 When you are using any variant of a bar or column chart, you can apply texture styles, such as cylinder, wedge, and light-to-dark, to the individual bars or columns.  
  
 In addition to these drawing styles, you can add borders and shadows to many chart elements to give the illusion of depth. For more information on other ways to format the chart, see [Formatting a Chart &#40;Report Builder and SSRS&#41;](formatting-a-chart-report-builder-and-ssrs.md).  
  
> [!NOTE]  
>  [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]  
  
### To add bevel or emboss styles to a pie or doughnut chart  
  
1.  On the **View** tab, select **Properties** to open the Properties pane.  
  
2.  Select the pie or doughnut chart that you want to enhance. Select a data field in the chart, not the entire chart.  
  
3.  In the Properties pane, expand the **CustomAttributes** node.  
  
4.  For PieDrawingStyle, select a style from the drop-down list.  
  
> [!NOTE]  
>  You can't have 3D and bevel or emboss styles on the same chart. If you have enabled 3D for the chart, you will not see the PieDrawingStyle property.  
  
 ![Pie chart with concave drawing style](../media/rs-piedrawingeffects-concave.gif "Pie chart with concave drawing style")  
  
### To add texture styles to a bar or column chart  
  
1.  Select the bar or column chart that you want to enhance. Select a data field in the chart, not the entire chart.  
  
2.  Open the Properties pane.  
  
3.  Expand the **CustomAttributes** node.  
  
4.  For DrawingStyle, select a style from the drop-down list.  
  
> [!NOTE]  
>  You can't have 3D and bevel or emboss styles on the same chart. If you have enabled 3D for the chart, you will not see the PieDrawingStyle property.  
  
 ![Bar chart with LightToDark drawing effect](../media/rs-bardrawingeffects-lighttodark.gif "Bar chart with LightToDark drawing effect")  
  
## See Also  
 [Bar Charts &#40;Report Builder and SSRS&#41;](charts-report-builder-and-ssrs.md)   
 [Column Charts &#40;Report Builder and SSRS&#41;](column-charts-report-builder-and-ssrs.md)   
 [Pie Charts &#40;Report Builder and SSRS&#41;](pie-charts-report-builder-and-ssrs.md)   
 [Formatting a Chart &#40;Report Builder and SSRS&#41;](formatting-a-chart-report-builder-and-ssrs.md)  
  
  
