---
title: "Add a moving average to a chart in a paginated report"
description: Learn how the Moving Average formula price indicator can be shown on a chart to identify trends in Report Builder.
author: maggiesMSFT
ms.author: maggies
ms.date: 09/25/2024
ms.service: reporting-services
ms.subservice: report-design
ms.topic: conceptual
ms.custom:
  - updatefrequency5
---
# Add a moving average to a chart in a paginated report (Report Builder)

[!INCLUDE[ssrs-appliesto](../../includes/ssrs-appliesto.md)] [!INCLUDE [ssrs-appliesto-ssrs-rb](../../includes/ssrs-appliesto-ssrs-rb.md)] [!INCLUDE [ssrs-appliesto-pbi-rb](../../includes/ssrs-appliesto-pbi-rb.md)] [!INCLUDE [ssrb-applies-to-ssdt-yes](../../includes/ssrb-applies-to-ssdt-yes.md)]

A moving average is an average of the data in your series, calculated over a defined period of time. In paginated reports, the moving average can be shown on the chart to identify significant trends.  

:::image type="content" source="../../reporting-services/media/report-builder-column-chart-tutorial.png" alt-text="Screenshot of a Sales chart with a bar graph and line graph.":::

  
 The Moving Average formula is the most popular price indicator used in technical analyses. Many other formulas, including mean, median and standard deviation, can also be derived from a series on the chart. When when you specify a moving average, each formula might have one or more parameters that must be specified.  
 
 The [Tutorial: Add a column chart to your report (Report Builder)](../tutorial-add-a-column-chart-to-your-report-report-builder.md) walks you through adding a moving average to a chart, if you'd like to try it with sample data.
  
 When a moving average formula is added in Design mode, the line series that is added is only a visual placeholder. The chart calculates the data points of each formula during report processing.  
  
 Built-in support for trend lines isn't available in [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)].  
  
> [!NOTE]  
>  [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]  
  
## Add a calculated moving average to a series on the chart  
  
1.  Right-click on a field in the **Values** area and select **Add Calculated Series**. The **Calculated Series Properties** dialog opens.  
  
1.  Select the **Moving average** option from the **Formula** list.  
  
1.  Specify an integer value for the **Period** that represents the period of the moving average.  
  
    > [!NOTE]  
    >  The period is the number of days used to calculate a moving average. If date/time values are not specified on the x-axis, the period is represented by the number of data points used to calculate a moving average. If there is only one data point, the moving average formula does not calculate. The moving average is calculated starting at the second point. If you specify the **Start from first point** option, the chart will start the moving average at the first point. If there is only one data point, the point in the calculated moving average is identical to the first point in your original series.  
  
## Related content

- [Tutorial: Add a column chart to your report (Report Builder)](../tutorial-add-a-column-chart-to-your-report-report-builder.md)
- [Format a chart &#40;Report Builder&#41;](../../reporting-services/report-design/formatting-a-chart-report-builder-and-ssrs.md)
- [Charts &#40;Report Builder&#41;](../../reporting-services/report-design/charts-report-builder-and-ssrs.md)
- [Add empty points to a chart &#40;Report Builder&#41;](../../reporting-services/report-design/add-empty-points-to-a-chart-report-builder-and-ssrs.md)
