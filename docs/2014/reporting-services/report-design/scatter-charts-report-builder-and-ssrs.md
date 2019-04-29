---
title: "Scatter Charts (Report Builder and SSRS) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
ms.assetid: 2520ae24-0609-4890-807d-3267018aba8e
author: maggiesMSFT
ms.author: maggies
manager: kfile
---
# Scatter Charts (Report Builder and SSRS)
  A scatter chart displays a series as a set of points. Values are represented by the position of the points on the chart. Categories are represented by different markers on the chart. Scatter charts are typically used to compare aggregated data across categories. For more information on how to add data to a scatter chart, see [Charts &#40;Report Builder and SSRS&#41;](charts-report-builder-and-ssrs.md)  
  
 The following illustration shows an example of a scatter chart.  
  
 ![Scatter chart](../media/rs-scatterchart.gif "Scatter chart")  
  
> [!NOTE]  
>  [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]  
  
## Variations  
  
-   **Bubble.** Bubble charts display the difference between two values of a data point based on the size of the bubble. The larger the bubble, the larger the difference between the two values.  
  
-   **3-D Bubble**. A bubble chart displayed in 3-D.  
  
## Data Considerations for a Scatter Chart  
  
-   Scatter charts are commonly used for displaying and comparing numeric values, such as scientific, statistical, and engineering data.  
  
-   Use the scatter chart when you want to compare large numbers of data points without regard to time. The more data that you include in a scatter chart, the better the comparisons that you can make.  
  
-   The bubble chart requires two values (top and bottom) per data point.  
  
-   Scatter charts are ideal for handling the distribution of values and clusters of data points. This is the best chart type if your dataset contains many points (for example, several thousand points). Displaying multiple series on a point chart is visually distracting and should be avoided. In this scenario, consider using a line chart.  
  
-   By default, scatter charts display data points as circles. If you have multiple series on a scatter chart, consider changing the marker shape of each point to be square, triangle, diamond, or another shape.  
  
## See Also  
 [Charts &#40;Report Builder and SSRS&#41;](charts-report-builder-and-ssrs.md)   
 [Chart Types &#40;Report Builder and SSRS&#41;](chart-types-report-builder-and-ssrs.md)   
 [Formatting a Chart &#40;Report Builder and SSRS&#41;](formatting-a-chart-report-builder-and-ssrs.md)   
 [Line Charts &#40;Report Builder and SSRS&#41;](line-charts-report-builder-and-ssrs.md)  
  
  
