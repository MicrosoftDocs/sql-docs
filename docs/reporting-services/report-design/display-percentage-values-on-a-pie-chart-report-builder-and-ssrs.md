---
title: "Display percentage values on pie chart in paginated report"
description: Learn how to display percentage values in a paginated report on a pie chart, in the legend or in the pie slices, in Report Builder.
author: maggiesMSFT
ms.author: maggies
ms.date: 06/18/2024
ms.service: reporting-services
ms.subservice: report-design
ms.topic: conceptual
ms.custom: updatefrequency5
---
# Display percentage values on pie chart in a paginated report (Report Builder)

[!INCLUDE[ssrs-appliesto](../../includes/ssrs-appliesto.md)] [!INCLUDE [ssrs-appliesto-ssrs-rb](../../includes/ssrs-appliesto-ssrs-rb.md)] [!INCLUDE [ssrs-appliesto-pbi-rb](../../includes/ssrs-appliesto-pbi-rb.md)] [!INCLUDE [ssrb-applies-to-ssdt-yes](../../includes/ssrb-applies-to-ssdt-yes.md)]

In paginated reports, by default the legend shows categories. You might also want percentages in the legend or the pie slices themselves.

![Screenshot of a pie chart showing percentages for the slices of the pie.](../../reporting-services/media/report-builder-pie-chart-preview-percents.png)

 The [Tutorial: Add a Pie Chart to Your Report (Report Builder)](../tutorial-add-a-pie-chart-to-your-report-report-builder.md) walks you through adding percentages to pie slices, if you'd like to practice with sample data.

## Display percentage values as labels on a pie chart

1. Add a pie chart to your report. For more information, see [Add a Chart to a Report (Report Builder and SSRS)](../../reporting-services/report-design/add-a-chart-to-a-report-report-builder-and-ssrs.md).

1. On the design surface, right-click on the pie and select **Show Data Labels**. The data labels should appear within each slice on the pie chart.

1. On the design surface, right-click on the labels and select **Series Label Properties**. The **Series Label Properties** dialog box appears.

1. Type **#PERCENT** for the **Label data** option.

1. (Optional) To specify how many decimal places the label shows, type "#PERCENT{P*n*}" where *n* is the number of decimal places to display. For example, to display no decimal places, type "#PERCENT{P0}".

## Display percentage values in the legend of a pie chart

1. On the design surface, right-click on the pie chart and select **Series Properties**. The **Series Properties** dialog box displays.

1. In **Legend**, type **#PERCENT** for the **Custom legend text** property.

## Related content

* [Tutorial: Add a Pie Chart to Your Report (Report Builder)](../tutorial-add-a-pie-chart-to-your-report-report-builder.md)
* [Pie Charts (Report Builder)](../../reporting-services/report-design/pie-charts-report-builder-and-ssrs.md)
* [Formatting the Legend on a Chart (Report Builder)](../../reporting-services/report-design/chart-legend-formatting-report-builder.md)
* [Display Data Point Labels Outside a Pie Chart (Report Builder)](../../reporting-services/report-design/display-data-point-labels-outside-a-pie-chart-report-builder-and-ssrs.md)
