---
title: "Start Pie Chart Values at the Top of the Pie (Report Builder and SSRS) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
ms.assetid: d0e6fb59-ca4e-4d70-97cb-0ad183da21d3
author: maggiesMSFT
ms.author: maggies
manager: kfile
---
# Start Pie Chart Values at the Top of the Pie (Report Builder and SSRS)
  By default in pie charts, the first value in the dataset starts at 90 degrees from the top of the pie. You may prefer that the first value start from the top.  
  
> [!NOTE]  
>  [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]  
  
### To Start the Pie Chart at the Top of the Pie  
  
1.  Click the pie itself.  
  
2.  If the **Properties** pane is not open, on the **View** tab, click **Properties**.  
  
3.  In the **Properties** pane, under **Custom Attributes**, change **PieStartAngle** from **0** to **270**.  
  
4.  Click **Run** to preview your report.  
  
 The first value now starts at the top of the pie chart.  
  
## See Also  
 [Formatting a Chart &#40;Report Builder and SSRS&#41;](formatting-a-chart-report-builder-and-ssrs.md)   
 [Pie Charts &#40;Report Builder and SSRS&#41;](charts-report-builder-and-ssrs.md)  
  
  
