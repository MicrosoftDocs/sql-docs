---
title: "Start pie chart values at the top of the pie in a paginated report | Microsoft Docs"
description: Learn how to start pie chart values a the top of the chart in a paginated report rather than the default 90 degrees from the top.  
ms.date: 03/01/2017
ms.service: reporting-services
ms.subservice: report-design


ms.topic: conceptual
ms.assetid: d0e6fb59-ca4e-4d70-97cb-0ad183da21d3
author: maggiesMSFT
ms.author: maggies
---
# Start pie chart values at the top of the pie in a paginated report (Report Builder)

[!INCLUDE[ssrs-appliesto](../../includes/ssrs-appliesto.md)] [!INCLUDE [ssrs-appliesto-ssrs-rb](../../includes/ssrs-appliesto-ssrs-rb.md)] [!INCLUDE [ssrs-appliesto-pbi-rb](../../includes/ssrs-appliesto-pbi-rb.md)] [!INCLUDE [ssrb-applies-to-ssdt-yes](../../includes/ssrb-applies-to-ssdt-yes.md)]

In pie charts in paginated reports, by default the first value in the dataset starts at 90 degrees from the top of the pie. 

![Screenshot of a report builder pie chart with the dataset starting at 90 degrees.](../../reporting-services/media/report-builder-pie-chart-start-at-90.png)

*Chart values start at 90 degrees.*

You might want the first value to start at the top instead. 

![Screenshot of a report builder pie chart with the dataset starting at the top.](../../reporting-services/media/report-builder-pie-chart-start-at-top.png)

*Chart values start at the top of the chart.*
  
## To Start the Pie Chart at the Top of the Pie  
  
1.  Click the pie itself.  
  
2.  If the **Properties** pane is not open, on the **View** tab, click **Properties**.  
  
3.  In the **Properties** pane, under **Custom Attributes**, change **PieStartAngle** from **0** to **270**.  
  
4.  Click **Run** to preview your report.  
  
 The first value now starts at the top of the pie chart.  
  
## See Also  
 [Formatting a Chart &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/formatting-a-chart-report-builder-and-ssrs.md)   
 [Pie Charts &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/pie-charts-report-builder-and-ssrs.md)  
  
  
