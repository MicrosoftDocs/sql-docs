---
title: "Change the text of a legend item in a paginated report | Microsoft Docs"
description: Find out how to change the text of a legend item in a paginated report to show more information about the individual data points in Report Builder.
ms.date: 03/01/2017
ms.service: reporting-services
ms.subservice: report-design


ms.topic: conceptual
ms.assetid: 9e82fa34-17ed-494f-b25d-03dcc353a21f
author: maggiesMSFT
ms.author: maggies
---
# Chart legend - change item text in a paginated report (Report Builder)

[!INCLUDE[ssrs-appliesto](../../includes/ssrs-appliesto.md)] [!INCLUDE [ssrs-appliesto-ssrs-rb](../../includes/ssrs-appliesto-ssrs-rb.md)] [!INCLUDE [ssrs-appliesto-pbi-rb](../../includes/ssrs-appliesto-pbi-rb.md)] [!INCLUDE [ssrb-applies-to-ssdt-yes](../../includes/ssrb-applies-to-ssdt-yes.md)]

  When a field is placed in the Values area of the paginated report chart, a legend item is automatically generated that contains the name of this field. Every legend item is connected to an individual series on the chart, with the exception of shape charts, where the legend is connected to individual data points instead of individual series.  
  
 On shape charts, you can change the text of a legend item to show more information about the individual data points. For example, if you want to show the values of the data points as percentages in the legend, you can use a keyword such as **#PERCENT**. You can append .NET Framework format codes in conjunction with keywords to apply numeric and date formats. For more information about keywords, see [Formatting Data Points on a Chart &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/formatting-data-points-on-a-chart-report-builder-and-ssrs.md).  
  
 ![Sharp Chart](../../reporting-services/report-design/media/sharpchart.png "Sharp Chart")  
  
 On non-shape charts, you can change the text of a legend item. For example, if your series name is "Series1", you may want to change the text to something more descriptive like "Sales for 2008".  
  
> [!NOTE]  
>  [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]  
  
### To modify the text that appears in the legend on a Shape chart  
  
1.  Right-click on a series, or right-click on a field in the **Values** area, and select **Series Properties**.  
  
2.  Click **Legend** and in the **Custom legend text** box, type a keyword.  
  
 The following table provides examples of chart-specific keywords to use for the **Custom Legend Text** property. For more information about keywords, see [Formatting Data Points on a Chart &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/formatting-data-points-on-a-chart-report-builder-and-ssrs.md).  
  
|Keyword|Description|Example of what appears as text in the legend|  
|-------------|-----------------|---------------------------------------------------|  
|`#PERCENT{P1}`|Displays the percentage of the total value to one decimal place.|85.0%|  
|`#VALY`|Displays the actual numeric value of the data field.|17000|  
|`#VALY{C2}`|Displays the actual numeric value of the data field as a currency to two decimal places.|$17000.00|  
|`#AXISLABEL (#PERCENT{P0})`|Displays the text representation of the category field, followed by the percentage that each category displays on the chart.|Michael Blythe (85%)|  
  
> [!NOTE]  
>  The #AXISLABEL keyword can only be evaluated at run time when there is not a field specified in the **Series Groups** area.  
  
### To modify the text that appears in the legend on a non-Shape chart  
  
1.  Right-click on a series, or right-click on a field in the **Values** area, and select **Series Properties**.  
  
2.  Click **Legend** and in the **Custom legend text** box, type a legend label. The series is updated with your text.  
  
## See Also  
 [Formatting the Legend on a Chart &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/chart-legend-formatting-report-builder.md)   
 [Formatting Series Colors on a Chart &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/formatting-series-colors-on-a-chart-report-builder-and-ssrs.md)   
 [Hide Legend Items on the Chart &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/chart-legend-hide-items-report-builder.md)  
  
  
